using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace ExtraVanilla
{
	public class ExtraVanilla : Mod
	{
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