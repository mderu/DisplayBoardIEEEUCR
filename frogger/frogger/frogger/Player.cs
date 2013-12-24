﻿using System;
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
    class Player : frogger.Object
    {
        public Player(Vector2 position) : base(position)
        {
        }

        //player update should take kinect input
        
        public override void update(float time = .01666f)
        {
            //use keyboard input until we get kinect workin
            //W and S move between rows
            
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.A))
            {
                position.X -= 5;
            }
            else if(kb.IsKeyDown(Keys.D))
            {
                position.X += 5;
            }
            if (kb.IsKeyDown(Keys.W))
            {
                //make sure player can only go row by row
            }

        }
    }


}
