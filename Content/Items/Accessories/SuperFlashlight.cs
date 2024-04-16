using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class SuperFlashlight : ModItem
	{
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
				.AddIngredient(ItemID.ShinePotion, 1)
				.AddIngredient(ModContent.ItemType<Flashlight>(), 1)
				.AddTile(TileID.TinkerersWorkbench)
                .AddDecraftCondition(Condition.CorruptWorld)
                .Register();

            CreateRecipe()
				.AddIngredient(ItemID.CrimsonHeart, 1)
				.AddIngredient(ItemID.ShinePotion, 1)
				.AddIngredient(ModContent.ItemType<Flashlight>(), 1)
				.AddTile(TileID.TinkerersWorkbench)
                .AddDecraftCondition(Condition.CrimsonWorld)
                .Register();
        }
    }
}