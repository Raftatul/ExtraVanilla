using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ExtraVanilla.Content.Projectiles;

namespace ExtraVanilla.Content.Items.Weapons
{
    public class SpinningSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;

            Item.damage = 12; 
            Item.knockBack = 2;
            
            Item.useAnimation = 15;
            Item.useTime = 15;

            Item.noMelee = true;
            Item.DamageType = DamageClass.Melee;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.shootSpeed = 20f;
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