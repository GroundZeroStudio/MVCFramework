using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

internal class Init : MonoBehaviour
{
    private void Start()
    {
        Model.Instance.Initialize();
        View.Instance.Initialize();
        Controller.Instance.Initialize();

        GameConfig.Instance.Initialize();
    }

    private void OnDestroy()
    {
        Model.OnDestory();
        View.OnDestory();
        Controller.OnDestory();
    }
}
