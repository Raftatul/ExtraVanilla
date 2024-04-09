using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Common.GlobalItems
{
    internal class AutoReuse : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.DamageType == DamageClass.Summon;
        }

        public override void SetDefaults(Item item)
        {
            item.autoReuse = true;
        }
    }
}
