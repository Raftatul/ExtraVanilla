using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Common.GlobalItems
{
	public class ShadowOrbToCrimsonHeart : GlobalItem
	{
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.TheUndertaker, 1);
            baseRecipe
                .AddIngredient(ItemID.Musket, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class ShadowOrb : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.CrimsonHeart, 1);
            baseRecipe
                .AddIngredient(ItemID.ShadowOrb, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class BandofStarPower : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.PanicNecklace, 1);
            baseRecipe
                .AddIngredient(ItemID.BandofStarpower, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class Vilethorn : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.CrimsonRod, 1);
            baseRecipe
                .AddIngredient(ItemID.Vilethorn, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class BallOHurt : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.TheRottedFork, 1);
            baseRecipe
                .AddIngredient(ItemID.BallOHurt, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class EaterofWorldsBag : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.BrainOfCthulhuBossBag, 1);
            baseRecipe
                .AddIngredient(ItemID.EaterOfWorldsBossBag, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class EbonWood : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.Shadewood, 1);
            baseRecipe
                .AddIngredient(ItemID.Ebonwood, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class DemoniteOre : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.CrimtaneOre, 1);
            baseRecipe
                .AddIngredient(ItemID.DemoniteOre, 1)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }

    public class CursedFlame : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.Ichor, 1);
            baseRecipe
                .AddIngredient(ItemID.CursedFlame, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class RottenChunk : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.Vertebrae, 1);
            baseRecipe
                .AddIngredient(ItemID.RottenChunk, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class CorruptionKey : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.CrimsonKey, 1);
            baseRecipe
                .AddIngredient(ItemID.RottenChunk, 1)
                .AddIngredient(ItemID.CrimstoneBlock, 300)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}