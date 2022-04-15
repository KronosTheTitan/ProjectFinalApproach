using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class GameObjectECS
{
    List<Component> components = new List<Component>();
    List<GameObject> childeren = new List<GameObject>();
    GameObject parent = null;

    public Vec2 transform;
    public GameObjectECS()
    {

    }
    public Component[] GetComponent<T>()
    {
        List<Component> output = new List<Component>();
        foreach(Component component in components)
        {
            if (component is T)
                output.Add(component);
        }
        return output.ToArray();
    }
    public Component[] GetComponentsInChilderen<T>()
    {
        List<Component> output = new List<Component>();
        foreach (GameObject child in childeren)
        {
            output.AddRange(GetComponentsInChilderen<T>());
            output.AddRange(GetComponent<T>());
        }
        return output.ToArray();
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
    }
}
