using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Buffs
{
	public class AttackSpeedBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("You have swift hand !");
			Description.SetDefault("Increase melee speed by 25%");
		}

        public override void Update(Player player, ref int buffIndex)
		{
			
			//player.meleeSpeed += player.meleeSpeed * 0.25f;
		}
	}
}