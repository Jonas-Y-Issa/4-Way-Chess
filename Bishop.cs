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

    public class Bishop : Base
    {

        public Bishop(Texture2D Pawn1Sprite, int x, int y, int Player, Color tint)
        {
            this.PawnSprite = Pawn1Sprite;
            this.xx = x;
            this.yy = y;
            this.player = Player;
            //this.Clr = tint;
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

                if (Pawn1Box.Contains(mousePoint2) && Grab == false || k == i)
                {
                    for (int u = 0; u < 160; u++)
                    {
                        if (_currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (WhiteBox[u].Contains(mousePoint) && Pawn1Box.Contains(mousePoint) && lastMove == false)
                            {
                                CreatePreviousBox(u);
                            }
                        }
                        for (int e = 0; e < 10; e++)
                        {
                            if (!PreviousWBox.Contains(this.Pawn1Box))
                            {
                                if (MovePawn(u, e))
                                {
                                    MovePawnLocation(u);
                                    tst = false;
                                    resetDX();
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool MovePawn(int u, int e)
        {

            if (_currentMouseState.LeftButton == ButtonState.Released
                && _previousMouseState.LeftButton == ButtonState.Pressed
                && WhiteBox[u].Contains(mousePoint)
                && WhiteBox[u].Intersects(Pawn1Box)
                && selectingPrevious == false
                 && !move_willCheck(u, 0))
            {
                    if (WhiteBox[u].X == PreviousWBox.X + (55 * e)
                                   && WhiteBox[u].Y == PreviousWBox.Y + (55 * e))
                    {
                        for (int o = 0; o < 64; o++)
                        {
                            for (int p = 1; p < 13; p++)
                            {

                                if (Game1.all[o].X == PreviousWBox.X + (55 * p)
                                    && Game1.all[o].Y == PreviousWBox.Y + (55 * p))
                                {
                                    ListSE(o);

                                    if (PreviousWBox.Y < dx_lst[0].x)
                                    {
                                        tst = true;
                                    }

                                }
                                if (tst && o == 63 && WhiteBox[u].Y < dx_lst[0].x)
                                {

                                    return true;

                                }
                                for (int kj = 0; kj < 64; kj++)
                                {
                                    if (Game1.all[kj].Contains(mousePoint) && Game1.allP[kj] != this.player)
                                    {
                                        if (tst && o == 63 && WhiteBox[u].Y == dx_lst[0].x)
                                        {

                                            BishopcanKill();
                                            return true;
                                        }
                                    }
                                }
                                if (o == 63 && tst == false)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    else if (WhiteBox[u].X == PreviousWBox.X - (55 * e)
                            && WhiteBox[u].Y == PreviousWBox.Y + (55 * e))
                    {
                        for (int o = 0; o < 64; o++)
                        {
                            for (int p = 1; p < 13; p++)
                            {

                                if (Game1.all[o].X == PreviousWBox.X - (55 * p)
                                    && Game1.all[o].Y == PreviousWBox.Y + (55 * p))
                                {
                                    ListSW(o);

                                    if (PreviousWBox.Y < dx_lst[0].x)
                                    {
                                        tst = true;
                                    }
                                }
                                if (tst && o == 63 && WhiteBox[u].Y < dx_lst[0].x)
                                {
                                    return true;
                                }
                                for (int kj = 0; kj < 64; kj++)
                                {
                                    if (Game1.all[kj].Contains(mousePoint) && Game1.allP[kj] != this.player)
                                    {
                                        if (tst && o == 63 && WhiteBox[u].Y == dx_lst[0].x)
                                        {

                                            BishopcanKill();
                                            return true;
                                        }
                                    }
                                }
                                if (o == 63 && tst == false)
                                {
                                    return true;
                                }
                            }
                        }
                    }

                    else if (WhiteBox[u].X == PreviousWBox.X - (55 * e)
                       && WhiteBox[u].Y == PreviousWBox.Y - (55 * e))
                    {
                        for (int o = 0; o < 64; o++)
                        {
                            for (int p = 1; p < 13; p++)
                            {

                                if (Game1.all[o].X == PreviousWBox.X - (55 * p)
                                    && Game1.all[o].Y == PreviousWBox.Y - (55 * p))
                                {
                                    ListNW(o);

                                    if (PreviousWBox.Y > dx_lst[0].x)
                                    {
                                        tst = true;
                                    }

                                }
                                if (tst && o == 63 && WhiteBox[u].Y > dx_lst[0].x)
                                {
                                    return true;
                                }
                                for (int kj = 0; kj < 64; kj++)
                                {
                                    if (Game1.all[kj].Contains(mousePoint) && Game1.allP[kj] != this.player)
                                    {
                                        if (tst && o == 63 && WhiteBox[u].Y == dx_lst[0].x)
                                        {

                                            BishopcanKill();
                                            return true;
                                        }
                                    }
                                }
                                if (o == 63 && tst == false)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    else if (WhiteBox[u].X == PreviousWBox.X + (55 * e)
                                  && WhiteBox[u].Y == PreviousWBox.Y - (55 * e))
                    {
                        for (int o = 0; o < 64; o++)
                        {
                            for (int p = 1; p < 13; p++)
                            {

                                if (Game1.all[o].X == PreviousWBox.X + (55 * p)
                                    && Game1.all[o].Y == PreviousWBox.Y - (55 * p))
                                {
                                    ListNE(o);

                                    if (PreviousWBox.Y > dx_lst[0].x)
                                    {
                                        tst = true;
                                    }

                                }
                                if (tst && o == 63 && WhiteBox[u].Y > dx_lst[0].x)
                                {
                                    return true;
                                }
                                for (int kj = 0; kj < 64; kj++)
                                {
                                    if (Game1.all[kj].Contains(mousePoint) && Game1.allP[kj] != this.player)
                                    {
                                        if (tst && o == 63 && WhiteBox[u].Y == dx_lst[0].x)
                                        {

                                            BishopcanKill();
                                            return true;
                                        }
                                    }
                                }
                                if (o == 63 && tst == false)
                                {
                                    return true;
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