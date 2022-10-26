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


//namespace _4_Way_Chess
//{
//    public class Camera2d
//    {
//        public static GraphicsDevice graphicsDevice;
//        public static GraphicsDeviceManager graphics;

//        public static Matrix _transform; // Matrix Transform
//        public Vector2 _pos; // Camera Position
//        protected float _rotation; // Camera Rotation
//        protected float _zoom; // Camera Zoom
//        public static long whoiam
//        {
//            get { return Game1.whoiam; }
//        }
//        public static long playerList1
//        {
//            get { return Game1.playerList1; }
//        }
//        public static long playerList2
//        {
//            get { return Game1.playerList2; }
//        }
//        public static long playerList3
//        {
//            get { return Game1.playerList3; }
//        }
//        public static long playerList4
//        {
//            get { return Game1.playerList4; }
//        }


//        public static Rectangle ViewPortCoord;

//        #region get




//        #endregion

//        public Camera2d()
//        {
//            _zoom = 0.5f;

//            _rotation = 0.0f;

//            _pos = new Vector2(0, 0);


//        }





//        // Sets and gets zoom
//        public float Zoom
//        {
//            get { return _zoom; }
//            set
//            {
//                _zoom = value;
//                //if (_zoom < 0.80f)
//                //{
//                //    _zoom = 0.80f;
//                //} // Negative zoom will flip image

//            }





//        }

//        public float Rotation
//        {
//            get { return _rotation; }
//            set { _rotation = value; }
//        }





//        // Auxiliary function to move the camera
//        public void Move(Vector2 amount)
//        {
//            _pos += amount;
//        }




//        public Matrix get_transformation(GraphicsDevice graphicsDevice,
//            int windowWidth, int windowHeight)
//        {

//            Viewport viewPort = graphicsDevice.Viewport;


//            _transform =
//              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
//                                         Matrix.CreateRotationZ(Rotation) *
//                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
//                                         Matrix.CreateTranslation(new Vector3(viewPort.Width * 0.5f, viewPort.Height * 0.5f, 0));
//            return _transform;
//        }


//        public static MouseState _currentMouseState;
//        MouseState _previousMouseState;


//        //public static Vector2 Transform
//        //  (
//        //  Vector2 _mousePosition = new Vector2(viewport.X, viewport.Y),
//        //  Matrix.Invert(Resolution._ScaleMatrix)
//        //  )


//        //public static Vector2 transformFrom
//        //{
//        //    get { return new Vector2(Pawn1.TransformFrom.X, Pawn1.TransformFrom.Y); }
//        //}

//        //public static Vector2 TransformPwn1
//        //{
//        //    get
//        //    {
//        //        //mousePosition = Vector2.Transform(RawMousePosition, Matrix.Invert(Resolution.scaleMatrix));
//        //        mousePosition = Vector2.Transform(transformFrom, Matrix.Invert(Camera2d._transform));
//        //        return mousePosition;
//        //    }
//        //}
//        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
//        {
//            _previousMouseState = _currentMouseState;
//            _currentMouseState = Mouse.GetState();



//            //_mousePosition.X = Transform.X;
//            //_mousePosition.Y = Transform.Y;



//            //if (whoiam == playerList2)
//            //{
//            //    _rotation = 0.0f;
//            //    _pos = new Vector2(400, 240);
//            //}
//            //else if (whoiam == playerList1)
//            //{
//            //    _rotation = 15.707f;
//            //    _pos = new Vector2(35, 195);
//            //}


//        }

//    }


//}
