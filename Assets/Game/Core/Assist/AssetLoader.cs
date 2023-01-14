using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetLoader : TSingleton<AssetLoader>
{
    private Dictionary<string, AssetBundle> AB = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Object> ABAsset = new Dictionary<string, Object>();
    public GameObject LoadPrefabFormAB(string rName)
    {
        if(ABAsset.ContainsKey(rName))
        {
            return ABAsset[rName] as GameObject;
        }
        if (AB.ContainsKey("Prefabs"))
        {
            var rABAsset = AB["Prefabs"].LoadAsset<GameObject>(rName);
            ABAsset.Add(rName, rABAsset);
            return rABAsset;
        }
        var rab = AssetBundle.LoadFromFile($"{Application.streamingAssetsPath}/gui/prefabs.ab");
        if(rab == null)
        {
            Debug.LogError("Prefabs的AB包未找到");
            return null;
        }
        var rabAsset = rab.LoadAsset<GameObject>(rName);
        AB.Add("Prefabs", rab);
        ABAsset.Add(rName, rabAsset);
        return rabAsset;
    }

    public Sprite LoadSpriteFormAB(string rName)
    {
        if (ABAsset.ContainsKey(rName))
        {
            return ABAsset[rName] as Sprite;
        }
        if (AB.ContainsKey("Textures"))
        {
            var rABAsset = AB["Textures"].LoadAllAssets<Sprite>();
            foreach(var rAsset in rABAsset)
            {
                ABAsset.Add(rAsset.name, rAsset);
            }
            if (ABAsset.ContainsKey(rName))
            {
                return ABAsset[rName] as Sprite;
            }
        }
        var rab = AssetBundle.LoadFromFile($"{Application.streamingAssetsPath}/gui/textures.ab");
        if (rab == null)
        {
            Debug.LogError("Texture的AB包未找到");
            return null;
        }

        AB.Add("Textures", rab);
        var rabAsset = rab.LoadAllAssets<Sprite>();
        foreach (var rAsset in rabAsset)
        {
            ABAsset.Add(rAsset.name, rAsset);
        }
        if (ABAsset.ContainsKey(rName))
        {
            return ABAsset[rName] as Sprite;
        }

        return null;
    }
}
