using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Items.Accessories
{
    internal class WoodenShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wooden Shield");
            /* Tooltip.SetDefault("3 defense" +
                "\nGives a chance of not receive knockback"); */
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.shopCustomPrice = Item.sellPrice(copper: 50);
            Item.accessory = true;
            Item.hasVanityEffects = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().WoodenShield = true;
            player.statDefense += 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 30)
                .AddRecipeGroup("IronBar", 7)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
