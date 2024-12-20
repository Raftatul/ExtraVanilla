﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class CritHeart : ModItem
	{
		private int maxCritBonus = 16;

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			float critBonus = (player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().MissingHealth / (player.statLifeMax2 / maxCritBonus));
			critBonus = Math.Clamp(critBonus, 0, maxCritBonus);
			player.GetCritChance(DamageClass.Generic) += critBonus;
		}
	}
}