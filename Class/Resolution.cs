//////////////////////////////////////////////////////////////////////////
////License:  The MIT License (MIT)
////Copyright (c) 2010 David Amador (http://www.david-amador.com)
////
////Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
////
////The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
////
////THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_Way_Chess
{
    static class Resolution
    {
        public static GraphicsDeviceManager _Device = null;
        public static float ratio;
        public static int _Width = 800;
        public static int _Height = 600;
        static private int _VWidth = 1024;
        static private int _VHeight = 768;
        static private Matrix _ScaleMatrix;
        static private bool _FullScreen = false;
        static private bool _dirtyMatrix = true;
        static private Vector2 VPXY;
        public static bool thisT;
        public static float _rotation; // Camera Rotation
        public static Viewport vp = new Viewport();
        public static Vector2 _pos = new Vector2();


        static public void Init(ref GraphicsDeviceManager device)
        {
            _Width = device.PreferredBackBufferWidth;
            _Height = device.PreferredBackBufferHeight;
            _Device = device;
            _dirtyMatrix = true;

            
            ApplyResolutionSettings();
        }

        static public Matrix getTransformationMatrix()
        {
            if (_dirtyMatrix) RecreateScaleMatrix();

            return _ScaleMatrix;
        }

        static public void SetResolution(int Width, int Height, bool FullScreen)
        {
            _Width = Width;
            _Height = Height;

            _FullScreen = FullScreen;

            ApplyResolutionSettings();
        }

        static public void SetVirtualResolution(int Width, int Height)
        {
            _VWidth = Width;
            _VHeight = Height;

            _dirtyMatrix = true;
        }

        static private void ApplyResolutionSettings()
        {

#if XBOX360
           _FullScreen = true;
#endif

            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (_FullScreen == false)
            {
                if ((_Width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (_Height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                    && _Height > 0
                    && _Width > 0)
                {
                    _Device.PreferredBackBufferWidth = _Width;
                    _Device.PreferredBackBufferHeight = _Height;
                    _Device.IsFullScreen = _FullScreen;
                    _Device.ApplyChanges();
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate through the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == _Width) && (dm.Height == _Height))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        _Device.PreferredBackBufferWidth = _Width;
                        _Device.PreferredBackBufferHeight = _Height;
                        _Device.IsFullScreen = _FullScreen;
                        _Device.ApplyChanges();
                    }
                }
            }

            _dirtyMatrix = true;

            _Width = _Device.PreferredBackBufferWidth;
            _Height = _Device.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Sets the device to use the draw pump
        /// Sets correct aspect ratio
        /// </summary>
        static public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            FullViewport();
            // Clear to Black
            _Device.GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio
            ResetViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
            _Device.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

      

        static private void RecreateScaleMatrix()
        {

         
            float ratioX = (float)((float)1920 / (float)_VWidth);
            float ratioY = (float)((float)1080 / (float)_VHeight);
            // use whichever multiplier is smaller
            if (thisT)
            {
                ratio = ratioX < ratioY ? ratioY : ratioX;
                
            }
            else
            {
                ratio = ratioX < ratioY ? ratioX : ratioY;
            }
    
            _dirtyMatrix = false;

            _ScaleMatrix = Matrix.CreateTranslation(new Vector3(_pos.X, _pos.Y, 0)) *
                              Matrix.CreateRotationZ(_rotation) *
                              Matrix.CreateScale(new Vector3(
                         (float)_Device.PreferredBackBufferWidth / _VWidth / ratio,
                         (float)_Device.PreferredBackBufferHeight / _VHeight / ratio,
                         1f)) *
                              Matrix.CreateTranslation(new Vector3(vp.Width * 0.5f, vp.Height * 0.5f, 0));
           

        }


        static public void FullViewport()
        {
            vp.X = (int)_pos.X;
            vp.Y = (int)_pos.Y;
            vp.Width = _Width;
            vp.Height = _Height;
            _Device.GraphicsDevice.Viewport = vp;
        }

        /// <summary>
        /// Get virtual Mode Aspect Ratio
        /// </summary>
        /// <returns>aspect ratio</returns>
        static public float getVirtualAspectRatio()
        {
            return (float)_VWidth / (float)_VHeight;
        }

        static public void ResetViewport()
        {
            float targetAspectRatio = getVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = _Device.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;

            if (height > _Device.PreferredBackBufferHeight)
            {
                height = _Device.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            // set up the new viewport centered in the backbuffer

            vp.X = 0;
            vp.Y = 0;
            vp.Width = width;
            vp.Height = height;
            vp.MinDepth = 0;
            vp.MaxDepth = 1;

            VPXY = new Vector2(vp.X, vp.Y);


            if (changed)
            {
                _dirtyMatrix = true;
            }

            _Device.GraphicsDevice.Viewport = vp;
        }





        public static Vector2 mousePosition;

        public static Vector2 RawMousePosition
        {
            get { return new Vector2(Game1._currentMouseState1.X, Game1._currentMouseState1.Y); }
        }

        public static Vector2 MousePosition
        {
            get
            {
                //mousePosition = Vector2.Transform(RawMousePosition, Matrix.Invert(Resolution._ScaleMatrix));
                mousePosition = Vector2.Transform(RawMousePosition - VPXY, Matrix.Invert(Resolution._ScaleMatrix));
                return mousePosition;
            }
        }





        public static void Update(GameTime gameTime)
        {


            //_pos = new Vector2(((float)_Device.PreferredBackBufferWidth / _VWidth / ratio) + 400,
            //                   ((float)_Device.PreferredBackBufferHeight / _VHeight / ratio) + 400);

            if (Game1.whoiam == Game1.playerList1)
            {

                _rotation = 0f;
                _pos = new Vector2(0, 0);


            }
        
        }






    }
}
