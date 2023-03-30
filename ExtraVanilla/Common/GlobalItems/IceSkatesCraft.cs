using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Content.GlobalItems
{
	public class LavaCharmsCraft : GlobalItem
	{
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.IceSkates, 1);
            baseRecipe
                .AddIngredient(ItemID.IceBlock, 500)
                .AddIngredient(ItemID.SnowBlock, 250)
                .AddIngredient(ModContent.ItemType<Items.ColdSilk>(), 25)
                .AddRecipeGroup("IronBar", 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}