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
    enum CurrentControlling
    {
        one,
        two,
        three

    }
    public class PlayScreen : GameScreen
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TimeSpan timer, eTime, cTime;
        string time;
        bool debugMode;
        Random random;
        private SpriteFont mFont;
        int ChanceGen;
        Texture2D background;
        Texture2D blackSquare;
        Texture2D ground;
        Texture2D torch;
        Texture2D lightmask;
        RenderTarget2D mainScene;
        RenderTarget2D lightMask;
        RenderTarget2D SecondScene;
        RenderTarget2D lightMask2;

        Effect lightingEffect;
        private KeyboardState mPreviousKeyboardState;
        const int LIGHTOFFSET = 200;
        Camera2D Camera;
        Player P1;
        Player2 P2;
        Player3 P3;
        Texture2D playerHealthBar;
        bool isStatsShown;
        Ecnomnics E1;
        CurrentControlling Control = CurrentControlling.one;
        Texture2D BGStats;
        Texture2D BG;
        List<Water> WaterList;
        List<Food> FoodList;
        List<Treats> TreatLists;
        List<Bat> BatList;
 
        List<Snake> SnakeList;
        Texture2D WaterTexture;
        Texture2D FoodTexture;
        TimeSpan previousSpawnTime;
        TimeSpan enemySpawnTime;
        SpriteFont _spr_font;
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;
        ContentManager Content;
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        SpriteFont spriteFont;

        public PlayScreen()
        {
            Camera = new Camera2D();
            isStatsShown = false;
            E1 = new Ecnomnics();
            WaterList = new List<Water>();
            FoodList = new List<Food>();
            TreatLists = new List<Treats>();

           BatList = new List<Bat>();
           SnakeList = new List<Snake>();
        }


        public override void Initialize()
        {

           
        }

        public override void LoadContent()
        {
             Content = ScreenManager.Content;
          
            spriteBatch = ScreenManager.SpriteBatch; 
            P1 = new Player();
            P1.LoadContent(Content);
            P2 = new Player2();
            P2.LoadContent(Content);
            P3 = new Player3();
            P3.LoadContent(Content);
            random = new Random();
            cTime = new TimeSpan(0, 0, 0);
            time = "";
            debugMode = false;
            ChanceGen = random.Next(5);
            mFont = Content.Load<SpriteFont>("MyFont");

            background = Content.Load<Texture2D>("bg5");
            blackSquare = Content.Load<Texture2D>("blacksquare");
            ground = Content.Load<Texture2D>("ground");
            torch = Content.Load<Texture2D>("torch");
            lightmask = Content.Load<Texture2D>("lightmask");
            lightingEffect = Content.Load<Effect>("lighting");
            BGStats = Content.Load<Texture2D>("StatsBG");
            BG = Content.Load<Texture2D>("BG");
           WaterTexture = Content.Load<Texture2D>("Water");
           enemySpawnTime = TimeSpan.FromSeconds(4.0f);
           FoodTexture = Content.Load<Texture2D>("Food");

            var pp = ScreenManager.GraphicsDevice.PresentationParameters;
            pp.BackBufferHeight = 4000;
            pp.BackBufferWidth = 4000;
            
            mainScene = new RenderTarget2D(ScreenManager.GraphicsDevice, 4000, 4000);
            lightMask = new RenderTarget2D(ScreenManager.GraphicsDevice, 4000, 4000);

            SecondScene = new RenderTarget2D(ScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            lightMask2 = new RenderTarget2D(ScreenManager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
        

            playerHealthBar = Content.Load<Texture2D>("HealthBar");

            spriteFont = Content.Load<SpriteFont>("MyFont");
            
        }


        public override void UnloadContent()
        {
            Content.Unload();

        }

        public override void Update(GameTime gameTime, bool covered)
        {

            //timer += gameTime.ElapsedGameTime;

            //eTime = gameTime.TotalGameTime;
                                                                                                                                                                                                                                                                
            //if (timer.Seconds == 10)
            //{
            //    time += ":0" + (int)(timer.Seconds);
            //    ChanceGen = random.Next(5);
            //    timer = new TimeSpan(0, 0, 0, 0, 0);
            //}

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }


            // Update
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
 
    
 
            UpdateControls(gameTime);
            CheckCollision();
            UpdateCamera(gameTime);
            //UpdateTreats(gameTime);                                                                                                                                                  
            //UpdateWater(gameTime);
            //UpdateFood(gameTime);
    
            P1.Update(gameTime);
            //P2.Update(gameTime);
            //P3.Update(gameTime);

            base.Update(gameTime, covered);
        }

        private void UpdateCamera(GameTime gameTime)
        {
            if(Camera._pos.Y <= -1500)
            {
                Camera._pos.Y = -1500;

            }
            if (Camera._pos.Y >= 2000)
            {
                Camera._pos.Y = 2000;

            }
            if (Camera._pos.X <= -4000)
            {
                Camera._pos.X = -4000;
            }
            //if (Camera._pos.X >= 3000)
            //{
            //    Camera._pos.X = 3000;
            //}
           

        }

        private void UpdateControls(GameTime gameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();


            if (aCurrentKeyboardState.IsKeyDown(Keys.D) && debugMode == false)
            {
                debugMode = true;
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.F) && debugMode == true)
            {

                debugMode = false;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                if (Control == CurrentControlling.one)
                {
                    P1.DoMoveLeft();
                }
                if (Control == CurrentControlling.two)
                {
                    P2.DoMoveLeft();
                }
                if (Control == CurrentControlling.three)
                {
                    P3.DoMoveLeft();
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (Control == CurrentControlling.one)
                {
                    P1.DoMoveRight();
                }
                if (Control == CurrentControlling.two)
                {
                    P2.DoMoveRight();
                }
                if (Control == CurrentControlling.three)
                {
                    P3.DoMoveRight();
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Down))
            {
                if (Control == CurrentControlling.one)
                {
                    P1.DoMoveDown();
                }
                if (Control == CurrentControlling.two)
                {
                    P2.DoMoveDown();
                }
                if (Control == CurrentControlling.three)
                {
                    P3.DoMoveDown();
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Up))
            {
                if (Control == CurrentControlling.one)
                {
                    P1.DoMoveUp();
                }
                if (Control == CurrentControlling.two)
                {
                    P2.DoMoveUp();
                }
                if (Control == CurrentControlling.three)
                {
                    P3.DoMoveUp();
                }
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Q))
            {
                isStatsShown = true;

            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.E))
            {
                isStatsShown = false;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.D1))
            {
                Control = CurrentControlling.one;
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.D2))
            {
                Control = CurrentControlling.two;
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.D3))
            {
                Control = CurrentControlling.three;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Z) && Control == CurrentControlling.one)
            {
                Camera._zoom += 0.001f;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.X))
            {
                Camera._zoom = 0.1f;
                Camera._pos = new Vector2(-1125, 225);

            }


            if (aCurrentKeyboardState.IsKeyDown(Keys.W))
            {
                Camera._pos.Y -= 25.0f;
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.S))
            {
                Camera._pos.Y += 25.0f;

            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.A))
            {
                Camera._pos.X -= 25.0f;
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.D))
            {
                Camera._pos.X += 25.0f;
            }

            mPreviousKeyboardState = aCurrentKeyboardState;

        }

        private bool CheckCollision()
        {
            Rectangle P1Rect = new Rectangle((int)P1.mPlayerPos.X, (int)P1.mPlayerPos.Y, P1.mPlayer.Width, P1.mPlayer.Height);
            Rectangle P2Rect = new Rectangle((int)P2.mPlayerPos.X, (int)P2.mPlayerPos.Y, P2.mPlayer.Width, P2.mPlayer.Height);
            Rectangle P3Rect = new Rectangle((int)P3.mPlayerPos.X, (int)P3.mPlayerPos.Y, P3.mPlayer.Width, P3.mPlayer.Height);
            Rectangle WaterRect;
            Rectangle FoodRect;
            Rectangle TreatsRect;
            for (int i = 0; i < WaterList.Count(); i++)
            {
                WaterRect = new Rectangle((int)WaterList[i].Position.X - WaterList[i].Width,(int) WaterList[i].Position.Y - WaterList[i].Height,
                    WaterList[i].Width, WaterList[i].Height);



                if(P1Rect.Intersects(WaterRect))
                {


                }

                if(P2Rect.Intersects(WaterRect))
                {

                }
                if (P3Rect.Intersects(WaterRect))
                {

                }
            }

            for (int i = 0; i < FoodList.Count(); i++)
            {

                FoodRect = new Rectangle((int)FoodList[i].Position.X - FoodList[i].Width, (int)FoodList[i].Position.Y - FoodList[i].Height,
                    FoodList[i].Width, FoodList[i].Height);

                if (P1Rect.Intersects(FoodRect))
                {


                }

                if (P2Rect.Intersects(FoodRect))
                {

                }
                if (P3Rect.Intersects(FoodRect))
                {

                }
            }

            for (int i = 0; i < TreatLists.Count(); i++)
            {
                TreatsRect = new Rectangle((int)TreatLists[i].Position.X - TreatLists[i].Width, (int)TreatLists[i].Position.Y - TreatLists[i].Height,
                    TreatLists[i].Width, TreatLists[i].Height);

                if (P1Rect.Intersects(TreatsRect))
                {


                }

                if (P2Rect.Intersects(TreatsRect))
                {

                }
                if (P3Rect.Intersects(TreatsRect))
                {

                }
                










            }

            
            

            return false;
        }


        private void AddWater()
        {
             
            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(WaterTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            Vector2 position = new Vector2(200, 200); 

            Water enemy = new Water();

            enemy.Initialize(enemyAnimation, position);

            WaterList.Add(enemy);
        }

        private void AddFood()
        {
            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(FoodTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            Vector2 position = new Vector2(200, 200);

           Food enemy = new Food();

            enemy.Initialize(enemyAnimation, position);

            FoodList.Add(enemy);


        }

        private void AddTreats()
        {
            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(FoodTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            Vector2 position = new Vector2(200, 200);

            Treats enemy = new Treats();

            enemy.Initialize(enemyAnimation, position);

            TreatLists.Add(enemy);

        }


        private void AddSnake()
        {
            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(FoodTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            Vector2 position = new Vector2(200, 200);

            Snake enemy = new Snake();

            enemy.Initialize(enemyAnimation, position);

            SnakeList.Add(enemy);
        }

        private void UpdateFood(GameTime gameTime)
        {
            for (int i = 0; i < 10; i++)
            {
                AddFood();
            }
            for (int i = 0; i < FoodList.Count(); i++)
            {
                FoodList[i].Update(gameTime);
                if (FoodList[i].Active == false)
                {

                    FoodList.RemoveAt(i);
                }
            }

        }

        private void UpdateWater(GameTime gameTime)
        {
            for (int i = 0; i < 1; i++)
            {

                AddWater();
            }
            for (int i = 0; i < WaterList.Count(); i++)
            {
                WaterList[i].Update(gameTime);
                if (WaterList[i].Active == false)
                {
                    //if (WaterList[i].Health <= 0)
                    //{
                    //    WaterList[i].Active = false;
                    //}
                    WaterList.RemoveAt(i);
                }
            }

        }

       



        private void drawMain(GameTime gameTime)
        {

            ScreenManager.GraphicsDevice.SetRenderTarget(mainScene);
            //ScreenManager.GraphicsDevice.Clear(Color.Blue);

            spriteBatch.Begin();
           
            spriteBatch.Draw(BG, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 5f, SpriteEffects.None, 0);
            //for (int i = 0; i < WaterList.Count(); i++)
            //{
            //    WaterList[i].Draw(spriteBatch);
            //}

            P1.Draw(spriteBatch);

           
            spriteBatch.End();

            ScreenManager.GraphicsDevice.SetRenderTarget(null);
        }

        private void drawSecondScreen(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.SetRenderTarget(SecondScene);

            spriteBatch.Begin();
            spriteBatch.Draw(BG, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 5f, SpriteEffects.None, 0);

            //for (int i = 0; i < FoodList.Count(); i++)
            //{
            //    FoodList[i].Draw(spriteBatch);
            //}
            P2.Draw(spriteBatch);

          
            spriteBatch.End();

            ScreenManager.GraphicsDevice.SetRenderTarget(null);
        }



  


        private void drawLightMask(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.SetRenderTarget(lightMask);
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            
            spriteBatch.Draw(lightmask, new Vector2(P1.mPlayerPos.X - LIGHTOFFSET, P1.mPlayerPos.Y - LIGHTOFFSET), Color.White);



            spriteBatch.End();

            ScreenManager.GraphicsDevice.SetRenderTarget(null);
        }

        private void drawSecondLightMask(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.SetRenderTarget(lightMask2);
            ScreenManager.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);


            spriteBatch.Draw(lightmask, new Vector2(P2.mPlayerPos.X - LIGHTOFFSET, P2.mPlayerPos.Y - LIGHTOFFSET), Color.White);



            spriteBatch.End();

            ScreenManager.GraphicsDevice.SetRenderTarget(null);



        }

 
        public override void Draw(GameTime gameTime)
        {
            drawMain(gameTime);
            drawLightMask(gameTime);
            drawSecondScreen(gameTime);
            drawSecondLightMask(gameTime);
     


            spriteBatch.Begin();
            spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            spriteBatch.Draw(BG, new Vector2(500, 0), Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default,
                            RasterizerState.CullCounterClockwise, null, Camera.get_transformation(ScreenManager.GraphicsDevice));


            lightingEffect.Parameters["lightMask"].SetValue(lightMask);
            lightingEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(mainScene, new Vector2(-6000, -3000), Color.White);




            lightingEffect.Parameters["lightMask"].SetValue(lightMask2);
            lightingEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(SecondScene, new Vector2(-1000, -3000), Color.White);


            spriteBatch.End();

            spriteBatch.Begin();

            if (isStatsShown)
            {
                spriteBatch.Draw(BGStats, new Vector2(300, 50), Color.White);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340, (int)110, 0, 10),
                    new Rectangle(0, 25, playerHealthBar.Width, 30), Color.Green, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340,
             (int)110, (int)(playerHealthBar.Width * ((double)E1.FoodSupply / 100)), 20),
             new Rectangle(0, 25, playerHealthBar.Width, 25), Color.Green, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);


                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340, (int)200, 0, 10),
                new Rectangle(0, 25, playerHealthBar.Width, 30), Color.Blue, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340,
             (int)200, (int)(playerHealthBar.Width * ((double)E1.WaterSupply / 100)), 20),
             new Rectangle(0, 25, playerHealthBar.Width, 25), Color.Blue, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340, (int)290, 0, 10),
            new Rectangle(0, 25, playerHealthBar.Width, 30), Color.Pink, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340,
             (int)290, (int)(playerHealthBar.Width * ((double)E1.Happiness / 100)), 20),
             new Rectangle(0, 25, playerHealthBar.Width, 25), Color.Pink, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340, (int)390, 0, 10),
            new Rectangle(0, 25, playerHealthBar.Width, 30), Color.Yellow, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340,
             (int)390, (int)(playerHealthBar.Width * ((double)E1.TribeSize / 100)), 20),
             new Rectangle(0, 25, playerHealthBar.Width, 25), Color.Yellow, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340, (int)490, 0, 10),
            new Rectangle(0, 25, playerHealthBar.Width, 30), Color.Purple, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.Draw(playerHealthBar, new Rectangle((int)340,
             (int)490, (int)(playerHealthBar.Width * ((double)E1.worldSupplies / 100)), 20),
             new Rectangle(0, 25, playerHealthBar.Width, 25), Color.Purple, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0.0f);

                spriteBatch.DrawString(mFont, "Current controlling: " + Control.ToString(), new Vector2(300, 525), Color.White);

                spriteBatch.DrawString(mFont, "Current food supply: " + E1.Happiness.ToString(), new Vector2(300, 70), Color.White);

                spriteBatch.DrawString(mFont, "Current Water supply: " + E1.WaterSupply.ToString(), new Vector2(300, 150), Color.White);

                spriteBatch.DrawString(mFont, "Current happiness: " + E1.Happiness.ToString(), new Vector2(300, 250), Color.White);

                spriteBatch.DrawString(mFont, "Current tribe size: " + E1.TribeSize.ToString(), new Vector2(300, 340), Color.White);

                spriteBatch.DrawString(mFont, "Current World supplies: " + E1.worldSupplies.ToString(), new Vector2(300, 440), Color.White);

            }

            frameCounter++;

            string fps = string.Format("fps: {0}", frameRate);

          

            spriteBatch.DrawString(spriteFont, fps, new Vector2(33, 33), Color.Black);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(32, 32), Color.White);

           spriteBatch.DrawString(mFont, "The Cave of Desinity", new Vector2(


            //if (debugMode)
            //{
            spriteBatch.DrawString(mFont, "Camera X pos: " + Camera._pos.X.ToString(), new Vector2(300, 440), Color.White);

            spriteBatch.DrawString(mFont, "Camera Y pos" + Camera._pos.ToString(), new Vector2(300, 500), Color.White);

            //}
            spriteBatch.End();
          
        }
       
    }
}
