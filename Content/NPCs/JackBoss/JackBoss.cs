using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace ExtraVanilla.Content.NPCs.JackBoss
{
    [AutoloadBossHead]
	public class JackBoss : ModNPC
	{
		private int frame = 0;
		private int shootSpeed = 30;

		private int ai;

		private int stage = 0;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jack");
			Main.npcFrameCount[NPC.type] = 8;
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.lifeMax = 5000;
			NPC.damage = 25;
			NPC.defense = 15;
			NPC.knockBackResist = 0f;
			NPC.width = 48;
			NPC.height = 30;
			NPC.npcSlots = 1f;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath54; 
			//npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/TestBossDeath");
			Music = MusicID.Boss1;
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            base.ApplyDifficultyAndPlayerScaling(numPlayers, balance, bossAdjustment);
        }
        
        public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];

			Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;

			if(NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
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
						ExtraVanilla.Talk("...", new Color(175, 75, 255));
					}
					stage++;
					break;

				case 1:
					NPC.damage = 25;
					frame = 0;
					NPC.rotation = 0;
					Chasing(NPC, target + new Vector2(0, -50), (float)(distance > 300 ? 9f : 6f), 30f);
					OrientToPlayer(target);
					break;

				case 2:
					frame = 0;
					NPC.spriteDirection = -player.direction;
					Chasing(NPC, target + new Vector2(0, -150), 15f, 0f);
                    break;

				case 3:
					frame = 0;
					NPC.rotation = 0;
					Chasing(NPC, target + new Vector2(0, -300), (float)(distance > 300 ? 13f : 7f), 60);
					if (NPC.ai[0] % (NPC.life <= (NPC.lifeMax / 2) ? 30 : 60) == 0)
					{
						Vector2 velocity = target - NPC.position;
						velocity.Normalize();
						Shoot(NPC.Center, velocity * 10, ProjectileID.CultistBossFireBall, 15, 1, SoundID.DD2_BetsyFireballShot);
					}
					OrientToPlayer(target);
					break;
			}

			if ((double)NPC.ai[0] <= 300)
			{
				stage = 1;
			}
			else if ((double)NPC.ai[0] <= 750)
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

		private void OrientToPlayer(Vector2 target)
        {
            if (NPC.Center.X <= target.X)
            {
				NPC.spriteDirection = -1;
            }
            else
            {
				NPC.spriteDirection = 1;
            }
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

		public override void OnKill()
        {
            if (!Main.expertMode)
            {
				int rand = Main.rand.Next(0, 3);
				Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Items.Accessories.Flashlight>(), 1);
				if (rand == 1)
				{
					Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Items.Accessories.CritHeart>());
				}
				if (rand == 2)
				{
					Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Items.Accessories.LivingBoots>());
				}
				rand = Main.rand.Next(0, 10);
				if (rand == 1)
				{
					Item.NewItem(NPC.GetSource_FromAI(), NPC.position, ModContent.ItemType<Pets.Jack.JackItem>());
				}
			}
        }

        /*public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
			bestiaryEntry.Info.Add(new FlavorTextBestiaryInfoElement("..."));
            base.SetBestiary(database, bestiaryEntry);
        }*/

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