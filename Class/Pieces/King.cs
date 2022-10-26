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

    public class King : Base
    {
        public King(Texture2D Pawn1Sprite, int x, int y, int Player, Color tint)
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
                 || (WhoamI == playerList4 && Moved4 == false && playerTurn == 4 && player == 4))
                )
            {
                //if (Testing == 0)
                //{
                //    if (!checkcheck(player))
                //    {
                //        checkMate_nextTurn();
                //    }
                //    Testing = 1;
                //}
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

                        if (MovePawn(u))
                        {

                            MovePawnLocation(u);
                            tst = false;
                            resetDX();
                        }
                    }
                }
            }
         
            v = 0;
        }



        public override bool MovePawn(int u)
        {
            if (_currentMouseState.LeftButton == ButtonState.Released

                && WhiteBox[u].Contains(mousePoint)
                && WhiteBox[u].Intersects(Pawn1Box)
                && selectingPrevious == false 
                && !move_willCheck(u, player))
            {
                for (int kj = 0; kj < 64; kj++)
                {

                    if ((Pawn1Box.Left > PreviousWBox.Left - 55 && Pawn1Box.Left < PreviousWBox.Right + 55)
                         && (WhiteBox[u].Y >= PreviousWBox.Y - 55 && WhiteBox[u].Y < PreviousWBox.Bottom + 55)
                         && (WhiteBox[u].X >= PreviousWBox.Left - 55 && WhiteBox[u].X <= PreviousWBox.Right + 55)
                         && !PawnContains())
                    {
                        return true;
                    }
                    else if ((Pawn1Box.Left > PreviousWBox.Left - 55 && Pawn1Box.Left < PreviousWBox.Right + 55)
                         && (WhiteBox[u].Y >= PreviousWBox.Y - 55 && WhiteBox[u].Y < PreviousWBox.Bottom + 55)
                         && (WhiteBox[u].X >= PreviousWBox.Left - 55 && WhiteBox[u].X <= PreviousWBox.Right + 55)
                         && Game1.all[kj].Contains(mousePoint) && Game1.allP[kj] != this.player)
                    {
                        killThings();

                        return true;
                    }
                }
            }
            return false;
        }
    }
}


