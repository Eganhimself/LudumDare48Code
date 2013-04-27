﻿using System;
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
    class IntroScreen : GameScreen
    {
        Texture2D Logo;
        SpriteFont font;


        TimeSpan timer;

        ContentManager content;




        string GoBack;

        public IntroScreen()
        {






        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");


            Logo = content.Load<Texture2D>("Logo");

        }








        public override void Update(GameTime gameTime, bool covered)
        {

            timer += gameTime.ElapsedGameTime;



            if (timer.Seconds == 5)
            {

                Remove();
                ScreenManager.AddScreen(new MainMenuScreen());
                timer = new TimeSpan(0, 0, 0, 0, 0);
            }

        }












        /// <summary>
        /// Draws the pause menu screen. This darkens down the gameplay screen
        /// that is underneath us, and then chains to the base MenuScreen.Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {


            ScreenManager.GraphicsDevice.Clear(Color.White);

            ScreenManager.SpriteBatch.Begin();

            ScreenManager.SpriteBatch.Draw(Logo, Vector2.Zero, Color.White);


            ScreenManager.SpriteBatch.End();
        }
    }
}
