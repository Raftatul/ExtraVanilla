using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ExtraVanilla.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class LavaProofHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increase mining speed by 25%");
		}

		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.Green;
			Item.defense = 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<LavaProofBreastplate>() && legs.type == ModContent.ItemType<LavaProofLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grant immune to lava for 10 seconds"
				+ "\nGrant the ability to swim in lava";
			player.lavaMax = 600;
            if (player.lavaWet)
            {
				player.accFlipper = true;
            }
		}

        public override void UpdateEquip(Player player)
        {
			player.pickSpeed -= player.pickSpeed * 0.25f;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LeadHelmet, 1)
				.AddIngredient(ModContent.ItemType<HotSilk>(), 10)
				.AddIngredient(ItemID.Glass, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}