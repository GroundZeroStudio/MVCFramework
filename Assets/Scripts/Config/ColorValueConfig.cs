using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorValueTable
{
    public Dictionary<int, ColorValueConfig> Table { get; set; }

}

public class ColorValueConfig
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
