using System;                                   // System contains a lot of default C# libraries
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.ECS;
using static GXPEngine.ECS.EntityComponent;

public class MyGame : Game
{

    Manager manager;

    EasyDraw lines, canvas3;

    Player e;
    Box e1;

    List<Line> collisionLines = new List<Line>();

    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        manager = new Manager(this);

        EasyDraw canvas = new EasyDraw(800, 600, false);
        canvas.Clear(Color.MediumPurple);
        canvas.Fill(Color.Yellow);

        // Add the canvas to the engine to display it:
        AddChild(canvas);

        e = new Player();
        //e.addComponent<KeyboardComponent>();

        manager.addEntity(e);

        e1 = new Box(e);
        //e.addComponent<KeyboardComponent>();

        manager.addEntity(e1);

        EasyDraw canvas2 = new EasyDraw(800, 100)
        {
            y = 500
        };
        canvas2.Clear(Color.Red);
        AddChild(canvas2);

        canvas3 = new EasyDraw(100, 100)
        {
            y = 0,
            x = 300
        };
        canvas3.Clear(Color.Blue);
        AddChild(canvas3);

        lines = new EasyDraw(800, 600, false);
        lines.SetColor(255, 0, 255);
        lines.StrokeWeight(5);
        AddChild(lines);

        //LevelLoader.LoadLevel("document.xml");

        collisionLines.Add(new Line(200, 400, 700, 400));
        collisionLines.Add(new Line(0, 200, 200, 200));
        collisionLines.Add(new Line(200, 200, 200, 400));

        foreach (Line line in collisionLines)
        {
            AddChild(line);
        }

