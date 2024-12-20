﻿using Terraria;
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
