using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Common.GlobalNPCs
{
    public class WeaponEffectNpc : GlobalNPC
    {
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            // Check if the NPC is not a boss
            if (!npc.boss)
            {
                // Make the NPC float when it takes damage
                npc.velocity.Y = -8f;

                // Set a timer to stop the NPC from floating after 2 second
                npc.ai[0] = 120;
            }
        }

        public override void AI(NPC npc)
        {
            // Check if the NPC's floating timer is greater than 0
            if (npc.ai[0] > 0)
            {
                // Decrement the timer
                npc.ai[0]--;

                // If the timer has reached 0, stop the NPC from floating
                if (npc.ai[0] == 0)
                {
                    npc.velocity.Y = 0;
                }
            }
        }
    }
}