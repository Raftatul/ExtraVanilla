using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ExtraVanilla.Content.GlobalItems
{
    class HealingPotion : GlobalItem
    {
        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.healLife > 0)
            {
                player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().HealReceive = item.healLife;
                if (player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().HealPotionBonus)
                {
                    Projectile.NewProjectile(Item.GetSource_NaturalSpawn(), player.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.ProjHeartBonus>(), 0, 0, item.playerIndexTheItemIsReservedFor);
                }
            }
            return base.ConsumeItem(item, player);
        }
    }
}
