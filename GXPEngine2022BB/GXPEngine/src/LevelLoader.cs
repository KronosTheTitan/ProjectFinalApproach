using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GXPEngine;
class LevelLoader
{
    public static void LoadLevel(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(path);

        XmlElement root = doc.DocumentElement;
        Console.WriteLine(root.Name);
        List<XmlElement> objects;
    }
}
