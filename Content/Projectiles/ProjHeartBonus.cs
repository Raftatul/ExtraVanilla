using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtraVanilla.Content.Projectiles
{
	public class ProjHeartBonus : ModProjectile
	{
		private int startLifeTime;
		private int quaterLifeTime;
		private int index = 0;
		private int walkDistance = 10;

		private List<Vector2> targetPos = new List<Vector2>();
		private Vector2 spawnPoint;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("BonusHeart");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;              //The width of projectile hitbox 
			Projectile.height = 22;         // The height of projectile hitbox
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 900;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 1;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			startLifeTime = Projectile.timeLeft;
			quaterLifeTime = startLifeTime / 4;
			Projectile.netUpdate = true;
		}

        public override bool PreDraw(ref Color lightColor)
        {
			quaterLifeTime--;
			if (quaterLifeTime == 0)
			{
				quaterLifeTime = startLifeTime / 4;
				Projectile.frame++;
			}
			return base.PreDraw(ref lightColor);
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);

            spawnPoint = Main.player[Projectile.owner].position;
            targetPos.Add(spawnPoint + new Vector2(-1, -1) * walkDistance);
            targetPos.Add(spawnPoint + new Vector2(1, 1) * walkDistance);
            targetPos.Add(spawnPoint + new Vector2(1, -1) * walkDistance);
            targetPos.Add(spawnPoint + new Vector2(-1, 1) * walkDistance);
        }

        public override void AI()
        {
			if (targetPos.Count == 0)
				return;

			int distance = ((int)Vector2.Distance(targetPos[index], Projectile.Center));
			if (distance <= 1)
            {
				index++;
				if (index == targetPos.Count)
                {
					index = 0;
                }
			}
			Chasing(Projectile, targetPos[index], 1, 60);
			Projectile.rotation = Projectile.velocity.X;
        }

		private void Chasing(Projectile projectile, Vector2 targetPoint, float speed, float turnResistance)
		{
			var move = targetPoint - projectile.Center;
			float length = move.Length();
			if (length > speed)
			{
				move *= speed / length;
			}
			move = (projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			length = move.Length();
			if (length > speed)
			{
				move *= speed / length;
			}
			projectile.velocity = move;
		}

		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
            {
				Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ItemID.Heart);
            }

			for (int i = 0; i < 50; i++)
			{
				Vector2 direction = new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)) * 5;
				int dust = Dust.NewDust(Projectile.position, ((int)direction.X), ((int)direction.Y), DustID.Iron);
				Main.dust[dust].scale = Main.rand.NextFloat(0.5f, 2f);
				Main.dust[dust].noGravity = true;
			}


			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.DoubleJump, Projectile.position);
		}
	}
}