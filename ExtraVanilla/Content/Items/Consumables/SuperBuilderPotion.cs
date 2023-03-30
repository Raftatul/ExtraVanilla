using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraVanilla;

namespace ExtraVanilla.Content.Items.Consumables
{
    public class SuperBuilderPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Builder Potion");
            Tooltip.SetDefault("Allow you to build super fast !");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(silver: 5);
            Item.buffType = ModContent.BuffType<Buffs.SuperBuilderBuff>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 72000; //The amount of time the buff declared in item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BuilderPotion, 10)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}