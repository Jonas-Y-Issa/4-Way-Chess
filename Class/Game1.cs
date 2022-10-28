using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Net;
using System.ComponentModel;
using Class.GameSetup;

namespace _4_Way_Chess
{
    public class Game1 : Game
    {

        #region Display

        GraphicsDeviceManager graphics;
        public static int displayWidth;
        public static int displayHeight;

        #endregion

        #region Menu Declarations

        SpriteBatch spriteBatch;

        StartMenu Menu;
        MenuBase MOptions = new MenuBase("Main Menu");
        MenuBase Priv = new MenuBase("Private");
        MenuBase LN = new MenuBase("LAN");
        MenuBase Opt = new MenuBase("Options");
        MenuBase Cred = new MenuBase("Credits");
        MenuBase Qu = new MenuBase("Queue");
        public static MenuBase Active;
        MenuBase PreviousActive;


        public enum MenuState
        {
            Menu,
        };

        public static MenuState menuEnum;

        #endregion

        #region Board Declarations

        LoadScreen loadScrn;
        Board board;
        Settings inGame_settings;
        ScoreBoard inGame_scoreboard;
        Base baseP;

        public static MyCollection<P1> PawnN = new MyCollection<P1>();
        public static MyCollection<P2> PawnE = new MyCollection<P2>();
        public static MyCollection<P3> PawnS = new MyCollection<P3>();
        public static MyCollection<P4> PawnW = new MyCollection<P4>();
        public static MyCollection<Rook> RookN = new MyCollection<Rook>();
        public static MyCollection<Knight> KnightN = new MyCollection<Knight>();
        public static MyCollection<Bishop> BishopN = new MyCollection<Bishop>();
        public static MyCollection<Queen> QueenN = new MyCollection<Queen>();
        public static MyCollection<King> KingN = new MyCollection<King>();



        public static int getIND;
        public static string getPawn;
        public static Rectangle[] AllPawns = new Rectangle[40];
        public static bool Occupied;


        Texture2D PawnNSprite;
        Texture2D PawnESprite;
        Texture2D PawnSSprite;
        Texture2D PawnWSprite;
        Texture2D HorseNSprite;
        Texture2D RookNSprite;
        Texture2D BishopNSprite;
        Texture2D QueenNSprite;
        Texture2D KingNSprite;
        Texture2D Wallpaper;

        public static int LockTime;
        public static bool Grab;

        public static Rectangle[] all = new Rectangle[64];
        public static int[] allP = new int[64];

        Rectangle[] WhiteBox
        {
            get { return Board.WhiteBox; }
        }

        #endregion

        #region InputDevices

        public static MouseState _currentMouseState1;
        public static MouseState _previousMouseState1;
        Texture2D cursorHand;
        KeyboardState keyState;
        public static Point mousePoint;
        public static Vector2 MousePosition
        {
            get { return Resolution.MousePosition; }
        }
        #endregion

        #region ServerId's

        string serverIP = "14.202.157.211";

        public static long whoiam;
        public static long playerList1;
        public static long playerList2;
        public static long playerList3;
        public static long playerList4;

        #endregion

        #region SendPack

        public static string sort;

        string pType;
        int pNum;
        int pNuM;
        int Ind;
        int yyy;
        int xxx;
        public static bool moveIm;
        public static int playerTurn = 1;
        public static bool Moved1;
        public static bool Moved2;
        public static bool Moved3;
        public static bool Moved4;

        #endregion

        #region PlayerName Display

        Vector2 playerNamePosition;
        string[] playerName = new string[4];

        #endregion


        public static bool Entered;
        public static Process exeProcess;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";
            Resolution.SetVirtualResolution(800, 450);
            Resolution.SetResolution(800, 450, false);

            Window.AllowUserResizing = true;
            Window.Title = "4-Way Chess";

            //graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            #region Display Setup
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Resolution.vp.X = 0;
            Resolution.vp.Y = 0;
            cursorHand = Content.Load<Texture2D>("cursor");
            #endregion

            #region Menu

