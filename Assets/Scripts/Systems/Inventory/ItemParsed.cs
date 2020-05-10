using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ItemParsed : MonoBehaviour
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    [XmlAttribute("name")]
    public string Name { get; set; }
    [XmlAttribute("value")]
    public float Value { get; set; }
    [XmlAttribute("type")]
    public string Type { get; set; }
}
