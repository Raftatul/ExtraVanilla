using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Items.Consumables
{
	public class SpikyBossBag : ModItem
	{
        public override int BossBagNPC => Mod.Find<ModNPC>("SpikyBoss").Type;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiky Boss Bag");
            Tooltip.SetDefault("<right> to open");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.GoldCoin, 10);
            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.WoodenArrow, Main.rand.Next(20, 30));
            int rand = Main.rand.Next(0, 3);
            if (rand == 1)
            {
			    player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Items.Accessories.CritHeart>());
            }
            if (rand == 2)
            {
                player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Items.Accessories.LivingBoots>());
            }
            rand = Main.rand.Next(0, 10);
            if (rand == 1)
            {
                player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Pets.Jack.JackItem>());
            }
            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<Items.Accessories.Flashlight>());
        }
	}
}
