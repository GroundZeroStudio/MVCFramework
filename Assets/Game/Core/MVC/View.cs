using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class View : TSingleton<View>
    {
        public GameObject CanvasRootObj; //= CanvasRoot.Instance.CanvasRootObj;

        private Dictionary<string, GameObject> ViewDict = new Dictionary<string, GameObject>();
        
        public void Initialize()
        {
            this.CanvasRootObj = CanvasRoot.Instance.CanvasRootObj;
        }

        public void Add(string rName, GameObject rObj)
        {
            ViewDict.Add(rName, rObj);
        }

        public void Remove(string rName)
        {
            ViewDict.Remove(rName);
        }

        //执行顺序：Awake->OnEnable->OnOpen->Start->Update
        public void Open(string rName, Action<ControllerBase> rCallBack = null)
        {
            if(ViewDict.TryGetValue(rName,out var rView))
            {
                rView.SetActive(true);
                if (rCallBack != null)
                {
                    var rController = rView.GetComponent<ControllerBase>();
                    rCallBack.Invoke(rController);
                }
                return;
            }
            var rObj = UtilTool.AddUIToCanvasRoot(CanvasRootObj.transform, rName);
            var rObjController = rObj.GetComponent<ControllerBase>();
            rObjController.Open();
            if (rCallBack != null)
            {
                rCallBack.Invoke(rObjController);
            }
            if(!rObjController.IsNeedRepeatInit)
                this.Add(rName, rObj);

        }

        public void Close(string rName)
        {
            CanvasRootObj.transform.Find(rName).GetComponent<ControllerBase>().Close();
            this.Remove(rName);
        }
        public void Show(string rName)
        {
            CanvasRootObj.transform.Find(rName).gameObject.SetActive(true);
        }

        public void Hide(string rName)
        {
            CanvasRootObj.transform.Find(rName).gameObject.SetActive(false);
        }

    }
}

