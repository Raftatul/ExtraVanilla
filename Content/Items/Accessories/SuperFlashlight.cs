using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;
using Microsoft.Xna.Framework;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class SuperFlashlight : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Super Flashlight");
			/* Tooltip.SetDefault("This flashlight is so powerful that you no longer need to equip it!" +
				"\nMark as favorite to take effect"); */
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.Orange;
		}

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
			Lighting.AddLight(Item.position, new Vector3(2, 2, 2));
        }

        public override void UpdateInventory(Player player)
        {
            if (Item.favorited)
            {
				Lighting.AddLight(player.position, new Vector3(2, 2, 2));
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			Lighting.AddLight(player.position, new Vector3(2, 2, 2));
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ItemID.ShadowOrb, 1)
				.AddIngredient(ItemID.CrimsonHeart, 1)
				.AddIngredient(ItemID.ShinePotion, 1)
				.AddIngredient(ModContent.ItemType<Flashlight>(), 1)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
    }
}