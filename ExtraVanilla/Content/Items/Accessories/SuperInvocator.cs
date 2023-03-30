using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class SuperInvocator : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+6 minions slots" 
			                   + "\nIncrease summon damage by 15%");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Pink;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions += 6;
			player.GetDamage(DamageClass.Summon) += 0.15f;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SoulofLight, 15)
				.AddIngredient(ItemID.GreenThread, 6)
				.AddIngredient(ItemID.WizardHat, 1)
				.AddTile(TileID.CrystalBall)
				.Register();
		}
	}
}