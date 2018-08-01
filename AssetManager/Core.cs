using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AssetManagerDemo {
    public class Core : Game {
        //public static GameServiceContainer services;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        AssetManager assets;

        Sprite sprite;
        Sprite sprite2;
        Sprite sprite3;
        Sprite sprite4;

        public Core(int w, int h, bool fullScreen, bool vSync) {
            graphics = new GraphicsDeviceManager(this);

            //services = Services;
            //services.AddService(typeof(GraphicsDeviceManager), graphics);
            Services.AddService(typeof(GraphicsDeviceManager), graphics);

            //assets = new AssetManager(services);
            assets = new AssetManager(Services);

            if (w > 0 || h > 0) {
                GeneralUtils.SetResolution(this, w, h, fullScreen, vSync);
            }
            else {
                //GeneralUtils.SetResolution(this, 800, 600, false, true);
                GeneralUtils.SetResolution(this, 1024, 768, false, true);
                //GeneralUtils.SetResolution(this, 1366, 768, false, true);
                //GeneralUtils.SetResolution(this, 1920, 1080, true, true);
            }

            GeneralUtils.SetLogFile($"log_{DateTime.UtcNow.ToFileTimeUtc()}.txt");
        }


        protected override void Initialize() {
            base.Initialize();
        }


        protected override void LoadContent() {        
            spriteBatch = new SpriteBatch(GraphicsDevice);

            assets.LoadAll(typeof(Texture2D), "Textures", SearchOption.AllDirectories);

            sprite = new Sprite(assets.Get<Texture2D>("test"), new Vector2(0, 0), new Rectangle(1, 1, 32, 32), Color.Red, 0f, 0f);
            sprite2 = new Sprite(assets.Get<Texture2D>("test"), new Vector2(33, 0), new Rectangle(667, 1, 32, 32), Color.White, 0f, 0f);

            SpriteSheet spriteSheet = new SpriteSheet(assets.Get<Texture2D>("test"), GraphicsDevice);
            //spriteSheet.MapSprite("1", new Rectangle(1, 1, 32, 32));
            //sprite3 = spriteSheet.GetSprite("1");
            //sprite3.Position = new Vector2(50, 50);
            spriteSheet.MapSpritesUniformly("test", 32, 32, 1, 1, 5);
            sprite3 = spriteSheet.GetSprite("test_4_0");
            sprite3.Position = new Vector2(100, 100);

            sprite4 = new Sprite(assets.Get<Texture2D>("spaceCraft3_SE"), new Vector2(200, 200));
        }


        protected override void UnloadContent() { }


        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(27, 30, 41));

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, null);
            sprite.Draw(spriteBatch);
            sprite2.Draw(spriteBatch);
            sprite3.Draw(spriteBatch);
            sprite4.Draw(spriteBatch);
            spriteBatch.End();

            Window.Title = $"Asset Manager Demo / {(1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.0")} FPS / {(GC.GetTotalMemory(false) / 1048576.0f).ToString("F")} MB Memory alloc.";

            base.Draw(gameTime);
        }
    }
}
