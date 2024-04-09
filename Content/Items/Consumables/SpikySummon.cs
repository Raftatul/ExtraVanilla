using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ExtraVanilla.Content.Items.Consumables
{
    class SpikySummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NPCs.SpikyBoss.SpikyBoss>()) && !Main.dayTime;
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            int type = ModContent.NPCType<NPCs.SpikyBoss.SpikyBoss>();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPos = player.position + new Vector2(1500 * player.direction, -1500);
                NPC.NewNPC(Item.GetSource_NaturalSpawn(), ((int)spawnPos.X), ((int)spawnPos.Y), Mod.Find<ModNPC>("SpikyBoss").Type);
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("IronBar", 10)
                .AddIngredient(ItemID.Wood, 5)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
