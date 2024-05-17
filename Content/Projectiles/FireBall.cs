using ExtraVanilla.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Projectiles
{
    public class FireBall : ModProjectile
    {
        private int explosionRadius = 30;

        private int effectCount = 120;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = effectCount;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;              //The width of Projectile hitbox 
            Projectile.height = 32;         // The height of Projectile hitbox
            Projectile.friendly = true;         //Can the Projectile deal damage to enemies?
            Projectile.hostile = false;
            Projectile.light = 0.5f;            //How much light emit around the Projectile
            Projectile.ignoreWater = false;          //Does the Projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the Projectile collide with tiles?
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texture = TextureAssets.Projectile[Type].Value;
            var drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            for (int i = 0; i < effectCount; i++)
            {
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin;
                float alpha = (1f / i);

                Main.EntitySpriteDraw(texture, drawPos, new Rectangle(0, Projectile.frame * Projectile.height, Projectile.width, Projectile.height), Projectile.GetAlpha(lightColor) * alpha, Projectile.rotation, drawOrigin, Projectile.scale,
                    SpriteEffects.None);
            }

            return true;
        }

        public override void AI()
        {
            Projectile.rotation -= MathHelper.ToRadians(6);
        }

        public override bool PreKill(int timeLeft)
        {
            Lighting.AddLight(Projectile.position, 255, 255, 255);
            SoundEngine.PlaySound(new SoundStyle("ExtraVanilla/Assets/Sounds/Projectiles/Explosion", SoundType.Sound), Projectile.position);

            PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), 20f, 6f, 60, 1000f, FullName);
            Main.instance.CameraModifiers.Add(modifier);

            ExplodeDamage();

            if (!Main.LocalPlayer.GetModPlayer<ExtraVanillaPlayer>().explosifStaffSafe)
                ExplodeTiles();

            return true;
        }

        private void ExplodeDamage()
        {
            // If the Projectile dies without hitting an enemy, crate a small explosion that hits all enemies in the area.
            if (Projectile.penetrate == 1)
            {
                // Makes the Projectile hit all enemies as it circunvents the penetrate limit.
                Projectile.maxPenetrate = -1;
                Projectile.penetrate = -1;

                Vector2 oldSize = Projectile.Size;
                // Resize the Projectile hitbox to be bigger.
                Projectile.position = Projectile.Center;
                Projectile.Size += new Vector2(explosionRadius * 16);
                Projectile.Center = Projectile.position;

                Projectile.tileCollide = false;
                Projectile.velocity *= 0.01f;
                // Damage enemies inside the hitbox area
                Projectile.Damage();
                Projectile.scale = 0.01f;

                //Resize the hitbox to its original size
                Projectile.position = Projectile.Center;
                Projectile.Size = new Vector2(10);
                Projectile.Center = Projectile.position;
            }

            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.TreasureSparkle, 0, 0, 100, Color.Lime, 0.8f);
                dust.noGravity = true;
                dust.velocity *= 2f;
                dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.TreasureSparkle, 0f, 0f, 100, Color.Lime, 0.5f);
            }
        }

        private void ExplodeTiles()
        {
            int minTileX = (int)(Projectile.Center.X / 16f - explosionRadius);
            int maxTileX = (int)(Projectile.Center.X / 16f + explosionRadius);
            int minTileY = (int)(Projectile.Center.Y / 16f - explosionRadius);
            int maxTileY = (int)(Projectile.Center.Y / 16f + explosionRadius);

            Projectile.ExplodeTiles(Projectile.Center, explosionRadius, minTileX, maxTileX, minTileY, maxTileY, false);

            Console.WriteLine("EXPLOSION");
        }
    }
}