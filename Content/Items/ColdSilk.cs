using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Content.Items
{
	public class ColdSilk : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.material = true;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 2);
			Item.rare = ItemRarityID.White;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.Silk, 3)
				.AddIngredient(ItemID.IceBlock, 1)
				.AddTile(TileID.Loom)
				.Register();
		}
    }
}