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
        static public List<Object> allObjects;
        protected Vector2 position;

        //It's cool that they don't have heights and all, because
        //all the rows are the same size.
        //
        //But these things need a width.
        
        protected string spriteKey;
        public Object(Vector2 position)
        {
            this.position = position;
            allObjects.Add(this);
        }
        //Added for convience
        public Object(int x, int y, int w = 64)
        {
            this.position = new Vector2(x, y);
            allObjects.Add(this);
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
            //we can check collisions here
        }
        public virtual void moveBy(float mX = 0, float mY = 0)
        {
            position.X = position.X + mX;
            position.Y = position.Y + mY;
        }
        //draw call
        public virtual void draw(SpriteBatch batch)
        {
            batch.Draw(Game1.getSprite(spriteKey), position, Color.White);
        }
        //return position
        public Vector2 getPosition()
        {
            return position;
        }
        public virtual void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        //simple bounding box collision
        public bool getCollisionWithObject(Vector2 newPosition)
        {
            return (position.X <= newPosition.X && position.X + getWidth() >= newPosition.X);
        }
        //we include tolerances with the collision checking so we can make it easier 
        public bool getCollisionWithObjectTolerances(Vector2 newPosition, int tolerance)
        {
            return (position.X <= newPosition.X + tolerance && newPosition.X + tolerance <= position.X + getWidth());
        }
        public int getWidth()
        {
            return Game1.getSprite(spriteKey).Width;
        }
    }
}
