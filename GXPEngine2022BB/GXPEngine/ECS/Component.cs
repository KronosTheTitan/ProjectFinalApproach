using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Component
{
    public GameObjectECS gameObject;
    bool _isActive = true;
    public bool isActive
    {
        get
        {
            return _isActive;
        }
    }
    public Vec2 transform
    {
        get
        {
            return gameObject.transform;
        }
    }
    public Component(GameObjectECS pGameObjectECS)
    {
        gameObject = pGameObjectECS;
        gameObject.AddComponent(this);
        Start();
    }
    public virtual void Start()
    {

    }
    public virtual void UpdateECS()
    {

    }
}
