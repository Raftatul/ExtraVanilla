﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Dusts
{
    internal class StarDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 100, 100);
            dust.scale = 0.3f;
            dust.velocity *= 2f;

            dust.color = Color.Lerp(Color.Purple, Color.Cyan, Main.rand.NextFloat());
        }

        public override bool PreDraw(Dust dust)
        {
            Vector2 drawPos = dust.position - Main.screenPosition;
            Vector2 drawCenter = dust.frame.Size() / 2;

            Color outlineColor = dust.color * 2f;
            outlineColor.A = 69;

            //Main.spriteBatch.Draw(Texture2D.Value, drawPos, dust.frame, outlineColor, dust.rotation, drawCenter, dust.scale * 1.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Texture2D.Value, drawPos, dust.frame, dust.color, dust.rotation, drawCenter, dust.scale, SpriteEffects.None, 0f);

            return false;
        }

        public override bool Update(Dust dust)
        {
            // Calls every frame the dust is active
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.99f;

            float light = 0.35f * dust.scale;

            Lighting.AddLight(dust.position, light, light, light);

            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}