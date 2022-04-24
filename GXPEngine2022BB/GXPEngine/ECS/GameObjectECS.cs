﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class GameObjectECS
{
    List<Component> components = new List<Component>();
    List<GameObject> childeren = new List<GameObject>();
    GameObject parent = null;

    public Chunk chunk;
    public Chunk newChunk;

    public Vec2 transform = new Vec2();
    public Vec2 oldTransform;
    public Vec2 velocity;
    public GameObjectECS()
    {
        chunk = ChunkLoader.Instance.chunks[(int)transform.x / ChunkLoader.Instance.chunkSize, (int)transform.y / ChunkLoader.Instance.chunkSize];
        chunk.gameObjects.Add(this);

        ChunkLoader.Instance.LateUpdates += LateUpdate;
    }
    public List<Component> GetComponent(Type type)
    {
        List<Component> output = new List<Component>();
        foreach(Component component in components)
        {
            if(component.GetType() == type)
            {
                output.Add(component);
            }
        }
        return output;
    }
    public List<Component> GetComponentsInChilderen(Type type)
    {
        List<Component> output = new List<Component>();
        foreach (GameObject child in childeren)
        {
            output.AddRange(GetComponentsInChilderen(type));
        }
        output.AddRange(GetComponent(type));
        return output;
    }
    public void AddComponent(Component component)
    {
        components.Add(component);
    }
    public void RemoveComponent(Component component)
    {
        components.Remove(component);
    }
    public void Destroy()
    {
        components.Clear();
        foreach (GameObject gameObject in childeren)
            gameObject.Destroy();
    }
    public void Update()
    {
        foreach (Component component in components)
            component.UpdateECS();
        oldTransform = transform;
        transform += velocity;
        checkChunk();        
    }
    public void checkChunk()
    {
        int newPosX = (int)transform.x / ChunkLoader.Instance.chunkSize;
        if (newPosX >= ChunkLoader.Instance.maxX)
            newPosX = ChunkLoader.Instance.maxX - 1;
        int newPosY = (int)transform.y / ChunkLoader.Instance.chunkSize;
        if (newPosY >= ChunkLoader.Instance.maxY)
            newPosY = ChunkLoader.Instance.maxY - 1;
        try
        {
            newChunk = ChunkLoader.Instance.chunks[newPosX, newPosY];
        }
        catch(IndexOutOfRangeException e)
        {

        }
    }
    public void LateUpdate()
    {
        if (!ChunkLoader.Instance.loadedChunks.Contains(chunk)) return;
        chunk.gameObjects.Remove(this);
        newChunk.gameObjects.Add(this);
        if (newChunk != chunk)
            foreach (Component component in components)
                component.OnChunkChange();
        chunk = newChunk;
    }
}
