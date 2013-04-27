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
    public class HelpScreen : GameScreen
    {


        Rectangle textBox;
        Texture2D debugColor;
        SpriteFont font;

        Texture2D move;
        Texture2D jump;
        Texture2D attack;
        MenuEntry Done;
        string Gamestring;

        ContentManager content;
        Texture2D Player1Controls;


        string GoBack;

        public HelpScreen()
        {






        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");


            GoBack = "Press the back Button to exit";

            Gamestring = "Snoo Snoo puts fear into mens hearts";

            font = content.Load<SpriteFont>("MyFont");

        }








        public override void Update(GameTime gameTime, bool covered)
        {

            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            if (aCurrentKeyboardState.IsKeyDown(Keys.A))
            {

                Remove();
                ScreenManager.AddScreen(new MainMenuScreen());
            }

        }


        public override void Draw(GameTime gameTime)
        {


            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.SpriteBatch.Begin();


            ScreenManager.SpriteBatch.DrawString(font, Gamestring, new Vector2(100, 300), Color.White);

            ScreenManager.SpriteBatch.End();

        }
    }
}