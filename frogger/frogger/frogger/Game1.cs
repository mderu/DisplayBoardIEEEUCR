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
        static SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Random rand;
        private int top;
        

        Player player;
        public const int width = 800;
        public const int height = 600;
        public const int startingLives = 5;
        public const int startingX = 200;
        public const int startingY = 256;
        public const int MAX_SPEED = 3;

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

            generateNewLevel();
            
            //put the player at the bottom of the screen
            player = new Player(new Vector2(startingX, startingY), startingLives);
        }

        public void generateNewLevel()
        {
            
            //generate a new starting level
            Row.allRows = new List<Row>();
            //generateChunk();
            new Row(64 * 0, 2.5f, Spawns.CAR);
            new Row(64 * 1, 0.5f, Spawns.CAR);
            new Row(64 * 2, -2.5f, Spawns.LOG);
            new Row(64 * 3, 1.0f, Spawns.CAR);
            top = 0;
        }

        public float getRandomSpeed()
        {
            Random rnd = new Random();
            float speed = (float)((rnd.NextDouble() * (MAX_SPEED*2)) - MAX_SPEED);
            return speed;
        }

        public void generateChunk()
        {
            Random rnd = new Random();
            int type = rnd.Next(0, 3);
            if (type == 0) //Generate stream
            {
                new Row(64 * (top-2), getRandomSpeed(), Spawns.LOG);
                new Row(64 * (top - 1), getRandomSpeed(), Spawns.LOG);
                new Row(64 * top, getRandomSpeed(), Spawns.LOG);
                top = -2;
            }
            else if (type == 1) //Generate Road
            {
                new Row(64 * (top-2), getRandomSpeed(), Spawns.CAR);
                new Row(64 * (top-1), getRandomSpeed(), Spawns.CAR);
                new Row(64 * 0, getRandomSpeed(), Spawns.CAR);
                top = -2;
            }
            else if (type == 2) //Generate Safe spot
            {
                new Row(64 * 0, 0f, Spawns.FREESPACE);
                top = 0;
            }

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
            font = Content.Load<SpriteFont>("Segoe");
            sprites.Add("placeholder", this.Content.Load<Texture2D>("placeholder"));
            sprites.Add("road", this.Content.Load<Texture2D>("road"));
            sprites.Add("water", this.Content.Load<Texture2D>("water"));
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


        protected void reset()
        {
            ResetElapsedTime();
            player.playerReset();
            generateNewLevel();
        }



        /// <summary>
        /// Checks the height to see if the view needs to be moved up
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected  void checkHeight()
        {
            if (player.getPosition().Y < height / 2)
            {
                //shift everything downward and create a new row
                //and delete the last row
                for (int i = 0; i < Row.allRows.Count; i++)
                {
                    Row.allRows[i].setPosition(Row.allRows[i].getPosition() + new Vector2(0, 64));
                }
                player.setPosition(player.getPosition() + new Vector2(0, 64));
                //randomly generate a row
                //new Row(0, 1, Spawns.LOG);
                top++;
                if (top > 0)
                {
                    generateChunk();
                }
            }
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
                Row.allRows[i].update(elapsedTime);
            }

            player.update(elapsedTime);
            base.Update(gameTime);
			//So here we should check if the player has reached a certain height
            checkHeight();
            
            if (player.getPosition().X > width || player.isDead())
            {
                //player just died
                reset();
            }
            
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
            //draw score and lives
            //use the difference at the bottom of the screen for this
            spriteBatch.DrawString(font, "Score: " + (player.getNumSteps() * 5), new Vector2(0, (height-30)), Color.Red);
            spriteBatch.DrawString(font, "Lives Remaining: " + player.getLives(), new Vector2(200, (height-30)), Color.Red);
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
