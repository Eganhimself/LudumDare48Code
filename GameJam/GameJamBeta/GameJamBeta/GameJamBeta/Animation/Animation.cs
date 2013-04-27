using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace GameJamBeta
{
    class Animation
    {

        Texture2D spriteStrip;
        float scale;

        int elaspedTime;

        int frameTime;

        int frameCount;

        int currentFrame;

        public float Rotation;

        Color color;

        Rectangle sourceRectangle = new Rectangle();

        Rectangle desintationRectangle = new Rectangle();

        public int frameWidth;
        public int frameHeight;
        public Vector2 Origin
        {
            get { return new Vector2(frameWidth / 2.0f, frameHeight); }
        }

        public bool Active;
        public bool Looping;

        public Vector2 Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount,
            int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;
            Rotation = 0;
            Active = true;



        }


        public void Update(GameTime gameTime)
        {
            if (Active == false)
                return;

            elaspedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elaspedTime > frameTime)
            {

                currentFrame++;

                if (currentFrame == frameCount)
                {

                    currentFrame = 0;

                    if (Looping == false)
                        Active = false;

                }

                elaspedTime = 0;

            }

            sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            desintationRectangle = new Rectangle((int)Position.X - (int)(frameWidth * scale) / 2,
                (int)Position.Y - (int)(frameHeight * scale) / 2,
                (int)(frameWidth * scale),
                (int)(frameHeight * scale));



        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {


                spriteBatch.Draw(spriteStrip, desintationRectangle, sourceRectangle, color, Rotation, new Vector2(frameWidth / 2, frameHeight / 2), SpriteEffects.None, 0.0f);

            }

        }
    }
}
