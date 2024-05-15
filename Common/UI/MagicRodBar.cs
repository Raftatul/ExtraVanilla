using ExtraVanilla.Common.Players;
using ExtraVanilla.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraVanilla.Common.UI
{
    internal class MagicRodBar : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.HAlign = (area.VAlign = 0.5f);
            //area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(60, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(110, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(26, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("ExtraVanilla/Common/UI/MagicRodBar")); // Frame of our resource bar
            barFrame.Width.Set(110, 0f);
            barFrame.Height.Set(26, 0f);

            gradientA = new Color(138, 25, 25); // A dark purple
            gradientB = new Color(201, 91, 91); // A light purple

            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (Main.LocalPlayer.HeldItem.ModItem is not MagicRod)
                return;

            if (Main.LocalPlayer.GetModPlayer<ExtraVanillaPlayer>().CanUseMagicRod)
                return;

            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<ExtraVanillaPlayer>();

            // Calculate quotient
            float quotient = (float)modPlayer.MagicRodCooldown / modPlayer.MaxMagicRodCooldown; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                // float percent = (float)i / steps; // Alternate Gradient Approach
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class ExampleResourceUISystem : ModSystem
    {
        private UserInterface ExampleResourceBarUserInterface;

        internal MagicRodBar ExampleResourceBar;

        public static LocalizedText ExampleResourceText { get; private set; }

        public override void Load()
        {
            ExampleResourceBar = new();
            ExampleResourceBarUserInterface = new();
            ExampleResourceBarUserInterface.SetState(ExampleResourceBar);

            string category = "UI";
            ExampleResourceText ??= Mod.GetLocalization($"{category}.ExampleResource");
        }

        public override void UpdateUI(GameTime gameTime)
        {
            ExampleResourceBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExtraVanilla: Example Resource Bar",
                    delegate
                    {
                        ExampleResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
