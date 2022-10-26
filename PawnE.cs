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

    public class P2 : Base
    {
        public P2(Texture2D Sprite, int x, int y, int Player, Color tnt)
        {
            this.PawnSprite = Sprite;
            this.player = Player;
            this.Clr = tnt;
            this.xx = x;
            this.yy = y;
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using System.Text;
//using System.Collections;
//
//using Lidgren.Network;

//namespace _4_Way_Chess
//{

//    public class P1
//    {
//        #region Class Declaring
//        public static NetClient client
//        {
//            get { return Game1.client; }
//        }
//        public static string sort
//        {
//            get { return Game1.sort; }
//            set { Game1.sort = value; }
                
//        }


//        Texture2D PawnSprite;
//        public Rectangle Pawn1Box;
//        Rectangle PreviousWBox;
//        Rectangle[] WhiteBox
//        {
//            get { return Board.WhiteBox; }
//        }

//        public bool Dead;

//        public static int getIND
//        {
//            get { return Game1.getIND; }
//        }
//        public static string getPawn
//        {
//            get { return Game1.getPawn; }
//        }



//        public static MyCollection<P2> PawnE
//        {
//            get { return Game1.PawnE; }
//        }
//        public static MyCollection<P3> PawnS
//        {
//            get { return Game1.PawnS; }
//        }
//        public static MyCollection<P4> PawnW
//        {
//            get { return Game1.PawnW; }
//        }





//        public Rectangle[] AllPawns
//        {
//            get { return Game1.AllPawns; }
//        }



//        MouseState _currentMouseState;
//        MouseState _previousMouseState;
//        Point mousePoint;
//        Point mousePoint2 = new Point();

//        #endregion

//        #region Support

//        bool shouldMove;
//        bool Grab1;

//        int test;


//        public bool Grab
//        {
//            get { return Game1.Grab; }
//            set { Game1.Grab = value; }
//        }

//        bool selectingPrevious;

//        bool notPrevious;
//        bool lastMove;

//        public static bool Occupied
//        {
//            get { return Game1.Occupied; }
//            set { Game1.Occupied = value; }

//        }
        
//        public static bool Moved1
//        {
//            get { return Game1.Moved1; }
//            set { Game1.Moved1 = value; }
//        }

//        int firstMove = 110;
//        bool Moveing;

//        #endregion


//        #region ViewPort Coords

//        public static Vector2 MousePosition
//        {
//            get { return Resolution.MousePosition; }
//        }
//        public static Vector2 TransformFrom;

//        #endregion

//        #region OutMsg

//        bool MoveIm
//        {
//            get { return Game1.moveIm; }
//            set { Game1.moveIm = value; }
//        }
//        public int thisX;
//        public int thisY;

//        #endregion

//        #region IncMsg

//        long Player1
//        {
//            get { return Game1.playerList1; }
//        }
//        int playerTurn
//        {
//            get { return Game1.playerTurn; }
//        }
//        long WhoamI
//        {
//            get { return Game1.whoiam; }
//        }
//        public int xx;
//        public int yy;

//        public int LockTime
//        {
//            get { return Game1.LockTime; }
//        }
//        #endregion

//        #region Buffers

//        int k;
//        int i = -2;
//        int g;
//        int j = 191;
//        int itst = 1;
//        bool reset;

//        #endregion

//        public P1(Texture2D Pawn1Sprite, int x, int y)
//        {
//            this.PawnSprite = Pawn1Sprite;
//            this.xx = x;
//            this.yy = y;
//        }

//        public void Update(GameTime gameTime)
//        {

//            if (Dead)
//            {
//                Pawn1Box = new Rectangle(0, 0, 53, 53);
//            }
//            else
//            {
//                Pawn1Box = new Rectangle(xx, yy, 53, 53);
//            }

//            #region Mouse

//            _previousMouseState = _currentMouseState;
//            _currentMouseState = Mouse.GetState();

//            mousePoint = new Point((int)MousePosition.X, (int)MousePosition.Y);

//            if (_currentMouseState.LeftButton == ButtonState.Pressed && reset == false)
//            {
//                mousePoint2 = new Point((int)MousePosition.X, (int)MousePosition.Y);
//            }

//            #endregion

//            #region Pawn Mechanics


//            if (PreviousWBox.Contains(mousePoint))
//            {
//                selectingPrevious = true;
//            }
//            else
//            {
//                selectingPrevious = false;
//            }

         
       
//            if (WhoamI == Player1 && playerTurn == 1 && !Dead && Moved1 == false)
//            {
//                for (int u = 0; u < 160; u++)
//                {
//                        if (Pawn1Box.Contains(mousePoint2) && Grab == false || k == i)
//                        {
//                            if (_currentMouseState.LeftButton == ButtonState.Pressed)
//                            {
//                                if (WhiteBox[u].Contains(mousePoint) && Pawn1Box.Contains(mousePoint) && lastMove == false)
//                                {
//                                    CreatePreviousBox(u);
//                                }
//                            }
//                            if (MovePawn(u))
//                            {
                               
//                                MovePawnLocation(u);
                             
//                            }
//                            else if (canKill(u))
//                            {
//                                Kill(u, getIND);
//                            }
//                        }
                    
//                }
//            }
//            #endregion
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.Draw(PawnSprite, Pawn1Box, Color.White);
//        }

//        public void LoadContent()
//        {
//            Pawn1Box = new Rectangle();
//            PreviousWBox = new Rectangle();
//        }

//        void ResetPawnLocation()
//        {
//            thisX = PreviousWBox.X;
//            thisY = PreviousWBox.Y;
//            test = 0;
           
//        }

//        void CreatePreviousBox(int u)
//        {
//            PreviousWBox = WhiteBox[u];
//            lastMove = true;
//        }

//        internal bool ContainsMP()
//        {
//            LockTime >= 30 && 
//            if (!Grab && _currentMouseState.LeftButton == ButtonState.Pressed && Pawn1Box.Contains(mousePoint)
//                || j == itst && Grab)
//            {
//                j = itst;
//                Grab = true;
//                k = i;
//                reset = true;
//                thisX = (int)MousePosition.X;
//                thisY = (int)MousePosition.Y;
//                if (_currentMouseState.LeftButton == ButtonState.Released)
//                {
//                    ResetPawnLocation();
//                    if (xx == thisX && yy == thisY)
//                    {
//                        if (LockTime >= 10)
//                        {
//                            Grab = false;
//                            j = 191;
//                        }
//                    }
//                }
//                return true;
//            }
//            else
//            {

//                return false;
//            }
//        }

//        bool MovePawn(int u)
//        {
//            if (_currentMouseState.LeftButton == ButtonState.Released 
//                && WhiteBox[u].Contains(mousePoint) 
//                && WhiteBox[u].Intersects(Pawn1Box)
//                && !PawnE[getIND].Pawn1Box.Contains(mousePoint)
//                && !PawnS[getIND].Pawn1Box.Contains(mousePoint)
//                && !PawnW[getIND].Pawn1Box.Contains(mousePoint)
//                && selectingPrevious == false
//                && Pawn1Box.Top >= PreviousWBox.Bottom 
//                && WhiteBox[u].X == PreviousWBox.X
//                && WhiteBox[u].Y >= PreviousWBox.Bottom)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        void MovePawnLocation(int u)
//        {
//            Moved1 = true;
//            thisX = WhiteBox[u].X;
//            thisY = WhiteBox[u].Y;
//            k = 0;
//            reset = false;
//            lastMove = false;
//            if (xx == thisX && yy == thisY)
//            {
//                if (LockTime >= 10)
//                {
//                    Grab = false;
//                    j = 191;
//                }
//            }
//        }

//        bool canKill(int u)
//        {
//            if (_currentMouseState.LeftButton == ButtonState.Released                 
//                && WhiteBox[u].Contains(mousePoint) 
//                && WhiteBox[u].Intersects(Pawn1Box) 
//                && selectingPrevious == false
//                && ((WhiteBox[u].Right <= PreviousWBox.Left) 
//                || (WhiteBox[u].Left >= PreviousWBox.Right))
//                && WhiteBox[u].X != PreviousWBox.X
//                && WhiteBox[u].Y == PreviousWBox.Top + 55
//                && ((PawnE[getIND].Pawn1Box.Contains(mousePoint)) 
//                || (PawnS[getIND].Pawn1Box.Contains(mousePoint)) 
//                || (PawnW[getIND].Pawn1Box.Contains(mousePoint)))
//                )
//            {

//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        void Kill(int u, int i)
//        {
//            Moved1 = true;
//            thisX = WhiteBox[u].X;
//            thisY = WhiteBox[u].Y;
//            k = 0;

//            sort = "killing";

//            reset = false;
//            lastMove = false;
       
//            Grab = false;
//            j = 191;
           
//        }
//    }
//}
