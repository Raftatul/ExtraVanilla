﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class WoodenBoots : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(copper: 30);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxRunSpeed += 1;
			player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().WoodenBoots = true;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.Wood, 50)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
    }
}