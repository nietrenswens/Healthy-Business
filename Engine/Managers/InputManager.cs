using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HealthyBusiness.Engine.Managers
{
    public class InputManager
    {
        private static InputManager _inputManager = null!;

        public KeyboardState LastKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }
        public MouseState LastMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public static InputManager GetInputManager()
        {
            if (_inputManager == null)
            {
                _inputManager = new InputManager();
            }
            return _inputManager;
        }

        private InputManager()
        {
            LastKeyboardState = Keyboard.GetState();
            CurrentKeyboardState = Keyboard.GetState();
            LastMouseState = Mouse.GetState();
            CurrentMouseState = Mouse.GetState();
        }

        public void Update()
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }

        public bool LeftMousePressed()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
        }

        public bool RightMousePressed()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed && LastMouseState.RightButton == ButtonState.Released;
        }

        public Vector2 GetMousePosition()
        {
            return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public bool IsMouseScrollingUp()
        {
            return CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }

        public bool IsMouseScrollingDown()
        {
            return CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }
    }
}
