using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Collider : GXPEngine.ECS.EntityComponent.Component
{
    public bool trigger;
    public override void init()
    {
    }
    public Collider(bool pTrigger)
    {
        trigger = pTrigger;
    }
}