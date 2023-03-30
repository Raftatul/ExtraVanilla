using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Items.Global
{
	public class WormholePotionCraft : GlobalItem
	{
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.WormholePotion, 1);
            baseRecipe
                .AddIngredient(ItemID.BottledWater, 1)
                .AddIngredient(ItemID.EnchantedNightcrawler, 1)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}