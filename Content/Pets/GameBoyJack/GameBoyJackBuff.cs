﻿using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Pets.GameBoyJack
{
    public class GameBoyJackBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().GameBoyJackPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<GameBoyJack>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<GameBoyJack>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}