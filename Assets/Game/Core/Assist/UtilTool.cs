using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.UI;

public static class UtilTool
{
    public static GameObject AddUIToCanvasRoot(this Transform rParent, string rName)
    {
        var ABPrefab = AssetLoader.Instance.LoadPrefabFormAB(rName);
        var ViewPrefab = GameObject.Instantiate(ABPrefab);
        ViewPrefab.name = ABPrefab.name;
        ViewPrefab.transform.SetParent(rParent, false);
        return ViewPrefab;
    }

    public static EventType StringToEventType(string name)
    {
        return (EventType)EventType.Parse(typeof(EventType), name);
    }
    public static T Get<T>(this List<object> rList, int nIndex)
    {
        if (nIndex < 0 || nIndex > rList.Count || rList == null)
            return default(T);
        return (T)rList[nIndex];

    }

    public static object ToObject(this JsonData jsonData, Type rType)
    {
        if (rType == typeof(int))
        {
            return int.Parse(jsonData.ToString());
        }
        else if (rType == typeof(uint))
        {
            return uint.Parse(jsonData.ToString());
        }
        else if (rType == typeof(long))
        {
            return long.Parse(jsonData.ToString());
        }
        else if (rType == typeof(ulong))
        {
            return ulong.Parse(jsonData.ToString());
        }
        else if (rType == typeof(float))
        {
            return float.Parse(jsonData.ToString());
        }
        else if (rType == typeof(double))
        {
            return double.Parse(jsonData.ToString());
        }
        else if (rType == typeof(bool))
        {
            return bool.Parse(jsonData.ToString());
        }
        else if (rType == typeof(byte))
        {
            return byte.Parse(jsonData.ToString());
        }
        else if (rType == typeof(short))
        {
            return short.Parse(jsonData.ToString());
        }
        else if (rType == typeof(ushort))
        {
            return ushort.Parse(jsonData.ToString());
        }
        else if (rType == typeof(string))
        {
            if (string.IsNullOrEmpty(jsonData.ToString()))
                return "";
            return jsonData.ToString();
        }
        Debug.LogError("该类型不是基础类型,不能将JsonData转换为该类型");
        return null;
    }

    public static void DataBinding(UIDataBinding rData, object rProp)
    {
        rData.Prop.SetValue(rData.PropOnwer, rProp);
    }
}

