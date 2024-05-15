using ExtraVanilla.Common.Players;
using ExtraVanilla.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Items.Weapons
{
    public class MagicRod : ModItem
    {
        private int ai = 0;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LastPrism);

            Item.shoot = ModContent.ProjectileType<MagicRodHoldout>();
            Item.shootSpeed = 30f;
            Item.noMelee = true;

            // Avoid loading assets on dedicated servers. They don't use graphics cards.
            if (!Main.dedServ)
            {
                // The following code creates an effect (shader) reference and associates it with this item's type Id.
                GameShaders.Armor.BindShader(
                    Item.type,
                    new ArmorShaderData(Mod.Assets.Request<Effect>("Assets/Effects/ExampleEffect"), "ExampleDyePass") // Be sure to update the effect path and pass name here.
                );
            }
        }

        // Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<MagicRodHoldout>()] <= 0 && player.GetModPlayer<ExtraVanillaPlayer>().CanUseMagicRod;
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(new SoundStyle("ExtraVanilla/Assets/Music/MagicRodCasting", SoundType.Ambient));

            return false;
        }
    }
}