            Menu = new StartMenu(Content);
            Priv = new MenuBase(Content, "Online", new MenuBase[] {}, new string[] { "Host", "Join", "", "Back" });
            Opt = new MenuBase(Content, "Options", new MenuBase[] {}, new string[] { "-", "-", "-", "Back" });
            LN = new MenuBase(Content, "LAN", new MenuBase[] {}, new string[] { "Host", "Join", "", "Back" });
            MOptions = new MenuBase(Content, "Main Menu", new MenuBase[] { Priv, LN, Opt }, new string[] { "", "Quit" });
            Active = MOptions;

            Qu = new MenuBase(Content, "Que", new MenuBase[] { }, new string[] { });

            Menu.LoadContent();
            MOptions.LoadContent();
            Priv.LoadContent();
            Opt.LoadContent();
            LN.LoadContent();
            Qu.LoadContent();

            #endregion

            #region Board

            board = new Board(Content);
            loadScrn = new LoadScreen(Content);
            inGame_settings = new Settings(Content);
            inGame_scoreboard = new ScoreBoard(Content);

            inGame_scoreboard.LoadContent();
            inGame_settings.LoadContent();

            #endregion

            #region Load Pieces

            baseP = new Base();
            baseP.LoadContent();

            PawnNSprite = Content.Load<Texture2D>("PawnN");
            PawnESprite = Content.Load<Texture2D>("PawnE");
            PawnSSprite = Content.Load<Texture2D>("PawnS");
            PawnWSprite = Content.Load<Texture2D>("PawnW");
            HorseNSprite = Content.Load<Texture2D>("Knight");
            RookNSprite = Content.Load<Texture2D>("Rook");
            BishopNSprite = Content.Load<Texture2D>("Bishop");
            QueenNSprite = Content.Load<Texture2D>("Queen");
            KingNSprite = Content.Load<Texture2D>("King");
            Wallpaper = Content.Load<Texture2D>("background");
            #region N Set
            PawnN.Add(new P1(PawnNSprite, WhiteBox[8].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[9].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[10].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[11].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[12].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[13].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[14].X, WhiteBox[9].Y, 1, Color.Green));
            PawnN.Add(new P1(PawnNSprite, WhiteBox[15].X, WhiteBox[9].Y, 1, Color.Green));

            RookN.Add(new Rook(RookNSprite, WhiteBox[0].X, WhiteBox[0].Y, 1, Color.Green));
            RookN.Add(new Rook(RookNSprite, WhiteBox[7].X, WhiteBox[0].Y, 1, Color.Green));

