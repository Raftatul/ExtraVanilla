using ExtraVanilla.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Projectiles
{
    public class MagicRodHoldout : ModProjectile
    {
        // This value controls how many frames it takes for the Prism to reach "max charge". 60 frames = 1 second.
        public const float MaxCharge = 180f;

        // The vanilla Last Prism is an animated item with 5 frames of animation. We copy that here.
        private const int NumAnimationFrames = 1;

        private static Asset<Texture2D> _laserTexture;

        // This property encloses the internal AI variable Projectile.ai[0]. It makes the code easier to read.
        private float FrameCounter
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        //The distance charge particle from the player center
        private const float MOVE_DISTANCE = 60f;

        // The actual distance is stored in the ai0 field
        // By making a property to handle this it makes our life easier, and the accessibility more readable
        public float Distance
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        private const float AimResponsiveness = 0.8f;

        private Vector2 headPos;

        private int dust = -1;

        private int castDuration = 810;

        public override void SetStaticDefaults()
        {
            _laserTexture = ModContent.Request<Texture2D>("ExtraVanilla/Content/Projectiles/Laser");

            Main.projFrames[Projectile.type] = NumAnimationFrames;

            // Signals to Terraria that this Projectile requires a unique identifier beyond its index in the Projectile array.
            // This prevents the issue with the vanilla Last Prism where the beams are invisible in multiplayer.
            ProjectileID.Sets.NeedsUUID[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            // Use CloneDefaults to clone all basic Projectile statistics from the vanilla Last Prism.
            Projectile.CloneDefaults(ProjectileID.LastPrism);

            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.height = 95;
            Projectile.width = 27;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //DrawLaser(Main.spriteBatch, _laserTexture.Value, Main.player[Projectile.owner].Center, Projectile.velocity, 10, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);

            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Filters.Scene.Activate("ExampleEffect");
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);

            // Update the frame counter.
            FrameCounter += 1f;

            UpdateAnimation();

            // Updating a filter
            //Filters.Scene["ExampleEffect"].GetShader().UseProgress(Projectile.ai[0] / 300);
            Filters.Scene["ExampleEffect"].GetShader().UseIntensity(1f);

            if (Projectile.owner == Main.myPlayer)
            {
                // Slightly re-aim the Prism every frame so that it gradually sweeps to point towards the mouse.
                UpdateAim(rrp, player.HeldItem.shootSpeed);

                if (dust == -1)
                    dust = Dust.NewDust(headPos, 1, 1, ModContent.DustType<StarPower>());
                Main.dust[dust].position = headPos;

                Dust.NewDust(headPos, 1, 1, ModContent.DustType<StarDust>());

                if (Projectile.ai[0] % 2 == 0)
                {
                    bool manaIsAvailable = player.CheckMana(2, true, true);

                    if (!manaIsAvailable)
                    {
                        Player.HurtInfo hurtInfo = new()
                        {
                            Dodgeable = false,
                            Damage = 1,
                            DamageSource = PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.identity),
                            SoundDisabled = true,
                            DustDisabled = true,
                        };

                        if (player.statLife > 2)
                            player.statLife -= 2;
                        else
                            player.Hurt(hurtInfo, true);
                    }
                }
            }

            UpdatePlayerVisuals(player, rrp);

            SetLaserPosition(player);

            if (Projectile.ai[0] == castDuration)
            {
                Shoot(player, headPos);
            }

            bool stillInUse = player.channel && !player.noItems && !player.CCed;

            if (!stillInUse)
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            Main.dust[dust].active = false;

            if (Main.netMode != NetmodeID.Server)
            {
                Filters.Scene.Deactivate("ExampleEffect");
            }

            if (Projectile.ai[0] < castDuration)
            {
                SoundEngine.SoundPlayer.StopAll(new SoundStyle("ExtraVanilla/Assets/Music/MagicRodCasting", SoundType.Ambient));

                AdvancedPopupRequest request = default;
                request.Text = "Wait... WHERE IS MY EXPLOSION ?????";
                request.DurationInFrames = 180;
                request.Velocity = Vector2.UnitY * 5f;
                request.Color = Colors.RarityRed;

                PopupText.NewText(request, Projectile.position);
            }
        }

        private void UpdateAnimation()
        {
            Projectile.frameCounter++;

            // As the Prism charges up and focuses the beams, its animation plays faster.
            int framesPerAnimationUpdate = FrameCounter >= MaxCharge ? 2 : FrameCounter >= (MaxCharge * 0.66f) ? 3 : 4;

            // If necessary, change which specific frame of the animation is displayed.
            if (Projectile.frameCounter >= framesPerAnimationUpdate)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= NumAnimationFrames)
                {
                    Projectile.frame = 0;
                }
            }
        }

        private void UpdateAim(Vector2 source, float speed)
        {
            // Get the player's current aiming direction as a normalized vector.
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }

            // Change a portion of the Prism's current velocity so that it points to the mouse. This gives smooth movement over time.
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, AimResponsiveness));
            headPos = source + aim * 60;
            aim *= speed;

            if (aim != Projectile.velocity)
            {
                Projectile.netUpdate = true;
            }
            Projectile.velocity = aim;
        }

        private void UpdatePlayerVisuals(Player player, Vector2 playerHandPos)
        {
            // Place the Prism directly into the player's hand at all times.
            Projectile.Center = playerHandPos;
            // The beams emit from the tip of the Prism, not the side. As such, rotate the sprite by pi/2 (90 degrees).
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;

            // The Prism is a holdout Projectile, so change the player's variables to reflect that.
            // Constantly resetting player.itemTime and player.itemAnimation prevents the player from switching items or doing anything else.
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            // If you do not multiply by Projectile.direction, the player's hand will point the wrong direction while facing left.
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
        }

        // The core function of drawing a laser
        private void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            // Draws the laser 'body'
            for (float i = transDist; i <= Distance; i += step)
            {
                Color c = Color.White;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
                    new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
            }

            // Draws the laser 'tail'
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
                new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
        }

        /*
        *   Sets the end of the laser position based on where it collides with something
        */
        private void SetLaserPosition(Player player)
        {
            for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
            {
                var start = player.Center + Projectile.velocity * Distance;
                if (!Collision.CanHit(player.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }
        }

        private void Shoot(Player player, Vector2 spawnPos)
        {
            Vector2 shootDirection = Vector2.Normalize(headPos - player.Center);
            Vector2 shootVelocity = shootDirection * 50;

            Projectile.NewProjectile(Projectile.GetSource_None(), spawnPos, shootVelocity, ModContent.ProjectileType<FireBall>(), 500000, 50);

            Main.LocalPlayer.velocity += -shootDirection * 10;

            //player.GetModPlayer<ExtraVanillaPlayer>().CanUseMagicRod = false;

            Projectile.Kill();
        }
    }
}
