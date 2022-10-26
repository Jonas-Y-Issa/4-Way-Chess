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


namespace _4_Way_Chess
{

    public class TextBox
    {

        ContentManager cont;
        StartMenu Menu;
        bool Active;
        string enteredText = "";
        Rectangle NameInputBox;
        Texture2D NameSprite;
        SpriteFont font;
        string txtCursor = "|";
        int timer;
        KeyboardState keyState;


        public static bool Entered
        {
            get { return Game1.Entered; }
            set { Game1.Entered = value; }
        }
    

        public TextBox(ContentManager contentManager)
        {
            cont = contentManager;
        }

        public void LoadContent()
        {
            NameSprite = cont.Load<Texture2D>("Text");
            font = cont.Load<SpriteFont>("Arial");        
        }

        public void Update(GameTime gameTime)
        {
            NameInputBox = new Rectangle((int)((int)(Game1.testW * Resolution.ratio) / 2) - (int)(NameInputBox.Width / 2), 350, 218, 30);
        
            
            
            //NameInputBox = new Rectangle(Resolution._Device.PreferredBackBufferWidth / 2 - 218 / 2, Resolution._Device.PreferredBackBufferHeight / 3, 218, 30);

            //NameInputBox = new Rectangle(500, 200, 218, 30);
           
            NameSprite = cont.Load<Texture2D>("Text");
            font = cont.Load<SpriteFont>("Arial");
            keyState = Keyboard.GetState();
            MB(Game1._currentMouseState1, Game1._previousMouseState1, Game1.mousePoint, NameInputBox);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            position(spriteBatch, NameInputBox, NameSprite);
            position(spriteBatch, font, new Vector2(NameInputBox.X + 5, NameInputBox.Y + 3));
        }

        public void MB(MouseState mB, MouseState pMB, Point mP, Rectangle ChatBox)
        {
            if (mB.LeftButton == ButtonState.Released && pMB.LeftButton == ButtonState.Pressed && ChatBox.Contains(mP)
                || Active)
            {
                Active = true;
            }
            if (mB.LeftButton == ButtonState.Released && pMB.LeftButton == ButtonState.Pressed && !ChatBox.Contains(mP))
            {
                Active = false;
            }
        }

        public void position(SpriteBatch spriteBatch, SpriteFont font, Vector2 Position)
        {
            //if (!Active)
            //{
            //    enteredText = "";
            //}
                if (enteredText.Length == 0)
                {
                    spriteBatch.DrawString(font, "Enter Name", Position, Color.Gray);
                }
                timer += 1;
                if (Active)
                {
                    if (timer <= 40)
                    {
                        txtCursor = "|";
                    }
                    else if (timer <= 80)
                    {
                        txtCursor = " ";
                    }
                    else if (timer > 120)
                    {
                        timer = 0;
                    }
                }
                else
                {
                    txtCursor = " ";
                    timer = 0;
                }
               
                    spriteBatch.DrawString(font, enteredText + txtCursor, Position, Color.Black);



                    if (enteredText.Length > -1 && keyState.IsKeyDown(Keys.Enter))
                    {

                        Entered = true;
                        //SendName = true;
                        //if (playerList2 != 0)
                        //{
                        //    playGame = true;
                        //}
                        //else
                        //{
                            //Loading = true;
                        //}
                        //startMenu = false;
                    }

                
        }

        public void position(SpriteBatch spriteBatch, Rectangle ChatBox, Texture2D ChatArea)
        {
            spriteBatch.Draw(ChatArea, ChatBox, Color.White);
        }

        public void keyboardCharacterEntered(char character)
        {
            if (Active)
            {

                if (character == 8)
                {
                    if (enteredText.Length != 0)
                        enteredText = enteredText.Substring(0, (enteredText.Length - 1));
                }
                else if (enteredText.Length <= 10)
                {
                    enteredText += character;
                  

                }

            }
        }

    }
}
