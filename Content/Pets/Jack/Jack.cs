using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Pets.Jack
{
    public class Jack : ModProjectile
    {
        private int animationStep = 0;
        private static int _delay = 300;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DD2PetGato);
            Projectile.width = 42;
            Projectile.height = 26;
            animationStep = 0;
            Projectile.netUpdate = true;            
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.petFlagDD2Gato = false;
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Common.Players.ExtraVanillaPlayer modPlayer = player.GetModPlayer<Common.Players.ExtraVanillaPlayer>();
            if (player.dead)
            {
                modPlayer.JackPet = false;
            }
            if (modPlayer.JackPet)
            {
                Projectile.timeLeft = 2;
            }

            NPC bossFind = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].boss)
                {
                    bossFind = Main.npc[i];
                    break;
                }
            }

            //Animation
            if (bossFind != null && bossFind.active)
            {
                animationStep++;
                if (animationStep >= _delay)
                {
                    Projectile.frame = 5;
                    if (animationStep >= _delay + 10)
                    {
                        animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 4;
                }
            }
            else if (Main.bloodMoon || Main.invasionProgressAlpha != 0 || Main.eclipse)
            {
                animationStep++;
                if(animationStep >= _delay)
                {
                    Projectile.frame = 3;
                    if(animationStep >= _delay + 10)
                    {
                        animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 2;
                }
            }
            else if (Main.player[Projectile.owner].statLife <= Main.player[Projectile.owner].GetModPlayer<Common.Players.ExtraVanillaPlayer>().GetPlayerHalfLife(Main.player[Projectile.owner]))
            {
                animationStep++;
                if (animationStep >= _delay)
                {
                    Projectile.frame = 7;
                    if (animationStep >= _delay + 10)
                    {
                        animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 6;
                }
            }
            else
            {
                animationStep++;
                if (animationStep >= _delay)
                {
                    Projectile.frame = 1;
                    if (animationStep >= _delay + 5)
                    {
                        animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 0;
                }
            }
        }
    }
}