using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace frogger
{
    //Go ahead and rename Spawns if you like, it was late at 
    public enum Spawns { GATOR = 0, LOG = 1, CAR = 2, FREESPACE = 3}
    class Row : frogger.Object
    {
        public List<Object> objects;
        public static List<Row> allRows;
        
        //in pixels per update (1/60th a second)
        float speed;
        Spawns type;

        public Row(int y, float speed, Spawns tipe = Spawns.CAR) : base(new Vector2(0, y))
        {
            objects = new List<Object>();
            allRows.Add(this);
            this.speed = speed;
            type = tipe;
            if (type <= Spawns.LOG)
            {
                //need to setup like
                //water tiles
            }
            //Here we set up the row so it is already populated
            //This one is optimized for cars
            for (int i = Game1.rand.Next() % 250; i < Game1.width; i += Game1.rand.Next() % 250 + 64 * 2)
            {
                objects.Add(new Object(i, y));
                setObjectSprite();
            }

        }
        private void addObject()
        {
            if ((speed > 0 && objects[objects.Count - 1].getPosition().X > 0)
                ||
                 (speed < 0 && objects[objects.Count - 1].getPosition().X + objects[objects.Count - 1].getWidth() > Game1.getWidth()))
            {
                objects.Add(new Object(speed < 0 ? Game1.getWidth() : 0, (int)position.Y));
                setObjectSprite();
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

        //A function that is designed to see if an object is currently part of the row 
        //as virtue of y positions
        public bool objectInRow(Object obj)
        {
            //really meant to be used by a player
            return obj.getPosition().Y == getPosition().Y;
            //Look at your code Ben. Are you proud of yourself?
            /*if (obj.getPosition().Y == getPosition().Y)
            {
                return true;
            }
            return false;*/
        }
        //we have to ovveride the setposition call so that objects are taken into accout
        public override void setPosition(Vector2 newPosition)
        {
            position = newPosition;
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].setPosition(objects[i].getPosition() + new Vector2(0, 64));
            }
        }
        public void drawRow(SpriteBatch batch)
        {
            //draw tile first
            if (type == Spawns.CAR)
            {
                for (int i = 0; i < 13; i++)
                {
                    Vector2 p = position;
                    p.X += 64 * i;
                    batch.Draw(Game1.getSprite("road"), p, Color.White);
                }
            }
            if (type == Spawns.LOG || type == Spawns.GATOR)
            {
                for (int i = 0; i < 13; i++)
                {
                    Vector2 p = position;
                    p.X += 64 * i;
                    batch.Draw(Game1.getSprite("water"), p, Color.White);
                }
            }
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].draw(batch);
            }
            
        }

        //some return funciotns
        public float getSpeed()
        {
            return speed;
        }
        public bool isWater()
        {
            return type <= Spawns.LOG;
        }
        private void setObjectSprite()
        {
            switch (type)
            {
                case Spawns.CAR:
                    objects[objects.Count - 1].setSprite("placeholder");
                    break;
                case Spawns.GATOR:
                    objects[objects.Count - 1].setSprite("placeholder");
                    break;
                case Spawns.LOG:
                    objects[objects.Count - 1].setSprite("placeholder");
                    break;
                default:
                    objects[objects.Count - 1].setSprite("placeholder");
                    break;

            }
        }
    }
}
