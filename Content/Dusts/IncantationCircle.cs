using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Dusts
{
    internal class IncantationCircle : ModDust
    {
        private int _timer = 0;

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 252, 258);
            dust.scale = 0;
            dust.velocity *= 2f;

            dust.color = Color.Lerp(Color.Purple, Color.Cyan, Main.rand.NextFloat());
        }

        public override bool PreDraw(Dust dust)
        {
            Vector2 drawPos = dust.position - Main.screenPosition;
            Vector2 drawCenter = dust.frame.Size() / 2;

            Main.spriteBatch.Draw(Texture2D.Value, drawPos, dust.frame, dust.color, dust.rotation, drawCenter, dust.scale, SpriteEffects.None, 1);

            return false;
        }

        public override bool Update(Dust dust)
        {
            dust.rotation += MathHelper.ToRadians(2f);
            dust.scale = MathHelper.Lerp(dust.scale, ((MathF.Cos(_timer++ / 16f) / 4f) + 4f) * 0.6f, 0.02f);

            Lighting.AddLight(dust.position, new Vector3(2, 0, 0));

            return false;
        }
    }
}
