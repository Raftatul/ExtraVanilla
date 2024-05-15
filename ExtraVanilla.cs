using ExtraVanilla.Content.Items;
using ExtraVanilla.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Chat;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExtraVanilla
{
    public class ExtraVanilla : Mod
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                // Attempt to load the effect
                Asset<Effect> dyeEffect = Assets.Request<Effect>("Assets/Effects/ExampleEffect");
                Asset<Effect> screenEffect = Assets.Request<Effect>("Assets/Effects/MagicRodSceneEffect");

                GameShaders.Armor.BindShader(ModContent.ItemType<ExampleDye>(), new ArmorShaderData(dyeEffect, "ExampleDyePass"));
                GameShaders.Armor.BindShader(ModContent.ItemType<MagicRod>(), new ArmorShaderData(dyeEffect, "ExampleDyePass"));

                // Register the screen shader with a reasonable priority
                Filters.Scene["ExampleEffect"] = new Filter(new ScreenShaderData(screenEffect, "FilterMyShader"), EffectPriority.Medium);
            }
        }

        public static void Talk(string message, Color textColor)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(message, textColor);
            }
            else
            {
                NetworkText text = NetworkText.FromLiteral(message);
                ChatHelper.BroadcastChatMessage(text, textColor);
            }
        }
    }
}