using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace ExtraVanilla.Content.Projectiles
{
    class FlyingShield : ModProjectile
    {
		private float ai;

		private bool spawn;

		private Vector2 spawnPoint;
		private Vector2 targetPos;

		private float timer;
		private int switcher = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flying Shield");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			Projectile.width = 36;              //The width of projectile hitbox 
			Projectile.height = 35;         // The height of projectile hitbox
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 1;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.netUpdate = true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return false;
		}

        public override void AI()
		{
			timer++;
			Projectile.rotation = Projectile.velocity.X;
			int distance = ((int)Vector2.Distance(Projectile.Center, targetPos));
			if(Projectile.velocity.Length() > 0.5f && !spawn)
            {
				Projectile.velocity -= Projectile.velocity * 0.1f;
            }
            else
            {
                if (!spawn)
                {
					spawn = true;
					spawnPoint = Projectile.position;
					targetPos = new Vector2(spawnPoint.X - 50, spawnPoint.Y);
				}
				if(distance < 20)
                {
					switcher++;
					if(switcher % 2 == 0)
                    {
						targetPos = new Vector2(spawnPoint.X + 50, spawnPoint.Y);
                    }
                    else
                    {
						targetPos = new Vector2(spawnPoint.X - 50, spawnPoint.Y);
					}
                }
				Chasing(Projectile, targetPos, 1, 180);
			}
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
			for(int i = 0; i < 20; i++)
            {
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.BorealWood);
            }
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}
