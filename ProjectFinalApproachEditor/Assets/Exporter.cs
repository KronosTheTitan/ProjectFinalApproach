using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Exporter : MonoBehaviour
{
    [SerializeField]
    public string exportPath;

    [SerializeField]
    List<GameObject> gameObjects;
    private void Start()
    {
        XmlDocument doc = new XmlDocument();

        XmlElement root = doc.DocumentElement;

        XmlElement element1 = doc.CreateElement(string.Empty, "Mainbody", string.Empty);
        doc.AppendChild(element1);

        

        foreach (GameObject gameObject in gameObjects)
        {
            XmlElement gameObjectXML = doc.CreateElement(string.Empty, "GameObject", string.Empty);
            gameObjectXML.SetAttribute("x",transform.position.x.ToString());
            gameObjectXML.SetAttribute("y",transform.position.y.ToString());
            Component[] components = gameObject.GetComponents<Component>();
            foreach(Component component in components)
            {
                XmlElement xmlElement = component.Export(doc);
                if(xmlElement != null)
                    gameObjectXML.AppendChild(xmlElement);
            }
            element1.AppendChild(gameObjectXML);
        }
        doc.Save(exportPath);
    }
}
