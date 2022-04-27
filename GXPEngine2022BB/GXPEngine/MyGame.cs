using System;                                   // System contains a lot of default C# libraries
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.ECS;
using static GXPEngine.ECS.EntityComponent;

public class MyGame : Game
{

    Manager manager;
    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        manager = new Manager(this);

        EasyDraw canvas = new EasyDraw(800, 600, false);
        canvas.Clear(Color.MediumPurple);
        canvas.Fill(Color.Yellow);

        // Add the canvas to the engine to display it:
        AddChild(canvas);

        Player e = new Player();
        //e.addComponent<KeyboardComponent>();

        manager.addEntity(e);

        EasyDraw canvas2 = new EasyDraw(800, 100)
        {
            y = 500
        };
        canvas2.Clear(Color.Red);
        AddChild(canvas2);

        EasyDraw canvas3 = new EasyDraw(100, 100)
        {
            y = 0,
            x = 400
        };
        canvas3.Clear(Color.Blue);
        AddChild(canvas3);

        //LevelLoader.LoadLevel("document.xml");
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}