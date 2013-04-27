using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GameJamBeta
{
    class Camera2D
    {
        public float _zoom;
        public Matrix _transform;
        public Vector2 _pos;
        protected float _rotation;

        public Camera2D()
        {
            _zoom = 0.1f;
            _rotation = 0.0f;
            _pos = new Vector2(-1125, 225 );

        }


        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.06f)
                {
                    _zoom = 0.1f;
                }
            }

        }

        public void Update(GameTime gameTime)
        {
            if (_zoom < 0.06f)
            {
                _zoom = 0.1f;
            }




        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Matrix get_transformation(GraphicsDevice device)
        {
            _transform = Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(device.Viewport.Width * 0.5f, device.Viewport.Height * 0.5f, 0));

            return _transform;
        }
    }
}
