using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;


namespace _4_Way_Chess
{

    public class MenuBase
    {
        public string title;
        ContentManager Content;
        SpriteFont font;
        SoundEffect MenuNote;

        int x;
        float Scale = 2;
        public List<string> text = new List<string>();
        List<Color> clr = new List<Color>();
        public MenuBase[] Menus = new MenuBase[] {};
        public List<Rectangle> menuOpt = new List<Rectangle>();
        Color textColor = Color.Black;
        Color offsettextColor = Color.White;
        int hoverIndex = -1;
        public int hIndex { get { return hoverIndex; } }

        public MenuBase(ContentManager contentManager, string txtTitle, MenuBase[] menuList, string[] strList)
        {
            title = txtTitle;
            Menus = menuList;
            for (int i = 0; i < menuList.Length; i++)
            {
                text.Add(menuList[i].title);
            }
            for (int i = 0; i < strList.Length; i++)
            {
                text.Add(strList[i]);
            }
            Content = contentManager;
        }
        public MenuBase(string txtTitle)
        {
            title = txtTitle;

        }

        public void LoadContent()
        {
            MenuNote = Content.Load<SoundEffect>("MenuTone");

            for (int i = 0; i < text.Count(); i++)
            {
                clr.Add(textColor);
                menuOpt.Add(new Rectangle());
            }
        }

        public void Update(GameTime gameTime)
        {
            x = (int)((Game1.testW * Resolution.ratio) / 2);
            font = Content.Load<SpriteFont>("SpriteFont1");

            for (int i = 0; i < text.Count(); i++)
            {
                menuOpt[i] = new Rectangle((int)(x - ((font.MeasureString(text[i]).X / 2))), 350 + (i * (int)(font.MeasureString(text[i]).Y * Scale)), (int)(font.MeasureString(text[i]).X*Scale), (int)(font.MeasureString(text[i]).Y * Scale));

                if (menuOpt[i].Contains(Game1.mousePoint))
                {
                    clr[i] = offsettextColor;
                    if (hoverIndex != i)
                    {
                        MenuNote.Play(0.1f, 0.5f, 0.5f);
                        hoverIndex = i;
                    }
                }
                else
                {
                    clr[i] = textColor;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < text.Count(); i++)
            {
                spriteBatch.DrawString(font, text[i], new Vector2(x - ((int)((font.MeasureString(text[i]).X * Scale) / 2)), 350 + (i * (int)(font.MeasureString(text[i]).Y * Scale))), clr[i],0,new Vector2(),Scale, new SpriteEffects(), 0);
            }
        }
    }
}
