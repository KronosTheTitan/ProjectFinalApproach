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

    void LateUpdate()
    {        
        lines.ClearTransparent();
        if (!Input.GetMouseButton(0)) return;
        if (!canvas3.HitTestPoint(Input.mouseX, Input.mouseY)) return;
        Vec2 v = new Vec2(e.x + e.width / 2, e.y + e.height / 2);
        Vec2 v1 = new Vec2(canvas3.x + canvas3.width / 2, canvas3.y + canvas3.height / 2);

        Vec2 v2 = v - v1;

        v2.Normalize();
        v2 *= 300;

        v2 += v1;

        lines.Line(v1.x, v1.y, v2.x, v2.y);
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}