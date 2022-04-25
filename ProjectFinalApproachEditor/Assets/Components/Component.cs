using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Component : MonoBehaviour
{
    public virtual XmlElement Export(XmlDocument doc)
    {
        return null;
    }
}
