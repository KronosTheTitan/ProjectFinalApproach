using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PolygonCollider : Component
{
    [SerializeField]
    Transform[] points;

    [SerializeField]
    int[] lines;

    public override XmlElement Export(XmlDocument doc)
    {
        XmlElement element = doc.CreateElement(string.Empty,"PolygonCollider",string.Empty);

        XmlElement vecs = doc.CreateElement(string.Empty, "Points", string.Empty);

        element.AppendChild(vecs);

        foreach(Transform transform in points)
        {
            XmlElement vec = doc.CreateElement(string.Empty, "vec2", string.Empty);

            vec.SetAttribute("x", transform.position.x.ToString());
            vec.SetAttribute("y", transform.position.y.ToString());

            vecs.AppendChild(vec);
        }

        XmlElement xLines = doc.CreateElement(string.Empty, "Lines", string.Empty);

        element.AppendChild(xLines);

        foreach(int i in lines)
        {
            XmlElement xInt = doc.CreateElement(string.Empty, "int", string.Empty);

            xInt.SetAttribute("int", i.ToString());

            xLines.AppendChild(xInt);
        }

        return element;
    }
}
