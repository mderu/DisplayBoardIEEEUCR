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

namespace frogger
{
    class Object
    {
        protected Vector2 position;

        //It's cool that they don't have heights and all, because
        //all the rows are the same size.
        //
        //But these things need a width.
        
        protected string spriteKey;
        public Object(Vector2 position)
        {
            this.position = position;
        }
        //Added for convience
        public Object(int x, int y, int w = 64)
        {
            this.position = new Vector2(x, y);
        }

        //class to load content from the main game class
        public void loadContent(ContentManager content, string asset)
        {
            //Deprecated due to spawning new objects
            //It is impossible to load all of the sprites
            //at the start of the program into objects, because the
            //objects get spawned later. Instead, I have decided to
            //keep the sprites in Game1, with a static 
            //Map/Dictionary to give newly spawned objects access
            //to their sprites.
        }
        public void setSprite(string s)
        {
            spriteKey = s;
        }

        public virtual void update(float time = .01666f)
        {

        }
        public virtual void moveBy(int mX = 0, int mY = 0)
        {
            position.X = position.X + mX;
            position.Y = position.Y + mY;
        }
        //draw call
        public void draw(SpriteBatch batch)
        {
            batch.Draw(Game1.getSprite(spriteKey), position, Color.White);
        }
        //return position
        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public int getWidth()
        {
            return Game1.getSprite(spriteKey).Width;
        }
    }
}
