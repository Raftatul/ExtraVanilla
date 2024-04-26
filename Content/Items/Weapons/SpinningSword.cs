using ExtraVanilla.Content.Projectiles;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Items.Weapons
{
    public class SpinningSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 2;

            Item.useAnimation = 30;
            Item.useTime = 30;

            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            
            Item.shootSpeed = 25f;
            Item.value = Item.sellPrice(silver: 45);
            Item.shoot = ModContent.ProjectileType<SpinningSwordProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 20)
                .AddRecipeGroup("IronBar", 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
