using System;                                   // System contains a lot of default C# libraries
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using static GXPEngine.ECS.EntityComponent;
using GXPEngine.Systems;

public class MyGame : Game
{
    public Manager manager;

    public Camera camera;
    public MyGame() : base(800, 600, false, pPixelArt: true)     // Create a window that's 800x600 and NOT fullscreen
    {
        manager = new Manager(this);
        Console.WriteLine("MyGame initialized");
        camera = new Camera(0, 0, 800, 600);
        AddChild(camera);

        //Bullet_Component bullet = new Bullet_Component(gameObject2);

        Entity gameObject3 = new Entity("square.png");
        gameObject3.y = height / 2 + 5;
        gameObject3.x = width / 2;
        gameObject3.SetOrigin(gameObject3.width / 2, gameObject3.height / 2);
        gameObject3.width = width;
        gameObject3.height = 10;

        /*Vec2[] points =
        {
            new Vec2(0,1),
            new Vec2(9,1),
            new Vec2(9,2),
            new Vec2(20,2),
            new Vec2(20,0),
            new Vec2(24,0),
            new Vec2(24,1),
            new Vec2(26,1),
            new Vec2(26,2),
            new Vec2(30,2),
            new Vec2(30,3),
            new Vec2(43,3),
            new Vec2(43,2),
            new Vec2(46,2),
            new Vec2(46,1),
            new Vec2(47,1),
            new Vec2(47,2),
            new Vec2(52,2),
            new Vec2(52,7),
            new Vec2(48,7),
            new Vec2(48,8),
            new Vec2(47,8),
            new Vec2(47,7),
            new Vec2(43,7),
            new Vec2(43,6),
            new Vec2(41,6),
            new Vec2(41,5),
            new Vec2(37,5),
            new Vec2(37,6),
            new Vec2(36,6),
            new Vec2(36,5),
            new Vec2(29,5),
            new Vec2(29,4),
            new Vec2(24,4),
            new Vec2(24,6),
            new Vec2(20,6),
            new Vec2(20,5),
            new Vec2(21,5),
            new Vec2(21,4),
            new Vec2(0,4)
        };
        Console.WriteLine(points.Length);
        for(int i = 0; i<points.Length;i++)
        {
            points[i] = points[i] * 64;
        }
        int[] lines =
        {
            0,
            1,

            1,
            2,

            2,
            3,

            3,4,
            4,5,
            5,6,
            6,7,
            7,8,
            8,9,
            9,10,
            10,11,
            11,12,
            12,13,
            13,14,
            14,15,
            15,16,
            16,17,
            17,18,
            18,19,
            19,20,
            20,21,
            21,22,
            22,23,
            23,24,
            24,25,
            25,26,
            26,27,
            27,28,
            28,29,
            29,30,
            30,31,
            31,32,
            32,33,
            33,34,
            34,35,
            35,36,
            36,37,
            37,38,
            38,39,
            39,0
        };
        gameObject3.addComponent<PolygonCollider>(points, lines, false);*/
        manager.addEntity(gameObject3);

        LevelLoader.LoadLevel(this);
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        CollisionManager.Instance.OnStep();
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}