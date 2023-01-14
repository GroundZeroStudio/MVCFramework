using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using LitJson;
using System.IO;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ConfigNameAttribute : Attribute
{
    public string ConfigName;

    public ConfigNameAttribute(string rName)
    {
        this.ConfigName = rName;
    }
}

public class GameConfig : TSingleton<GameConfig>
{
    public static readonly Type[] EmptyTypes = new Type[0];

    [ConfigName("ColorValueConfig")]
    public ColorValueTable ColorValue = new ColorValueTable();




    /// <summary>
    /// 反序列化数据
    /// </summary>
    public void Initialize()
    {
        string rDir = "Assets/Resources/Config/";

        Type rType = this.GetType();
        var rGameConfigAllFields = rType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField);
        foreach (var rGameConfigField in rGameConfigAllFields)
        {
            var rConfigNameAttr = rGameConfigField.GetCustomAttribute<ConfigNameAttribute>();
            if (rConfigNameAttr == null)
                continue;

            var rTable = rGameConfigField.FieldType.GetConstructor(EmptyTypes).Invoke(null);        //实例化ConfigTable
            rGameConfigField.SetValue(this, rTable);                                                //添加到GameConfig中

            var rTableFieldType = rGameConfigField.FieldType.GetProperty("Table")?.PropertyType;    //拿取table字段类型
            if (rTableFieldType == null) 
                continue;

            var rGenericParams = rTableFieldType.GetGenericArguments();     //获取table中的两个泛型参数
            if (rGenericParams.Length < 2) 
                continue;

            var rJsonPath = rDir + rGameConfigField.Name + ".json";
            JsonData rJsonData = JsonMapper.ToObject(Resources.Load<TextAsset>($"Config/{rGameConfigField.Name}").text);     //File.ReadAllText(rJsonPath);
            IDictionary rObj = (IDictionary)rTableFieldType.GetConstructor(EmptyTypes).Invoke(null);    //实例化Table字典，用于储存数据

            var rDics = rJsonData.ValueAsObject();  //JsonData转换为字典形式
            foreach(var rDicItem in rDics)
            {
                var rConfigType = rGenericParams[1]; 
                var rConfigObj = rConfigType.GetConstructor(EmptyTypes).Invoke(null);       //实例化Config
     
                var rConfigTypeFields = rConfigType.GetProperties();
                for (int i = 0; i < rConfigTypeFields.Length; i++)
                {
                    var rConfigProperty = rConfigType.GetProperty(rConfigTypeFields[i].Name);
                    JsonData rData = rDicItem.Value[rConfigTypeFields[i].Name];
                    rConfigProperty.SetValue(rConfigObj, rData.ToObject(rConfigProperty.PropertyType));
                }
                rObj.Add(int.Parse(rDicItem.Key), rConfigObj);
            }
            rGameConfigField.FieldType.GetProperty("Table")?.SetValue(rTable, rObj);

        }
    }
}
