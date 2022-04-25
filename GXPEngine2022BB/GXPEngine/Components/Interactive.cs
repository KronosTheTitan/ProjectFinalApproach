using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Interactive : Component
{
    public bool onFire;
    public bool isWet;
    public bool isCharged;

    public bool flamable;
    public bool conducts;

    public Interactive(GameObjectECS gameObject, bool pOnfire = false, bool pIsWet = false, bool pIsCharged = false, bool pFlamable = false, bool pConducts = false) : base(gameObject)
    {
        onFire = pOnfire;
        isWet = pIsWet;


        flamable = pFlamable;
        conducts = pConducts;
    }
}
