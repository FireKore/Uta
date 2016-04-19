using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Uta.TextEngine
{
    class TextSyllab
    {
        private int delay;
        private String text;
        private Vector2 position;

        public TextSyllab(int delay, String text)
        {
            this.delay = delay;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, this.text, position, color);
        }

        public int getDelay()
        {
            return this.delay;
        }

        public Vector2 getSize(SpriteFont font)
        {
            return font.MeasureString(this.text);
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
