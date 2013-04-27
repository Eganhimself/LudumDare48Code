using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace GameJamBeta
{
   public class InputSystem
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        public bool MenuUp
        {
            get { return IsNewPressedKey(Keys.Up); }
        }
        public bool PadMenuUp
        {
            get { return IsNewButtonPressed(Buttons.DPadUp); }
        }

        public bool MenuDown
        {
            get { return IsNewPressedKey(Keys.Down); }
        }
        public bool PadMenuDown
        {
            get { return IsNewButtonPressed(Buttons.DPadDown); }

        }
        public bool MenuSelect
        {
            get { return IsNewPressedKey(Keys.Enter); }
        }
        public bool PadMenuSelect
        {
            get { return IsNewButtonPressed(Buttons.Start); }
        }
        public bool MenuCancel
        {
            get { return IsNewPressedKey(Keys.Escape); }
        }
        public bool PauseGame
        {
            get { return IsNewPressedKey(Keys.Escape); }
        }
        public bool TalentScreen
        {
            get { return IsNewPressedKey(Keys.T); }
        }
        public bool MoveUp
        {
            get { return IsPressedKey(Keys.Up); }
        }
        public bool MoveDown
        {
            get { return IsPressedKey(Keys.Down); }
        }
        public bool MoveLeft
        {
            get { return IsPressedKey(Keys.Left); }
        }
        public bool MoveRight
        {
            get { return IsPressedKey(Keys.Right); }
        }
        public bool Fire
        {
            get { return IsPressedKey(Keys.Space); }
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousGamePadState = currentGamePadState;
            //currentGamePadState = GamePad.GetState(playerIndex);
        }

        private bool IsNewPressedKey(Keys key)
        {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
        private bool IsPressedKey(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        private bool IsNewButtonPressed(Buttons button)
        {
            return previousGamePadState.IsButtonUp(button) && currentGamePadState.IsButtonDown(button);
        }
        private bool IsPressedButton(Buttons button)
        {
            return currentGamePadState.IsButtonDown(button);
        }


    }
}
