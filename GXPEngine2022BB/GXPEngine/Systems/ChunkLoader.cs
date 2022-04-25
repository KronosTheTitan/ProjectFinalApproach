using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class ChunkLoader
{
    static ChunkLoader _instance = new ChunkLoader();

    public static ChunkLoader Instance
    {
        get
        {
            return _instance;
        }
    }
    private ChunkLoader()
    {

    }
    public List<Chunk> tempChunks = new List<Chunk>();
    public List<Chunk> loadedChunks = new List<Chunk>();

    public int maxX = 3;
    public int maxY = 3;
    public int chunkSize = 512;
    public Chunk[,] chunks;
    int loadRange = 1;

    public delegate void LateUpdatesDelegate();
    public event LateUpdatesDelegate LateUpdates;

    bool active;
    public bool systemActive
    {
        get
        {
            return active;
        }
    }

    public void LoadNewChunks(int pX,int pY)
    {
        Console.WriteLine("Loading chunks");

        loadedChunks.Clear();

        for (int x = pX-loadRange;x<pX+loadRange;x++)
        {
            for (int y = pY - loadRange; y < pY + loadRange; y++)
            {
                try
                {
                    if (!loadedChunks.Contains(chunks[x, y]))
                        loadedChunks.Add(chunks[x, y]);

                    //Console.WriteLine(loadedChunks.Count + " : " + x + " : " + y);
                }
                catch(IndexOutOfRangeException e)
                {
                    //Console.WriteLine(x + " : " + y);
                }
            }
        }
    }
    public void Start()
    {
        chunks = new Chunk[maxX, maxY];
        for (int x = 0;x<maxX;x++)
        {
            for(int y = 0; y < maxY; y++)
            {
                chunks[x, y] = new Chunk(x, y, y * chunkSize, (y + 1) * chunkSize, x * chunkSize, (x + 1) * chunkSize);
            }
        }
    }
    public void Update()
    {
        tempChunks = loadedChunks;
        foreach (Chunk chunk in tempChunks)
        {
            chunk.Update();
        }
        LateUpdates();
    }
    public void ToggleChunkSystem(bool target)
    {
        active = target;
    }
}
