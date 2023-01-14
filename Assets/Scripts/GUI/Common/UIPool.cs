using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPool : MonoBehaviour
{
    private List<GameObject> Pool = new List<GameObject>();
    public GameObject RootPrefab;
    public GameObject ItemPrefab;
    public int PoolSize = 5;


    private void Awake()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject rObj = GameObject.Instantiate(ItemPrefab, RootPrefab.GetComponent<RectTransform>());
            rObj.SetActive(false);
            Pool.Add(rObj);
        }
    }

    public List<GameObject> GetPrefabsInPool(RectTransform rRectTransform , int nCount)
    {
        List<GameObject> rObjects = new List<GameObject>();
        if (Pool.Count < nCount)
        {
            int nPoolCount = Pool.Count;
            for (int i = 0; i < nCount - nPoolCount; i++)
            {
                GameObject rObj = GameObject.Instantiate(ItemPrefab, RootPrefab.GetComponent<RectTransform>());
                rObj.SetActive(false);
                Pool.Add(rObj);
            }
        }
        for (int i = 0; i < nCount; i++)
        {
            Pool[i].SetActive(true);
            Pool[i].transform.SetParent(rRectTransform);
            rObjects.Add(Pool[i]);
        }
        return rObjects;
    }

    public void ReturnPrefabsToPool()
    {
        foreach(var rObj in Pool)
        {
            rObj.SetActive(false);
            rObj.transform.SetParent(RootPrefab.GetComponent<RectTransform>());
        }
    }
}
