using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ControllerBase : MonoBehaviour
    {
        public bool IsNeedRepeatInit { get; set; } = false;
        public void Open()
        {
            this.OnOpen();
        }
        public  void Close()
        {
            this.OnClose();
            Destroy(this.gameObject);
        }

        //空接口，仅供子类继承
        protected virtual void OnOpen()
        {

        }

        protected virtual void OnClose()
        {

        }
    }
}

