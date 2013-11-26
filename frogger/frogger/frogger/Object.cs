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
        public Object()
        {
            position = new Vector2(50, 50);
        }
        //class to load content from the main game class
        public void LoadContent(ContentManager content, string asset)
        {
            this.sprite = content.Load<Texture2D>("content/placeholder.png");
        }

        public void update()
        {
        }

    }
}
