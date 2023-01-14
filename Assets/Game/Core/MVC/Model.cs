using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Model : TSingleton<Model>
    {
        private List<ModelBase> ModelList = new List<ModelBase>();
        public Model()
        {
        }
        public void Initialize()
        {
            foreach(var rModel in this.ModelList)
            {
                rModel.OnInitialize();
            }
        }


    }
}

