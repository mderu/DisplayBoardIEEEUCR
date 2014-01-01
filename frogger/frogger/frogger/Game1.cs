using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace frogger
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static Dictionary<string, Texture2D> sprites;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Random rand;
        

        Player player;

        public const int width = 800;
        public const int height = 600;
        public Game1()
        {
            //make sure we initialize a static field first......
            frogger.Object.allObjects = new List<Object>();


            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            Content.RootDirectory = "Content";
            rand = new Random();
            sprites = new Dictionary<string, Texture2D>();

            Row.allRows = new List<Row>();
            new Row(64*0, 2.5f);
			new Row(64*1, 2);
            new Row(64*2, 1.5f);
            new Row(64 * 3, 1, Spawns.LOG);
			//put the player at the bottom of the screen
            player = new Player(new Vector2(200, 256));
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites.Add("placeholder", this.Content.Load<Texture2D>("placeholder"));
            sprites.Add("road", this.Content.Load<Texture2D>("road"));
            player.setSprite("placeholder");
            //player.loadContent(this.Content, "test");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < Row.allRows.Count; i++)
            {
                /*
                for (int j = 0; j < allRows[i].objects.Count; j++)
                {
                    allRows[i].objects[j].update(elapsedTime);
                }
                */
                Row.allRows[i].update(elapsedTime);
            }
            player.update(elapsedTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < Row.allRows.Count; i++)
            {
                Row.allRows[i].drawRow(this.spriteBatch);
            }
            player.draw(this.spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

        }

        /// <summary>
        /// Used for giving several classes access to the width
        /// of the screen.
        /// </summary>
        /// <returns>Returns the width of the screen</returns>
        public static int getWidth()
        {
            return width;
        }
        /// <summary>
        /// The bread and butter of our graphics drawings. Instead of
        /// letting each object load in it's own Texture2D, all of them
        /// are saved in the static "sprites" map. When objects need to
        /// be drawn they get their sprite from here.
        /// </summary>
        /// <param name="s">The key to the mapped sprite.</param>
        /// <returns>The sprite mapped at the key.</returns>
        public static Texture2D getSprite(string s)
        {
            return sprites[s];
        }
    }
}
