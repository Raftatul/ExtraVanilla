using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Pets.Jack
{
    public class JackItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DD2PetGato);
            Item.shoot = ModContent.ProjectileType<Jack>();
            Item.buffType = ModContent.BuffType<JackBuff>();
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