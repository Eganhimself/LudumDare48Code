using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace GameJamBeta
{
    public class MainMenuScreen : MenuScreen
    {
        MenuEntry play, help, quit;



        public MainMenuScreen()
        {

            play = new MenuEntry(this, "Play Game");
            help = new MenuEntry(this, "Help");
            quit = new MenuEntry(this, "Quit game");

            Removed += new EventHandler(MainMenuRemove);

            Selected = Color.Red;
            NonSelected = Color.Black;





        }

        public override void Initialize()
        {
            play.SetPosition(new Vector2(600, 400), true);
            play.Selected += new EventHandler(PlaySelect);
            help.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), play, true);
            help.Selected += new EventHandler(HelpSelect);

            quit.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), help, true);

            MenuEntries.Add(play);
            MenuEntries.Add(help);
            MenuEntries.Add(quit);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Content;
            SpriteFont = content.Load<SpriteFont>(@"MyFont");
        }

        void PlaySelect(object sender, EventArgs e)
        {
            Remove();
            ScreenManager.AddScreen(new PlayScreen());
        }

        void HelpSelect(object sender, EventArgs e)
        {
            Remove();
            ScreenManager.AddScreen(new HelpScreen());
        }

        void QuitSelect(object sender, EventArgs e)
        {
            Remove();
          
        }

        void MainMenuRemove(object sender, EventArgs e)
        {
            MenuEntries.Clear();
        }
    }
}
