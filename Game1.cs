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

namespace _4_Way_Chess
{
    public class Game1 : Game
    {
        #region Class Declarations
        GraphicsDeviceManager graphics;
        GraphicsDevice gd;
        SpriteBatch spriteBatch;
        StartMenu Menu;
        Board board;
        //Camera2d cam = new Camera2d();
        Random rand = new Random();
        LoadScreen loadScrn;
        Settings inGame_settings;
        ScoreBoard inGame_scoreboard;
        MenuOptions MOptions;
        Credits Cred;
        TextBox txtBox;
        Options Opt;
        Private Priv;
        Lan LN;
        QueLayout Qu;
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
        public static Point mousePoint;
        public static Vector2 MousePosition
        {
            get { return Resolution.MousePosition; }
        }

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
        Texture2D Wallpaper2;

        #endregion


        string serverIP = "14.202.157.211";

        public static int Loaded = 0;

        public static Rectangle[] all = new Rectangle[64];
        public static int[] allP = new int[64];


        #region ConsoleMech

        Texture2D cursorHand;
        public static int testW;
        public static int testH;
        int randInt;
        bool Pause;

        #endregion

        #region InputDevices

        public static MouseState _currentMouseState1;
        public static MouseState _previousMouseState1;
        KeyboardState keyState;

        #endregion

        #region Box

        public static int LockTime;
        public static bool Grab;

        #endregion

        #region 3D Declarations

        Model TestModel;
        Vector3 modelPosition = Vector3.Zero;
        float modelRotation = 0.0f;
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);

        #endregion

        #region ServerId's

        public static long whoiam;
        public static long playerList1;
        public static long playerList2;
        public static long playerList3;
        public static long playerList4;

        #endregion

        Rectangle[] WhiteBox
        {
            get { return Board.WhiteBox; }
        }

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
        Vector2 VersionPosition;
        SpriteFont font;
        SpriteFont font1;
        string[] playerName = new string[4];

        #endregion

        Color BgColor = new Color(21, 21, 21);

        public enum MenuState
        {
            Menu,
            Online,
            HostOnline,
            JoinOnline,
            Private,
            HostPrivate,
            JoinPrivate,
            Lan,
            HostLan,
            JoinLan,
            Options,
            Credits,
            Loading,
            Game,
            Name,
            EnterIp,
        };

        public static MenuState menuEnum;

        public static bool Entered;

        public static Process exeProcess;

        int t;
        int g;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Resolution.Init(ref graphics);


            Content.RootDirectory = "Content";
            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(800, 450);
            Resolution.SetResolution(800, 450, false);


            //Resolution.FullViewport();

            Window.AllowUserResizing = true;
            Window.Title = "4-Way Chess";

