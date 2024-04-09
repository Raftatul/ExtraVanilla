using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System;
using Microsoft.Xna.Framework;

namespace ExtraVanilla.Content.Items.Accessories
{
	public class Flashlight : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.hasVanityEffects = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Blue;
		}

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
			Lighting.AddLight(Item.position, new Vector3(2, 2, 2));
			base.Update(ref gravity, ref maxFallSpeed);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			Lighting.AddLight(player.position, new Vector3(2, 2, 2));
		}
    }
}