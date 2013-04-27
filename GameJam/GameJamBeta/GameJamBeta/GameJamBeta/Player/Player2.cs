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
    class Player2
    {
        public Texture2D mPlayer;
        public Vector2 mPlayerPos;
       const int move_up = -3;
        const int move_down = 3;
        const int move_left = -3;
        const int move_right = 3;
        Vector2 mDirction = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;
        // Physics state
      
        public Vector2 Position
        {
            get { return mPlayerPos; }
            set { mPlayerPos = value; }
        }

        public int mPlayerHealth;
        bool isActive;
        public Player2()
        {

            mPlayerPos = new Vector2(300, 300);
            mPlayerHealth = 100; 
         

        }

        public void LoadContent(ContentManager content)
        {

            mPlayer = content.Load<Texture2D>("Player");

        }

        public void Update(GameTime gameTime)
        {


        }

        public void DoMoveUp()
        {
            mPlayerPos.Y += move_up;
        }

        public void DoMoveDown()
        {
            mPlayerPos.Y += move_down;

        }

        public void DoMoveLeft()
        {
            mPlayerPos.X += move_left;
        }

        public void DoMoveRight()
        {

            mPlayerPos.X += move_right;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mPlayer, mPlayerPos, Color.White);

        }
    }
}
