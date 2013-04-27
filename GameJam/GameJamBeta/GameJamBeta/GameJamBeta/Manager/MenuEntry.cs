using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace GameJamBeta
{
    public class MenuEntry
    {
        string entryTitle;
        public string Entrytitle
        {
            get { return entryTitle; }
            set { entryTitle = value; }
        }

        Texture2D entryTexture;
        public Texture2D EntryTexture
        {
            get { return entryTexture; }
            set { entryTexture = value; }

        }

        Vector2 initialPosition;
        public Vector2 InitialPosition
        {
            get { return initialPosition; }
        }

        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }

        }

        Vector2 acceleration;
        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        MenuScreen menuScreen;
        public MenuScreen MenuScreen
        {
            get { return menuScreen; }
            set { menuScreen = value; }
        }

        public event EventHandler Highlighted;

        public virtual void Highlight()
        {
            if (Highlighted != null)
                Highlighted(this, EventArgs.Empty);
        }

        /// <summary>
        /// When the entry is selected, do something like
        /// load a new screen or load its sub-menu.
        /// </summary>
        public event EventHandler Selected;

        public virtual void Select()
        {
            if (Selected != null)
                Selected(this, EventArgs.Empty);
        }

        public MenuEntry(MenuScreen screen, string title)
        {
            this.menuScreen = screen;
            entryTitle = title;
        }
        public MenuEntry(MenuScreen screen, Texture2D texture)
        {
            this.menuScreen = screen;
            entryTexture = texture;
        }
        public MenuEntry(MenuScreen screen, string title, Texture2D texture)
        {
            this.menuScreen = screen;
            entryTitle = title;
            entryTexture = texture;
        }

        public virtual void Update(GameTime gameTime, bool isSelected)
        {
            position = new Vector2(initialPosition.X, initialPosition.Y);

            //We want base.Update to come before our actual logic so the transition updates.
            if (menuScreen.ScreenState == ScreenState.TransitionOn || menuScreen.ScreenState == ScreenState.TransitionOff)
            {
                acceleration = new Vector2((float)Math.Pow(menuScreen.TransitionPercent - 1, 2), 0);
                acceleration.X *= menuScreen.TransitionDirection * -150;

                position += acceleration;
            }
        }

        public virtual void Animate(GameTime gameTime, bool isSelected) { }

        public virtual void Draw(GameTime gameTime, bool isSelected)
        {
            Color color = isSelected ? menuScreen.Selected : menuScreen.NonSelected;
            color = new Color(color.R, color.G, color.B, menuScreen.ScreenAlpha);

            SpriteBatch spriteBatch = menuScreen.ScreenManager.SpriteBatch;
            SpriteFont spriteFont = menuScreen.SpriteFont;

            Vector2 entryPosition = new Vector2(position.X, position.Y);

            float pulse = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3) + 1);
            float scale = isSelected ? (1 + pulse * 0.05f) : 0.8f;

            if (entryTexture != null)
            {
                spriteBatch.Draw(entryTexture, position, new Color(255, 255, 255, menuScreen.ScreenAlpha));

                if (spriteFont != null && entryTitle.Length > 0)
                {
                    Vector2 textDims = spriteFont.MeasureString(entryTitle);

                    entryPosition += new Vector2((entryTexture.Width / 2) - (textDims.X / 2),
                        (entryTexture.Height / 2) - (textDims.Y / 2));

                    spriteBatch.DrawString(spriteFont, entryTitle, entryPosition, color);
                }
            }
            else if (spriteFont != null && entryTitle.Length > 0)
            {
                spriteBatch.DrawString(spriteFont, entryTitle, entryPosition, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
            }

        }

        public void SetRelativePosition(Vector2 relativePosition, MenuEntry entry, bool initialPosition)
        {
            Vector2 position = new Vector2(entry.position.X, entry.position.Y);
            if (entry.entryTexture != null)
                position.Y += entry.entryTexture.Height;
            SetPosition(Vector2.Add(position, relativePosition), initialPosition);
        }

        /// <summary>
        /// Sets the position of the entry
        /// </summary>
        /// <param name="position">The desired position</param>
        /// <param name="initialPosition">True if the passed position is an initial position for this entry, as in the entry will default to this position after animations and effects</param>
        public void SetPosition(Vector2 position, bool initialPosition)
        {
            if (initialPosition)
            {
                this.initialPosition = position;
            }
            this.position = position;
        }
    }
}
