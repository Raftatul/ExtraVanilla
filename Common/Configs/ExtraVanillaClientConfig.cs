using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ExtraVanilla.Common.Configs
{
    public class ExtraVanillaClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        public bool startMessage;
    }
}
