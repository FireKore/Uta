using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uta_.TextEngine;

namespace Uta.TextEngine
{
    class TextEngine
    {
        private String subFileUri;
        private List<TextLine> textLines = new List<TextLine>();
        private List<Dictionary<String, String>> textStyles = new List<Dictionary<String, String>>();

        public TextEngine(String subtitlesFile)
        {
            subFileUri = subtitlesFile;
        }

        public void Initialize()
        {
            System.IO.StreamReader subFile = new System.IO.StreamReader(subFileUri);
            String line = "";
            String format = "";

            line = subFile.ReadLine();
            line.Trim();

            while (line != null)
            {

                if (line.Contains("["))
                {
                    if (line.Contains("Style"))
                    {
                        line = subFile.ReadLine();
                        line.Trim();
                        while (line != null && String.Compare(line, "") != 0 && !line.Contains("["))
                        {
                            if (line.Contains("Format:"))
                            {
                                format = line.Split(':')[1].Trim();
                            }
                            if(line.Contains("Style:"))
                            {
                                //AddTextStyle(line.Split(':')[1].Trim().Split(','), format.Split(','));
                            }
                            line = subFile.ReadLine();
                            line.Trim();
                        }
                    }
                    if (line.Contains("Events"))
                    {
                        line = subFile.ReadLine();
                        line.Trim();
                        while (line != null && String.Compare(line, "") != 0 && !line.Contains("["))
                        {
                            if (line.Contains("Format:"))
                            {
                                format = line.Split(':')[1].Trim();
                            }
                            if (line.Contains("Dialogue:"))
                            {
                                textLines.Add(new TextLine(line.Substring(10).Trim().Split(','), format.Split(',')));
                            }
                            line = subFile.ReadLine();
                            if(line != null)
                                line.Trim();    
                        }
                    }
                }
                line = subFile.ReadLine();
                if (line != null)
                    line.Trim();
            }

            subFile.Close();
        }

        public void setFont(SpriteFont font)
        {
            foreach(TextLine line in textLines)
            {
                line.setFont(font);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (TextLine line in textLines)
            {
                line.Draw(gameTime, spriteBatch);
            }
            //spriteBatch.DrawString(font, "pouet", new Vector2(100, 100), Color.Black);
        }

        private void AddTextStyle(String[] style, String[] formats)
        {
            Dictionary<String, String> toAdd = new Dictionary<string, string>();
            for(int i = 0; i < formats.Length; ++i)
            {
                toAdd.Add(formats[i], style[i]);
            }
        }
    }
}
