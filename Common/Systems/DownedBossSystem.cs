using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExtraVanilla.Common.Systems
{
    internal class DownedBossSystem : ModSystem
    {
        /*
        //Mod
        public static bool downedSpiky = false;

        //Vanilla
        public static bool downedQueenBee = false;
        public static bool downedEyeofCthulhu = false;
        public static bool downedKingSlime = false;

        public override void OnWorldLoad()
        {
            downedSpiky = false;
            downedKingSlime = false;
            downedEyeofCthulhu = false;
            downedQueenBee = false;
        }

        public override void OnWorldUnload()
        {
            downedSpiky = false;
            downedSpiky = false;
            downedKingSlime = false;
            downedEyeofCthulhu = false;
            downedQueenBee = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedSpiky)
            {
                tag["downedSpiky"] = true;
            }
            if (downedKingSlime)
            {
                tag["downedKingSlime"] = true;
            }
            if (downedEyeofCthulhu)
            {
                tag["downedEyeofCthulhu"] = true;
            }
            if (downedQueenBee)
            {
                tag["downedQueenBee"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedSpiky = tag.ContainsKey("downedSpiky");
            downedKingSlime = tag.ContainsKey("downedKingSlime");
            downedEyeofCthulhu = tag.ContainsKey("downedEyeofCthulhu");
            downedQueenBee = tag.ContainsKey("downedQueenBee");
        }

        public override void NetSend(BinaryWriter writer)
        {
            bool[] flags = new bool[] {
                downedSpiky,
                downedKingSlime,
                downedEyeofCthulhu,
                downedQueenBee,
            };
            BitArray bitArray = new BitArray(flags);
            byte[] bytes = new byte[(bitArray.Length - 1) / 8 + 1]; // Calculation for correct length of the byte array
            bitArray.CopyTo(bytes, 0);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }

        public override void NetReceive(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            byte[] bytes = reader.ReadBytes(length);
            BitArray bitArray = new BitArray(bytes);
            downedSpiky = bitArray[0];
            downedKingSlime = bitArray[1];
            downedEyeofCthulhu = bitArray[2];
            downedQueenBee = bitArray[3];
        }

        public override void PreUpdateWorld()
        {
            foreach (Player player in Main.player)
            {
                if (downedSpiky == true)
                {
                    player.pickSpeed -= player.pickSpeed * 0.1f;
                }
                //KingSlime in player
                if (downedEyeofCthulhu == true)
                {
                    player.GetDamage(DamageClass.Generic) += 0.1f;
                }
                if (downedQueenBee == true)
                {
                    player.dashDelay -= ((int)(player.dashDelay * 0.25f));
                }
            }
        }
        */
    }
}
