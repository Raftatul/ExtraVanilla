using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Buffs
{
	public class MegaRegen : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mega Regen !!!");
			// Description.SetDefault("Look at your health bar");
		}

        public override void Update(Player player, ref int buffIndex)
		{
			//player.lifeRegen += 6;
		}
	}
}