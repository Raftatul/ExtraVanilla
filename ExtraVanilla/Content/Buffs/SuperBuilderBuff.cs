using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Buffs
{
	public class SuperBuilderBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bob the builder");
			Description.SetDefault("Excellent way to build bridge");
		}

        public override void Update(Player player, ref int buffIndex)
		{
			player.tileSpeed += 100;
			player.wallSpeed += 100;
			player.GetModPlayer<Common.Players.ExtraVanillaPlayer>().SuperBuilderBuffer = true;
		}
	}
}