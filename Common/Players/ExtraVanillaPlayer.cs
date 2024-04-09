using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ExtraVanilla.Common.Players
{
    class ExtraVanillaPlayer : ModPlayer
    {
        public bool JackPet;
        public bool GameBoyJackPet;
        public bool WoodenBoots;
        public bool SuperBuilderBuffer;
        public bool HealPotionBonus;
        public bool LivingBoots;
        public bool WoodenShield;

        public int HealReceive;
        public float MissingHealth;

        public override void OnEnterWorld()
        {
            Main.NewText("[ExtraVenilla] Hey ! Considering giving feedback is a great way for me to improve this mod.", Color.Aqua);
            Main.NewText("You can contact me (Raftatul) on the official Tmodloader discord server ! Have a great game !", Color.Aqua);
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[] {
                new Item(ModContent.ItemType<Content.Pets.Jack.JackItem>()),
            };
        }

        public override void ResetEffects()
        {
            JackPet = false;
            GameBoyJackPet = false;
            WoodenBoots = false;
            SuperBuilderBuffer = false;
            HealPotionBonus = false;
            LivingBoots = false;
            WoodenShield = false;
        }

        public override void PreUpdate()
        {
            MissingHealth = Player.statLifeMax2 - Player.statLife;

            if (Player.velocity.X * Math.Sign(Player.velocity.X) > 3 && WoodenBoots)
            {
                if (Player.velocity.Y == 0)
                {
                    int dust = Dust.NewDust(Player.position + new Vector2(0, Player.height), 1, 1, DustID.WoodFurniture, 0, Main.rand.Next(-1, 1));
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1f;
                }
            }
            if (SuperBuilderBuffer)
            {
                Player.ClearBuff(BuffID.Builder);
            }
        }

        public override void UpdateEquips()
        {
            if (LivingBoots && Player.statLife <= GetPlayerHalfLife(Player))
            {
                float speedMultiplier = 0.25f;
                Player.moveSpeed += Player.moveSpeed * speedMultiplier;
                Player.accRunSpeed += Player.accRunSpeed * speedMultiplier;
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (WoodenShield)
            {
                int rand = Main.rand.Next(0, 3);
                if (rand == 1)
                {
                    Player.noKnockback = true;
                }
            }

            base.ModifyHurt(ref modifiers);
        }

        public int GetPlayerHalfLife(Player targetPlayer)
        {
            return targetPlayer.statLifeMax2 / 2;
        }
    }
}
