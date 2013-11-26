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
        
        protected Texture2D sprite;
        public Object(Vector2 position)
        {
            this.position = position;
        }
        //class to load content from the main game class
        public void loadContent(ContentManager content, string asset)
        {
            this.sprite = content.Load<Texture2D>("placeholder");
        }

        public void update()
        {
        }
        //draw call
        public void draw(SpriteBatch batch)
        {
            batch.Draw(sprite, position, Color.White);
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
    }
}
