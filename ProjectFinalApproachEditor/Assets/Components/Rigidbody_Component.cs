using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Rigidbody_Component : Component
{
    public float radius=1;
    public float weight=1;
    public float bounciness=1;
    public override XmlElement Export(XmlDocument doc)
    {
        XmlElement element = doc.CreateElement(string.Empty, "Rigidbody", string.Empty);
        element.SetAttribute("radius",radius.ToString());
        element.SetAttribute("weight",weight.ToString());
        element.SetAttribute("bounciness",bounciness.ToString());
        return element;
    }
}