            KnightN.Add(new Knight(HorseNSprite, WhiteBox[1].X, WhiteBox[0].Y, 1, Color.Green));
            KnightN.Add(new Knight(HorseNSprite, WhiteBox[6].X, WhiteBox[0].Y, 1, Color.Green));

            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[2].X, WhiteBox[0].Y, 1, Color.Green));
            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[5].X, WhiteBox[0].Y, 1, Color.Green));

            QueenN.Add(new Queen(QueenNSprite, WhiteBox[3].X, WhiteBox[0].Y, 1, Color.Green));
            KingN.Add(new King(KingNSprite, WhiteBox[4].X, WhiteBox[0].Y, 1, Color.Green));

            #endregion

            #region E Set

            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[25].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[39].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[53].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[67].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[81].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[95].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[109].Y, 2, new Color(0, 138, 255)));
            PawnE.Add(new P2(PawnNSprite, WhiteBox[36].X, WhiteBox[123].Y, 2, new Color(0, 138, 255)));

            RookN.Add(new Rook(RookNSprite, WhiteBox[37].X, WhiteBox[25].Y, 2, new Color(0, 138, 255)));
            RookN.Add(new Rook(RookNSprite, WhiteBox[37].X, WhiteBox[123].Y, 2, new Color(0, 138, 255)));

            KnightN.Add(new Knight(HorseNSprite, WhiteBox[37].X, WhiteBox[39].Y, 2, new Color(0, 138, 255)));
            KnightN.Add(new Knight(HorseNSprite, WhiteBox[37].X, WhiteBox[109].Y, 2, new Color(0, 138, 255)));


            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[37].X, WhiteBox[53].Y, 2, new Color(0, 138, 255)));
            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[37].X, WhiteBox[95].Y, 2, new Color(0, 138, 255)));

            QueenN.Add(new Queen(QueenNSprite, WhiteBox[37].X, WhiteBox[81].Y, 2, new Color(0, 138, 255)));
            KingN.Add(new King(KingNSprite, WhiteBox[37].X, WhiteBox[67].Y, 2, new Color(0, 138, 255)));

            #endregion

            #region S Set

            PawnS.Add(new P3(PawnNSprite, WhiteBox[136].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[137].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[138].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[139].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[140].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[141].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[142].X, WhiteBox[149].Y, 3, Color.Red));
            PawnS.Add(new P3(PawnNSprite, WhiteBox[143].X, WhiteBox[149].Y, 3, Color.Red));

            RookN.Add(new Rook(RookNSprite, WhiteBox[144].X, WhiteBox[155].Y, 3, Color.Red));
            RookN.Add(new Rook(RookNSprite, WhiteBox[151].X, WhiteBox[155].Y, 3, Color.Red));

            KnightN.Add(new Knight(HorseNSprite, WhiteBox[145].X, WhiteBox[155].Y, 3, Color.Red));
            KnightN.Add(new Knight(HorseNSprite, WhiteBox[150].X, WhiteBox[155].Y, 3, Color.Red));


            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[146].X, WhiteBox[155].Y, 3, Color.Red));
            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[149].X, WhiteBox[155].Y, 3, Color.Red));

            QueenN.Add(new Queen(QueenNSprite, WhiteBox[147].X, WhiteBox[155].Y, 3, Color.Red));
            KingN.Add(new King(KingNSprite, WhiteBox[148].X, WhiteBox[155].Y, 3, Color.Red));

            #endregion

            #region W Set

            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[25].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[39].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[53].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[67].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[81].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[95].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[109].Y, 4, Color.Orange));
            PawnW.Add(new P4(PawnNSprite, WhiteBox[25].X, WhiteBox[123].Y, 4, Color.Orange));

            RookN.Add(new Rook(RookNSprite, WhiteBox[24].X, WhiteBox[24].Y, 4, Color.Orange));
            RookN.Add(new Rook(RookNSprite, WhiteBox[24].X, WhiteBox[122].Y, 4, Color.Orange));

            KnightN.Add(new Knight(HorseNSprite, WhiteBox[24].X, WhiteBox[38].Y, 4, Color.Orange));
            KnightN.Add(new Knight(HorseNSprite, WhiteBox[24].X, WhiteBox[108].Y, 4, Color.Orange));

            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[24].X, WhiteBox[52].Y, 4, Color.Orange));
            BishopN.Add(new Bishop(BishopNSprite, WhiteBox[24].X, WhiteBox[94].Y, 4, Color.Orange));

            QueenN.Add(new Queen(QueenNSprite, WhiteBox[24].X, WhiteBox[66].Y, 4, Color.Orange));
            KingN.Add(new King(KingNSprite, WhiteBox[24].X, WhiteBox[80].Y, 4, Color.Orange));


            #endregion

            #endregion

        }

        protected override void Update(GameTime gameTime)
        {
            #region Input
            _currentMouseState1 = Mouse.GetState();
            keyState = Keyboard.GetState();
            mousePoint = new Point((int)MousePosition.X, (int)MousePosition.Y);

            if (_currentMouseState1.LeftButton == ButtonState.Pressed)
            {
                cursorHand = Content.Load<Texture2D>("Hand");
            }
            else
            {
                cursorHand = Content.Load<Texture2D>("cursor");

            }
            #endregion

            #region Display

            if (Window.ClientBounds.Width < 800)
            {
                Resolution.SetVirtualResolution(800, Window.ClientBounds.Height);
                Resolution.SetResolution(800, Window.ClientBounds.Height, false);
            }
            if (Window.ClientBounds.Height < 600)
            {
                Resolution.SetVirtualResolution(Window.ClientBounds.Width, 600);
                Resolution.SetResolution(Window.ClientBounds.Width, 600, false);
            }
            else if (Window.ClientBounds.Height > Window.ClientBounds.Width - 200)
            {
                Resolution.SetVirtualResolution(Window.ClientBounds.Width, Window.ClientBounds.Width - 200);
                Resolution.SetResolution(Window.ClientBounds.Width, Window.ClientBounds.Width - 200, false);
            }
            else
            {
                Resolution.SetVirtualResolution(Window.ClientBounds.Width, Window.ClientBounds.Height);
                Resolution.SetResolution(Window.ClientBounds.Width, Window.ClientBounds.Height, false);
            }

            displayWidth = Window.ClientBounds.Width;
            displayHeight = Window.ClientBounds.Height;

            Resolution.thisT = true;

            #endregion

            if (Active.title != "Game" || Active.title != "Loading")
            {
                Menu.Update(gameTime);
            }
            switch (menuEnum)
            {
                case MenuState.Menu:
                    if (Active.hIndex > -1) {
                        if (Click()
                            && Active.menuOpt[Active.hIndex].Contains(mousePoint)
                            && Active.hIndex < Active.Menus.Length)
                        {

                            PreviousActive = Active;
                            Active = MOptions.Menus[MOptions.hIndex];
                        }
                    }
                    Active.Update(gameTime);

                    break;
            }

            #region Pawns

            for (int o = 0; o < 8; o++)
            {
                all[o] = PawnN[o].Pawn1Box;
                all[(o + 8)] = PawnE[o].Pawn1Box;
                all[(o + 16)] = PawnS[o].Pawn1Box;
                all[(o + 24)] = PawnW[o].Pawn1Box;

                all[(o + 32)] = RookN[o].Pawn1Box;
                all[(o + 40)] = KnightN[o].Pawn1Box;
                all[(o + 48)] = BishopN[o].Pawn1Box;
            }
            for (int o = 0; o < 4; o++)
            {
                all[(o + 56)] = QueenN[o].Pawn1Box;
                all[(o + 60)] = KingN[o].Pawn1Box;

            }
            for (int o = 0; o < 8; o++)
            {
                allP[o] = PawnN[o].player;
                allP[(o + 8)] = PawnE[o].player;
                allP[(o + 16)] = PawnS[o].player;
                allP[(o + 24)] = PawnW[o].player;

                allP[(o + 32)] = RookN[o].player;
                allP[(o + 40)] = KnightN[o].player;
                allP[(o + 48)] = BishopN[o].player;
            }
            for (int o = 0; o < 4; o++)
            {
                allP[(o + 56)] = QueenN[o].player;
                allP[(o + 60)] = KingN[o].player;

            }

            #endregion

            #region Case

            /*        
             *        
                        switch (menuEnum)
                        {
                            case MenuState.Name:

                                txtBox.Update(gameTime);
                                break;
                            case MenuState.Loading:

                                loadScrn.Update(gameTime);
                                break;
                            case MenuState.Game:

                                board.Update(gameTime);
                                UpdatePawns(gameTime);
                                inGame_settings.Update(gameTime);
                                inGame_scoreboard.Update(gameTime);
                                break;

                            case MenuState.HostPrivate:

                                Qu.Update(gameTime);


                                if (Click() && Qu.menuOpt[0].Contains(Game1.mousePoint))
                                {
                                }
                                if (Click() && Qu.menuOpt[1].Contains(Game1.mousePoint))
                                {
                                    menuEnum = MenuState.Loading;
                                }
                                break;
                            case MenuState.JoinPrivate:

                                Qu.Update(gameTime);



                                if (Click() && Qu.menuOpt[0].Contains(Game1.mousePoint))
                                {
                                }
                                break;
                        }
            */
            #endregion

            #region Player
            if (playerList1 == null)
            {
                playerName[0] = "";

            }

            if (playerTurn != 1)
            {
                Moved1 = false;
            }
            if (playerTurn != 2)
            {
                Moved2 = false;
            }
            if (playerTurn != 3)
            {
                Moved3 = false;
            }
            if (playerTurn != 4)
            {
                Moved4 = false;
            }

            #endregion

            #region Input
            if (keyState.IsKeyDown(Keys.Escape) 
                || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                ||(Click() && Active.hIndex > -1
                && Active.menuOpt[Active.hIndex].Contains(mousePoint)))
            {
                if (Active.text[Active.hIndex] == "Back")
                {
                    Active = PreviousActive;
                }
                if (Active.text[Active.hIndex] == "Quit")
                {
                    this.Exit();
                }
            }

            MouseButtonReset();
            _previousMouseState1 = _currentMouseState1;
            #endregion


            Resolution.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            //Resolution.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                Resolution.getTransformationMatrix());

            Menu.Draw(spriteBatch);
            Active.Draw(spriteBatch);


            /*
            if (menuEnum != MenuState.Game && menuEnum != MenuState.Loading)
            {
                graphics.GraphicsDevice.Clear(BgColor);
                Menu.Draw(spriteBatch);

                switch (menuEnum)
                {
                    case MenuState.Menu:

                        MOptions.Draw(spriteBatch);
                        break;
                    case MenuState.Lan:

                        LN.Draw(spriteBatch);
                        break;
                    case MenuState.Credits:

                        Cred.Draw(spriteBatch);
                        break;

                    case MenuState.Options:

                        Opt.Draw(spriteBatch);
                        break;
                    case MenuState.HostPrivate:

                        Qu.Draw(spriteBatch);

                        for (int i = 0; i < 2; i++)
                        {
                            if (playerName[i] != null)
                            {
                                spriteBatch.DrawString(font, playerName[i], new Vector2(((displayWidth * Resolution.ratio) / 2) - ((int)font.MeasureString(playerName[i]).X / 2), 300 + (i * 180)), Color.White);

                            }
                        }
                        break;
                    case MenuState.JoinPrivate:

                        Qu.Draw(spriteBatch);

                        for (int i = 0; i < 2; i++)
                        {
                            if (playerName[i] != null)
                            {
                                spriteBatch.DrawString(font, playerName[i], new Vector2(((displayWidth * Resolution.ratio) / 2) - ((int)font.MeasureString(playerName[i]).X / 2), 300 + (i * 180)), Color.White);

                            }
                        }
                        break;
                }
            }
            switch (menuEnum)
            {
                case MenuState.Loading:

                    graphics.GraphicsDevice.Clear(Color.Black);
                    loadScrn.Draw(spriteBatch);
                    break;
                case MenuState.Game:

                    graphics.GraphicsDevice.Clear(Color.Black);

                    //spriteBatch.DrawString(font, playerName, playerNamePosition, Color.White);
                    spriteBatch.Draw(Wallpaper, new Rectangle((int)(displayWidth * Resolution.ratio) / 2 - ((int)(Wallpaper.Width * 2.5) / 2), (int)(displayHeight * Resolution.ratio) / 2 - ((int)(Wallpaper.Height * 2.5) / 2), (int)(Wallpaper.Width * 2.5), (int)(Wallpaper.Height * 2.5)), Color.White);
                    board.Draw(spriteBatch);

                    foreach (P1 pn in PawnN)
                    {
                        pn.Draw(spriteBatch);
                    }
                    foreach (P2 pe in PawnE)
                    {
                        pe.Draw(spriteBatch);
                    }
                    foreach (P3 ps in PawnS)
                    {
                        ps.Draw(spriteBatch);
                    }
                    foreach (P4 pw in PawnW)
                    {
                        pw.Draw(spriteBatch);
                    }
                    foreach (Rook rk in RookN)
                    {
                        rk.Draw(spriteBatch);
                    }
                    foreach (Knight kn in KnightN)
                    {
                        kn.Draw(spriteBatch);
                    }
                    foreach (Bishop bp in BishopN)
                    {
                        bp.Draw(spriteBatch);
                    }
                    foreach (Queen qn in QueenN)
                    {
                        qn.Draw(spriteBatch);
                    }
                    foreach (King kg in KingN)
                    {
                        kg.Draw(spriteBatch);
                    }

                    break;
            }

            */
            if (IsMouseInsideWindow())
            {
                spriteBatch.Draw(cursorHand, new Vector2(Resolution.MousePosition.X, Resolution.MousePosition.Y), Color.White);
            }

            spriteBatch.End();

            spriteBatch.Begin();
            /*
            if (menuEnum == MenuState.Game)
            {
                inGame_settings.Draw(spriteBatch);
                inGame_scoreboard.Draw(spriteBatch);
            }
            */
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
      Resolution.getTransformationMatrix());


            if (IsMouseInsideWindow())
            {
                spriteBatch.Draw(cursorHand, new Vector2(Resolution.MousePosition.X, Resolution.MousePosition.Y), Color.White);
            }

            //if (playerName[0] != null)
            //{
            //    spriteBatch.DrawString(font, playerName[0], new Vector2(((displayWidth * Resolution.ratio) / 2), 480), Color.White);
            //}
            spriteBatch.End();


            base.Draw(gameTime);
        }

        #region Methods

        public void ECS(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }

        void UpdatePawns(GameTime gameTime)
        {
            #region Update Pawns



            foreach (P1 pn in PawnN)
            {
                pn.Update(gameTime);
            }
            foreach (P2 pe in PawnE)
            {
                pe.Update(gameTime);
            }
            foreach (P3 ps in PawnS)
            {
                ps.Update(gameTime);
            }
            foreach (P4 pw in PawnW)
            {
                pw.Update(gameTime);
            }
            foreach (Rook rk in RookN)
            {
                rk.Update(gameTime);
            }
            foreach (Knight kn in KnightN)
            {
                kn.Update(gameTime);
            }
            foreach (Bishop bp in BishopN)
            {
                bp.Update(gameTime);
            }
            foreach (Queen qn in QueenN)
            {
                qn.Update(gameTime);
            }
            foreach (King kg in KingN)
            {
                kg.Update(gameTime);
            }
            foreach (P1 pn in PawnN)
            {
                if (whoiam == playerList1 && playerTurn == 1 && Moved1 == false)
                {
                    if (pn.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 1;
                        pType = "p";
                        Ind = PawnN.IndexOf(pn);

                        if (pn.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "n";
                            getIND = PawnN.IndexOf(pn);
                        }
                    }

                }
            }


            foreach (P2 pe in PawnE)
            {
                if (whoiam == playerList2 && playerTurn == 2 && Moved2 == false)
                {
                    if (pe.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 2;
                        pType = "p";
                        Ind = PawnE.IndexOf(pe);

                        if (pe.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "e";
                            getIND = PawnE.IndexOf(pe);
                        }
                    }
                }
            }


            foreach (P3 ps in PawnS)
            {
                if (whoiam == playerList3 && playerTurn == 3 && Moved3 == false)
                {
                    if (ps.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 3;
                        pType = "p";
                        Ind = PawnS.IndexOf(ps);

                        if (ps.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "s";
                            getIND = PawnS.IndexOf(ps);
                        }
                    }
                }
            }


            foreach (P4 pw in PawnW)
            {
                if (whoiam == playerList4 && playerTurn == 4 && Moved4 == false)
                {
                    if (pw.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 4;
                        pType = "p";
                        Ind = PawnW.IndexOf(pw);

                        if (pw.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "w";
                            getIND = PawnW.IndexOf(pw);
                        }
                    }
                }
            }


            foreach (Rook rk in RookN)
            {
                if ((whoiam == playerList1 && playerTurn == 1 && Moved1 == false && rk.player == 1)
                    || (whoiam == playerList2 && playerTurn == 2 && Moved2 == false && rk.player == 2)
                    || (whoiam == playerList3 && playerTurn == 3 && Moved3 == false && rk.player == 3)
                    || (whoiam == playerList4 && playerTurn == 4 && Moved4 == false && rk.player == 4))
                {
                    if (rk.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 5;
                        pType = "r";
                        Ind = RookN.IndexOf(rk);

                        if (rk.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "r";
                            getIND = RookN.IndexOf(rk);
                        }
                    }
                }
            }
            foreach (Knight kn in KnightN)
            {
                if ((whoiam == playerList1 && playerTurn == 1 && Moved1 == false && kn.player == 1)
                   || (whoiam == playerList2 && playerTurn == 2 && Moved2 == false && kn.player == 2)
                   || (whoiam == playerList3 && playerTurn == 3 && Moved3 == false && kn.player == 3)
                   || (whoiam == playerList4 && playerTurn == 4 && Moved4 == false && kn.player == 4))
                {
                    if (kn.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 6;
                        pType = "r";
                        Ind = KnightN.IndexOf(kn);

                        if (kn.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "r";
                            getIND = KnightN.IndexOf(kn);
                        }
                    }
                }
            }
            foreach (Bishop bp in BishopN)
            {
                if ((whoiam == playerList1 && playerTurn == 1 && Moved1 == false && bp.player == 1)
                   || (whoiam == playerList2 && playerTurn == 2 && Moved2 == false && bp.player == 2)
                   || (whoiam == playerList3 && playerTurn == 3 && Moved3 == false && bp.player == 3)
                   || (whoiam == playerList4 && playerTurn == 4 && Moved4 == false && bp.player == 4))
                {
                    if (bp.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 7;
                        pType = "r";
                        Ind = BishopN.IndexOf(bp);

                        if (bp.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "r";
                            getIND = BishopN.IndexOf(bp);
                        }
                    }
                }
            }
            foreach (Queen qn in QueenN)
            {
                if ((whoiam == playerList1 && playerTurn == 1 && Moved1 == false && qn.player == 1)
                   || (whoiam == playerList2 && playerTurn == 2 && Moved2 == false && qn.player == 2)
                   || (whoiam == playerList3 && playerTurn == 3 && Moved3 == false && qn.player == 3)
                   || (whoiam == playerList4 && playerTurn == 4 && Moved4 == false && qn.player == 4))
                {
                    if (qn.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 8;
                        pType = "r";
                        Ind = QueenN.IndexOf(qn);

                        if (qn.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "r";
                            getIND = QueenN.IndexOf(qn);
                        }
                    }
                }
            }
            foreach (King kg in KingN)
            {
                if ((whoiam == playerList1 && playerTurn == 1 && Moved1 == false && kg.player == 1)
                   || (whoiam == playerList2 && playerTurn == 2 && Moved2 == false && kg.player == 2)
                   || (whoiam == playerList3 && playerTurn == 3 && Moved3 == false && kg.player == 3)
                   || (whoiam == playerList4 && playerTurn == 4 && Moved4 == false && kg.player == 4))
                {
                    if (kg.ContainsMP())
                    {
                        if (_currentMouseState1.LeftButton == ButtonState.Pressed)
                        {
                            Grab = true;
                        }
                        pNum = 9;
                        pType = "r";
                        Ind = KingN.IndexOf(kg);

                        if (kg.Pawn1Box.Contains(mousePoint))
                        {
                            getPawn = "r";
                            getIND = KingN.IndexOf(kg);
                        }
                    }
                }
            }

            #endregion

        }

        public bool Click()
        {
            if (Game1._previousMouseState1.LeftButton == ButtonState.Pressed && Game1._currentMouseState1.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void MouseButtonReset()
        {
            if (_currentMouseState1.RightButton == ButtonState.Pressed)
            {
            }

            if (_currentMouseState1.LeftButton == ButtonState.Pressed)
            {
                LockTime = 0;
            }
            else
            {
                LockTime += 1;
            }

            if (LockTime >= 50)
            {
                LockTime = 50;
            }
        }

        public bool IsMouseInsideWindow()
        {
            MouseState ms = Mouse.GetState();
            Point pos = new Point(ms.X, ms.Y);
            return GraphicsDevice.Viewport.Bounds.Contains(pos);
        }

        #endregion

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {

            if (Window.ClientBounds.Width < 800)
            {
                Resolution.SetVirtualResolution(800, Window.ClientBounds.Height);
                Resolution.SetResolution(800, Window.ClientBounds.Height, false);

            }
            if (Window.ClientBounds.Height < 600)
            {
                Resolution.SetVirtualResolution(Window.ClientBounds.Width, 600);
                Resolution.SetResolution(Window.ClientBounds.Width, 600, false);
            }
            else if (Window.ClientBounds.Height > Window.ClientBounds.Width - 200)
            {
                Resolution.SetVirtualResolution(Window.ClientBounds.Width, Window.ClientBounds.Width - 200);
                Resolution.SetResolution(Window.ClientBounds.Width, Window.ClientBounds.Width - 200, false);
            }
            else
            {
                Resolution.SetVirtualResolution(Window.ClientBounds.Width, Window.ClientBounds.Height);
                Resolution.SetResolution(Window.ClientBounds.Width, Window.ClientBounds.Height, false);
            }

            displayWidth = Window.ClientBounds.Width;
            displayHeight = Window.ClientBounds.Height;

        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
        }

    }
}




