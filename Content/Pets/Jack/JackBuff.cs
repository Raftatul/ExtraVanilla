﻿using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Pets.Jack
{
    public class JackBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
            // DisplayName.SetDefault("Jack");
            // Description.SetDefault("A \"cute\" little robot follow you !");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().JackPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Jack>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<Jack>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}