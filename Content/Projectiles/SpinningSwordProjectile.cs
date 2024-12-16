using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using Terraria;

namespace ExtraVanilla.Content.Projectiles
{
    public class SpinningSwordProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 92;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            float num = 46f;
            float num2 = 2f;
            float quarter = -(float)Math.PI / 4f;

            Player player = Main.player[Projectile.owner];

            Vector2 relativePoint = player.RotatedRelativePoint(player.MountedCenter);
            
            if (player.dead)
            {
                Projectile.Kill();
                return;
            }

            int sign = Math.Sign(player.direction);

            Projectile.velocity = new Vector2(sign, 0f);

            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation = new Vector2(sign, 0f - player.gravDir).ToRotation() + quarter + (float)Math.PI;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.rotation -= (float)Math.PI / 2f;
                }
            }

            Projectile.ai[0] += 1f;

            Projectile.rotation += (float)Math.PI * 2f * num2 / num * (float)sign;

            bool isDone = Projectile.ai[0] == (num / 2f);
            if (Projectile.ai[0] >= num || (isDone && !player.controlUseItem))
            {
                Projectile.Kill();
                player.reuseDelay = 2;
            }
            else if (isDone)
            {
                Vector2 mouseWorld = Main.MouseWorld;
                int dir = (player.DirectionTo(mouseWorld).X > 0f) ? 1 : -1;
                if ((float)dir != Projectile.velocity.X)
                {
                    player.ChangeDir(dir);
                    Projectile.velocity = new Vector2(dir, 0f);
                    Projectile.netUpdate = true;
                    Projectile.rotation -= (float)Math.PI;
                }
            }

            Projectile.position = relativePoint - Projectile.Size / 2f;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;

            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = MathHelper.WrapAngle(Projectile.rotation);
        }
    }
}
