using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Bullet_Component : Component
{
    public Bullet_Component(GameObjectECS pGameObjectECS) : base(pGameObjectECS)
    {
        pGameObjectECS.AddComponent(new Component_Sprite(pGameObjectECS, "square.png"));
        gameObject.velocity = new Vec2(3, 0);
    }
}
