using System;                                   // System contains a lot of default C# libraries
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.ECS;
using static GXPEngine.ECS.EntityComponent;
using GXPEngine.Level;

public class MyGame : Game
{
    /*
    Manager manager;

    EasyDraw lines, canvas3;

    Player e;
    Box e1;

    List<Line> collisionLines = new List<Line>();
    */

    Level level;
    public static Camera camera;

    public MyGame() : base(800, 600, false, false, -1, -1, true)     // Create a window that's 800x600 and NOT fullscreen
    {
        targetFps = 60;
        level = new Level();
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}