using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;
using System.Collections;
using _4_Way_Chess;


namespace Class.GameSetup
{

    sealed class LoadScreen
    {
        Texture2D Background;
        Rectangle BackgroundBox;
        int dotTimer;
        string LoadMsg = "";
        Vector2 LoadingPosition;
        SpriteFont font;



        public LoadScreen(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("Arial");
            LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            BackgroundBox = new Rectangle((int)(Game1.displayWidth * Resolution.ratio / 2 - BackgroundBox.Width / 2), (int)(Game1.displayHeight * Resolution.ratio / 2 - BackgroundBox.Height / 2 - 100), 500, 500);

            LoadingPosition = new Vector2(BackgroundBox.X + BackgroundBox.Width / 2 - font.MeasureString("Loading Game").X / 2, BackgroundBox.Y + BackgroundBox.Height / 2 - font.MeasureString("Loading Game").Y + 14);


            dotTimer += 1;
            ///change to Loading<<<
            if (dotTimer < 30)
            {
                LoadMsg = "Loading Game";
            }
            else if (dotTimer < 60)
            {
                LoadMsg = "Loading Game.";
            }
            else if (dotTimer < 90)
            {
                LoadMsg = "Loading Game..";
            }
            else if (dotTimer < 120)
            {
                LoadMsg = "Loading Game...";
            }
            else if (dotTimer >= 280)
            {
                dotTimer = 0;
                Game1.menuEnum = Game1.MenuState.Game;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, LoadMsg, LoadingPosition, Color.White);

            spriteBatch.Draw(Background, BackgroundBox, Color.White);

        }

        public void LoadContent(ContentManager contentManager)
        {
            LoadingPosition = new Vector2();
            BackgroundBox = new Rectangle();
            Background = contentManager.Load<Texture2D>("Alpha");

        }
    }
}
