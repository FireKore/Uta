using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uta.TextEngine;

namespace Uta_.TextEngine
{
    class TextLine
    {
        private TimeSpan startTime;
        private TimeSpan endTime;
        private String style;
        private List<TextSyllab> syllabs = new List<TextSyllab>();
        private SpriteFont font;

        public TextLine(string[] text, string[] format)
        {
            for (int i = 0; i < format.Length; ++i)
            {
                if(String.Compare(format[i].Trim(), "Start") == 0)
                {
                    int h = 0, m = 0, s = 0, ms = 0;
                    int.TryParse(text[i].Split(':')[0], out h);
                    int.TryParse(text[i].Split(':')[1], out m);
                    int.TryParse(text[i].Split(':')[2].Split('.')[0], out s);
                    int.TryParse(text[i].Split(':')[2].Split('.')[1], out ms);
                    this.startTime = new TimeSpan(0, h, m, s, ms);
                }
        
                if (String.Compare(format[i].Trim(), "End") == 0)
                {
                    int h = 0, m = 0, s = 0, ms = 0;
                    int.TryParse(text[i].Split(':')[0], out h);
                    int.TryParse(text[i].Split(':')[1], out m);
                    int.TryParse(text[i].Split(':')[2].Split('.')[0], out s);
                    int.TryParse(text[i].Split(':')[2].Split('.')[1], out ms);
                    this.endTime = new TimeSpan(0, h, m, s, ms);
                }

                if(String.Compare(format[i].Trim(), "Style") == 0)
                {
                    this.style = text[i];
                }

                if (String.Compare(format[i].Trim(), "Text") == 0)
                {
                    int totalDelay = 0;
                    String[] syllabs = text[i].Split('{');
                    if (syllabs.Length > 1)
                    {
                        for (int j = 1; j < syllabs.Length; ++j)
                        {
                            int delay = 0;
                            this.syllabs.Add(new TextSyllab(totalDelay * 10, syllabs[j].Split('}')[1]));
                            int.TryParse(syllabs[j].Split('}')[0].Substring(2), out delay);
                            totalDelay += delay;
                        }
                    }
                    else
                    {
                        this.syllabs.Add(new TextSyllab(0, syllabs[0]));
                    }
                }
            }

            //TODO : compute every syllab's position according to the font and the margins
            for (int i = 0; i < this.syllabs.Count; ++i)
            {
                this.syllabs[i].setPosition(new Vector2(10 + 50 * i, 200));
            }

        }

        public void setFont(SpriteFont font)
        {
            this.font = font;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds >= this.startTime.TotalMilliseconds && gameTime.TotalGameTime.TotalMilliseconds <= this.endTime.TotalMilliseconds)
            {
                double millisEllapsed = gameTime.TotalGameTime.TotalMilliseconds - this.startTime.TotalMilliseconds;
                foreach (TextSyllab syllab in syllabs)
                {
                    syllab.Draw(spriteBatch, this.font, syllab.getDelay() >= millisEllapsed ? Color.Red : Color.White);
                }
            }
        }

        private Vector2 getTotalSize()
        {
            Vector2 size = new Vector2();
            size.Y = syllabs[0].getSize(this.font).Y;
            foreach(TextSyllab syllab in syllabs)
            {
                size.X += syllab.getSize(this.font).X;
            }
            return size;
        }
    }
}
