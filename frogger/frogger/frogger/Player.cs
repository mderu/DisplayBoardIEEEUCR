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
    class Player : frogger.Object
    {
        protected bool wKeyUp;
        public Player(Vector2 position)
            : base(position)
        {
            wKeyUp = true;
        }

        //player update should take kinect input

        public override void update(float time = .01666f)
        {
            //use keyboard input until we get kinect workin
            //W and S move between rows
            for (int i = 0; i < Row.allRows.Count; i++)
            {
                if (Row.allRows[i].objectInRow(this))
                {
                    bool hitObject = false;
                    for (int j = 0; j < Row.allRows[i].objects.Count(); j++ )
                    {
                        //This isn't entirely copy pasta, I had to change conditions slightly
                        //It also adds in a failsafe if we want to be lazy.
                        //We can spawn logs in one after the other, making it look like a
                        //continuous, long log, and frogger is capable of walking across them.
                        if (Row.allRows[i].isWater() && (Row.allRows[i].objects[j].getCollisionWithObjectTolerances(position, 32) ||
                                    (Row.allRows[i].objects[j].getCollisionWithObject(position) && 
                                    (j + 1 < Row.allRows[i].objects.Count() && (Row.allRows[i].objects[j + 1].getCollisionWithObject(position)) ||
                                         (j-1 > 0 && (Row.allRows[i].objects[j - 1].getCollisionWithObject(position))))
                                    )
                                )
                            )

                        {
                            hitObject = true;
                            break;
                        }
                        else if (!Row.allRows[i].isWater() && 
                                ((Row.allRows[i].objects[j].getCollisionWithObjectTolerances(position,5)) ||
                                (Row.allRows[i].objects[j].getCollisionWithObjectTolerances(position, 59)))
                            )
                        {
                            //if !water, then they just hit a car.
                            //Which is bad, so set game over
                            hitObject = true;
                            playerReset();
                            break;
                        }
                    }
                    if (!hitObject && Row.allRows[i].isWater())
                    {
                        //If they did not hit an object and the row is water
                        //they just lost, because a frog that lived his whole
                        //tadpole life in water is unable to swim.
                        position.Y += 64*5;
                    }
                    if (Row.allRows[i].isWater())
                    {
                        this.moveBy(Row.allRows[i].getSpeed() * (int)(time * 60f), 0);
                    }
                }
            }
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.A))
            {
                position.X -= 5;
            }
            else if(kb.IsKeyDown(Keys.D))
            {
                position.X += 5;
            }
            if (kb.IsKeyDown(Keys.W) && wKeyUp)
            {
                //make sure player can only go row by row
                //64 pixel increments
                position.Y -= 64;
                wKeyUp = false;
            }
            else if (kb.IsKeyUp(Keys.W))
            {
                wKeyUp = true;
            }

        }

        public void playerReset()
        {
            //reset player position
            position.Y += 64 * 5;
            position.X = 200;
        }
    }


}

