using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ExtraVanilla.Content.Projectiles;
using Microsoft.Xna.Framework;

namespace ExtraVanilla.Content.Items.Weapons
{
    public class Chakram : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 24; 
            Item.knockBack = 5;
            
            Item.useAnimation = 15;
            Item.useTime = 15;

            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;

            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<ChakramProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += new Vector2(0, -10);
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}