using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Engine
{
    public class Animation
    {
        public bool IsFinished { get; set; }

        protected Texture2D _texture;

        private int _frameWidth;
        private int _frameHeight;
        private int _currentFrameIndex;
        private int _time;
        private int _frameDuration;
        private int _rowPlaying;
        private bool _isLooping;

        public Animation(int frameWidth, int frameHeight, int frameDuration, int rowPlaying, bool isLooping = true)
        {
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _frameDuration = frameDuration;
            _rowPlaying = rowPlaying;
            _isLooping = isLooping;
        }

        public void SetRow(int row)
        {
            _rowPlaying = row;
            _currentFrameIndex = 0;
            _time = 0;
            IsFinished = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsFinished) return;

            _time += gameTime.ElapsedGameTime.Milliseconds;
            int totalFrames = _texture.Width / _frameWidth;

            if (_isLooping)
            {
                _currentFrameIndex = (_time / _frameDuration) % totalFrames;
            }
            else
            {
                _currentFrameIndex = _time / _frameDuration;
                if (_currentFrameIndex >= totalFrames)
                {
                    _currentFrameIndex = totalFrames - 1;
                    IsFinished = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1f)
        {
            if (IsFinished) return;

            Rectangle sourceRectangle = new Rectangle(
                _currentFrameIndex * _frameWidth,
                _rowPlaying * _frameHeight,
                _frameWidth,
                _frameHeight
            );

            Point scaledSize = new Point(
                (int)(_frameWidth * scale),
                (int)(_frameHeight * scale)
            );

            Rectangle destinationRectangle = new Rectangle(
                position.ToPoint(),
                scaledSize
            );

            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public int CurrentRow => _rowPlaying;

    }

}
