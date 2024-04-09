using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class StoneofLife : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Of Life");
			// Tooltip.SetDefault("After healing, spawns a flying heart that drops additional hearts after a time");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().HealPotionBonus = true;
		}
    }
}