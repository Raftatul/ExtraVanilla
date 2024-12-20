﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class LifeCharm : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(gold: 11);
			Item.rare = ItemRarityID.Lime;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().HealPotionBonus = true;
			player.lifeRegen += 1;
			player.pStone = true;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.CharmofMyths, 1)
				.AddIngredient(ModContent.ItemType<LifeStone>(), 1)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
    }
}