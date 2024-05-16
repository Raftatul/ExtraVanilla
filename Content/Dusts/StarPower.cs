using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExtraVanilla.Content.Dusts
{
    internal class StarPower : ModDust
    {
        private int _timer = 0;

        private Asset<Texture2D> _glowTexture;

        public override void SetStaticDefaults()
        {
            _glowTexture = ModContent.Request<Texture2D>("ExtraVanilla/Content/Dusts/GlowStarPower");
        }

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 100, 100);
        }

        public override bool Update(Dust dust)
        {
            dust.rotation += MathHelper.ToRadians(5f);
            dust.scale = MathF.Cos(_timer++ / 16f) + 4f;
            dust.scale *= 0.3f;

            dust.color = Color.Lerp(Color.Purple, Color.Cyan, MathF.Cos(_timer++ / 32f));

            Lighting.AddLight(dust.position, new Vector3(2, 2, 2));

            return false;
        }

        public override bool PreDraw(Dust dust)
        {
            Vector2 drawPos = dust.position - Main.screenPosition;
            Vector2 drawCenter = dust.frame.Size() / 2;

            Color outlineColor = Color.White;
            outlineColor.A = 30;

            Main.spriteBatch.Draw(_glowTexture.Value, drawPos, dust.frame, outlineColor, dust.rotation, drawCenter, dust.scale * 1.5f, SpriteEffects.None, 1);
            Main.spriteBatch.Draw(Texture2D.Value, drawPos, dust.frame, dust.color, dust.rotation, drawCenter, dust.scale, SpriteEffects.None, 1);

            return false;
        }
    }
}
