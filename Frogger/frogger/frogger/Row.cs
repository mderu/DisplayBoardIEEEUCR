using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace frogger
{
    public enum Spawns { GATOR, CAR, LOG }
    class Row : frogger.Object
    {
        public List<Object> objects;
        //in pixels per update (1/60th a second)
        int speed;
        bool isWater;

        public Row(int y, int speed, bool agua = false) : base(new Vector2(0, y))
        {
            objects = new List<Object>();
            objects.Add(new Object(new Vector2(0, y)));
            objects[objects.Count - 1].setSprite("placeholder");
            this.speed = speed;
            isWater = agua;
        }
        private void addObject()
        {
            if ((speed > 0 && objects[objects.Count - 1].getPosition().X > 0)
                ||
                 (speed < 0 && objects[objects.Count - 1].getPosition().X + objects[objects.Count - 1].getWidth() > Game1.getWidth()))
            {
                objects.Add(new Object(speed < 0 ? Game1.getWidth() : 0, (int)position.Y));
                objects[objects.Count - 1].setSprite(isWater ? "placeholder" : "placeholder");
                objects[objects.Count - 1].moveBy(-objects[objects.Count - 1].getWidth(), 0);
            }
        }
        public override void update(float time = .01666f)
        {
            bool createObject = true;
            
            if (objects.Count < 4)
            {
                addObject();
                System.Diagnostics.Debug.WriteLine("ForcedCreation");
            }
            for (int i = 0; i < objects.Count; i++)
            {
                //Deletes all logs/cars at the end of the screen (if moving left)
                if (speed < 0 && objects[i].getPosition().X + objects[i].getWidth() < 0)
                {
                    objects.RemoveAt(i);
                    objects.TrimExcess();
                    System.Diagnostics.Debug.WriteLine("Removed");
                    continue;
                }
                //Deletes all logs/cars at the end of the screen (if moving right)
                else if (speed > 0 && objects[i].getPosition().X > Game1.getWidth())
                {
                    objects.RemoveAt(i);
                    objects.TrimExcess();
                    System.Diagnostics.Debug.WriteLine("RemovedHere");
                    continue;
                }
                else if (speed > 0 && objects[i].getPosition().X < 0)
                {
                    //If there is an object moving right, and it's x is still less than zero,
                    //we cannot create a new log without them overlapping.
                    createObject = false;
                }
                else if (speed < 0 && objects[i].getPosition().X + objects[i].getWidth() > Game1.getWidth())
                {
                    //Same as previous, except the opposite way.
                    createObject = false;
                }

                objects[i].moveBy(speed * (int)(time * 60f), 0);

            }
            if (createObject && Game1.rand.NextDouble() < .005 * Math.Abs(speed))
            {
                addObject();
            }
        }

    }
}