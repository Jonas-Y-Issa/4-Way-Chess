//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using System.Text;
//using System.Collections;
//

//namespace _4_Way_Chess
//{

//    public class BoxBackup
//    {

//        public Vector2 Pawn1xy;
//        Rectangle Pawn1Box;
//        protected Guid? _UniqueId;  //local member variable which stores the object's UniqueId
//        Rectangle PreviousWBox;
//        int ii;

//        int windowWidth;
//        int windowHeight;
//        int cc;
//        int uu;
//        bool firstMoveBool;
//        bool selectingPrevious;
//        bool test;
//        bool contained;
//        bool contain2
//        {
//            get { return PawnS.contained; }
//            set { PawnS.contained = value; }
//        }
//        bool Moveing;
//        bool notPrevious;
//        bool cannotMove;
//        bool lastMove;
//        public static bool noMoves
//        {
//            get { return Board.noMoves; }
//            set { Board.noMoves = value; }
//        }
//        int k;
//        int j;
//        int y;
//        int firstMove = 55;
//        int it;
//        MouseState _currentMouseState;
//        MouseState _previousMouseState;
//        bool reset;
//        Rectangle[] WhiteBox
//        {
//            get { return Board.WhiteBox; }
//        }
//        Vector2[] Squarexy
//        {
//            get { return Board.Squarexy; }
//        }
//        long Player1
//        {
//            get { return Game1.playerList1; }
//        }
//        long Player2
//        {
//            get { return Game1.playerList2; }
//        }
//        long Player3
//        {
//            get { return Game1.playerList3; }
//        }
//        long Player4
//        {
//            get { return Game1.playerList4; }
//        }
//        long WhoamI
//        {
//            get { return Game1.whoiam; }
//        }
//        int playerTurn
//        {
//            get { return Game1.playerTurn; }
//            set { Game1.playerTurn = value; }
//        }

//        bool MoveIm
//        {
//            get { return Game1.moveIm; }
//            set { Game1.moveIm = value; }
//        }

//        int virtualViewportX
//        {
//            get { return Resolution.virtualViewportX; }
//        }
//        int virtualViewportY
//        {
//            get { return Resolution.virtualViewportY; }
//        }
//        public static Vector2 MousePosition
//        {
//            get { return Camera2d.MousePosition; }
//        }
//        public static Vector2 TransformFrom;
//        bool occupiedEnemy;

//        int itst;


//        public BoxBackup(Texture2D Pawn1Sprite, int x, int y, int width, int height)
//        {
//            // TODO: Complete member initialization
//            this.Pawn1Sprite_2 = Pawn1Sprite;
//            this.xx = x;
//            this.yy = y;
//            this.Width = width;
//            this.Height = height;
//        }

//        public Texture2D Pawn1Sprite_2 { get; set; }
//        public int xx { get; set; }
//        public int yy { get; set; }
//        public int Width { get; set; }
//        public int Height { get; set; }

//        public static Point mousePoint;



//        public void Update(GameTime gameTime)
//        {
//            _previousMouseState = _currentMouseState;
//            _currentMouseState = Mouse.GetState();



//            mousePoint = new Point((int)MousePosition.X, (int)MousePosition.Y);
//            var mousePoint2 = new Point();

//            if (_currentMouseState.LeftButton == ButtonState.Pressed && reset == false)
//            {
//                mousePoint2 = new Point((int)MousePosition.X, (int)MousePosition.Y);
//            }

//            if (!noMoves)
//            {
//                Pawn1Box = new Rectangle(
//                               xx,
//                               yy,
//                               50,
//                               50);
//            }

//            if (PreviousWBox.Contains(mousePoint))
//            {
//                selectingPrevious = true;
//            }
//            else
//            {
//                selectingPrevious = false;
//            }


//            for (int o = 0; o < 8; o++)
//            {
//                for (int i = 0; i < 8; i++)
//                {
//                    for (int u = 0; u < 64; u++)
//                    {

