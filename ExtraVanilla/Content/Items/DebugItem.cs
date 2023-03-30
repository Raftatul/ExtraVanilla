using System.Xml;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Colors = On.Terraria.ID.Colors;

namespace ExtraVanilla.Content.Items
{
    internal class DebugItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Change the time");
        }

        public override void SetDefaults()
        {
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
        }

        public override bool? UseItem(Player player)
        {
            if(Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.SinglePlayer)
            {
                if (Main.dayTime)
                {
                    ExtraVanilla.Talk("Time change at night", Color.BlueViolet);
                    Main.SkipToTime(0, false);
                }
                else
                {
                    ExtraVanilla.Talk("Time change at day", Color.BlueViolet);
                    Main.SkipToTime(0, true);
                }
                return false;
            }
            return base.UseItem(player);
        }
    }
}
