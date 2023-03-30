using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Common.GlobalItems
{
	public class CrimsonHeartToShadowOrb : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.Musket, 1);
            baseRecipe
                .AddIngredient(ItemID.TheUndertaker, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
    
    public class CrimsonHeart : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.ShadowOrb, 1);
            baseRecipe
                .AddIngredient(ItemID.CrimsonHeart, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class PanicNecklace : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.BandofStarpower, 1);
            baseRecipe
                .AddIngredient(ItemID.PanicNecklace, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class CrimsonRod : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.Vilethorn, 1);
            baseRecipe
                .AddIngredient(ItemID.CrimsonRod, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class TheRottedFork : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.BallOHurt, 1);
            baseRecipe
                .AddIngredient(ItemID.TheRottedFork, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class BrainOfCthulhuBossBag : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.EaterOfWorldsBossBag, 1);
            baseRecipe
                .AddIngredient(ItemID.BrainOfCthulhuBossBag, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class Shadewood : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.Ebonwood, 1);
            baseRecipe
                .AddIngredient(ItemID.Shadewood, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class CrimtaneOre : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.DemoniteOre, 1);
            baseRecipe
                .AddIngredient(ItemID.CrimtaneOre, 1)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }

    public class Ichor : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.CursedFlame, 1);
            baseRecipe
                .AddIngredient(ItemID.Ichor, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class Vertebrae : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.RottenChunk, 1);
            baseRecipe
                .AddIngredient(ItemID.Vertebrae, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class CrimsonKey : GlobalItem
    {
        public override void AddRecipes()
        {
            Recipe baseRecipe = Recipe.Create(ItemID.CorruptionKey, 1);
            baseRecipe
                .AddIngredient(ItemID.CrimsonKey, 1)
                .AddIngredient(ItemID.EbonstoneBlock, 300)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}