//                        var PawnPoint = new Point((int)Pawn1xy.X, (int)Pawn1xy.Y);
//                        TransformFrom = new Vector2((int)Pawn1xy.X, (int)Pawn1xy.Y);


//                        y = i + 8;

//                        if (WhoamI == Player1 && playerTurn == 0)
//                        {

//                            if (PreviousWBox.Contains(Pawn1Box))
//                            {
//                                notPrevious = false;
//                            }
//                            else
//                            {
//                                notPrevious = true;
//                            }



//                            if (Pawn1Box.Contains(mousePoint2) || k == i)
//                            {
//                                ii = i;

//                                if (_currentMouseState.LeftButton == ButtonState.Pressed && cannotMove == false)
//                                {
//                                    Moveing = true;
//                                    MoveIm = true;

//                                    if (WhiteBox[u].Contains(mousePoint) && Pawn1Box.Contains(mousePoint) && lastMove == false)
//                                    {
//                                        PreviousWBox = WhiteBox[u];
//                                        lastMove = true;
//                                    }
//                                    else
//                                    {
//                                        Moveing = false;
//                                    }
//                                    if (Pawn1Box.Contains(mousePoint2) || k == i)
//                                    {
//                                        if (Moveing == false)
//                                        {

//                                            test = true;
//                                            k = i;
//                                            reset = true;
//                                            xx = (int)MousePosition.X;
//                                            yy = (int)MousePosition.Y;


//                                            if (WhiteBox[y].Contains(Pawn1Box))
//                                            {
//                                                firstMove = 110;
//                                            }


//                                        }
//                                        else
//                                        {
//                                            firstMove = 55;
//                                        }
//                                    }
//                                }
//                                else
//                                {
//                                    if (_currentMouseState.LeftButton == ButtonState.Released && Pawn1Box.Intersects(WhiteBox[u]))
//                                    {
//                                        k = -1;
//                                        //if (Pawn1xy[i].X != PreviousWBox.X || Pawn1xy[i].Y != PreviousWBox.Y)
//                                        //{
//                                        //    cannotMove = true;
//                                        //}  
//                                        reset = false;


//                                        if (selectingPrevious == false && Pawn1Box.Y < PreviousWBox.Bottom + firstMove && notPrevious == true && occupiedEnemy == false)
//                                        {
//                                            test = true;
//                                            if (Pawn1Box.Left < PreviousWBox.Right && Pawn1Box.Left > PreviousWBox.Left && Pawn1Box.Y > PreviousWBox.Top && WhiteBox[u].Contains(mousePoint))
//                                            {
//                                                xx = WhiteBox[u].X;
//                                                yy = WhiteBox[u].Y;
//                                                //playerTurn = 1;
//                                                contain2 = false;
//                                                contained = true;
//                                                //+ WhiteBox[u].Height / 2 - Pawn1Box[i].Height / 2
//                                            }
//                                        }

//                                    }
//                                    else
//                                    {
//                                        xx = PreviousWBox.X;
//                                        yy = PreviousWBox.Y;
//                                        lastMove = false;
//                                        occupiedEnemy = false;

//                                    }
//                                }
//                            }
//                        }

//                    }
//                }
//            }
//        }
//        public void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.Draw(Pawn1Sprite_2, Pawn1Box, Color.White);
//        }
//        public void LoadContent(ContentManager contentManager, string ClayR,
//       int x, int y)
//        {

//            Pawn1Box = new Rectangle((int)Pawn1xy.X, (int)Pawn1xy.Y, 50, 50);


//            PreviousWBox = new Rectangle();

//            _currentMouseState = Mouse.GetState();
//            _previousMouseState = _currentMouseState;

//        }



//        public bool ContainsMP()
//        {
//            if (Pawn1Box.Contains(mousePoint) || j == itst)
//            {
//                j = itst;
//                return true;
//            }
//            else
//            {
//                j = 191;
//                return false;
//            }

//        }


//    }


//}