        OnAfterStep += LateUpdate;
    }

    public bool IsCollidingWithLine(Line l)
    {
        return l.isHorizontal ? 
            ((l.x1 < e.x && e.x < l.x2) || 
            (l.x1 < e.x + e.width && e.x + e.width < l.x2) || 
            (e.x < l.x1 && l.x1 < e.x + e.width) || 
            (e.x < l.x2 && l.x2 < e.x + e.width)) && 
            e.y < l.y1 && l.y1 < e.y + e.height : 
            ((l.y1 < e.y && e.y < l.y2) || 
            (l.y1 < e.y + e.height && e.y + e.height < l.y2) || 
            (e.y < l.y1 && l.y1 < e.y + e.height) || 
            (e.y < l.y2 && l.y2 < e.y + e.height)) && 
            e.x < l.x1 && l.x1 < e.x + e.width;
    }

    public Line GetPriorityCollision(List<Line> collidedLines)
    {
        if (collidedLines.Count == 0) return null;
        if (collidedLines.Count == 1) return collidedLines[0];

        float minCorrection = 10000;

        Line chosenLine = collidedLines[0];

        foreach (Line l in collidedLines)
        {
            float correction = Mathf.Min(1000, l.isHorizontal ? 
                e._velocity.y > 0 ? 
                (e.y + e.height - l.midPoint.y) / e._velocity.y : 
                (e.y - l.midPoint.y) / e._velocity.y : 
                e._velocity.x > 0 ? (e.x + e.width - l.midPoint.x) / e._velocity.x : 
                (e.x - l.midPoint.x) / e._velocity.x);
            if (correction < minCorrection)
            {
                minCorrection = correction;
                chosenLine = l;
            }

        }
        return chosenLine;
    }

    public bool CheckCollisions(List<Line> currLines)
    {

        List<Line> collidedLines = new List<Line>();
        for (int i = 0; i < currLines.Count; i++)
        {
            if (IsCollidingWithLine(currLines[i]))
            {
                collidedLines.Add(currLines[i]);
            }
        }
        if (collidedLines.Count <= 0) return false;

        //let chosenLine = this.GetPriorityCollision(collidedLines);
        Line chosenLine = GetPriorityCollision(collidedLines);

        if (chosenLine.isHorizontal)
        {
            if (isGrapple)
            {
                if (e._oldVelocity.y >= 0)
                {
                    e.y = chosenLine.y1 - e.height;
                }
                else
                {
                    e.y = chosenLine.y1;
                }
                e._velocity.y = 0;
            } else
            {
                if (e._velocity.y >= 0)
                {
                    e.y = chosenLine.y1 - e.height;
                }
                else
                {
                    e.y = chosenLine.y1;
                }
            }
        } else
        {
            if (isGrapple)
            {
                if (e._oldVelocity.x > 0)
                {
                    e.x = chosenLine.x1 - e.width;
                }
                else
                {
                    e.x = chosenLine.x1;
                }
                e._velocity.x = 0;
                //e._velocity.x = 0 - e._velocity.x / 2;
            }
            else
            {
                if (e._velocity.x > 0)
                {
                    e.x = chosenLine.x1 - e.width;
                }
                else if (e._velocity.x < 0)
                {
                    e.x = chosenLine.x1;
                }
                else
                {
                    if (e._oldVelocity.x > 0)
                    {
                        e.x = chosenLine.x1 - e.width;
                    }
                    else
                    {
                        e.x = chosenLine.x1;
                    }
                }
                //e._velocity.x = 0 - e._velocity.x / 2;
            }
        }
        if (collidedLines.Count > 1)
        {
            CheckCollisions(currLines);
        }
        return true;
    }

    void Update()
    {
        if (isGrapple)  {
            e.ignore = true;
        } else
        {
            e.ignore = false;

        }
        //CheckCollisions(collisionLines);
    }


    Vec2 rope;
    Vec2 grapple;

    float ropeAngleVelocity;

    Vec2 grappleOrign;

    float ropeAngle;

    float ropeLength;

    float ropeLengthOld;

    public float NextGrappleLenght()
    {
        if (grappleOrign.Length() < ropeLength) return grappleOrign.Length();
        if (ropeLengthOld >= ropeLength) return ropeLengthOld *= .97f;
        return ropeLength;
    }

    public float NextRopeAccelerationInput()
    {
        //Radians clamp
        if (ropeAngle > -0.872664626) return 0f;
        if (ropeAngle < -2.2689280276) return 0f;
        if (!(Input.GetKey(Key.D) ^ Input.GetKey(Key.A))) return 0f;
        return Input.GetKey(Key.A) ? -0.1f : 0.1f;
    }

    void UpdateGrapple()
    {
        isGrapple = false;
        if (Input.GetMouseButtonDown(0))
        {
            rope = new Vec2(e.x + e.width / 2, e.y + e.height / 2);
            grapple = new Vec2(canvas3.x + canvas3.width / 2, canvas3.y + canvas3.height / 2);

            ropeAngleVelocity = -.005f * e._velocity.x;

            grappleOrign = rope - grapple;

            ropeAngle = grappleOrign.GetAngleRadians() - Mathf.PI;

            ropeLength = 300;

            ropeLengthOld = grappleOrign.Length();
        }
        lines.ClearTransparent();
        if (!Input.GetMouseButton(0)) return;
        if (!canvas3.HitTestPoint(Input.mouseX, Input.mouseY)) return;
        //Console.WriteLine(NextRopeAccelerationInput());
        float ropeAcceleration = -.005f * (Mathf.Cos(ropeAngle) + NextRopeAccelerationInput());
        //Console.WriteLine(ropeAcceleration);
        ropeAngleVelocity += ropeAcceleration;
        //Console.WriteLine(ropeAngle);
        ropeAngle += ropeAngleVelocity;

        //Console.WriteLine(ropeAngleVelocity);

        ropeAngleVelocity *= .99f;

        Vec2 v = Vec2.GetUnitVectorRad(ropeAngle - Mathf.PI);

        v *= NextGrappleLenght();

        left = ropeAngleVelocity > 0;

        //Console.WriteLine(ropeAngleVelocity);

        rope = grapple + v;

        Vec2 speed = rope - new Vec2(e.x + e.width / 2, e.y + e.height / 2);
        e._velocity = speed;

        lines.Line(grapple.x, grapple.y, rope.x, rope.y);

        isGrapple = true;
    }

    bool left = false;
    bool isGrapple = false;

    void LateUpdate()
    {
        UpdateGrapple();

        if (CheckCollisions(collisionLines))
        {
            if (!Input.GetMouseButton(0)) return;
            ropeAngleVelocity = 0;
            rope = new Vec2(e.x + e.width / 2, e.y + e.height / 2);
            grappleOrign = rope - grapple;

            ropeAngle = grappleOrign.GetAngleRadians() - Mathf.PI;
            ropeLengthOld = grappleOrign.Length();
        }
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}