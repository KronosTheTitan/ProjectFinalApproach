﻿using GXPEngine.GraplingHook;
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

        internal Manager manager;

        public Level()
        {
            grapplePoints = new List<GrapplePoint>();
            grapplePoints.Add(new GrapplePoint());

            manager = new Manager(Game.main);
            lines = new List<Line>();
            player = new Player()
            {
                x = 500
            };
            manager.addEntity(player);
            boxes = new List<Box>();


            //Static for testing

            lines.Add(new Line(200, 400, 700, 400));
            lines.Add(new Line(0, 200, 200, 200));
            lines.Add(new Line(200, 200, 200, 400));

            Box box = new Box(player);

            box._position = new Vec2(400, 300);

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
        }

        public static bool IsCollidingWithLine(Line l, Entity e)
        {
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