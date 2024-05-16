using ExtraVanilla.Common.Players;
using ExtraVanilla.Content.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
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

        public override bool ConsumeItem(Player player) => false;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            ExtraVanillaPlayer modPlayer = player.GetModPlayer<ExtraVanillaPlayer>();
            modPlayer.explosifStaffSafe = !modPlayer.explosifStaffSafe;
            Console.WriteLine(modPlayer.explosifStaffSafe);

            if (modPlayer.explosifStaffSafe)
                Main.NewText("Safe Mode enabled !");
            else
                Main.NewText("Safe Mode disabled !");
        }
    }
}
