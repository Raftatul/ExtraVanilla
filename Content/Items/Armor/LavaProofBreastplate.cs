using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ExtraVanilla.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class LavaProofBreastplate : ModItem
	{
        public override void SetDefaults()
		{
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<LavaProofHelmet>() && legs.type == ModContent.ItemType<LavaProofBreastplate>();
		}

        public override void UpdateEquip(Player player)
        {
			player.buffImmune[BuffID.OnFire] = true;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LeadChainmail, 1)
				.AddIngredient(ModContent.ItemType<HotSilk>(), 15)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}