using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Buffs
{
	public class SuperBuilderBuff : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
		{
			player.tileSpeed += 100;
			player.wallSpeed += 100;
			player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().SuperBuilderBuffer = true;
		}
	}
}