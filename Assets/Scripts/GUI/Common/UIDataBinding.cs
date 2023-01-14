using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using System;

public class UIDataBinding : MonoBehaviour
{
    [ValueDropdown("PorpListStr")]
    public string PropStr;

    private string[] PorpListStr = new string[0];

    [HideInInspector]
    public PropertyInfo Prop;
    [HideInInspector]
    public Component PropOnwer;

    public bool GameObjectActive 
    {
        get 
        {
            return this.gameObject.activeSelf;
        }
        set
        {
            this.gameObject.SetActive(value);
        }
    }

    private void Awake()
    {
        this.Prop = GetProperty(this.gameObject);
    }

    [OnInspectorGUI]
    public void GetPath()
    {
        List<string> PorpStrList = new List<string>(); 
        var rComponents = this.gameObject.GetComponents<Component>();
        foreach(var rComponent in rComponents)
        {
            var rTypeProps = rComponent.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.GetSetMethod(false) != null && prop.GetGetMethod(false) != null);
            foreach(var rTypeProp in rTypeProps)
            {
                string rStr = $"{rComponent.GetType().FullName}/{rTypeProp.Name}:{rTypeProp.PropertyType.Name}";
                PorpStrList.Add(rStr);
            }
        }
        PorpListStr = PorpStrList.ToArray();
    }

    public PropertyInfo GetProperty(GameObject rGo)
    {
        if (string.IsNullOrEmpty(PropStr)) 
            return null;

        var rViewPathStrs = PropStr.Split('/');
        if (rViewPathStrs.Length < 2) return null;

        var rViewClassName = rViewPathStrs[0].Trim();
        var rViewProp = rViewPathStrs[1].Trim();

        var rViewPropStrs = rViewProp.Split(':');
        if (rViewPropStrs.Length < 1) return null;

        var rViewPropName = rViewPropStrs[0].Trim();

        var rViewDatabindingProp = rGo.GetComponents<Component>()
            .Where(comp => comp != null &&
                           comp.GetType().FullName.Equals(rViewClassName) &&
                           comp.GetType().GetProperty(rViewPropName) != null)
            .FirstOrDefault();
        this.PropOnwer = rViewDatabindingProp;
        return rViewDatabindingProp.GetType().GetProperty(rViewPropName);
    }
} 
