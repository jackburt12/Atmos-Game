using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("ItemCollection")]
public class ItemDatabase
{

    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Item> items = new List<Item>();

    public static ItemDatabase Load(string path)
    {

        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));

        StringReader reader = new StringReader(_xml.text);

        ItemDatabase items = serializer.Deserialize(reader) as ItemDatabase;

        reader.Close();

        return items;

    }

}
