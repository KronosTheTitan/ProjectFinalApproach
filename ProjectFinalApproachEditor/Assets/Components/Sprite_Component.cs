using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Component : Component
{
    public string path;
    public override XmlElement Export(XmlDocument doc)
    {
        XmlElement element = doc.CreateElement(string.Empty, "Sprite_Component", string.Empty);
        element.SetAttribute("path", path);
        return element;
    }
}
