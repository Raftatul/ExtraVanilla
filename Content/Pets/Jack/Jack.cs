using ExtraVanilla.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Pets.Jack
{
    public class Jack : ModProjectile
    {
        private int _animationStep = 0;
        private static int _delay = 300;

        private Player _playerOwner;
        private ExtraVanillaPlayer _modPlayerOwner;

        private bool _HalfLifeFlag = false;

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
            _animationStep = 0;
            Projectile.netUpdate = true;            
        }

        public override void OnSpawn(IEntitySource source)
        {
            WritePopup("Let's go on an adventure!", 300, Color.LightCoral);

            _playerOwner = Main.player[Projectile.owner];
            _modPlayerOwner = _playerOwner.GetModPlayer<Common.Players.ExtraVanillaPlayer>();
        }

        public override bool PreAI()
        {
            _playerOwner.petFlagDD2Gato = false;
            return true;
        }

        public override void AI()
        {
            if (_playerOwner.dead)
            {
                _modPlayerOwner.JackPet = false;
            }
            if (_modPlayerOwner.JackPet)
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

            if (_playerOwner.statLife <= _modPlayerOwner.GetPlayerHalfLife(_playerOwner))
            {
                if (!_HalfLifeFlag)
                {
                    _HalfLifeFlag = true;

                    WritePopup("Look at your health bar :)", 300, Color.LightCoral);
                }
            }
            else
                _HalfLifeFlag = false;

            //Animation
            if (bossFind != null && bossFind.active)
            {
                _animationStep++;
                if (_animationStep >= _delay)
                {
                    Projectile.frame = 5;
                    if (_animationStep >= _delay + 10)
                    {
                        _animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 4;
                }
            }
            else if (Main.bloodMoon || Main.invasionProgressAlpha != 0 || Main.eclipse)
            {
                _animationStep++;
                if(_animationStep >= _delay)
                {
                    Projectile.frame = 3;
                    if(_animationStep >= _delay + 10)
                    {
                        _animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 2;
                }
            }
            else if (_playerOwner.statLife <= _modPlayerOwner.GetPlayerHalfLife(_playerOwner))
            {
                _animationStep++;
                if (_animationStep >= _delay)
                {
                    Projectile.frame = 7;
                    if (_animationStep >= _delay + 10)
                    {
                        _animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 6;
                }
            }
            else
            {
                _animationStep++;
                if (_animationStep >= _delay)
                {
                    Projectile.frame = 1;
                    if (_animationStep >= _delay + 5)
                    {
                        _animationStep = 0;
                    }
                }
                else
                {
                    Projectile.frame = 0;
                }
            }
        }

        private void WritePopup(string text, int duration, Color color, Vector2 vel = default)
        {
            AdvancedPopupRequest request = default;
            request.Text = text;
            request.DurationInFrames = duration;
            request.Velocity = vel;
            request.Color = color;
            PopupText.NewText(request, Projectile.position);
        }
    }
}