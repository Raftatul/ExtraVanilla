using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ExtraVanilla.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class LavaProofLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
			// Tooltip.SetDefault("Grants immunity to fire blocks");
        }

        public override void SetDefaults()
		{
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.Green;
			Item.defense = 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<LavaProofHelmet>() && legs.type == ModContent.ItemType<LavaProofLeggings>();
		}

        public override void UpdateEquip(Player player)
        {
			player.fireWalk = true;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LeadGreaves, 1)
				.AddIngredient(ModContent.ItemType<HotSilk>(), 12)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}