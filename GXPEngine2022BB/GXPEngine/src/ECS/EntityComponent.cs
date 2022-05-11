using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.ECS
{
    using ComponentID = UInt32;

    public class EntityComponent
    {
        internal static string[] componentIds = new string[32];

        internal static ComponentID lastID = 0u;

        public class Component
        {
            public Entity entity;

            public virtual void init()
            {
            }

            public virtual void update()
            {
            }
        }

        public class Entity : AnimationSprite
        {
            private bool active = true;
            private List<Component> components;

            private Component[] componentArray = new Component[32];
            private bool[] componentBitSet = new bool[32];

            public Vec2 _velocity, _position;

            public Vec2 _oldVelocity, _oldPosition;

            public Vec2 _acceleration;

            private ComponentID getNewComponentTypeID()
            {
                return lastID++;
            }

            public ComponentID getComponentTypeID<T>()
            {
                UInt32 typeID;
                foreach (string s in componentIds)
                {
                    if (s == null) continue;
                    if (!componentIds.Contains(typeof(T).Name) || s.Length == 0) continue;
                    return (ComponentID)Array.FindIndex(componentIds, x => (x.Length > 0) && (x != null) && (x.Contains(typeof(T).Name)));
                }
                typeID = getNewComponentTypeID();
                Console.WriteLine("Type ID: " + typeID);
                componentIds[typeID] = typeof(T).Name;
                return typeID;
            }

            public Entity(string filename, int cols = 1, int rows = 1) : base(filename, cols, rows)
            {
                components = new List<Component>();
            }

            public bool isActive() { return active; }

            public void destroy() { active = false; }

            public virtual void update()
            {
            }

            public bool hasComponent<T>()
            {
                return componentBitSet[getComponentTypeID<T>()];
            }

            public T addComponent<T>(params object[] mParams)
            {
                T GetObject(params object[] args)
                {
                    return (T)Activator.CreateInstance(typeof(T), args);
                }
                dynamic c = GetObject(mParams);
                c.entity = this;
                components.Add(c);
                componentArray[(int)getComponentTypeID<T>()] = c;
                componentBitSet[getComponentTypeID<T>()] = true;

                c.init();
                return c;
            }

            public T getComponent<T>()
            {
                T result;
                try
                {
                    result = (T)Convert.ChangeType(componentArray[(int)getComponentTypeID<T>()], typeof(T));
                }
                catch
                {
                    result = default(T);
                }
                return result;
            }

            public void Update()
            {
                foreach (Component c in components)
                {
                    c.update();
                }
                update();
            }
        }

        public class Manager
        {
            private List<Entity> entities = new List<Entity>();

            public Manager(Game game)
            {
                _parent = game;
            }

            public Game _parent { get; }

            public void refresh()
            {
                entities.RemoveAll(mEntity => !mEntity.isActive());
            }

            public Entity addEntity(string filename)
            {
                Entity e = new Entity(filename);
                entities.Add(e);
                Game.main.AddChild(e);
                return e;
            }

            public void addEntity(Entity e)
            {
                entities.Add(e);
                Game.main.AddChild(e);
            }
        }
    }
}
