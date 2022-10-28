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
using static System.Net.Mime.MediaTypeNames;


namespace _4_Way_Chess
{

    public class MenuBase
    {
        public string title;
        ContentManager Content;
        SpriteFont font;
        SoundEffect MenuNote;
        int x;
        public int y = 350;
        public float Scale = 1;
        public List<string> text = new List<string>();
        List<Color> clr = new List<Color>();
        Vector2 position;
        public MenuBase[] Menus = new MenuBase[] {};
        public List<Rectangle> menuOpt = new List<Rectangle>();
        Color textColor = Color.White;
        Color offsettextColor = Color.Yellow;
        int hoverIndex = -1;
        public MenuBase previousActive;
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

        public virtual void LoadContent()
        {
            MenuNote = Content.Load<SoundEffect>("MenuTone");

        }

        public virtual void Update(GameTime gameTime)
        {
            Scale = ((Game1.displayHeight * Resolution.ratio) / ((Game1.displayWidth * Resolution.ratio)));
            if (Scale < 0.5)
            {
                Scale = 0.5f;
            }
            x = (int)((Game1.displayWidth * Resolution.ratio) / 2);
            font = Content.Load<SpriteFont>("Corporation");
            for (int i = 0; i < text.Count(); i++)
            {
                if (menuOpt.Count == 0 || i >= menuOpt.Count)
                {
                    clr.Add(textColor);
                    menuOpt.Add(new Rectangle());

                }

                menuOpt[i] = new Rectangle((int)vecPosition(text[i], i).X, (int)vecPosition(text[i], i).Y, (int)(font.MeasureString(text[i]).X*Scale), (int)(font.MeasureString(text[i]).Y * Scale));

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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < text.Count(); i++)
            {
                spriteBatch.DrawString(font, text[i], vecPosition(text[i], i), clr[i],0,new Vector2(),Scale, new SpriteEffects(), 0);
            }
        }

        public Vector2 vecPosition(string text, int index)
        {
            position = new Vector2(
                x - ((int)((font.MeasureString(text).X * Scale) / 2)),
                y + (index * (int)(font.MeasureString(text).Y * Scale)));

            return position;
        }
    }
}
