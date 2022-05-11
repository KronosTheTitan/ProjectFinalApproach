using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GXPEngine.ECS.EntityComponent;

namespace GXPEngine
{
    class GrappleComponent : Component
    {
        public bool isGrappling = false;

        public GrappleComponent()
        {
            Console.WriteLine("GrappleComponent: create");
        }

        public override void init()
        {
            Console.WriteLine("GrappleComponent: init");
        }
    }
}
