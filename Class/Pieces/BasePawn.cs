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
using Class.GameSetup;

namespace _4_Way_Chess
{
    public class Base
    {
        #region Class Declaring

        public Texture2D PawnSprite { get; set; }
        public static int Testing = 0;

        public Rectangle Pawn1Box = new Rectangle(0, 0, 55, 55);
        public Rectangle PreviousWBox;
        public Rectangle[] WhiteBox
        {
            get { return Board.WhiteBox; }
        }
        public int v2;

        public bool Dead;

        public static int getIND
        {
            get { return Game1.getIND; }
        }

        public static MyCollection<P1> PawnN
        {
            get { return Game1.PawnN; }
        }
        public static MyCollection<P2> PawnE
        {
            get { return Game1.PawnE; }
        }
        public static MyCollection<P3> PawnS
        {
            get { return Game1.PawnS; }
        }
        public static MyCollection<P4> PawnW
        {
            get { return Game1.PawnW; }
        }
        public static MyCollection<Rook> Rook
        {
            get { return Game1.RookN; }
        }
        public static MyCollection<Bishop> Bishop
        {
            get { return Game1.BishopN; }
        }
        public static MyCollection<Knight> Knight
        {
            get { return Game1.KnightN; }
        }
        public static MyCollection<Queen> Queen
        {
            get { return Game1.QueenN; }
        }
        public static MyCollection<King> King
        {
            get { return Game1.KingN; }
        }
        int danger_Level;
        #endregion

        #region PlayerInput

        public MouseState _currentMouseState;
        public MouseState _previousMouseState;
        public Point mousePoint;
        public Point mousePoint2 = new Point();
        public static Vector2 MousePosition
        {
            get { return Resolution.MousePosition; }
        }

        #endregion

        #region Support

        public bool Grab
        {
            get { return Game1.Grab; }
            set { Game1.Grab = value; }
        }

        public bool selectingPrevious;

        public bool lastMove;

        public static bool Moved1
        {
            get { return Game1.Moved1; }
            set { Game1.Moved1 = value; }
        }

        public static bool Moved2
        {
            get { return Game1.Moved2; }
            set { Game1.Moved2 = value; }
        }
        public static bool Moved3
        {
            get { return Game1.Moved3; }
            set { Game1.Moved3 = value; }
        }

        public static bool Moved4
        {
            get { return Game1.Moved4; }
            set { Game1.Moved4 = value; }
        }
        public int firstMove = 110;

        #endregion

        #region OutMsg

        public int thisX;
        public int thisY;

        #endregion

        #region IncMsg

        public int player;

        public Color Clr;

        public long playerList1
        {
            get { return Game1.playerList1; }
        }

        public long playerList2
        {
            get { return Game1.playerList2; }
        }

        public long playerList3
        {
            get { return Game1.playerList3; }
        }

        public long playerList4
        {
            get { return Game1.playerList4; }
        }

        public int playerTurn
        {
            get { return Game1.playerTurn; }
        }
        public long WhoamI
        {
            get { return Game1.whoiam; }
        }
        public int xx;
        public int yy;

        public int LockTime
        {
            get { return Game1.LockTime; }
        }
        #endregion

        #region Buffers
        public int k;
        public int i = -2;
        public int j = 191;
        public int itst = 1;
        public bool reset;

        #endregion

        #region SomeBishopMechanics

        public int tty;
        public bool tst;
        public Color tnt = new Color();
        public defX[] dx = new defX[64];
        public List<defX> dx_lst;
        bool nn;
        #endregion

        #region Knightstuff

        public int v = 0;


        #endregion

        Point[] Coords = new Point[] {
            new Point { X = -55, Y = -55 },
            new Point { X = 0, Y = -55 },
            new Point { X = 55, Y = -55 },
            new Point { X = 55, Y = 0 },
            new Point { X = 55, Y = 55 },
            new Point { X = 0, Y = 55 },
            new Point { X = -55, Y = 55 },
            new Point { X = -55, Y = 0 } };


        public class defX
        {
            public int x { get; set; }
        }

        public Base() { }

        public virtual void Update(GameTime gameTime)
        {
            doThese();
            if (_currentMouseState.MiddleButton == ButtonState.Pressed)
            {
                if (Testing == 0)
                {
                    if (!checkcheck(player))
                    {
                        checkMate_nextTurn();
                    }
                    Testing = 1;
                }

            }


            if (_currentMouseState.RightButton == ButtonState.Pressed)
            {
                Testing = 0;
            }


            if (!Dead
            && ((WhoamI == playerList1 && Moved1 == false && playerTurn == 1 && player == 1)
            || (WhoamI == playerList2 && Moved2 == false && playerTurn == 2 && player == 2)
            || (WhoamI == playerList3 && Moved3 == false && playerTurn == 3 && player == 3)
            || (WhoamI == playerList4 && Moved4 == false && playerTurn == 4 && player == 4)))
            {
                for (int u = 0; u < 160; u++)
                {

                    if (Pawn1Box.Contains(mousePoint2) && Grab == false || k == i)
                    {
                        if (_currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (WhiteBox[u].Contains(mousePoint) && Pawn1Box.Contains(mousePoint) && lastMove == false)
                            {
                                CreatePreviousBox(u);
                            }
                        }
                        if (MovePawn(u))
                        {
                            for (int n = 0; n < 64; n++)
                            {
                                if (player == 1)
                                {
                                    if (Game1.all[n].Y > PreviousWBox.Y && Game1.all[n].Y < WhiteBox[u].Y && Game1.all[n].X == PreviousWBox.X)
                                    {
                                        nn = true;
                                    }
                                }
                                else if (player == 2)
                                {
                                    if (Game1.all[n].X < PreviousWBox.X && Game1.all[n].X > WhiteBox[u].X && Game1.all[n].Y == PreviousWBox.Y)
                                    {
                                        nn = true;
                                    }
                                }
                                else if (player == 3)
                                {
                                    if (Game1.all[n].Y < PreviousWBox.Y && Game1.all[n].Y > WhiteBox[u].Y && Game1.all[n].X == PreviousWBox.X)
                                    {
                                        nn = true;
                                    }
                                }
                                else if (player == 4)
                                {
                                    if (Game1.all[n].X > PreviousWBox.X && Game1.all[n].X < WhiteBox[u].X && Game1.all[n].Y == PreviousWBox.Y)
                                    {
                                        nn = true;
                                    }
                                }
                                if (n == 63 && nn == false)
                                {
                                    MovePawnLocation(u);
                                }
                            }
                        }
                        else if (canKill(u))
                        {
                            MovePawnLocation(u);
                        }
                    }
                }
            }
        }

        #region Mouse

        public void doThese()
        {
            if (Dead)
            {
                Pawn1Box = new Rectangle(0, 0, 55, 55);
            }
            else
            {
                Pawn1Box = new Rectangle(xx, yy, 55, 55);
            }


            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            mousePoint = new Point((int)MousePosition.X, (int)MousePosition.Y);

            if (_currentMouseState.LeftButton == ButtonState.Pressed && reset == false)
            {
                mousePoint2 = new Point((int)MousePosition.X, (int)MousePosition.Y);
            }

        #endregion

            #region Pawn Mechanics


            if (PreviousWBox.Contains(mousePoint))
            {
                selectingPrevious = true;
            }
            else
            {
                selectingPrevious = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PawnSprite, Pawn1Box, Clr);
        }

        public virtual void LoadContent()
        {
            Pawn1Box = new Rectangle();
            PreviousWBox = new Rectangle();
            resetDX();
        }

        public void ResetPawnLocation()
        {
            thisX = PreviousWBox.X;
            thisY = PreviousWBox.Y;
            tst = false;
            resetDX();
            mousePoint2 = new Point(0, 0);
            k = 0;
            nn = false;
        }

        public void CreatePreviousBox(int u)
        {
            PreviousWBox = WhiteBox[u];
            lastMove = true;
        }

        public bool Occupado(int u)
        {
            for (int t = 0; t < Game1.all.Count(); t++)
            {
                if (WhiteBox[u].Contains(Game1.all[t]))
                {
                    return true;
                }

            }
            return false;
        }

        //public bool checkcheck(int P)
        //{
        //    int p = P - 1;
        //    for (int o = 0; o < 8; o++)
        //    {
        //        for (int u = 0; u < WhiteBox.Count(); u++)
        //        {
        //            if (check_Danger(u, p, true))
        //            {
        //                if (WhiteBox[u].X == King[p].Pawn1Box.X + Coords[o].X
        //                  && WhiteBox[u].Y == King[p].Pawn1Box.Y + Coords[o].Y)
        //                {
        //                    if (!Occupado(u))
        //                    {
        //                        if ((!check_Rook(u, P)) || (!check_Bishop(u, P)))
        //                        {
        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            return true;
        //                        }
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    return true;
        //}




