using GXPEngine.GraplingHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine.Level
{
    public class Level
    {
        internal static List<Line> lines;

        public static Player player;
        public static List<Box> boxes;
        public static List<GrapplePoint> grapplePoints;
        public static List<PressurePlate> pressurePlates;
        public static List<Door> doors;

        internal Manager manager;

        internal SpriteBatch background;

        internal int scale = 32;

        float width;
        float height;

        public Level()
        {
            pressurePlates = new List<PressurePlate>();
            background = new SpriteBatch();

            Sprite sp2 = new Sprite("Hall.png");
            Sprite sp = new Sprite("Attic.png");
            sp2.x = 32 * 50 * 2.1f;
            sp2.y = 32 * 8 * 2.1f;
            sp.width = Mathf.Round(32 * 53);
            sp.height = Mathf.Round(32 * 7);

            sp2.width = Mathf.Round(32 * 19 * 2.1f);
            sp2.height = Mathf.Round(32 * 12 * 2.1f);

            

            width = sp.width * 2.1f;
            height = sp.height * 2.1f;

            background.AddChild(sp);
            Game.main.AddChild(sp2);
            for (int i = 0; i < 9; i++)
            {
                background.AddChild(new Sprite("Wood_Platform.png")
                {
                    y = i * scale
                });
            }
            for (int i = 1; i < 22; i++)
            {
                background.AddChild(new Sprite("Wood_Platform.png")
                {
                    x = i * scale
                });
            }
            for (int i = 10; i < 22; i++)
            {
                background.AddChild(new Sprite("Wood_Platform.png")
                {
                    y = 1 * scale,
                    x = i * scale
                });
            }
            for (int i = 1; i < 22; i++)
            {
                background.AddChild(new Sprite("Wood_Platform.png")
                {
                    y = 4 * scale,
                    x = i * scale
                });
            }

            background.SetScaleXY(2.1f);
            AddChild(background);
            background.Freeze();



            //

            grapplePoints = new List<GrapplePoint>();
            grapplePoints.Add(new GrapplePoint());

            PressurePlate pressurePlate = new PressurePlate();

            pressurePlates.Add(pressurePlate);

            manager = new Manager(Game.main);
            lines = new List<Line>();
            player = new Player();
            player._position.x = 32 * 2.1f;
            player._position.y = 32 * 2 * 2.1f;
            manager.addEntity(player);
            boxes = new List<Box>();


            //Static for testing

            lines.Add(new Line(32 * 2.1f, 32 * 2.1f, 32 * 2.1f, 32 * 4 * 2.1f));
            lines.Add(new Line(32 * 2.1f, 32 * 4 * 2.1f, 32 * 18 * 2.1f, 32 * 4 * 2.1f));

            lines.Add(new Line(32 * 2.1f, 32 * 1 * 2.1f, 32 * 10 * 2.1f, 32 * 1 * 2.1f));
            lines.Add(new Line(32 * 10 * 2.1f, 32 * 1 * 2.1f, 32 * 10 * 2.1f, 32 * 2 * 2.1f));

            lines.Add(new Line(32 * 10 * 2.1f, 32 * 2 * 2.1f, 32 * 21 * 2.1f, 32 * 2 * 2.1f));
            lines.Add(new Line(32 * 21 * 2.1f, 32 * 2 * 2.1f, 32 * 21 * 2.1f, 32 * 0 * 2.1f));

            lines.Add(new Line(32 * 21 * 2.1f, 32 * 0 * 2.1f, 32 * 25 * 2.1f, 34 * 0 * 2.1f));
            lines.Add(new Line(32 * 25 * 2.1f, 32 * 0 * 2.1f, 32 * 25 * 2.1f, 32 * 1 * 2.1f));

            lines.Add(new Line(32 * 25 * 2.1f, 32 * 1 * 2.1f, 32 * 27 * 2.1f, 32 * 1 * 2.1f));
            lines.Add(new Line(32 * 27 * 2.1f, 32 * 1 * 2.1f, 32 * 27 * 2.1f, 32 * 2 * 2.1f));

            lines.Add(new Line(32 * 27 * 2.1f, 32 * 2 * 2.1f, 32 * 31 * 2.1f, 32 * 2 * 2.1f));
            lines.Add(new Line(32 * 31 * 2.1f, 32 * 2 * 2.1f, 32 * 31 * 2.1f, 32 * 3 * 2.1f));

            lines.Add(new Line(32 * 31 * 2.1f, 32 * 3 * 2.1f, 32 * 44 * 2.1f, 32 * 3 * 2.1f));
            lines.Add(new Line(32 * 44 * 2.1f, 32 * 3 * 2.1f, 32 * 44 * 2.1f, 32 * 2 * 2.1f));

            lines.Add(new Line(32 * 44 * 2.1f, 32 * 3 * 2.1f, 32 * 44 * 2.1f, 32 * 2 * 2.1f));
            lines.Add(new Line(32 * 44 * 2.1f, 32 * 3 * 2.1f, 32 * 44 * 2.1f, 32 * 2 * 2.1f));

            lines.Add(new Line(32 * 44 * 2.1f, 32 * 2 * 2.1f, 32 * 47 * 2.1f, 32 * 2 * 2.1f));
            lines.Add(new Line(32 * 47 * 2.1f, 32 * 2 * 2.1f, 32 * 47 * 2.1f, 32 * 0 * 2.1f));

            lines.Add(new Line(32 * 47 * 2.1f, 32 * 0 * 2.1f, 32 * 48 * 2.1f, 32 * 0 * 2.1f));
            lines.Add(new Line(32 * 48 * 2.1f, 32 * 0 * 2.1f, 32 * 48 * 2.1f, 32 * 2 * 2.1f));

            lines.Add(new Line(32 * 48 * 2.1f, 32 * 2 * 2.1f, 32 * 53 * 2.1f, 32 * 2 * 2.1f));
            lines.Add(new Line(32 * 53 * 2.1f, 32 * 2 * 2.1f, 32 * 53 * 2.1f, 32 * 7 * 2.1f));

            lines.Add(new Line(32 * 53 * 2.1f, 32 * 7 * 2.1f, 32 * 49 * 2.1f, 32 * 7 * 2.1f));

            lines.Add(new Line(32 * 49 * 2.1f, 32 * 7 * 2.1f, 32 * 49 * 2.1f, 32 * 18 * 2.1f));

            //lines.Add(new Line(32 * 49 * 2.1f, 32 * 8 * 2.1f, 32 * 48 * 2.1f, 32 * 8 * 2.1f));
            lines.Add(new Line(32 * 49 * 2.1f, 32 * 18 * 2.1f, 32 * 49 * 2.1f, 32 * 7 * 2.1f));

            lines.Add(new Line(32 * 48 * 2.1f, 32 * 7 * 2.1f, 32 * 44 * 2.1f, 32 * 7 * 2.1f));
            lines.Add(new Line(32 * 48 * 2.1f, 32 * 20 * 2.1f, 32 * 48 * 2.1f, 32 * 7 * 2.1f));

            lines.Add(new Line(32 * 44 * 2.1f, 32 * 7 * 2.1f, 32 * 44 * 2.1f, 32 * 6 * 2.1f));
            lines.Add(new Line(32 * 49 * 2.1f, 32 * 7 * 2.1f, 32 * 49 * 2.1f, 32 * 8 * 2.1f));

            lines.Add(new Line(32 * 49 * 2.1f, 32 * 7 * 2.1f, 32 * 49 * 2.1f, 32 * 8 * 2.1f));
            lines.Add(new Line(32 * 44 * 2.1f, 32 * 6 * 2.1f, 32 * 42 * 2.1f, 32 * 6 * 2.1f));

            lines.Add(new Line(32 * 42 * 2.1f, 32 * 6 * 2.1f, 32 * 42 * 2.1f, 32 * 5 * 2.1f));
            lines.Add(new Line(32 * 42 * 2.1f, 32 * 5 * 2.1f, 32 * 38 * 2.1f, 32 * 5 * 2.1f));

            lines.Add(new Line(32 * 38 * 2.1f, 32 * 5 * 2.1f, 32 * 38 * 2.1f, 32 * 6 * 2.1f));
            lines.Add(new Line(32 * 38 * 2.1f, 32 * 6 * 2.1f, 32 * 37 * 2.1f, 32 * 6 * 2.1f));

            lines.Add(new Line(32 * 37 * 2.1f, 32 * 6 * 2.1f, 32 * 37 * 2.1f, 32 * 5 * 2.1f));
            lines.Add(new Line(32 * 37 * 2.1f, 32 * 5 * 2.1f, 32 * 30 * 2.1f, 32 * 5 * 2.1f));

            lines.Add(new Line(32 * 30 * 2.1f, 32 * 5 * 2.1f, 32 * 30 * 2.1f, 32 * 4 * 2.1f));
            lines.Add(new Line(32 * 30 * 2.1f, 32 * 4 * 2.1f, 32 * 25 * 2.1f, 32 * 4 * 2.1f));

            lines.Add(new Line(32 * 25 * 2.1f, 32 * 4 * 2.1f, 32 * 25 * 2.1f, 32 * 6 * 2.1f));
            lines.Add(new Line(32 * 25 * 2.1f, 32 * 6 * 2.1f, 32 * 21 * 2.1f, 32 * 6 * 2.1f));

            lines.Add(new Line(32 * 21 * 2.1f, 32 * 6 * 2.1f, 32 * 21 * 2.1f, 32 * 5 * 2.1f));
            lines.Add(new Line(32 * 21 * 2.1f, 32 * 5 * 2.1f, 32 * 18 * 2.1f, 32 * 5 * 2.1f));

            lines.Add(new Line(32 * 18 * 2.1f, 32 * 5 * 2.1f, 32 * 18 * 2.1f, 32 * 4 * 2.1f));

            //level 2

            lines.Add(new Line(32 * 50 * 2.1f, 32 * 8 * 2.1f, 32 * 69 * 2.1f, 32 * 8 * 2.1f));
            lines.Add(new Line(32 * 69 * 2.1f, 32 * 8 * 2.1f, 32 * 69 * 2.1f, 32 * 16 * 2.1f));

            lines.Add(new Line(32 * 69 * 2.1f, 32 * 16 * 2.1f, 32 * 65 * 2.1f, 32 * 16 * 2.1f));
            lines.Add(new Line(32 * 65 * 2.1f, 32 * 16 * 2.1f, 32 * 65 * 2.1f, 32 * 17 * 2.1f));
            
            lines.Add(new Line(32 * 65 * 2.1f, 32 * 17 * 2.1f, 32 * 69 * 2.1f, 32 * 17 * 2.1f));
            lines.Add(new Line(32 * 69 * 2.1f, 32 * 17 * 2.1f, 32 * 69 * 2.1f, 32 * 20 * 2.1f));

            lines.Add(new Line(32 * 69 * 2.1f, 32 * 20 * 2.1f, 32 * 63 * 2.1f, 32 * 20 * 2.1f));
            lines.Add(new Line(32 * 59 * 2.1f, 32 * 19 * 2.1f, 32 * 63 * 2.1f, 32 * 19 * 2.1f));

            lines.Add(new Line(32 * 48 * 2.1f, 32 * 20 * 2.1f, 32 * 59 * 2.1f, 32 * 20 * 2.1f));

            lines.Add(new Line(32 * 50 * 2.1f, 32 * 18 * 2.1f, 32 * 50 * 2.1f, 32 * 8 * 2.1f));
             
            lines.Add(new Line(32 * 49 * 2.1f, 32 * 18 * 2.1f, 32 * 50 * 2.1f, 32 * 18 * 2.1f));

            lines.Add(new Line(32 * 59 * 2.1f, 32 * 19 * 2.1f, 32 * 59 * 2.1f, 32 * 20 * 2.1f));
            lines.Add(new Line(32 * 63 * 2.1f, 32 * 19 * 2.1f, 32 * 63 * 2.1f, 32 * 20 * 2.1f));

            lines.Add(new Line(32 * 50 * 2.1f, 32 * 13 * 2.1f, 32 * 51 * 2.1f, 32 * 13 * 2.1f));
            lines.Add(new Line(32 * 52 * 2.1f, 32 * 13 * 2.1f, 32 * 56 * 2.1f, 32 * 13 * 2.1f));

            lines.Add(new Line(32 * 56 * 2.1f, 32 * 13 * 2.1f, 32 * 56 * 2.1f, 32 * 12 * 2.1f));
            lines.Add(new Line(32 * 55 * 2.1f, 32 * 12 * 2.1f, 32 * 56 * 2.1f, 32 * 12 * 2.1f));
            lines.Add(new Line(32 * 55 * 2.1f, 32 * 12 * 2.1f, 32 * 55 * 2.1f, 32 * 13 * 2.1f));

            lines.Add(new Line(32 * 56 * 2.1f, 32 * 15 * 2.1f, 32 * 58 * 2.1f, 32 * 15 * 2.1f));
            lines.Add(new Line(32 * 56 * 2.1f, 32 * 9 * 2.1f, 32 * 57 * 2.1f, 32 * 9 * 2.1f));
            
            lines.Add(new Line(32 * 62 * 2.1f, 32 * 13 * 2.1f, 32 * 64 * 2.1f, 32 * 13 * 2.1f));




            doors = new List<Door>();

            Door d = new Door();

            doors.Add(d);
            pressurePlate.AddDoor(d);

            Box box = new Box(player);
            box._position.x = 32 * 4 * 2.1f;
            box._position.y = 32 * 2.1f;

            boxes.Add(box);
            manager.addEntity(box);

            //

            foreach (Line line in lines)
            {
                AddChild(line);
            }

            Game.main.OnAfterStep += LateUpdate;
        }

        private void AddChild(GameObject l)
        {
            Game.main.AddChild(l);
        }

        public void LateUpdate()
        {
            /*
            float clampXMax = width - 800;
            float clampYMax = height - 600;
            Vector2 playerTransform = TransformPoint(player.x, player.y);
            background.x = -Mathf.Clamp(playerTransform.x - (game.width - player.width) * .5f, 0, clampXMax);
            background.y = -Mathf.Clamp(playerTransform.y - (game.height - player.height) * .5f, 0, clampYMax);
            */
            Game.main.x = -player._position.x + 400;
            Game.main.y = -player._position.y + 300;
        }

        public static bool IsCollidingWithLine(Line l, Entity e)
        {
            if (!l.active) return false;
            return l.isHorizontal ?
                ((l.x1 < e._position.x && e._position.x < l.x2) ||
                (l.x1 < e._position.x + e.width && e._position.x + e.width < l.x2) ||
                (e._position.x < l.x1 && l.x1 < e._position.x + e.width) ||
                (e._position.x < l.x2 && l.x2 < e._position.x + e.width)) &&
                e._position.y < l.y1 && l.y1 < e._position.y + e.height :
                ((l.y1 < e._position.y && e._position.y < l.y2) ||
                (l.y1 < e._position.y + e.height && e._position.y + e.height < l.y2) ||
                (e._position.y < l.y1 && l.y1 < e._position.y + e.height) ||
                (e._position.y < l.y2 && l.y2 < e._position.y + e.height)) &&
                e._position.x < l.x1 && l.x1 < e._position.x + e.width;
        }
        public static bool RayCastLine(Line line)
        {
            return false;
        }

        public static Line GetPriorityCollision(List<Line> collidedLines, Entity e)
        {
            if (collidedLines.Count == 0) return null;
            if (collidedLines.Count == 1) return collidedLines[0];

            float minCorrection = 10000;

            Line chosenLine = collidedLines[0];

            foreach (Line l in collidedLines)
            {
                float correction = Mathf.Min(1000, l.isHorizontal ?
                    e._velocity.y > 0 ?
                    (e._position.y + e.height - l.midPoint.y) / e._velocity.y :
                    (e._position.y - l.midPoint.y) / e._velocity.y :
                    e._velocity.x > 0 ? (e._position.x + e.width - l.midPoint.x) / e._velocity.x :
                    (e._position.x - l.midPoint.x) / e._velocity.x);
                if (correction < minCorrection)
                {
                    minCorrection = correction;
                    chosenLine = l;
                }

            }
            return chosenLine;
        }
        /// <summary>
        /// Check if the passed in entity runs into any collisions
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool CheckCollisions(Entity e)
        {
            //create a list of all the lines the entity collided with.

            List<Line> collidedLines = new List<Line>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (IsCollidingWithLine(lines[i], e))
                {
                    collidedLines.Add(lines[i]);
                }
            }
            if (collidedLines.Count <= 0) return false;

            Line chosenLine = GetPriorityCollision(collidedLines, e);

            bool isGrapple = e.GetType() == typeof(Player) && ((Player)e).isGrapple;

            if (chosenLine.isHorizontal)
            {
                if (isGrapple)
                {
                    if (e._oldVelocity.y >= 0)
                    {
                        e._position.y = chosenLine.y1 - e.height;
                    }
                    else
                    {
                        e._position.y = chosenLine.y1;
                    }
                    e._velocity.y = 0;
                }
                else
                {
                    if (e._velocity.y >= 0)
                    {
                        e._position.y = chosenLine.y1 - e.height;
                    }
                    else
                    {
                        e._position.y = chosenLine.y1;
                    }
                }
            }
            else
            {
                if (isGrapple)
                {
                    if (e._oldVelocity.x > 0)
                    {
                        e._position.x = chosenLine.x1 - e.width;
                    }
                    else
                    {
                        e._position.x = chosenLine.x1;
                    }
                    e._velocity.x = 0;
                    //e._velocity.x = 0 - e._velocity.x / 2;
                }
                else
                {
                    if (e._velocity.x > 0)
                    {
                        e._position.x = chosenLine.x1 - e.width;
                    }
                    else if (e._velocity.x < 0)
                    {
                        e._position.x = chosenLine.x1;
                    }
                    else
                    {
                        if (e._oldVelocity.x > 0)
                        {
                            e._position.x = chosenLine.x1 - e.width;
                        }
                        else
                        {
                            e._position.x = chosenLine.x1;
                        }
                    }
                    //e._velocity.x = 0 - e._velocity.x / 2;
                }
            }
            if (collidedLines.Count > 1)
            {
                CheckCollisions(e);
            }
            return true;
        }
    }
}