            //IsMouseVisible = true;
            //graphics.ApplyChanges();
        }

        protected override void Initialize()
        {


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Arial");
            //font1 = Content.Load<SpriteFont>("SpriteFont1");

            board = new Board(Content);
            Menu = new StartMenu(Content);
            loadScrn = new LoadScreen(Content);
            txtBox = new TextBox(Content);
            inGame_settings = new Settings(Content);
            inGame_scoreboard = new ScoreBoard(Content);
            MOptions = new MenuOptions(Content);
            Cred = new Credits(Content);
            Opt = new Options(Content, font);
            Priv = new Private(Content, font);
            LN = new Lan(Content);
            Qu = new QueLayout(Content, font);
            baseP = new Base();

            Resolution.vp.X = 0;
            Resolution.vp.Y = 0;

            LN.LoadContent();
            Qu.LoadContent();
            Menu.LoadContent();
            txtBox.LoadContent();
            inGame_scoreboard.LoadContent();
            inGame_settings.LoadContent();
            MOptions.LoadContent();
            Cred.LoadContent();
            Opt.LoadContent();
            Priv.LoadContent();
            baseP.LoadContent();
            cursorHand = Content.Load<Texture2D>("cursor");
            inGame_scoreboard.LoadContent();

            inGame_settings.LoadContent();
            PawnNSprite = Content.Load<Texture2D>("PawnN");
            PawnESprite = Content.Load<Texture2D>("PawnE");
            PawnSSprite = Content.Load<Texture2D>("PawnS");
            PawnWSprite = Content.Load<Texture2D>("PawnW");
            HorseNSprite = Content.Load<Texture2D>("Horse");
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



        }

        protected override void Update(GameTime gameTime)
        {

            _currentMouseState1 = Mouse.GetState();

            if (_currentMouseState1.MiddleButton == ButtonState.Pressed)
            {
                testH = (int)MousePosition.X;

            }

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
            keyState = Keyboard.GetState();
            mousePoint = new Point((int)MousePosition.X, (int)MousePosition.Y);






            #region Client Form

            if (_currentMouseState1.LeftButton == ButtonState.Pressed)
            {
                cursorHand = Content.Load<Texture2D>("Hand");
            }
            else
            {
                cursorHand = Content.Load<Texture2D>("cursor");

            }

            IntPtr hWnd = this.Window.Handle;


            if (playerList1 == null)
            {
                playerName[0] = "";

            }

            //Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

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

            testW = Window.ClientBounds.Width;
            testH = Window.ClientBounds.Height;


            Resolution.thisT = true;

            #endregion

            #region ..

            //if (form.DisplayRectangle.Width < 800)
            //{
            //    Resolution.SetVirtualResolution(800, form.DisplayRectangle.Height);
            //    Resolution.SetResolution(800, form.DisplayRectangle.Height, false);

            //}
            //if (form.DisplayRectangle.Height < 600)
            //{
            //    Resolution.SetVirtualResolution(form.DisplayRectangle.Width, 600);
            //    Resolution.SetResolution(form.DisplayRectangle.Width, 600, false);
            //}
            //else if (form.DisplayRectangle.Height > form.DisplayRectangle.Width - 200)
            //{
            //    Resolution.SetVirtualResolution(form.DisplayRectangle.Width, form.DisplayRectangle.Width - 200);
            //    Resolution.SetResolution(form.DisplayRectangle.Width, form.DisplayRectangle.Width - 200, false);
            //}

            //testW = form.DisplayRectangle.Width;

            //Resolution.thisT = true;

            //if (Window.ClientBounds.Width < 400)
            ////{   
            //if (Window.ClientBounds.Width < 1100)
            //{

            //    Resolution.SetVirtualResolution(1100, Window.ClientBounds.Height);
            //    Resolution.SetResolution(1100, Window.ClientBounds.Height, false);

            //}
            //if(Window.ClientBounds.Height < 600)
            //{
            //    Resolution.SetVirtualResolution(Window.ClientBounds.Width, 600 );
            //    Resolution.SetResolution(Window.ClientBounds.Width, 600, false);
            //}
            //else
            //{
            //    Resolution.SetVirtualResolution(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            //    Resolution.SetResolution(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, false);

            //}




            //    if (keyState.IsKeyDown(Keys.Q))
            //{
            //    Resolution.SetVirtualResolution(128, 72);
            //    Resolution.SetResolution(128, 72, false);
            //}
            //if (keyState.IsKeyDown(Keys.W))
            //{
            //    Resolution.SetVirtualResolution(1280, 720);
            //    Resolution.SetResolution(1280, 720, false);
            //}
            //if (keyState.IsKeyDown(Keys.Z))
            //{
            //    Resolution.SetVirtualResolution(1980, 720);
            //    Resolution.SetResolution(1280, 720, false);
            //} 
            //if (keyState.IsKeyDown(Keys.E))
            //{
            //    Resolution.SetVirtualResolution(1920, 1080);
            //    Resolution.SetResolution(1920, 1080, false);
            //}
            //if (keyState.IsKeyDown(Keys.R))
            //{
            //    Resolution.SetVirtualResolution(1920, 1080);
            //    Resolution.SetResolution(1920, 1080, true);
            //}
            //if (userClickedTheResolutionChangeButton)
            //{
            //    graphics.IsFullScreen = userRequestedFullScreen;
            //    graphics.PreferredBackBufferHeight = userRequestedHeight;
            //    graphics.PreferredBackBufferWidth = userRequestedWidth;
            //    graphics.ApplyChanges();
            //}


            #endregion

            #region Case

            if (menuEnum != MenuState.Game || menuEnum != MenuState.Loading)
            {
                Menu.Update(gameTime);
            }
            switch (menuEnum)
            {
                case MenuState.Menu:

                    MOptions.Update(gameTime);

                    if (Click() && MOptions.menuOpt[1].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Private;
                    }
                    if (Click() && MOptions.menuOpt[2].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Lan;
                    }
                    if (Click() && MOptions.menuOpt[4].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Options;
                    }
                    if (Click() && MOptions.menuOpt[5].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Credits;
                    }
                    if (Click() && MOptions.menuOpt[6].Contains(Game1.mousePoint))
                    {
                        this.Exit();
                    }
                    break;
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
                case MenuState.Private:

                    Priv.Update(gameTime);

                    if (Click() && Priv.menuOpt[0].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.HostPrivate;
                    }
                    if (Click() && Priv.menuOpt[1].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.EnterIp;
                    }
                    if (Click() && Priv.menuOpt[3].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Menu;
                    }
                    break;
                case MenuState.Lan:

                    LN.Update(gameTime);
                    if (Click() && LN.menuOpt[3].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Menu;
                    }
                    break;
                case MenuState.Credits:

                    Cred.Update(gameTime);
                    if (Click() && Cred.menuOpt[9].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Menu;
                    }
                    break;
                case MenuState.Options:

                    Opt.Update(gameTime);
                    if (Click() && Opt.menuOpt[0].Contains(Game1.mousePoint))
                    {
                        menuEnum = MenuState.Menu;
                    }
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

            #region HuD

            //VersionPosition = new Vector2(880, 730);
            VersionPosition = new Vector2(15, Game1.testH * Resolution.ratio - 40);

            #endregion

            #region Pause Game

            //if (IsActive == true)
            //{
            //    Pause = false;
            //}
            //else
            //{
            //    Pause = true;
            //}

            //if (_currentMouseState1.X > 800 || _currentMouseState1.Y > 500 || _currentMouseState1.X < 0 || _currentMouseState1.Y < 0)
            //{
            //    Pause = true;
            //}
            //else
            //{
            //Pause = false;
            //}

            #endregion

            MouseButtonReset();

            // exit game if escape or Back is pressed
            if (keyState.IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            { this.Exit(); }
            _previousMouseState1 = _currentMouseState1;

            Resolution.Update(gameTime);
            base.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {

            //Resolution.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                Resolution.getTransformationMatrix());

            if (menuEnum != MenuState.Game && menuEnum != MenuState.Loading)
            {
                graphics.GraphicsDevice.Clear(BgColor);
                Menu.Draw(spriteBatch);

                switch (menuEnum)
                {
                    case MenuState.Menu:

                        MOptions.Draw(spriteBatch);
                        break;
                    case MenuState.Private:

                        Priv.Draw(spriteBatch);
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
                                spriteBatch.DrawString(font, playerName[i], new Vector2(((testW * Resolution.ratio) / 2) - ((int)font.MeasureString(playerName[i]).X / 2), 300 + (i * 180)), Color.White);

                            }
                        }
                        break;
                    case MenuState.JoinPrivate:

                        Qu.Draw(spriteBatch);

                        for (int i = 0; i < 2; i++)
                        {
                            if (playerName[i] != null)
                            {
                                spriteBatch.DrawString(font, playerName[i], new Vector2(((testW * Resolution.ratio) / 2) - ((int)font.MeasureString(playerName[i]).X / 2), 300 + (i * 180)), Color.White);

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
                    spriteBatch.Draw(Wallpaper, new Rectangle((int)(testW * Resolution.ratio) / 2 - ((int)(Wallpaper.Width * 2.5) / 2), (int)(testH * Resolution.ratio) / 2 - ((int)(Wallpaper.Height * 2.5) / 2), (int)(Wallpaper.Width * 2.5), (int)(Wallpaper.Height * 2.5)), Color.White);
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

            #region 3D

            //// Copy any parent transforms.
            //Matrix[] transforms = new Matrix[TestModel.Bones.Count];
            //TestModel.CopyAbsoluteBoneTransformsTo(transforms);

            //// Draw the model. A model can have multiple meshes, so loop.
            //foreach (ModelMesh mesh in TestModel.Meshes)
            //{
            //    // This is where the mesh orientation is set, as well 
            //    // as our camera and projection.
            //    foreach (BasicEffect effect in mesh.Effects)
            //    {
            //        effect.EnableDefaultLighting();
            //        effect.World = transforms[mesh.ParentBone.Index] *
            //            Matrix.CreateRotationY(modelRotation)
            //            * Matrix.CreateTranslation(modelPosition);
            //        effect.View = Matrix.CreateLookAt(cameraPosition,
            //            Vector3.Zero, Vector3.Up);
            //        effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            //            MathHelper.ToRadians(45.0f), aspectRatio,
            //            1.0f, 10000.0f);
            //    }
            //    // Draw the mesh, using the effects set above.
            //    mesh.Draw();
            //}

            #endregion

            if (IsMouseInsideWindow())
            {
                spriteBatch.Draw(cursorHand, new Vector2(Resolution.MousePosition.X, Resolution.MousePosition.Y), Color.White);
            }

            spriteBatch.DrawString(font, "Beta V4.2", VersionPosition, Color.Red);
            spriteBatch.End();

            spriteBatch.Begin();

            if (menuEnum == MenuState.Game)
            {
                inGame_settings.Draw(spriteBatch);
                inGame_scoreboard.Draw(spriteBatch);
            }

            spriteBatch.End();




            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
      Resolution.getTransformationMatrix());


            if (IsMouseInsideWindow())
            {
                spriteBatch.Draw(cursorHand, new Vector2(Resolution.MousePosition.X, Resolution.MousePosition.Y), Color.White);
            }

            //if (playerName[0] != null)
            //{
            //    spriteBatch.DrawString(font, playerName[0], new Vector2(((testW * Resolution.ratio) / 2), 480), Color.White);
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

        public int graphWid()
        {

            return Window.ClientBounds.Right;
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

            testW = Window.ClientBounds.Width;
            testH = Window.ClientBounds.Height;

        }

        protected override void OnExiting(object sender, EventArgs args)
        {


            base.OnExiting(sender, args);
        }




    }
}




