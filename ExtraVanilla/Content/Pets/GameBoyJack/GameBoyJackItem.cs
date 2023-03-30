using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Pets.GameBoyJack
{
    public class GameBoyJackItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("New Gen Robot Head");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DD2PetGato);
            Item.shoot = ModContent.ProjectileType<GameBoyJack>();
            Item.buffType = ModContent.BuffType<GameBoyJackBuff>();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}