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

        EasyDraw canvas2 = new EasyDraw(800, 100)
        {
            y = 500
        };
        canvas2.Clear(Color.Red);
        AddChild(canvas2);

        canvas3 = new EasyDraw(100, 100)
        {
            y = 0,
            x = 400
        };
        canvas3.Clear(Color.Blue);
        AddChild(canvas3);

        lines = new EasyDraw(800, 600, false);
        lines.SetColor(255, 0, 255);
        lines.StrokeWeight(5);
        AddChild(lines);

        //LevelLoader.LoadLevel("document.xml");

        OnAfterStep += LateUpdate;
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
    }


    Vec2 rope;
    Vec2 grapple;

    float ropeAngleVelocity;

    Vec2 grappleOrign;

    float ropeAngle;

    float ropeLength;

    float ropeLengthOld;

    public float easeInOutQuad(float x, float t, float b, float c, float d) 
    {
        if ((t/=d/2) < 1) return c/2*t* t + b;
        return -c/2 * ((--t)*(t-2) - 1) + b;
    }

void LateUpdate()
    {        
        if (Input.GetMouseButtonDown(0))
        {

            rope = new Vec2(e.x + e.width / 2, e.y + e.height / 2);
            grapple = new Vec2(canvas3.x + canvas3.width / 2, canvas3.y + canvas3.height / 2);

            ropeAngleVelocity = -.005f  * e._velocity.x;

            grappleOrign = rope - grapple;

            ropeAngle = grappleOrign.GetAngleRadians() - Mathf.PI;

            ropeLength = 300;

            ropeLengthOld = grappleOrign.Length();
        }
        lines.ClearTransparent();
        if (!Input.GetMouseButton(0)) return;
        if (!canvas3.HitTestPoint(Input.mouseX, Input.mouseY)) return;

        float ropeAcceleration = -.005f * (Mathf.Cos(ropeAngle) + (Input.GetKey(Key.D) ^ Input.GetKey(Key.A) ? Input.GetKey(Key.A) ? -1 : 1 : 0));

        ropeAngleVelocity += ropeAcceleration;
        ropeAngle += ropeAngleVelocity;

        ropeAngleVelocity *= .99f;

        Vec2 v = Vec2.GetUnitVectorRad(ropeAngle - Mathf.PI);

        //if (grappleOrign.Length() < ropeLength)
        //{
        //    v *= grappleOrign.Length();
        //} else
        //{
        //    //Console.WriteLine(ropeLengthOld + " " + grappleOrign.Length());
        //    if (ropeLengthOld >= ropeLength)
        //    {
        //        //float SiegFriet = grappleOrign.Length() - ropeLength;
        //        //float Alfred = ropeLengthOld - ropeLength - 20;
        //        //float Wilma = SiegFriet / 100;
        //        //Console.WriteLine(Alfred);
        //        //Console.WriteLine(SiegFriet);
        //        //float Leopold = (Alfred - SiegFriet) / SiegFriet;
        //        //Console.WriteLine("test");
        //        //Console.WriteLine(Leopold);
        //        //ropeLengthOld += Leopold * 15;
        //        ropeLengthOld *= .97f;
        //        v *= ropeLengthOld;

        //    } else
        //    {
        //        v *= ropeLength;
        //    }
        //}
        if (grappleOrign.Length() < ropeLength)
        {
            v *= grappleOrign.Length();
        }
        else
        {
            if (ropeLengthOld >= ropeLength)
            {
                ropeLengthOld *= .97f;
                v *= ropeLengthOld;
            }
            else
            {
                v *= ropeLength;
            }
        }


        rope = grapple + v;

        Vec2 speed = rope - new Vec2(e.x, e.y);
        e._velocity = speed;

        lines.Line(grapple.x, grapple.y, rope.x, rope.y);
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}