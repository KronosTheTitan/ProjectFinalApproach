using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Component_Sprite : Component
{
    public Sprite sprite;
    public Component_Sprite(GameObjectECS pGameObjectECS,string path) : base(pGameObjectECS)
    {
        sprite = new Sprite(path, false);
        MyGame.main.AddChild(sprite);
        sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
    }
    public override void UpdateECS()
    {
        base.UpdateECS();
        sprite.x = transform.x;
        sprite.y = transform.y;
    }
}