        public bool checkcheck(int P)
        {
            int p = P - 1;
         
                for (int u = 0; u < WhiteBox.Count(); u++)
                {
                    if (!check_Danger(u, p, true))
                    {
                        //    if (danger_Level == 1)
                        //    {
                        //        return false;
                        //    }

                        for (int uu = 0; uu < WhiteBox.Count(); uu++)
                        {
                            for (int o = 0; o < 8; o++)
                            {

                                //Check if can move around
                                if (WhiteBox[uu].X == King[p].Pawn1Box.X + Coords[o].X
                                      && WhiteBox[uu].Y == King[p].Pawn1Box.Y + Coords[o].Y)
                                {
                                    //Continues If said whitebox is not occupied
                                    //false == unoccupied spot
                                    if (!Occupado(uu))
                                    {
                                        //Then checks if the WhiteBox is in Danger from rooks or Bishops
                                        //false == Danger
                                        if ((!check_Rook(uu, P)) || (!check_Bishop(uu, P)))
                                        {
                                            //Check
                                            return false;
                                        }
                                        //else if there is no danger from rook or bishop
                                        else if ((check_Rook(uu, P)) && (check_Bishop(uu, P)))
                                        {
                                            //Not Check
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                        return false;
                    
                }
            }
            return true;
        }

        #region not now

        public void resetDX()
        {
            for (int o = 0; o < 64; o++)
            {
                dx[o] = new defX { x = 1 };
            }
        }

        public bool ContainsMP()
        {
            //LockTime >= 30 && 
            if (!Grab && _currentMouseState.LeftButton == ButtonState.Pressed && Pawn1Box.Contains(mousePoint)
                || j == itst && Grab)
            {
                j = itst;
                Grab = true;
                k = i;
                reset = true;
                thisX = (int)MousePosition.X - (Pawn1Box.Width / 4);
                thisY = (int)MousePosition.Y + 5;
                if (_currentMouseState.LeftButton == ButtonState.Released)
                {
                    ResetPawnLocation();
                    if (xx == thisX && yy == thisY)
                    {
                        if (LockTime >= 10)
                        {
                            Grab = false;
                            j = 191;
                        }
                    }
                }
                return true;
            }
            else
            {

                return false;
            }
        }

        #region Rook

        public void ListNS(int u)
        {
            foreach (Rectangle rect in Game1.all)
            {
                if (v != Game1.all.Count())
                {
                    if (rect.Y > PreviousWBox.Y && rect.X == WhiteBox[u].X)
                    {
                        dx[v] = new defX { x = rect.Y };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }
                    else
                    {
                        dx[v] = new defX { x = 999 };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }

                }
                if (v == Game1.all.Count())
                {
                    dx_lst = dx.OrderBy(g => g.x).ToList();
                }

            }
        }

        public void ListSN(int u)
        {
            foreach (Rectangle rect in Game1.all)
            {
                if (v != Game1.all.Count())
                {
                    if (rect.Y < PreviousWBox.Y && rect.X == WhiteBox[u].X)
                    {
                        dx[v] = new defX { x = rect.Y };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }
                    else
                    {
                        dx[v] = new defX { x = 1 };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }

                }
                if (v == Game1.all.Count())
                {
                    dx_lst = dx.OrderByDescending(g => g.x).ToList();
                }

            }
        }

        public void ListWE(int u)
        {
            foreach (Rectangle rect in Game1.all)
            {
                if (v != Game1.all.Count())
                {
                    if (rect.X > PreviousWBox.X && rect.Y == WhiteBox[u].Y)
                    {
                        dx[v] = new defX { x = rect.X };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }
                    else
                    {
                        dx[v] = new defX { x = 999 };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }

                }
                if (v == Game1.all.Count())
                {
                    dx_lst = dx.OrderBy(g => g.x).ToList();
                }

            }
        }

        public void ListEW(int u)
        {
            foreach (Rectangle rect in Game1.all)
            {
                if (v != Game1.all.Count())
                {
                    if (rect.X < PreviousWBox.X && rect.Y == WhiteBox[u].Y)
                    {
                        dx[v] = new defX { x = rect.X };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }
                    else
                    {
                        dx[v] = new defX { x = 1 };
                        if (v < Game1.all.Count())
                        {
                            v++;
                        }
                    }

                }
                if (v == Game1.all.Count())
                {
                    dx_lst = dx.OrderByDescending(g => g.x).ToList();
                }

            }
        }

        #endregion

        #region Bishop

        public void ListSW(int o)
        {
            for (int v = 0; v < 64; v++)
            {
                if (v > 0)
                {
                    tty = v - 1;
                }
                if (dx[v] == null)
                {
                    dx[v] = new defX { x = 999 };
                }
                if (dx[v].x == 1 || dx[v].x == 999 || dx[v].x == dx[tty].x)
                {
                    dx[v] = new defX { x = Game1.all[o].Y };
                }
            }
            dx_lst = dx.OrderBy(g => g.x).ToList();
        }

        public void ListNW(int o)
        {
            for (int v = 0; v < 64; v++)
            {
                if (v > 0)
                {
                    tty = v - 1;
                }
                if (dx[v] == null)
                {
                    dx[v] = new defX { x = 1 };
                }
                if (dx[v].x == 1 || dx[v].x == 999 || dx[v].x == dx[tty].x)
                {
                    dx[v] = new defX { x = Game1.all[o].Y };
                }
            }
            dx_lst = dx.OrderByDescending(g => g.x).ToList();
        }

        public void ListSE(int o)
        {
            for (int v = 0; v < 64; v++)
            {
                if (v > 0)
                {
                    tty = v - 1;
                }
                if (dx[v] == null)
                {
                    dx[v] = new defX { x = 999 };
                }
                if (dx[v].x == 1 || dx[v].x == 999 || dx[v].x == dx[tty].x)
                {
                    dx[v] = new defX { x = Game1.all[o].Y };
                }
            }
            dx_lst = dx.OrderBy(g => g.x).ToList();
        }

        public void ListNE(int o)
        {
            for (int v = 0; v < 64; v++)
            {
                if (v > 0)
                {
                    tty = v - 1;
                }
                if (dx[v] == null)
                {
                    dx[v] = new defX { x = 1 };
                }
                if (dx[v].x == 1 || dx[v].x == 999 || dx[v].x == dx[tty].x)
                {
                    dx[v] = new defX { x = Game1.all[o].Y };
                }
            }
            dx_lst = dx.OrderByDescending(g => g.x).ToList();
        }


        #endregion

        #region Contains

        public bool PawnContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (PawnN[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (PawnE[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (PawnS[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (PawnW[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (Rook[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (Bishop[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (Knight[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
            }
            for (int o = 0; o < 4; o++)
            {
                if (King[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
                else if (Queen[o].Pawn1Box.Contains(mousePoint))
                {
                    return true;
                }
            }
            return false;
        }

        public int? PawnNContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (PawnN[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;

        }
        public int? PawnEContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (PawnE[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;

        }
        public int? PawnSContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (PawnS[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;

        }
        public int? PawnWContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (PawnW[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;

        }
        public int? RookNContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (Rook[o].Pawn1Box.Contains(mousePoint) && o != player + 1)
                {
                    return o;
                }
            }
            return null;
        }
        public int? KnightNContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (Knight[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;
        }
        public int? BishopNContains()
        {
            for (int o = 0; o < 8; o++)
            {
                if (Bishop[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;
        }
        public int? QueenNContains()
        {
            for (int o = 0; o < 4; o++)
            {
                if (Queen[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;
        }
        public int? KingNContains()
        {
            for (int o = 0; o < 4; o++)
            {
                if (King[o].Pawn1Box.Contains(mousePoint))
                {
                    return o;
                }
            }
            return null;
        }

        //public int? RookContains()
        //{
        //    for (int o = 0; o < 8; o++)
        //    {
        //        if (.Pawn1Box.Contains(mousePoint))
        //        {
        //            return o;
        //        }
        //    }
        //    return null;

        //}

        //public Tuple<int, int> PawnPN()
        //{
        //    for (int o = 0; o < 8; o++)
        //    {
        //        if (PawnN[o].Pawn1Box.Contains(mousePoint))
        //        {
        //            return Tuple.Create(1, o);

        //        }
        //        else if (PawnE[o].Pawn1Box.Contains(mousePoint))
        //        {
        //            return Tuple.Create(2, o);
        //        }
        //        else if (PawnS[o].Pawn1Box.Contains(mousePoint))
        //        {
        //            return Tuple.Create(3, o);
        //        }
        //        else if (PawnW[o].Pawn1Box.Contains(mousePoint))
        //        {
        //            return Tuple.Create(4, o);
        //        }
        //    }
        //    return Tuple.Create(0, 0);
        //}

        #endregion

        public virtual bool MovePawn(int u)
        {
            if (_currentMouseState.LeftButton == ButtonState.Released
                && WhiteBox[u].Contains(mousePoint)
                && WhiteBox[u].Intersects(Pawn1Box)
                && selectingPrevious == false
                && !move_willCheck(u, 0))
            {
                switch (player)
                {
                    case 1:
                        if ((Pawn1Box.Top >= PreviousWBox.Bottom
                         && WhiteBox[u].X == PreviousWBox.X
                         && WhiteBox[u].Y >= PreviousWBox.Bottom
                         && WhiteBox[u].Y < PreviousWBox.Bottom + firstMove
                         && !PawnContains()))

                            return true;

                        break;


                    case 2:
                        if ((Pawn1Box.Left <= PreviousWBox.Left
                      && WhiteBox[u].Y == PreviousWBox.Y
                      && WhiteBox[u].X < PreviousWBox.X
                      && WhiteBox[u].X >= PreviousWBox.Left - firstMove
                      && !PawnContains()))

                            return true;

                        break;

                    case 3:
                        if ((Pawn1Box.Top <= PreviousWBox.Bottom
                 && WhiteBox[u].X == PreviousWBox.X
                 && WhiteBox[u].Bottom <= PreviousWBox.Top
                 && WhiteBox[u].Y >= PreviousWBox.Top - firstMove
                 && !PawnContains()))

                            return true;

                        break;

                    case 4:

                        if ((Pawn1Box.Left >= PreviousWBox.Right
                     && WhiteBox[u].Y == PreviousWBox.Y
                     && WhiteBox[u].X > PreviousWBox.X
                     && WhiteBox[u].X <= PreviousWBox.Left + firstMove
                     && !PawnContains()))
                            return true;

                        break;

                    default:

                        return false;
                }
                return false;

            }
            else
            {

                return false;
            }
        }

        public void MovePawnLocation(int u)
        {
            switch (player)
            {
                case 1:

                    Moved1 = true;
                    break;
                case 2:

                    Moved2 = true;
                    break;
                case 3:

                    Moved3 = true;
                    break;
                case 4:

                    Moved4 = true;
                    break;
            }


            thisX = WhiteBox[u].X;
            thisY = WhiteBox[u].Y;
            k = 0;
            reset = false;
            lastMove = false;
            firstMove = 55;
            Grab = false;
            j = 191;
            nn = false;
            Testing = 0;
            danger_Level = 0;
            

        }

        public void checkMate_nextTurn()
        {
            switch (player)
            {
                case 1:

                    Moved1 = true;
                    break;
                case 2:

                    Moved2 = true;
                    break;
                case 3:

                    Moved3 = true;
                    break;
                case 4:

                    Moved4 = true;
                    break;
            }

            k = 0;
            reset = false;
            lastMove = false;
            firstMove = 55;
            Grab = false;
            j = 191;
            nn = false;
            Testing = 0;
            danger_Level = 0;


        }
        public bool canKill(int u)
        {
            if (_currentMouseState.LeftButton == ButtonState.Released
                  && WhiteBox[u].Contains(mousePoint)
                  && WhiteBox[u].Intersects(Pawn1Box)
                  && WhiteBox[u].Y != PreviousWBox.Y
                  && WhiteBox[u].X != PreviousWBox.X
                  && selectingPrevious == false)
            {
                if ((player == 1
                    && ((WhiteBox[u].Right == PreviousWBox.Left)
                || (WhiteBox[u].Left == PreviousWBox.Right))
                && WhiteBox[u].Top == PreviousWBox.Bottom)

                ||
                    (player == 2
                    && ((WhiteBox[u].Top == PreviousWBox.Bottom)
                || (WhiteBox[u].Bottom == PreviousWBox.Top))
                && WhiteBox[u].Right == PreviousWBox.Left)

                ||

                    (player == 3
                    && ((WhiteBox[u].Right == PreviousWBox.Left)
                || (WhiteBox[u].Left == PreviousWBox.Right))
                && WhiteBox[u].Bottom == PreviousWBox.Top)

                ||

                (player == 4
                    && ((WhiteBox[u].Top == PreviousWBox.Bottom)
                || (WhiteBox[u].Bottom == PreviousWBox.Top))
                && WhiteBox[u].Left == PreviousWBox.Right)

                    )
                {
                    if (player == 1)
                    {
                        if (PawnEContains() != null)
                        {
                            Kill(v2 = PawnEContains() ?? default(int), "e");
                            return true;
                        }
                        else if (PawnSContains() != null)
                        {
                            Kill(v2 = PawnSContains() ?? default(int), "s");
                            return true;
                        }
                        else if (PawnWContains() != null)
                        {
                            Kill(v2 = PawnWContains() ?? default(int), "w");
                            return true;
                        }
                        else if (RookNContains() != null && RookNContains() >= 1)
                        {
                            Kill(v2 = RookNContains() ?? default(int), "r");
                            return true;
                        }
                        else if (BishopNContains() != null && BishopNContains() >= 1)
                        {
                            Kill(v2 = BishopNContains() ?? default(int), "b");
                            return true;
                        }
                        else if (KnightNContains() != null && KnightNContains() >= 1)
                        {
                            Kill(v2 = KnightNContains() ?? default(int), "k");
                            return true;
                        }
                        return false;
                    }
                    else if (player == 2)
                    {
                        if (PawnNContains() != null)
                        {
                            Kill(v2 = PawnNContains() ?? default(int), "n");
                            return true;
                        }
                        else if (PawnSContains() != null)
                        {
                            Kill(v2 = PawnSContains() ?? default(int), "s");
                            return true;
                        }
                        else if (PawnWContains() != null)
                        {
                            Kill(v2 = PawnWContains() ?? default(int), "w");
                            return true;
                        }
                        else if (RookNContains() != null && (RookNContains() != 2 || RookNContains() != 3))
                        {
                            Kill(v2 = RookNContains() ?? default(int), "r");
                            return true;
                        }
                        else if (BishopNContains() != null && (BishopNContains() != 2 || BishopNContains() != 3))
                        {
                            Kill(v2 = BishopNContains() ?? default(int), "b");
                            return true;
                        }
                        else if (KnightNContains() != null && (KnightNContains() != 2 || KnightNContains() != 3))
                        {
                            Kill(v2 = KnightNContains() ?? default(int), "k");
                            return true;
                        }
                        return false;
                    }
                    else if (player == 3)
                    {
                        if (PawnEContains() != null)
                        {
                            Kill(v2 = PawnEContains() ?? default(int), "e");
                            return true;
                        }
                        else if (PawnNContains() != null)
                        {
                            Kill(v2 = PawnNContains() ?? default(int), "n");
                            return true;
                        }
                        else if (PawnWContains() != null)
                        {
                            Kill(v2 = PawnWContains() ?? default(int), "w");
                            return true;
                        }
                        else if (RookNContains() != null && (RookNContains() != 4 || RookNContains() != 5))
                        {
                            Kill(v2 = RookNContains() ?? default(int), "r");
                            return true;
                        }
                        else if (BishopNContains() != null && (BishopNContains() != 4 || BishopNContains() != 5))
                        {
                            Kill(v2 = BishopNContains() ?? default(int), "b");
                            return true;
                        }
                        else if (KnightNContains() != null && (KnightNContains() != 4 || KnightNContains() != 5))
                        {
                            Kill(v2 = KnightNContains() ?? default(int), "k");
                            return true;
                        }
                        return false;

                    }
                    else if (player == 4)
                    {
                        if (PawnEContains() != null)
                        {
                            Kill(v2 = PawnEContains() ?? default(int), "e");
                            return true;
                        }
                        else if (PawnSContains() != null)
                        {
                            Kill(v2 = PawnSContains() ?? default(int), "s");
                            return true;
                        }
                        else if (PawnNContains() != null)
                        {
                            Kill(v2 = PawnNContains() ?? default(int), "n");
                            return true;
                        }
                        else if (RookNContains() != null && (RookNContains() != 6 || RookNContains() != 7))
                        {
                            Kill(v2 = RookNContains() ?? default(int), "r");
                            return true;
                        }
                        else if (BishopNContains() != null && (BishopNContains() != 6 || BishopNContains() != 7))
                        {
                            Kill(v2 = BishopNContains() ?? default(int), "b");
                            return true;
                        }
                        else if (KnightNContains() != null && (KnightNContains() != 6 || KnightNContains() != 7))
                        {
                            Kill(v2 = KnightNContains() ?? default(int), "k");
                            return true;
                        }
                        return false;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool BishopcanKill()
        {

            if (player == 1)
            {
                if (PawnEContains() != null)
                {
                    Kill(v2 = PawnEContains() ?? default(int), "e");
                    return true;
                }
                else if (PawnSContains() != null)
                {
                    Kill(v2 = PawnSContains() ?? default(int), "s");
                    return true;
                }
                else if (PawnWContains() != null)
                {
                    Kill(v2 = PawnWContains() ?? default(int), "w");
                    return true;
                }
                else if (RookNContains() != null && RookNContains() > 1)
                {
                    Kill(v2 = RookNContains() ?? default(int), "r");
                    return true;
                }
                else if (BishopNContains() != null && BishopNContains() > 1)
                {
                    Kill(v2 = BishopNContains() ?? default(int), "b");
                    return true;
                }
                else if (KnightNContains() != null && KnightNContains() > 1)
                {
                    Kill(v2 = KnightNContains() ?? default(int), "k");
                    return true;
                }
                else if (QueenNContains() != null && QueenNContains() != 0)
                {
                    Kill(v2 = QueenNContains() ?? default(int), "q");
                    return true;
                }
                else if (KingNContains() != null && KingNContains() != 0)
                {
                    Kill(v2 = KingNContains() ?? default(int), "kk");
                    return true;
                }
                return false;
            }
            else if (player == 2)
            {
                if (PawnNContains() != null)
                {
                    Kill(v2 = PawnNContains() ?? default(int), "n");
                    return true;
                }
                else if (PawnSContains() != null)
                {
                    Kill(v2 = PawnSContains() ?? default(int), "s");
                    return true;
                }
                else if (PawnWContains() != null)
                {
                    Kill(v2 = PawnWContains() ?? default(int), "w");
                    return true;
                }
                else if (RookNContains() != null && (RookNContains() != 2 || RookNContains() != 3))
                {
                    Kill(v2 = RookNContains() ?? default(int), "r");
                    return true;
                }
                else if (BishopNContains() != null && (BishopNContains() != 2 || BishopNContains() != 3))
                {
                    Kill(v2 = BishopNContains() ?? default(int), "b");
                    return true;
                }
                else if (KnightNContains() != null && (KnightNContains() != 2 || KnightNContains() != 3))
                {
                    Kill(v2 = KnightNContains() ?? default(int), "k");
                    return true;
                }
                else if (QueenNContains() != null && QueenNContains() != 1)
                {
                    Kill(v2 = QueenNContains() ?? default(int), "q");
                    return true;
                }
                else if (KingNContains() != null && KingNContains() != 1)
                {
                    Kill(v2 = KingNContains() ?? default(int), "kk");
                    return true;
                }
                return false;
            }
            else if (player == 3)
            {
                if (PawnEContains() != null)
                {
                    Kill(v2 = PawnEContains() ?? default(int), "e");
                    return true;
                }
                else if (PawnNContains() != null)
                {
                    Kill(v2 = PawnNContains() ?? default(int), "n");
                    return true;
                }
                else if (PawnWContains() != null)
                {
                    Kill(v2 = PawnWContains() ?? default(int), "w");
                    return true;
                }
                else if (RookNContains() != null && (RookNContains() != 4 || RookNContains() != 5))
                {
                    Kill(v2 = RookNContains() ?? default(int), "r");
                    return true;
                }
                else if (BishopNContains() != null && (BishopNContains() != 4 || BishopNContains() != 5))
                {
                    Kill(v2 = BishopNContains() ?? default(int), "b");
                    return true;
                }
                else if (KnightNContains() != null && (KnightNContains() != 4 || KnightNContains() != 5))
                {
                    Kill(v2 = KnightNContains() ?? default(int), "k");
                    return true;
                }
                else if (QueenNContains() != null && QueenNContains() != 2)
                {
                    Kill(v2 = QueenNContains() ?? default(int), "q");
                    return true;
                }
                else if (KingNContains() != null && KingNContains() != 2)
                {
                    Kill(v2 = KingNContains() ?? default(int), "kk");
                    return true;
                }
                return false;

            }
            else if (player == 4)
            {
                if (PawnEContains() != null)
                {
                    Kill(v2 = PawnEContains() ?? default(int), "e");
                    return true;
                }
                else if (PawnSContains() != null)
                {
                    Kill(v2 = PawnSContains() ?? default(int), "s");
                    return true;
                }
                else if (PawnNContains() != null)
                {
                    Kill(v2 = PawnNContains() ?? default(int), "n");
                    return true;
                }
                else if (RookNContains() != null && (RookNContains() != 6 || RookNContains() != 7))
                {
                    Kill(v2 = RookNContains() ?? default(int), "r");
                    return true;
                }
                else if (BishopNContains() != null && (BishopNContains() != 6 || BishopNContains() != 7))
                {
                    Kill(v2 = BishopNContains() ?? default(int), "b");
                    return true;
                }
                else if (KnightNContains() != null && (KnightNContains() != 6 || KnightNContains() != 7))
                {
                    Kill(v2 = KnightNContains() ?? default(int), "k");
                    return true;
                }
                else if (QueenNContains() != null && QueenNContains() != 3)
                {
                    Kill(v2 = QueenNContains() ?? default(int), "q");
                    return true;
                }
                else if (KingNContains() != null && KingNContains() != 3)
                {
                    Kill(v2 = KingNContains() ?? default(int), "kk");
                    return true;
                }
                return false;

            }
            else
            {
                return false;
            }

        }

        public bool RookcanKill(int u)
        {
            if (_currentMouseState.LeftButton == ButtonState.Released
                && WhiteBox[u].Contains(mousePoint)
                && ((WhiteBox[u].Y == PreviousWBox.Y)
                || (WhiteBox[u].X == PreviousWBox.X))
                && selectingPrevious == false)
            {

                return true;

            }
            return false;
        }

        public void Kill(int o, string t)
        {
            switch (player)
            {
                case 1:
                    switch (t)
                    {
                        case "e":
                            PawnE[o].Dead = true;
                            break;
                        case "s":
                            PawnS[o].Dead = true;
                            break;
                        case "w":
                            PawnW[o].Dead = true;
                            break;

                    }
                    break;

                case 2:
                    switch (t)
                    {
                        case "n":
                            PawnN[o].Dead = true;
                            break;
                        case "s":
                            PawnS[o].Dead = true;
                            break;
                        case "w":
                            PawnW[o].Dead = true;
                            break;
                    }
                    break;

                case 3:
                    switch (t)
                    {
                        case "e":
                            PawnE[o].Dead = true;
                            break;
                        case "n":
                            PawnN[o].Dead = true;
                            break;
                        case "w":
                            PawnW[o].Dead = true;
                            break;
                    }
                    break;

                case 4:
                    switch (t)
                    {
                        case "e":
                            PawnE[o].Dead = true;
                            break;
                        case "s":
                            PawnS[o].Dead = true;
                            break;
                        case "n":
                            PawnN[o].Dead = true;
                            break;
                    }
                    break;

            }
            switch (t)
            {
                case "r":
                    Rook[o].Dead = true;
                    break;
                case "b":
                    Bishop[o].Dead = true;
                    break;
                case "k":
                    Knight[o].Dead = true;
                    break;
                case "q":
                    Queen[o].Dead = true;
                    break;
                case "kk":
                    King[o].Dead = true;
                    break;
            }

        }

        public bool TestKill(Base obj)
        {

            if (obj.Pawn1Box.Contains(mousePoint) && obj.player != this.player)
            {

                obj.Dead = true;
                return true;
            }
            return false;
        }

        public void killThings()
        {
            foreach (Base obj in PawnN)
            {
                TestKill(obj);
            }
            foreach (Base obj in PawnE)
            {
                TestKill(obj);
            }
            foreach (Base obj in PawnS)
            {
                TestKill(obj);
            }
            foreach (Base obj in PawnW)
            {
                TestKill(obj);
            }
            foreach (Base obj in Rook)
            {
                TestKill(obj);
            }
            foreach (Base obj in Bishop)
            {
                TestKill(obj);
            }
            foreach (Base obj in Knight)
            {
                TestKill(obj);
            }
            foreach (Base obj in Queen)
            {
                TestKill(obj);
            }
            foreach (Base obj in King)
            {
                TestKill(obj);
            }
            Testing = 0;

        }

        /// <summary>
        /// These are so that friendly pices cannot move and endanger the king
        /// </summary>
        /// <param name="uu"></param>
        /// <returns></returns>
        /// 
#endregion

        public bool check_Danger(int uu, int Num, bool F_moveT_check)
        {
            if (check_SE(uu, Num) && check_SW(uu, Num) && check_NE(uu, Num) && check_NW(uu, Num))
            {
                if (check_S(uu, Num, F_moveT_check)
                    && check_N(uu, Num, F_moveT_check)
                    && check_E(uu, Num, F_moveT_check)
                    && check_W(uu, Num, F_moveT_check))
                {
                    return true;
                }
            }
            return false;
        }

        #region k
        public bool check_SE(int uu, int Num)
        {

                for (int o = 0; o < 8; o++)
                {
                    for (int p = 1; p < 13; p++)
                    {
                        if (Bishop[o].Pawn1Box.X == King[Num].Pawn1Box.X + (55 * p)
                                            && Bishop[o].Pawn1Box.Y == King[Num].Pawn1Box.Y + (55 * p)
                                            && Bishop[o].player != King[Num].player
                                            && player == King[Num].player)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                    && WhiteBox[uu].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                    && WhiteBox[uu].Y == King[Num].Pawn1Box.Y + (55 * (p - pp))
                                    )
                                {
                                    return true;


                                }
                            }
                            for (int u = 0; u < 64; u++)
                            {
                                for (int pp = 1; pp < p; pp++)
                                {
                                    if (p > pp
                                        //&& Game1.allP[u] == King[e].player
                                    && Game1.all[u].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                    && Game1.all[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                    {
                                        return true;


                                    }
                                }
                            }

                            return false;
                        }
                    }
                }
            
            return true;

        }
        public bool check_SW(int uu, int Num)
        {
                for (int o = 0; o < 8; o++)
                {
                    for (int p = 1; p < 13; p++)
                    {
                        if (Bishop[o].Pawn1Box.X == King[Num].Pawn1Box.X - (55 * p)
                                            && Bishop[o].Pawn1Box.Y == King[Num].Pawn1Box.Y + (55 * p)
                                            && Bishop[o].player != King[Num].player
                                            && player == King[Num].player)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                    && WhiteBox[uu].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                    && WhiteBox[uu].Y == King[Num].Pawn1Box.Y + (55 * (p - pp))
                                    )
                                {
                                    return true;


                                }
                            }
                            for (int u = 0; u < 64; u++)
                            {
                                for (int pp = 1; pp < p; pp++)
                                {
                                    if (p > pp
                                        //&& Game1.allP[u] == King[e].player
                                    && Game1.all[u].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                    && Game1.all[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                    {
                                        return true;


                                    }
                                }
                            }

                            return false;

                        }
                    }
                }
            
            return true;

        }
        public bool check_NE(int uu, int Num)
        {

            for (int e = 0; e < 4; e++)
            {
                for (int o = 0; o < 8; o++)
                {
                    for (int p = 1; p < 13; p++)
                    {
                        if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X + (55 * p)
                                            && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
                                            && Bishop[o].player != King[e].player
                                            && player == King[e].player)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                    && WhiteBox[uu].X == King[e].Pawn1Box.X + (55 * (p - pp))
                                    && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
                                    )
                                {
                                    return true;


                                }
                            }
                            for (int u = 0; u < 64; u++)
                            {
                                for (int pp = 1; pp < p; pp++)
                                {
                                    if (p > pp
                                        //&& Game1.allP[u] == King[e].player
                                    && Game1.all[u].X == King[e].Pawn1Box.X + (55 * (p - pp))
                                    && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp)))
                                    {
                                        return true;


                                    }
                                }
                            }
                            danger_Level += 1;

                            return false;

                        }
                    }
                }
            }
            return true;

        }
        public bool check_NW(int uu, int Num)
        {
                for (int o = 0; o < 8; o++)
                {
                    for (int p = 1; p < 13; p++)
                    {
                        if (Bishop[o].Pawn1Box.X == King[Num].Pawn1Box.X - (55 * p)
                                            && Bishop[o].Pawn1Box.Y == King[Num].Pawn1Box.Y - (55 * p)
                                            && Bishop[o].player != King[Num].player
                                            && player == King[Num].player)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                    && WhiteBox[uu].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                    && WhiteBox[uu].Y == King[Num].Pawn1Box.Y - (55 * (p - pp))
                                    )
                                {
                                    return true;


                                }
                            }
                            for (int u = 0; u < 64; u++)
                            {
                                for (int pp = 1; pp < p; pp++)
                                {
                                    if (p > pp
                                        //&& Game1.allP[u] == King[e].player
                                    && Game1.all[u].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                    && Game1.all[u].Y == King[Num].Pawn1Box.Y - (55 * (p - pp)))
                                    {
                                        return true;


                                    }
                                }
                            }
                            danger_Level += 1;

                            return false;

                        }
                    }
                }
            
            return true;

        }
        #endregion

        public bool check_S(int uu, int Num, bool F_moveT_check)
        {
            for (int o = 0; o < 8; o++)
            {
                for (int p = 1; p < 13; p++)
                {
                    if (Rook[o].Pawn1Box.X == King[Num].Pawn1Box.X
                                        && Rook[o].Pawn1Box.Y == King[Num].Pawn1Box.Y + (55 * p)
                                        && Rook[o].player != King[Num].player
                                        && player == King[Num].player)
                    {
                        for (int pp = 1; pp < p; pp++)
                        {
                            for (int u = 0; u < 64; u++)
                            {
                                if (p > pp
                                && Game1.all[u].X == King[Num].Pawn1Box.X
                                && Game1.all[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                {
                                    return true;
                                }
                            }
                        }
                        if (F_moveT_check)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                for (int u = 0; u < WhiteBox.Count(); u++)
                                {
                                    if (WhiteBox[u].X == King[Num].Pawn1Box.X
                                        && WhiteBox[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'S');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y - (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'N');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'E');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'W');
                                    }
                                }
                            }
                        }
                        danger_Level += 1;
                        return false;
                    }
                }
            }
            return true;
        }
        public bool check_N(int uu, int Num, bool F_moveT_check)
        {
            for (int o = 0; o < 8; o++)
            {
                for (int p = 1; p < 13; p++)
                {
                    if (Rook[o].Pawn1Box.X == King[Num].Pawn1Box.X
                                        && Rook[o].Pawn1Box.Y == King[Num].Pawn1Box.Y - (55 * p)
                                        && Rook[o].player != King[Num].player
                                        && player == King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                && Game1.all[u].X == King[Num].Pawn1Box.X
                                && Game1.all[u].Y == King[Num].Pawn1Box.Y - (55 * (p - pp)))
                                {
                                    return true;


                                }
                            }
                        }
                        if (F_moveT_check)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                for (int u = 0; u < WhiteBox.Count(); u++)
                                {
                                    if (WhiteBox[u].X == King[Num].Pawn1Box.X
                                        && WhiteBox[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'S');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y - (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'N');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'E');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'W');
                                    }
                                }
                            }
                        }
                        danger_Level += 1;

                        return false;

                    }
                }
            }

            return true;

        }
        public bool check_E(int uu, int Num, bool F_moveT_check)
        {
            for (int o = 0; o < 8; o++)
            {
                for (int p = 1; p < 13; p++)
                {
                    if (Rook[o].Pawn1Box.X == King[Num].Pawn1Box.X + (55 * p)
                                        && Rook[o].Pawn1Box.Y == King[Num].Pawn1Box.Y
                                        && Rook[o].player != King[Num].player
                                        && player == King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                && Game1.all[u].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                && Game1.all[u].Y == King[Num].Pawn1Box.Y)
                                {
                                    return true;


                                }
                            }
                        }

                        if (F_moveT_check)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                for (int u = 0; u < WhiteBox.Count(); u++)
                                {
                                    if (WhiteBox[u].X == King[Num].Pawn1Box.X
                                        && WhiteBox[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'S');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y - (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'N');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'E');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'W');
                                    }
                                }
                            }
                        }
                        danger_Level += 1;

                        return false;

                    }
                }
            }

            return true;

        }
        public bool check_W(int uu, int Num, bool F_moveT_check)
        {
            for (int o = 0; o < 8; o++)
            {
                for (int p = 1; p < 13; p++)
                {
                    if (Rook[o].Pawn1Box.X == King[Num].Pawn1Box.X - (55 * p)
                                        && Rook[o].Pawn1Box.Y == King[Num].Pawn1Box.Y
                                        && Rook[o].player != King[Num].player
                                        && player == King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                if (p > pp
                                && Game1.all[u].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                && Game1.all[u].Y == King[Num].Pawn1Box.Y)
                                {
                                    return true;


                                }
                            }
                        }
                        if (F_moveT_check)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {
                                for (int u = 0; u < WhiteBox.Count(); u++)
                                {
                                    if (WhiteBox[u].X == King[Num].Pawn1Box.X
                                        && WhiteBox[u].Y == King[Num].Pawn1Box.Y + (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'S');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y - (55 * (p - pp)))
                                    {
                                        return Check_for_Savior(u, Num, 'N');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X + (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'E');
                                    }
                                    else if (WhiteBox[uu].X == King[Num].Pawn1Box.X - (55 * (p - pp))
                                        && WhiteBox[uu].Y == King[Num].Pawn1Box.Y)
                                    {
                                        return Check_for_Savior(u, Num, 'W');
                                    }
                                }
                            }
                        }
                        danger_Level += 1;

                        return false;

                    }
                }
            }

            return true;

        }

        #region K
        /// <summary>
        /// These are to check the WhiteBox square the king is moving too, whether it is safe.
        /// </summary>
        /// <param name="uu"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        //public bool check_Bishop(int uu, int num)
        //{

        //    int Num = num - 1;
        //    for (int o = 0; o < 8; o++)
        //    {

        //        for (int p = 1; p < 13; p++)
        //        {


        //            //if (Bishop[o].Pawn1Box.X == PreviousWBox.X + (55 * p)
        //            //    && Bishop[o].Pawn1Box.Y == PreviousWBox.Y + (55 * p)
        //            //    && Bishop[o].Pawn1Box.X == Game1.all[u].X + (55 * pk)
        //            //    && Bishop[o].Pawn1Box.Y == Game1.all[u].Y + (55 * pk)
        //            //    && Bishop[o].player != King[e].player
        //            //    && player == King[e].player)
        //            //{

        //            //    return false;
        //            //} 
        //            if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X + (55 * p)
        //               && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y + (55 * p)
        //               && Bishop[o].player != King[Num].player)
        //            {
        //                for (int u = 0; u < 64; u++)
        //                {
        //                    for (int pp = 1; pp < p; pp++)
        //                    {

        //                        if (Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp))
        //                           && Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp)))
        //                        {
        //                            return true; ;
        //                        }
        //                    }
        //                }
        //                return false;

        //            }
        //            if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X - (55 * p)
        //             && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y - (55 * p)
        //             && Bishop[o].player != King[Num].player)
        //            {
        //                for (int u = 0; u < 64; u++)
        //                {
        //                    for (int pp = 1; pp < p; pp++)
        //                    {

        //                        if (Game1.all[u].X == WhiteBox[uu].X - (55 * (p - pp))
        //                           && Game1.all[u].Y == WhiteBox[uu].Y - (55 * (p - pp)))
        //                        {
        //                            return true; ;
        //                        }
        //                    }
        //                }
        //                return false;

        //            }
        //            if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X + (55 * p)
        //            && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y - (55 * p)
        //            && Bishop[o].player != King[Num].player)
        //            {
        //                for (int u = 0; u < 64; u++)
        //                {
        //                    for (int pp = 1; pp < p; pp++)
        //                    {

        //                        if (Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp))
        //                           && Game1.all[u].Y == WhiteBox[uu].Y - (55 * (p - pp)))
        //                        {
        //                            return true; ;
        //                        }
        //                    }
        //                }
        //                return false;

        //            }
        //            if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X - (55 * p)
        //             && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y + (55 * p)
        //             && Bishop[o].player != King[Num].player)
        //            {
        //                for (int u = 0; u < 64; u++)
        //                {
        //                    for (int pp = 1; pp < p; pp++)
        //                    {

        //                        if (Game1.all[u].X == WhiteBox[uu].X - (55 * (p - pp))
        //                           && Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp)))
        //                        {
        //                            return true; ;
        //                        }
        //                    }
        //                }
        //                return false;

        //            }

        //        }
        //    }
        //    return true;

        //}

        public bool check_Bishop(int uu, int num)
        {
            int Num = num - 1;
            if (check_BNE(uu, Num)
                  && check_BNW(uu, Num)
                  && check_BSE(uu, Num)
                  && check_BSW(uu, Num))
            {
                return true;
            }
            return false;
        }

        public bool check_BNE(int uu, int Num)
        {
            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {

                
                    if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X + (55 * p)
                    && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y - (55 * p)
                    && Bishop[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp))
                                   && Game1.all[u].Y == WhiteBox[uu].Y - (55 * (p - pp)))
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;

                    }

                }
            }
            return true;
        }
        public bool check_BNW(int uu, int Num)
        {
            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {


                    if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X - (55 * p)
                     && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y + (55 * p)
                     && Bishop[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X - (55 * (p - pp))
                                   && Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp)))
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;

                    }

                }
            }
            return true;
        }
        public bool check_BSE(int uu, int Num)
        {
            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {

                    if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X + (55 * p)
                       && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y + (55 * p)
                       && Bishop[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp))
                                   && Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp)))
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;

                    }
                }
            }
            return true;
        }
        public bool check_BSW(int uu, int Num)
        {
            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {

                 
                    if (Bishop[o].Pawn1Box.X == WhiteBox[uu].X - (55 * p)
                     && Bishop[o].Pawn1Box.Y == WhiteBox[uu].Y - (55 * p)
                     && Bishop[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X - (55 * (p - pp))
                                   && Game1.all[u].Y == WhiteBox[uu].Y - (55 * (p - pp)))
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;

                    }
                              
                }
            }
            return true;
        }

        public bool check_Rook(int uu, int num)
        {
            int Num = num - 1;
            if (check_RN(uu, Num)
                  && check_RE(uu, Num)
                  && check_RS(uu, Num)
                  && check_RW(uu, Num))
            {
                return true;
            }
            return false;
        }

        public bool check_RN(int uu, int Num)
        {
            for (int o = 0; o < 8; o++)
            {
                for (int p = 1; p < 13; p++)
                {
                    if (Rook[o].Pawn1Box.X == WhiteBox[uu].X
                             && Rook[o].Pawn1Box.Y == WhiteBox[uu].Y - (55 * p)
                             && Rook[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X
                                   && Game1.all[u].Y == WhiteBox[uu].Y - (55 * (p - pp)))
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        public bool check_RE(int uu, int Num)
        {

            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {

                    if (Rook[o].Pawn1Box.X == WhiteBox[uu].X + (55 * p)
                         && Rook[o].Pawn1Box.Y == WhiteBox[uu].Y
                         && Rook[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp))
                                   && Game1.all[u].Y == WhiteBox[uu].Y)
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;

                    }
                }
            }
            return true;
        }
        public bool check_RS(int uu, int Num)
        {

            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {

                    if (Rook[o].Pawn1Box.X == WhiteBox[uu].X
                       && Rook[o].Pawn1Box.Y == WhiteBox[uu].Y + (55 * p)
                       && Rook[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X
                                   && Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp)))
                                {
                                    return true; 
                                }
                            }
                        }
                        return false;

                    }
                }
            }
            return true;
        }
        public bool check_RW(int uu, int Num)
        {

            for (int o = 0; o < 8; o++)
            {

                for (int p = 1; p < 13; p++)
                {


                    if (Rook[o].Pawn1Box.X == WhiteBox[uu].X - (55 * p)
                    && Rook[o].Pawn1Box.Y == WhiteBox[uu].Y
                    && Rook[o].player != King[Num].player)
                    {
                        for (int u = 0; u < 64; u++)
                        {
                            for (int pp = 1; pp < p; pp++)
                            {

                                if (Game1.all[u].X == WhiteBox[uu].X - (55 * (p - pp))
                                   && Game1.all[u].Y == WhiteBox[uu].Y)
                                {
                                    return true; ;
                                }
                            }
                        }
                        return false;

                    }
                }
            }
            return true;
        }
        #endregion

        public bool Check_for_Savior(int u, int num, char Nesw)
        {
            for (int o = 0; o < Rook.Count(); o++)
            {
                for (int p = 1; p < 13; p++)
                {
                    switch (Nesw)
                    {
                        case 'N':
                        case 'S':
                            if (Rook[o].Pawn1Box.X == WhiteBox[u].X + (55 * p)
                                && Rook[o].Pawn1Box.Y == WhiteBox[u].Y
                                && Rook[o].player == King[num].player)
                            {
                                return check_nesw(o, p, u, num, Nesw, 1);
                            }
                            else if (Rook[o].Pawn1Box.X == WhiteBox[u].X - (55 * p)
                                && Rook[o].Pawn1Box.Y == WhiteBox[u].Y
                                && Rook[o].player == King[num].player)
                            {
                                return check_nesw(o, p, u, num, Nesw, 2);
                            }
                            break;
                        case 'E':
                        case 'W':
                            if (Rook[o].Pawn1Box.X == WhiteBox[u].X
                                && Rook[o].Pawn1Box.Y == WhiteBox[u].Y + (55 * p)
                                && Rook[o].player == King[num].player)
                            {
                                return check_nesw(o, p, u, num, Nesw, 3);
                            }
                            else if (Rook[o].Pawn1Box.X == WhiteBox[u].X
                                && Rook[o].Pawn1Box.Y == WhiteBox[u].Y - (55 * p)
                                && Rook[o].player == King[num].player)
                            {
                                return check_nesw(o, p, u, num, Nesw, 4);
                            }
                            break;
                    }
                }
            }
            return false;
        }

        public bool check_nesw(int o, int p, int u, int num, char Nesw, int dir)
        {
            for (int t = 0; t < Game1.all.Count(); t++)
            {
                for (int pp = 1; pp < p; pp++)
                {
                    switch (dir)
                    {
                        case 2:
                            if (Game1.all[t].X == WhiteBox[u].X - (55 * (p - pp))
                                && Game1.all[t].Y == WhiteBox[u].Y)
                            {
                                return true;
                            }
                            break;
                        case 1:
                            if (Game1.all[t].X == WhiteBox[u].X + (55 * (p - pp))
                                && Game1.all[t].Y == WhiteBox[u].Y)
                            {
                                return true;
                            }
                            break;
                        case 3:
                            if (Game1.all[t].X == WhiteBox[u].X
                                && Game1.all[t].Y == WhiteBox[u].Y + (55 * (p - pp)))
                            {
                                return true;
                            }
                            break;
                        case 4:
                            if (Game1.all[t].X == WhiteBox[u].X
                                && Game1.all[t].Y == WhiteBox[u].Y - (55 * (p - pp)))
                            {
                                return true;
                            }
                            break;
                    }
                }
            }
            return LocateKing(o, num, Nesw, dir);

        }

        public bool LocateKing(int o, int num, char Nesw, int dir)
        {

            switch (Nesw)
            {
                case 'N':
                case 'S':
                    if (dir == 1)
                    {
                        for (int g = 1; g < 14; g++)
                        {
                            if (Rook[o].Pawn1Box.X == King[num].Pawn1Box.X + (55 * g)
                                && Rook[o].Pawn1Box.Y == King[num].Pawn1Box.Y + (55 * g))
                            {


                                for (int L = 1; L < g; L++)
                                {
                                    for (int M = 0; M < Game1.all.Count(); M++)
                                    {
                                        if (Game1.all[M].X == King[num].Pawn1Box.X + (55 * (g - L))
                                            && Game1.all[M].Y == King[num].Pawn1Box.Y + (55 * (g - L)))
                                        {
                                            return true;

                                        }
                                    }
                                }


                                for (int j = 0; j < 7; j++)
                                {
                                    for (int n = 1; n < 13; n++)
                                    {
                                        if (Bishop[j].Pawn1Box.X == Rook[o].Pawn1Box.X + (55 * n)
                                               && Bishop[j].Pawn1Box.Y == Rook[o].Pawn1Box.Y + (55 * n)
                                            && Bishop[j].player != King[num].player)
                                        {
                                            for (int f = 1; f < n; f++)
                                            {
                                                for (int M = 0; M < Game1.all.Count(); M++)
                                                {
                                                    if (Game1.all[M].X == Rook[o].Pawn1Box.X + (55 * f)
                                                        && Game1.all[M].Y == Rook[o].Pawn1Box.Y + (55 * f))
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                return false;
                            }
                        }
                        return true;
                    }
                    break;
            }
            return true;

        }

        public bool move_willCheck(int uu, int Num)
        {

            #region n
            //for (int e = 0; e < 4; e++)
            //{
            //    for (int o = 0; o < 8; o++)
            //    {
            //        for (int p = 1; p < 13; p++)
            //        {
            //            for (int pk = 1; pk < 13; pk++)
            //            {
            //                if (Num == 1)
            //                {
            //                    if (Bishop[o].Pawn1Box.X == PreviousWBox.X + (55 * p)
            //                        && Bishop[o].Pawn1Box.Y == PreviousWBox.Y + (55 * p)
            //                        && Bishop[o].Pawn1Box.X == Game1.all[u].X + (55 * pk)
            //                        && Bishop[o].Pawn1Box.Y == Game1.all[u].Y + (55 * pk)
            //                        && Bishop[o].player != King[e].player
            //                        && player == King[e].player)
            //                    {
            //                        //for (int u = 0; u < 64; u++)
            //                        //{
            //                        //    for (int pp = 1; pp < p; pp++)
            //                        //    {
            //                        //        if (p > pp
            //                        //        //&& Game1.allP[u] == King[e].player
            //                        //         && Game1.all[u].X != Bishop[o].Pawn1Box.X + (55 * (p * pp))
            //                        //         && Game1.all[u].Y != Bishop[o].Pawn1Box.Y + (55 * (p * pp))
            //                        //         && Bishop[o].Pawn1Box.X == Game1.all[u].X + (55 * (p - pp))
            //                        //         && Bishop[o].Pawn1Box.Y == Game1.all[u].Y + (55 * (p - pp)))
            //                        //{
            //                        return true;
            //                        //        }
            //                        //    }
            //                        //}
            //                        //return true;

            //                    }
            //                }

            //if (Num == 1)
            //{
            //    if (Bishop[o].player != King[e].player
            //        && Bishop[o].Pawn1Box.X == PreviousWBox.X + (55 * p)
            //        && Bishop[o].Pawn1Box.Y == PreviousWBox.Y + (55 * p)
            //        && Game1.all[u].X != Bishop[o].Pawn1Box.X + (55 * (p * pp))
            //        && Game1.all[u].X != Bishop[o].Pawn1Box.X + (55 * (p * pp))
            //        && player == King[e].player)
            //    {


            //        //if (p > pp
            //        //    //&& Game1.allP[u] == King[e].player
            //        //&& Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp))
            //        //&& Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp)))
            //        //{
            //        return false;
            //        //}

            //    }
            //    return true;


            //}

            #endregion

            if (Num == 1)
            {
                if (check_Rook(uu, Num) && check_Bishop(uu, Num))
                {
                    return false;
                }
            }
            else
            {
                if (check_Danger(uu, Num, true))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
#endregion


//        public bool move_willCheck(int uu, int Num)
//        {

           
//                for (int e = 0; e < 4; e++)
//                {
//                    for (int o = 0; o < 8; o++)
//                    {
//                        for (int p = 1; p < 13; p++)
//                        {
//                            if (Num == 1)
//                            {
//                                if (Bishop[o].Pawn1Box.X == PreviousWBox.X + (55 * p)
//                                    && Bishop[o].Pawn1Box.Y == PreviousWBox.Y + (55 * p)
//                                    && Bishop[o].player != King[e].player
//                                    && player == King[e].player)
//                                {
//                                    for (int u = 0; u < 64; u++)
//                                    {
//                                        for (int pp = 1; pp < p; pp++)
//                                        {
//                                            if (p > pp
//                                            && Game1.allP[u] == King[e].player
//                                            && Game1.all[u].Y == WhiteBox[uu].Y + (55 * (p - pp))
//                                            && Game1.all[u].X == WhiteBox[uu].X + (55 * (p - pp)))
//                                            {
//                                                return false;

//                                            }
//                                        }
//                                    }
//                                    return true;
                                
//                                }
//                            }
//                            else
//                            {
//                                //SE
//                                if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X + (55 * p)
//                                    && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y + (55 * p)
//                                    && Bishop[o].player != King[e].player
//                                    && player == King[e].player)
//                                {
//                                    for (int pp = 1; pp < p; pp++)
//                                    {
//                                        if (p > pp
//                                            && WhiteBox[uu].X == King[e].Pawn1Box.X + (55 * (p - pp))
//                                            && WhiteBox[uu].Y == King[e].Pawn1Box.Y + (55 * (p - pp))
//                                            )
//                                        {
//                                            return false;

//                                        }
//                                    }
//                                    for (int u = 0; u < 64; u++)
//                                    {
//                                        for (int pp = 1; pp < p; pp++)
//                                        {
//                                            if (p > pp
//                                            && Game1.allP[u] == King[e].player
//                                            && Game1.all[u].X == King[e].Pawn1Box.X + (55 * (p - pp))
//                                            && Game1.all[u].Y == King[e].Pawn1Box.Y + (55 * (p - pp)))
//                                            {
//                                                return false;

//                                            }
//                                        }
//                                    }
//                                    return true;

//                                }
//                                #region stuff
//                                if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X - (55 * p)
//                                  && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y + (55 * p)
//                                  && Bishop[o].player != King[e].player
//                                  && player == King[e].player)
//                                {
//                                    for (int pp = 1; pp < p; pp++)
//                                    {
//                                        if (p > pp
//                                            && WhiteBox[uu].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                                            && WhiteBox[uu].Y == King[e].Pawn1Box.Y + (55 * (p - pp))                                            
//                                            )
//                                        {
//                                            return false;

//                                        }
//                                    }
//                                    for (int u = 0; u < 64; u++)
//                                    {
//                                        for (int pp = 1; pp < p; pp++)
//                                        {
//                                            if (p > pp
//                                            && Game1.allP[u] == King[e].player
//                                            && Game1.all[u].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                                            && Game1.all[u].Y == King[e].Pawn1Box.Y + (55 * (p - pp))
//                                                )
//                                            {
//                                                return false;

//                                            }
//                                        }
//                                    }
//                                    return true;

//                                }
//                                if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X - (55 * p)
//                                  && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
//                                  && Bishop[o].player != King[e].player
//                                  && player == King[e].player)
//                                {
//                                    for (int pp = 1; pp < p; pp++)
//                                    {
//                                        if (p > pp
//                                            && WhiteBox[uu].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                                            && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                                            )
//                                        {
//                                            return false;

//                                        }
//                                    }
//                                    for (int u = 0; u < 64; u++)
//                                    {
//                                        for (int pp = 1; pp < p; pp++)
//                                        {
//                                            if (p > pp
//                                            && Game1.allP[u] == King[e].player
//                                            && Game1.all[u].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                                            && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                                                )
//                                            {
//                                                return false;

//                                            }
//                                        }
//                                    }
//                                    return true;

//                                }
//                                if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X + (55 * p)
//                                     && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
//                                     && Bishop[o].player != King[e].player
//                                     && player == King[e].player)
//                                {
//                                    for (int pp = 1; pp < p; pp++)
//                                    {
//                                        if (p > pp
//                                            && WhiteBox[uu].X == King[e].Pawn1Box.X + (55 * (p - pp))
//                                            && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                                            )
//                                        {
//                                            return false;

//                                        }
//                                    }
//                                    for (int u = 0; u < 64; u++)
//                                    {
//                                        for (int pp = 1; pp < p; pp++)
//                                        {
//                                            if (p > pp
//                                            && Game1.allP[u] == King[e].player
//                                            && Game1.all[u].X == King[e].Pawn1Box.X + (55 * (p - pp))
//                                            && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                                                )
//                                            {
//                                                return false;

//                                            }
//                                        }
//                                    }

//                                    return true;
//                                }

//                                #endregion
//                            }
//                        }
//                    }
//                }
            

//            return false;
//        }
//    }
//}
//#endregion




//SE
//if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X + (55 * p)
//    && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y + (55 * p)
//    && Bishop[o].player != King[e].player
//    && player == King[e].player)
//{
//    for (int pp = 1; pp < p; pp++)
//    {
//        if (p > pp
//            && WhiteBox[uu].X == King[e].Pawn1Box.X + (55 * (p - pp))
//            && WhiteBox[uu].Y == King[e].Pawn1Box.Y + (55 * (p - pp))
//            )
//        {
//            return false;

//        }
//    }
//    for (int u = 0; u < 64; u++)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//            && Game1.allP[u] == King[e].player
//            && Game1.all[u].X == King[e].Pawn1Box.X + (55 * (p - pp))
//            && Game1.all[u].Y == King[e].Pawn1Box.Y + (55 * (p - pp)))
//            {
//                return false;

//            }
//        }
//    }
//    return true;

//}

//#region stuff
//if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X - (55 * p)
//  && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y + (55 * p)
//  && Bishop[o].player != King[e].player
//  && player == King[e].player)
//{
//    for (int pp = 1; pp < p; pp++)
//    {
//        if (p > pp
//            && WhiteBox[uu].X == King[e].Pawn1Box.X - (55 * (p - pp))
//            && WhiteBox[uu].Y == King[e].Pawn1Box.Y + (55 * (p - pp))                                            
//            )
//        {
//            return false;

//        }
//    }
//    for (int u = 0; u < 64; u++)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//            && Game1.allP[u] == King[e].player
//            && Game1.all[u].X == King[e].Pawn1Box.X - (55 * (p - pp))
//            && Game1.all[u].Y == King[e].Pawn1Box.Y + (55 * (p - pp))
//                )
//            {
//                return false;

//            }
//        }
//    }
//    return true;

//}
//if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X - (55 * p)
//  && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
//  && Bishop[o].player != King[e].player
//  && player == King[e].player)
//{
//    for (int pp = 1; pp < p; pp++)
//    {
//        if (p > pp
//            && WhiteBox[uu].X == King[e].Pawn1Box.X - (55 * (p - pp))
//            && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//            )
//        {
//            return false;

//        }
//    }
//    for (int u = 0; u < 64; u++)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//            && Game1.allP[u] == King[e].player
//            && Game1.all[u].X == King[e].Pawn1Box.X - (55 * (p - pp))
//            && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                )
//            {
//                return false;

//            }
//        }
//    }
//    return true;

//}
//if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X + (55 * p)
//     && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
//     && Bishop[o].player != King[e].player
//     && player == King[e].player)
//{
//    for (int pp = 1; pp < p; pp++)
//    {
//        if (p > pp
//            && WhiteBox[uu].X == King[e].Pawn1Box.X + (55 * (p - pp))
//            && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//            )
//        {
//            return false;

//        }
//    }
//    for (int u = 0; u < 64; u++)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//            && Game1.allP[u] == King[e].player
//            && Game1.all[u].X == King[e].Pawn1Box.X + (55 * (p - pp))
//            && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                )
//            {
//                return false;

//            }
//        }
//    }

//    return true;
//}

//#endregion







//public bool check_SW(int e, int o, int uu, int p)
//{
//    if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X - (55 * p)
//                         && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y + (55 * p)
//                         && Bishop[o].player != King[e].player
//                         && player == King[e].player)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//                && WhiteBox[uu].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                && WhiteBox[uu].Y == King[e].Pawn1Box.Y + (55 * (p - pp))
//                )
//            {
//                return true;

//            }
//        }
//        for (int u = 0; u < 64; u++)
//        {
//            for (int pp = 1; pp < p; pp++)
//            {
//                if (p > pp
//                && Game1.allP[u] == King[e].player
//                && Game1.all[u].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                && Game1.all[u].Y == King[e].Pawn1Box.Y + (55 * (p - pp))
//                    )
//                {
//                    return true;


//                }
//            }
//        }
//        return false;

//    }
//    return false;

//}
//public bool check_NW(int e, int o, int uu, int p)
//{

//    if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X - (55 * p)
//      && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
//      && Bishop[o].player != King[e].player
//      && player == King[e].player)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//                && WhiteBox[uu].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                )
//            {
//                return true;

//            }
//        }
//        for (int u = 0; u < 64; u++)
//        {
//            for (int pp = 1; pp < p; pp++)
//            {
//                if (p > pp
//                && Game1.allP[u] == King[e].player
//                && Game1.all[u].X == King[e].Pawn1Box.X - (55 * (p - pp))
//                && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                    )
//                {
//                    return true;

//                }
//            }
//        }
//        return false;

//    }
//    return false;

//}
//public bool check_NE(int e, int o, int uu, int p)
//{
//    if (Bishop[o].Pawn1Box.X == King[e].Pawn1Box.X + (55 * p)
//                     && Bishop[o].Pawn1Box.Y == King[e].Pawn1Box.Y - (55 * p)
//                     && Bishop[o].player != King[e].player
//                     && player == King[e].player)
//    {
//        for (int pp = 1; pp < p; pp++)
//        {
//            if (p > pp
//                && WhiteBox[uu].X == King[e].Pawn1Box.X + (55 * (p - pp))
//                && WhiteBox[uu].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                )
//            {
//                return true;

//            }
//        }
//        for (int u = 0; u < 64; u++)
//        {
//            for (int pp = 1; pp < p; pp++)
//            {
//                if (p > pp
//                && Game1.allP[u] == King[e].player
//                && Game1.all[u].X == King[e].Pawn1Box.X + (55 * (p - pp))
//                && Game1.all[u].Y == King[e].Pawn1Box.Y - (55 * (p - pp))
//                    )
//                {
//                    return true;

//                }
//            }
//        }

//        return false;
//    }
//    return true;


//}