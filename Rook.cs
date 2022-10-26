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

    public class Rook : Base
    {
        public Rook(Texture2D Pawn1Sprite, int x, int y, int Player, Color tint)
        {
            this.PawnSprite = Pawn1Sprite;
            this.xx = x;
            this.yy = y;
            this.player = Player;
            this.Clr = tint;
        }

        public override void Update(GameTime gameTime)
        {
            doThese();

            if (!Dead
                && ((WhoamI == playerList1 && Moved1 == false && playerTurn == 1 && player == 1)
                || (WhoamI == playerList2 && Moved2 == false && playerTurn == 2 && player == 2)
                || (WhoamI == playerList3 && Moved3 == false && playerTurn == 3 && player == 3)
                || (WhoamI == playerList4 && Moved4 == false && playerTurn == 4 && player == 4)))
            {
                for (int u = 0; u < 160; u++)
                {
                    for (int o = 0; o < 64; o++)
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

                                if (MovePawn(u, o))
                                {

                                    MovePawnLocation(u);
                                    tst = false;
                                    resetDX();
                                }

                                else if (KillsPawn(u, o))
                                {
                                    MovePawnLocation(u);
                                    tst = false;
                                    resetDX();

                                    #region kill

                                    killThings();
                                }
                                    #endregion
                            }
                        }
                }

                v = 0;
            }
        }

        #region j



        //&& ((((WhiteBox[u].X == PreviousWBox.X) //can only move down because its the same X co-ord
        //&& (PawnS[o].Pawn1Box.X == PreviousWBox.X  //&& PawnS[o].Pawn1Box.Y == PreviousWBox.Y + (55 * e) //if there is an enemy with the same x co-ord and the enemy is inline with the y co ord
        //&& PawnS[o].Pawn1Box.Y > WhiteBox[u].Y)

        //||

        //(((WhiteBox[u].Y > PreviousWBox.Y) //this makes it so the code is only if rook is moving down
        //&& (PawnS[o].Pawn1Box.X != PreviousWBox.X)) // and there are no obstacles up or down

        //||

        //((WhiteBox[u].Y < PreviousWBox.Y) //this makes it so the code is only if rook is moving up
        //&& (PawnS[o].Pawn1Box.X != PreviousWBox.X)) // and there are no obstacles up or down

        //||

        //((WhiteBox[u].X > PreviousWBox.X) //this makes it so the code is only if rook is moving right
        //&& (PawnS[o].Pawn1Box.Y != PreviousWBox.Y)) // and there are no obstacles left or right

        //||

        //((WhiteBox[u].X < PreviousWBox.X) //this makes it so the code is only if rook is moving right
        //&& (PawnS[o].Pawn1Box.Y != PreviousWBox.Y))) // and there are no obstacles left or right


        //)) //whitbox is less than enemy.Y

        //||

        //(
        //((WhiteBox[u].Y == PreviousWBox.Y) //can only move down because its the same X co-ord
        // //this makes it so the code is only if rook is moving right
        //&& 

        //((PawnS[o].Pawn1Box.Y == PreviousWBox.Y  //&& PawnS[o].Pawn1Box.Y == PreviousWBox.Y + (55 * e) //if there is an enemy with the same x co-ord and the enemy is inline with the y co ord
        //&& PawnS[o].Pawn1Box.X > WhiteBox[u].X)

        //||

        //(WhiteBox[u].X > PreviousWBox.X))
                

               
            //if (_currentMouseState.LeftButton == ButtonState.Released
            //    && WhiteBox[u].Contains(mousePoint)
            //    && WhiteBox[u].Intersects(Pawn1Box)
            //    && !PawnS[getIND].Pawn1Box.Contains(mousePoint)
            //    && selectingPrevious == false
              
            //    && 
            //    ((WhiteBox[u].X > PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)
            //    || (WhiteBox[u].X < PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)

            //    || (WhiteBox[u].Y > PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X
            //    && ((PawnS[o].Pawn1Box.X == WhiteBox[u].X && PawnS[o].Pawn1Box.Y > WhiteBox[u].Y)))


            //    || (WhiteBox[u].Y < PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X)
            //    )
                


            //    ) 
                




        
            //if (_currentMouseState.LeftButton == ButtonState.Released
            //    && WhiteBox[u].Contains(mousePoint)
            //    && WhiteBox[u].Intersects(Pawn1Box)
            //    && !PawnS[getIND].Pawn1Box.Contains(mousePoint)
            //    && selectingPrevious == false
              
            //    && 
            //    ((WhiteBox[u].X > PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)
            //    || (WhiteBox[u].X < PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)

            //    || (WhiteBox[u].Y > PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X
            //    && ((PawnS[o].Pawn1Box.X == WhiteBox[u].X) ? (PawnS[o].Pawn1Box.Y > WhiteBox[u].Y) : (e == 8)))


            //    || (WhiteBox[u].Y < PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X)
            //    )
                


            ////    )


            //if (PawnS[o].Pawn1Box.X == WhiteBox[u].X)
            //    {
            //        if (PawnS[o].Pawn1Box.Y > WhiteBox[u].Y)
            //        {
            //            e = 22;
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else if (e == 8)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }



            //}


                
                
        #endregion

        public bool MovePawn(int u, int o)
        {


            if (_currentMouseState.LeftButton == ButtonState.Released

                && WhiteBox[u].Contains(mousePoint)
                && WhiteBox[u].Intersects(Pawn1Box)
                && selectingPrevious == false
                 && !move_willCheck(u, 0))
            {

                if (WhiteBox[u].X < PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)
                {
                    for (int g = 1; g < 14; g++)
                    {
                        for (int M = 0; M < Game1.all.Count(); M++)
                        {
                            if (Game1.all[M].X == PreviousWBox.X - (55 * g)
                                 && Game1.all[M].Y == PreviousWBox.Y)
                            {
                                for (int L = 1; L < g; L++)
                                {
                                    if (WhiteBox[u].X == PreviousWBox.X - (55 * (g - L))
                                         && WhiteBox[u].Y == PreviousWBox.Y)
                                    {
                                        return true;

                                    }
                                }
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else if (WhiteBox[u].X >= PreviousWBox.Right && WhiteBox[u].Y == PreviousWBox.Y)
                {
                    for (int g = 1; g < 14; g++)
                    {
                        for (int M = 0; M < Game1.all.Count(); M++)
                        {
                            if (Game1.all[M].X == PreviousWBox.X + (55 * g)
                                 && Game1.all[M].Y == PreviousWBox.Y)
                            {
                                for (int L = 1; L < g; L++)
                                {
                                    if (WhiteBox[u].X == PreviousWBox.X + (55 * (g - L))
                                         && WhiteBox[u].Y == PreviousWBox.Y)
                                    {
                                        return true;

                                    }
                                }
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else if (WhiteBox[u].Y < PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X)
                {
                    for (int g = 1; g < 14; g++)
                    {
                        for (int M = 0; M < Game1.all.Count(); M++)
                        {
                            if (Game1.all[M].X == PreviousWBox.X
                                 && Game1.all[M].Y == PreviousWBox.Y - (55 * g))
                            {
                                for (int L = 1; L < g; L++)
                                {
                                    if (WhiteBox[u].X == PreviousWBox.X
                                         && WhiteBox[u].Y == PreviousWBox.Y - (55 * (g - L)))
                                    {
                                        return true;

                                    }
                                }
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else if (WhiteBox[u].Y >= PreviousWBox.Bottom && WhiteBox[u].X == PreviousWBox.X)
                {
                    for (int g = 1; g < 14; g++)
                    {
                        for (int M = 0; M < Game1.all.Count(); M++)
                        {
                            if (Game1.all[M].X == PreviousWBox.X
                                 && Game1.all[M].Y == PreviousWBox.Y + (55 * g))
                            {
                                for (int L = 1; L < g; L++)
                                {
                                    if (WhiteBox[u].X == PreviousWBox.X
                                         && WhiteBox[u].Y == PreviousWBox.Y + (55 * (g - L)))
                                    {
                                        return true;
                                    }
                                }
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public bool KillsPawn(int u, int o)
        {


            if (_currentMouseState.LeftButton == ButtonState.Released

                && WhiteBox[u].Contains(mousePoint)
                && WhiteBox[u].Intersects(Pawn1Box)
                && selectingPrevious == false
                && !move_willCheck(u, 0)
                )
            {
                for (int kk = 0; kk < 8; kk++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if ((PawnN[kk].Pawn1Box.Contains(mousePoint) && PawnN[kk].player != this.player)
                    || (PawnE[kk].Pawn1Box.Contains(mousePoint) && PawnE[kk].player != this.player)
                    || (PawnS[kk].Pawn1Box.Contains(mousePoint) && PawnS[kk].player != this.player)
                    || (PawnW[kk].Pawn1Box.Contains(mousePoint) && PawnW[kk].player != this.player)
                    || (Rook[kk].Pawn1Box.Contains(mousePoint) && Rook[kk].player != this.player)
                    || (Bishop[kk].Pawn1Box.Contains(mousePoint) && Bishop[kk].player != this.player)
                    || (Knight[kk].Pawn1Box.Contains(mousePoint) && Knight[kk].player != this.player)
                    || (Queen[z].Pawn1Box.Contains(mousePoint) && Queen[z].player != this.player)
                    || (King[z].Pawn1Box.Contains(mousePoint) && King[z].player != this.player))
                        {


                            foreach (Rectangle rect in Game1.all)
                            {

                                if (RookNContains() == null)
                                {
                                    if (WhiteBox[u].X > PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)
                                    {
                                        if (rect.Y == WhiteBox[u].Y)
                                        {
                                            ListWE(u);
                                            if (PreviousWBox.X == dx_lst[0].x)
                                            {
                                                tst = true;
                                            }
                                        }
                                        if (tst && v == Game1.all.Count() && WhiteBox[u].X == dx_lst[0].x)
                                        {
                                            return true;
                                        }
                                        if (o == 63 && tst == false)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }

                                    else if (WhiteBox[u].X < PreviousWBox.X && WhiteBox[u].Y == PreviousWBox.Y)
                                    {
                                        if (rect.Y == WhiteBox[u].Y)
                                        {
                                            ListEW(u);
                                            if (PreviousWBox.X == dx_lst[0].x)
                                            {
                                                tst = true;
                                            }
                                        }
                                        if (tst && v == Game1.all.Count() && WhiteBox[u].X == dx_lst[0].x)
                                        {
                                            return true;
                                        }
                                        if (o == 63 && tst == false)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else if (WhiteBox[u].Y < PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X)
                                    {
                                        if (rect.X == WhiteBox[u].X)
                                        {
                                            ListSN(u);
                                            if (PreviousWBox.Y > dx_lst[0].x)
                                            {
                                                tst = true;
                                            }
                                        }
                                        if (tst && v == Game1.all.Count() && WhiteBox[u].Y == dx_lst[0].x)
                                        {
                                            return true;
                                        }
                                        if (o == 63 && tst == false)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else if (WhiteBox[u].Y > PreviousWBox.Y && WhiteBox[u].X == PreviousWBox.X)
                                    {
                                        if (rect.X == WhiteBox[u].X)
                                        {
                                            ListNS(u);

                                            if (PreviousWBox.Y < dx_lst[0].x)
                                            {
                                                tst = true;
                                            }

                                        }
                                        if (tst && v == Game1.all.Count() && WhiteBox[u].Y == dx_lst[0].x)
                                        {
                                            return true;
                                        }
                                        if (o == 63 && tst == false)
                                        {
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                                tst = false;
                                return false;
                            }
                        }
                    }
                }
            }
            return false;

        }

        //public bool canKill(int u)
        //{
        //    if (_currentMouseState.LeftButton == ButtonState.Released
        //        && WhiteBox[u].Contains(mousePoint)
        //        && WhiteBox[u].Intersects(Pawn1Box)
        //        && selectingPrevious == false
        //        && ((WhiteBox[u].Right <= PreviousWBox.Left)
        //        || (WhiteBox[u].Left >= PreviousWBox.Right))
        //        && WhiteBox[u].X != PreviousWBox.X
        //        && WhiteBox[u].Y == PreviousWBox.Top + 55
        //        && ((PawnE[getIND].Pawn1Box.Contains(mousePoint))
        //        || (PawnS[getIND].Pawn1Box.Contains(mousePoint))
        //        || (PawnW[getIND].Pawn1Box.Contains(mousePoint)))
        //        )
        //    {

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void Kill(int u, int i)
        //{
        //    Moved1 = true;
        //    thisX = WhiteBox[u].X;
        //    thisY = WhiteBox[u].Y;
        //    k = 0;

        //    sort = "killing";

        //    reset = false;
        //    lastMove = false;

        //    Grab = false;
        //    j = 191;

        //}
    }
}



