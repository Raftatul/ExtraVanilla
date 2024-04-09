using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Common.GlobalNPCs
{
    public class CustomNpcLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.HeadlessHorseman)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.CursedCape>(), 80));
            }
            if (npc.type == NPCID.Mimic)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Accessories.StoneOfLife>(), 70));
            }
        }
    }
}
