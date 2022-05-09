using System;                                   // System contains a lot of default C# libraries
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	public List<GameObjectECS> gameObjects = new List<GameObjectECS>();
	public MyGame() : base(800, 600, false)		// Create a window that's 800x600 and NOT fullscreen
	{
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
		ChunkLoader.Instance.Start();
		GameObjectECS gameObject = new GameObjectECS();
		gameObject.transform = new Vec2(400, 0);
		gameObjects.Add(gameObject);
		//gameObject.transform = new Vec2(width / 2, height / 2);
		Player player = new Player(gameObject);

		GameObjectECS gameObject2 = new GameObjectECS();
		Component_Sprite sprite = new Component_Sprite(gameObject2, "circle.png");
		Rigidbody rigidbody = new Rigidbody(gameObject2);
		rigidbody.radius = sprite.sprite.width / 2;
		rigidbody.weight = 100;
		gameObject2.transform = new Vec2(width / 2, height / 4);

		//Bullet_Component bullet = new Bullet_Component(gameObject2);

		GameObjectECS gameObject3 = new GameObjectECS();
		gameObject3.objectStatic = true;

		Vec2[] points =
		{
			new Vec2(0,height/2),
			new Vec2(width,height/2),
			new Vec2(width,0),
			new Vec2(width-width/8,height/4)
		};
		int[] lines =
		{
			1,
			0,

			2,
			3
		};
		Collider collider = new PolygonCollider(gameObject3, points, lines);
		//LevelLoader.LoadLevel("document.xml");
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		UpdateManagerECS.Instance.Update();
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}