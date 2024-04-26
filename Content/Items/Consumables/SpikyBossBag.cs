using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Items.Consumables
{
	public class SpikyBossBag : ModItem
	{
        public override void SetStaticDefaults()
        {
            // This set is one that every boss bag should have.
            // It will create a glowing effect around the item when dropped in the world.
            // It will also let our boss bag drop dev armor..
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; // ..But this set ensures that dev armor will only be dropped on special world seeds, since that's the behavior of pre-hardmode boss bags.

            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.width = 32;
            Item.height = 32;
            Item.consumable = true;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Coins(50000, true));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Accessories.CritHeart>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pets.Jack.JackItem>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Accessories.Flashlight>(), 2));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Accessories.LivingBoots>(), 5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Weapons.Chakram>(), 50));
        }

       // public override void OpenBossBag(Player player)
       // {
       //     player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.GoldCoin, 10);
       //     player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.WoodenArrow, Main.rand.Next(20, 30));
       //     int rand = Main.rand.Next(0, 3);
       //     if (rand == 1)
       //     {
			    //player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Items.Accessories.CritHeart>());
       //     }
       //     if (rand == 2)
       //     {
       //         player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Items.Accessories.LivingBoots>());
       //     }
       //     rand = Main.rand.Next(0, 10);
       //     if (rand == 1)
       //     {
       //         player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Pets.Jack.JackItem>());
       //     }
       //     player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Items.Accessories.Flashlight>());
       // }
	}
}
