using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Content.GlobalItems
{
	public class LavaCharmCraft : GlobalItem
	{
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.LavaCharm, 1);
            baseRecipe
                .AddIngredient(ItemID.HellstoneBar, 50)
                .AddIngredient(ItemID.Obsidian, 50)
                .AddIngredient(ModContent.ItemType<Items.HotSilk>(), 25)
                .AddIngredient(ItemID.LavaBucket, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}