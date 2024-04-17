using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;

namespace ExtraVanilla.Content.Projectiles
{
    public class SpinningSwordProjectile : ModProjectile
    {
        private float homingPlayerSpeed = 20f;

        private float rotationSpeed = 15f;

        private bool npcHit = false;

        private bool returnToPlayer = false;

        private Vector2 startVelocity;

        //Visual
        private int effectCount = 7;

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = effectCount;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);

            startVelocity = Projectile.velocity;

            Projectile.velocity = Vector2.Zero;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            startVelocity = Projectile.velocity * 0.1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            returnToPlayer = true;
            Projectile.tileCollide = false;

            return false;
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
            float num = 60f;
            Player player = Main.player[Projectile.owner];

            if (!returnToPlayer)
            {
                Projectile.ai[0] += 1f;

                returnToPlayer = Projectile.ai[0] > num;
                Projectile.tileCollide = Projectile.ai[0] < num;

                Projectile.velocity = Vector2.Lerp(Projectile.velocity, startVelocity, 0.1f);
            }
            else
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.position.DirectionTo(player.position) * homingPlayerSpeed, 0.1f);

                if (Projectile.position.Distance(player.position) < 20)
                    Projectile.Kill();
            }

            Projectile.rotation += MathHelper.ToRadians(rotationSpeed);
        }
    }
}
