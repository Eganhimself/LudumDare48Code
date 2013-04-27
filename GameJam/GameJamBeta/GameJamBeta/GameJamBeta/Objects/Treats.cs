using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace GameJamBeta
{
    class Treats
    {
        public Animation enemyAnimation;

        public Vector2 Position;

        public bool Active;

        public int Damage;

        public int Value;

        public int Width
        {
            get { return enemyAnimation.frameWidth; }

        }

        public int Height
        {

            get { return enemyAnimation.frameHeight; }
        }

        float enemyMoveSpeed;
        public void Initialize(Animation animation, Vector2 position)
        {
            enemyAnimation = animation;

            Position = position;

            Active = true;

            Damage = 10;

            enemyMoveSpeed = 3f;

            Value = 100;
        }
        public void Update(GameTime gameTime)
        {


            enemyAnimation.Position = Position;

            enemyAnimation.Update(gameTime);


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            enemyAnimation.Draw(spriteBatch);
        }
    }
}
