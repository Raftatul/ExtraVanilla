using ExtraVanilla.Content.Items.Accessories;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Common.GlobalItems
{
    internal class CustomShimmerRecipes : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.WizardsHat;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.WizardsHat);
            recipe.AddIngredient(ModContent.ItemType<SuperInvocator>());
            recipe.AddCondition(Condition.TimeDay);
            recipe.AddCondition(Condition.TimeNight);
            recipe.Register();
        }
    }
}
