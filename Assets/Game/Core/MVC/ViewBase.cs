using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ViewBase : TSMono<ViewBase>
    {

        protected override void Awake()
        {
            //先调用父类的Awake函数
            base.Awake(); 

        }

    }
}

