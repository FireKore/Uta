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
        private Vector2 screenSize;

        public TextLine(string[] text, string[] format, Vector2 screenSize)
        {
            this.screenSize = screenSize;
            for (int i = 0; i < format.Length; ++i)
            {
                if(String.Compare(format[i].Trim(), "Start") == 0)
                {
                    int h = 0, m = 0, s = 0, cs = 0;
                    int.TryParse(text[i].Split(':')[0], out h);
                    int.TryParse(text[i].Split(':')[1], out m);
                    int.TryParse(text[i].Split(':')[2].Split('.')[0], out s);
                    int.TryParse(text[i].Split(':')[2].Split('.')[1], out cs);
                    this.startTime = new TimeSpan(0, h, m, s, cs*10);
                }
        
                if (String.Compare(format[i].Trim(), "End") == 0)
                {
                    int h = 0, m = 0, s = 0, cs = 0;
                    int.TryParse(text[i].Split(':')[0], out h);
                    int.TryParse(text[i].Split(':')[1], out m);
                    int.TryParse(text[i].Split(':')[2].Split('.')[0], out s);
                    int.TryParse(text[i].Split(':')[2].Split('.')[1], out cs);
                    this.endTime = new TimeSpan(0, h, m, s, cs*10);
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
                            this.syllabs.Add(new TextSyllab(totalDelay, syllabs[j].Split('}')[1]));
                            int.TryParse(syllabs[j].Split('}')[0].Substring(2), out delay);
                            totalDelay += delay * 10;
                        }
                    }
                    else
                    {
                        this.syllabs.Add(new TextSyllab(0, syllabs[0]));
                    }
                }
            }

            //TODO : compute every syllab's position according to the font and the margins

            /*for (int i = 0; i < this.syllabs.Count; ++i)
            {
                this.syllabs[i].setPosition(new Vector2(10 + 50 * i, 200));
            }*/

        }

        public void setFont(SpriteFont font)
        {
            this.font = font;
            //Computing the positions here cause we need the font, but the content are loaded after the initialization (much possibilities to improve this part)
            //TODO IMPROVE!
            if (getTotalSize().X >= (screenSize.X * 2) / 3)
            {
                //TODO cut the line in two or more part so everything is displayed on screen
            }
            else
            {
                float marginV = 10.0f;
                //TODO replace the above variable by the one in the style
                float posX = (screenSize.X - getTotalSize().X) / 2;
                foreach (TextSyllab syllab in this.syllabs)
                {
                    syllab.setPosition(new Vector2(posX, screenSize.Y - getTotalSize().Y - marginV));
                    posX += syllab.getSize(this.font).X;
                }
            }
        }

		public void Draw(TimeSpan ellapsedTime, SpriteBatch spriteBatch)
		{
			if(ellapsedTime >= this.startTime && ellapsedTime <= this.endTime)
            //if(MediaPlayer.PlayPosition >= this.startTime && MediaPlayer.PlayPosition <= this.endTime)
			{
				double millisEllapsed = ellapsedTime.TotalMilliseconds - this.startTime.TotalMilliseconds;
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
                if (size.Y < syllab.getSize(this.font).Y)
                    size.Y = syllab.getSize(this.font).Y;
            }
            return size;
        }
    }
}
