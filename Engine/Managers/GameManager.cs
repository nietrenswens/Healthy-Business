using HealthyBusiness.Data;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HealthyBusiness.Engine.Managers
{
    public class GameManager
    {
        private static GameManager _gameManager = null!;
        private Scene? _nextScene;
        private Game _game = null!;
        private SoundEffectInstance? _backgroundLoop;
        private string? _currentlyPlayingMusicKey;

        public ContentManager ContentManager { get; private set; } = null!;
        public GraphicsDevice GraphicsDevice { get; private set; } = null!;
        public Random RNG { get; private set; } = null!;
        public Scene CurrentScene { get; private set; } = null!;
        public GameData GameData { get; private set; }
        public Scene? CurrentlyLoadingScene => _nextScene;

        private GameManager()
        {
            RNG = new();
            GameData = new GameData();
        }

        public static GameManager GetGameManager()
        {
            if (_gameManager == null)
            {
                _gameManager = new GameManager();
            }
            return _gameManager;
        }

        public void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice, Game game)
        {
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;
            CurrentScene = new MainMenu();
            _game = game;
        }

        public void Load(ContentManager content)
        {
            CurrentScene.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            InputManager.GetInputManager().Update();
            if (_nextScene != null)
            {
                DoSceneTransition(_nextScene);
                _nextScene = null;
            }
            CurrentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }

        public void ChangeScene(Scene newScene)
        {
            _nextScene = newScene;
        }

        public void Exit()
        {
            _game.Exit();
        }

        public void RestartGame()
        {
            GameData = new GameData();
            _nextScene = new MainMenu();
        }

        private void DoSceneTransition(Scene newScene)
        {
            _nextScene = newScene;
            CurrentScene.Unload();
            CurrentScene = newScene;
            newScene.Load(ContentManager);
        }

        public void PlayLoopingMusic(SoundEffect music, float volume, string? currentMusic = null)
        {
            if (_backgroundLoop != null && _backgroundLoop.State == SoundState.Playing && _currentlyPlayingMusicKey == currentMusic)
                return;

            StopLoopingMusic();
            _backgroundLoop = music.CreateInstance();
            _backgroundLoop.IsLooped = true;
            _backgroundLoop.Volume = volume;
            _backgroundLoop.Play();

            _currentlyPlayingMusicKey = currentMusic;
        }

        public void StopLoopingMusic()
        {
            if (_backgroundLoop != null)
            {
                _backgroundLoop.Stop();
                _backgroundLoop.Dispose();
                _backgroundLoop = null;
            }
        }
    }
}
