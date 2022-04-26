using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Chunk
{
    public int xPos;
    public int yPos;

    public int leftX;
    public int rightX;

    public int topY;
    public int bottomY;

    public Chunk(int x,int y,int top,int bottom,int left, int right)
    {
        xPos = x;
        yPos = y;
        topY = top;
        bottomY = bottom;
        leftX = left;
        rightX = right;
    }

    public List<GameObjectECS> gameObjects = new List<GameObjectECS>();

    public List<Collider> colliders = new List<Collider>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public void Update()
    {
        GameObjectECS[] gameObjectsTemp = gameObjects.ToArray();
        foreach(GameObjectECS gameObject in gameObjectsTemp)
        {
            gameObject.Update();
        }
    }
}
