using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;

namespace ExtraVanilla.Content.NPCs.SpikyBoss
{
    [AutoloadBossHead]
	public class SpikyBoss : ModNPC
	{
		private int frame = 0;

		private int ai;

		private int stage = 0;

        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.lifeMax = 5000;
			NPC.damage = 25;
			NPC.defense = 15;
			NPC.knockBackResist = 0f;
			NPC.width = 100;
			NPC.height = 100;
			NPC.npcSlots = 1f;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1; 
			//npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/TestBossDeath");
			Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/SpikyTheme");

        }

		public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];

			Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;

			if(NPC.target < 0 || NPC.target == 255 || player.dead || !player.active || Main.dayTime)
            {
				NPC.TargetClosest(false);
				NPC.direction = 1;
				NPC.velocity.Y = NPC.velocity.Y - 0.1f;
				if(NPC.timeLeft > 20)
                {
					NPC.timeLeft = 20;
					return;
                }
				return;
            }

			//Increment AI
			ai++;

			NPC.ai[0] = (float)ai * 1f;
			int distance = ((int)Vector2.Distance(target, NPC.Center));

			switch (stage)
            {
				case 0:
                    if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.SinglePlayer)
                    {
	                    ExtraVanilla.Talk("You shouldn't have created me...", new Color(175, 75, 255));
                    }
					stage++;
					break;
				case 1:
					NPC.damage = 25;
					frame = 0;
					NPC.rotation = 0;
					Chasing(NPC, target + new Vector2(0, -50), (float)(distance > 300 ? 9f : 6f), 30f);
					break;
				case 2:
					NPC.damage = 50;
					frame = 1;
					NPC.rotation += 0.25f;
					player.AddBuff(BuffID.Slow, 1);
					Chasing(NPC, target, (float)(NPC.life <= (NPC.lifeMax / 2) ? 10f : 9f), 60f);
					break;
				case 3:
					frame = 2;
					NPC.rotation = 0;
					Chasing(NPC, target + new Vector2(0, -300), (float)(distance > 300 ? 13f : 7f), 60);
					if (NPC.ai[0] % (NPC.life <= (NPC.lifeMax / 2) ? 30 : 60) == 0)
					{
						Vector2 velocity = target - NPC.position;
						velocity.Normalize();
						Shoot(NPC.Center, velocity * 10, ProjectileID.CultistBossFireBall, 15, 1, SoundID.DD2_BetsyFireballShot);
					}
					break;
			}

			if ((double)NPC.ai[0] < 300 || (double)NPC.ai[0] >= 450 && (double)NPC.ai[0] < 750)
			{
				stage = 1;
			}
			else if ((double)NPC.ai[0] >= 300 && (double)NPC.ai[0] < 450)
			{
				stage = 2;
			}
			else if ((double)NPC.ai[0] >= 750)
			{
				stage = 3;
			}

			if ((double)NPC.ai[0] >= 950)
			{
				ai = 0;
			}

			NPC.netUpdate = true;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frame.Height = frameHeight;
			NPC.frame.Y = frame * frameHeight;
		}

		private void Chasing(NPC npc, Vector2 playerTarget, float speed, float turnResistance)
        {
			var move = playerTarget - npc.Center;
			float length = move.Length();
			if(length > speed)
            {
				move *= speed / length;
            }
			move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
			length = move.Length();
			if(length > speed)
            {
				move *= speed / length;
            }
			npc.velocity = move;
        }

		private void Shoot(Vector2 shootPos, Vector2 velocity, int projectileId, int damage, int knockBack, SoundStyle soundId)
        {
			Projectile.NewProjectile(NPC.GetSource_FromAI(), shootPos, velocity, projectileId, damage, knockBack);
			SoundEngine.PlaySound(soundId, shootPos);
        }

		public override void HitEffect(NPC.HitInfo hit)
        {
	        // If the NPC dies, spawn gore and play a sound
	        if (Main.netMode == NetmodeID.Server) {
		        // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
		        return;
	        }
	        
			if (NPC.life <= 0)
			{
				// These gores work by simply existing as a texture inside any folder which path contains "Gores/"
				int gore1 = Mod.Find<ModGore>("Gore1").Type;
				int gore2 = Mod.Find<ModGore>("Gore2").Type;
				int gore3 = Mod.Find<ModGore>("Gore3").Type;

				var entitySource = NPC.GetSource_Death();

				for (int i = 0; i < 2; i++)
				{
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
				}

				SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
			}
		}

        public override void OnKill()
        {
			//NPC.SetEventFlagCleared(ref Common.Systems.DownedBossSystem.downedSpiky, -1);
            if (!Main.expertMode)
            {
	            Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Items.Accessories.Flashlight>(), 1);
				if (Main.rand.NextBool(5))
				{
					Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Items.Accessories.LivingBoots>());
				}
				if (Main.rand.NextBool(2))
				{
					Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Pets.Jack.JackItem>());
				}
			}
        }

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
				new FlavorTextBestiaryInfoElement("It's your creation! But he wants to kill you...")
				});;
            base.SetBestiary(database, bestiaryEntry);
        }

		public override void BossLoot(ref string name, ref int potionType)
        {
			potionType = ItemID.HealingPotion;
        }

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.Consumables.SpikyBossBag>()));
		}
	}
}