using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Common.GlobalNPCs
{
    internal class VanillaBossNpc : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.KingSlime;
        }

        public override void OnKill(NPC npc)
        {
            //NPC.SetEventFlagCleared(ref Systems.DownedBossSystem.downedKingSlime, -1);
        }
    }

    internal class EyeofCthulhu : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.EyeofCthulhu;
        }

        public override void OnKill(NPC npc)
        {
            //NPC.SetEventFlagCleared(ref Systems.DownedBossSystem.downedEyeofCthulhu, -1);
        }
    }

    internal class QueenBee : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.QueenBee;
        }

        public override void OnKill(NPC npc)
        {
            //NPC.SetEventFlagCleared(ref Systems.DownedBossSystem.downedQueenBee, -1);
        }
    }
}
