using System;                                   // System contains a lot of default C# libraries
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using static GXPEngine.ECS.EntityComponent;

public class MyGame : Game
{
	Manager manager;
	public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		manager = new Manager(this);
		// Draw some things on a canvas:
		EasyDraw canvas = new EasyDraw(800, 600);
		canvas.Clear(Color.MediumPurple);
		canvas.Fill(Color.Yellow);
		canvas.Ellipse(width / 2, height / 2, 200, 200);
		canvas.Fill(50);
		canvas.TextSize(32);
		canvas.TextAlign(CenterMode.Center, CenterMode.Center);
		canvas.Text("Welcome!", width / 2, height / 2);

		// Add the canvas to the engine to display it:
		AddChild(canvas);
		Console.WriteLine("MyGame initialized");
		Entity gameObject = new Entity("circle.png");
		Rigidbody rigidbody = gameObject.addComponent<Rigidbody>(true);
		gameObject.x = 400;
		gameObject.y = 0;
		gameObject.SetOrigin(gameObject.width / 2, gameObject.height / 2);
		rigidbody.radius = gameObject.width / 2;
		rigidbody.weight = 100;
		rigidbody.position = new Vec2(600, 0);

		manager.addEntity(gameObject);

		Entity gameObject2 = new Entity("circle.png");
		gameObject2.SetOrigin(gameObject2.width / 2, gameObject2.height / 2);
		Rigidbody rigidbody1 = gameObject2.addComponent<Rigidbody>(true);
		rigidbody1.radius = gameObject2.width/2;
		rigidbody1.weight = 100;
		rigidbody1.position = new Vec2(width / 2, height / 4);
		manager.addEntity(gameObject2);

		//Bullet_Component bullet = new Bullet_Component(gameObject2);

		Entity gameObject3 = new Entity("square.png");
		gameObject3.y = height / 2+5;
		gameObject3.x = width / 2;
		gameObject3.SetOrigin(gameObject3.width / 2, gameObject3.height / 2);
		gameObject3.width = width;
		gameObject3.height = 10;

		Vec2[] points =
		{
			new Vec2(0,height/2),
			new Vec2(width,height/2),
			new Vec2(width,0),
			new Vec2(width-width/3,height/4)
		};
		int[] lines =
		{
			1,
			0,

			2,
			3
		};
		gameObject3.addComponent<PolygonCollider>(points, lines, false);
		manager.addEntity(gameObject3);
		//LevelLoader.LoadLevel("document.xml");
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