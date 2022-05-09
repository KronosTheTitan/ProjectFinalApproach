using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class UpdateManagerECS
{
    public static UpdateManagerECS Instance = new UpdateManagerECS();
    public void Update()
    {
        ChunkLoader.Instance.Update();
        CollisionManager.Instance.OnStep();
    }
}
