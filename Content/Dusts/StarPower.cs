using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Dusts
{
    internal class StarPower : ModDust
    {
        private int _timer = 0;

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 100, 100);
        }

        public override bool Update(Dust dust)
        {
            dust.rotation += MathHelper.ToRadians(5);
            dust.scale = MathF.Cos(_timer++ / 16) + 4;
            dust.scale *= 0.3f;

            dust.color = Color.Lerp(Color.Purple, Color.Cyan, MathF.Cos(_timer++ / 32f));

            Console.WriteLine(MathF.Cos(_timer++) + 4);

            return false;
        }

        public override bool PreDraw(Dust dust)
        {
            Vector2 drawPos = dust.position - Main.screenPosition;
            Vector2 drawCenter = dust.frame.Size() / 2;

            Color outlineColor = dust.color * 2f;
            outlineColor.A = 69;

            //Main.spriteBatch.Draw(Texture2D.Value, drawPos, dust.frame, outlineColor, dust.rotation, drawCenter, dust.scale * 1.5f, SpriteEffects.None, 1);
            Main.spriteBatch.Draw(Texture2D.Value, drawPos, dust.frame, dust.color, dust.rotation, drawCenter, dust.scale, SpriteEffects.None, 1);

            return false;
        }
    }
}
