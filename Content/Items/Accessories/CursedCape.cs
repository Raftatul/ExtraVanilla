using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;
using Microsoft.Xna.Framework;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class CursedCape : ModItem
	{
		private int maxDamageBonus = 16;

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			float damageBonus = player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().MissingHealth / (player.statLifeMax2 / maxDamageBonus);
			damageBonus = Math.Clamp(damageBonus, 0, maxDamageBonus);
			player.GetDamage(DamageClass.Generic) += damageBonus / 100;
		}
	}
}