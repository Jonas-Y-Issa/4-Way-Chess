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

    public class Knight : Base
    {
        public bool go;
        public Knight(Texture2D Pawn1Sprite, int x, int y, int Player, Color tint)
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
                    for (int g = 0; g < 8; g++)
                    {
                        for (int e = 1; e < 3; e++)
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
                                    
                                        MovePawnLocation(u);
                                        //BishopcanKill();
                                        killThings();
                                    //else if (canKill(u))
                                    //{
                                    //    Kill(u, getIND);
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }
        

        public override bool MovePawn(int u)
        {

            if (_currentMouseState.LeftButton == ButtonState.Released
                && WhiteBox[u].Contains(mousePoint)
                && WhiteBox[u].Intersects(Pawn1Box)
                && selectingPrevious == false

                &&

                (
                (WhiteBox[u].X == PreviousWBox.X + 110
                && WhiteBox[u].Y == PreviousWBox.Y + 55)

                 ||

                 (WhiteBox[u].X == PreviousWBox.X - 110
                && WhiteBox[u].Y == PreviousWBox.Y - 55)

                 ||

                 (WhiteBox[u].X == PreviousWBox.X - 55
                && WhiteBox[u].Y == PreviousWBox.Y + 110)

                 ||

                 (WhiteBox[u].X == PreviousWBox.X + 55
                && WhiteBox[u].Y == PreviousWBox.Y - 110)

               ||

                 (WhiteBox[u].X == PreviousWBox.X + 55
                && WhiteBox[u].Y == PreviousWBox.Y + 110)

                 ||

                 (WhiteBox[u].X == PreviousWBox.X - 55
                && WhiteBox[u].Y == PreviousWBox.Y - 110)

                 ||

                 (WhiteBox[u].X == PreviousWBox.X - 110
                && WhiteBox[u].Y == PreviousWBox.Y + 55)

                 ||

                 (WhiteBox[u].X == PreviousWBox.X + 110
                && WhiteBox[u].Y == PreviousWBox.Y - 55)
                )
                 && !move_willCheck(u, 0))
            {   
                for (int o = 0; o < 4; o++)
                {
                    if ((King[o].Pawn1Box.Contains(mousePoint) && King[o].player == this.player)
                            || (Queen[o].Pawn1Box.Contains(mousePoint) && Queen[o].player == this.player))
                    {
                        go = true;
                    }
                }
                for (int o = 0; o < 8; o++)
                {
                    if ((PawnN[o].Pawn1Box.Contains(mousePoint) && PawnN[o].player == this.player)
                        || (PawnE[o].Pawn1Box.Contains(mousePoint) && PawnE[o].player == this.player)
                        || (PawnS[o].Pawn1Box.Contains(mousePoint) && PawnS[o].player == this.player)
                        || (PawnW[o].Pawn1Box.Contains(mousePoint) && PawnW[o].player == this.player)
                        || (Knight[o].Pawn1Box.Contains(mousePoint) && Knight[o].player == this.player)
                        || (Bishop[o].Pawn1Box.Contains(mousePoint) && Bishop[o].player == this.player)
                        || (Rook[o].Pawn1Box.Contains(mousePoint) && Rook[o].player == this.player))
                    {
                        go = true;
                    }
              
                    if (go)
                    {
                        go = false;
                        return false;
                    }
                }
                
                return true;

            }
            else
            {
                return false;
            }
        }

    }
}



