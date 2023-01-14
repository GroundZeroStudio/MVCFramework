using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

public class UILoadingController : ControllerBase
{
    private UILoadingView mView;

    protected override void OnOpen()
    {
        mView = this.GetComponent<UILoadingView>();
        for (int i = 0; i < mView.ImageColors.Length; i++)
        {
            if (ColorUtility.TryParseHtmlString(GameConfig.Instance.ColorValue.Table[i+1].Value, out var rColor)) 
            {
                mView.ImageColors[i].color = rColor;
            }
            
        }
        base.OnOpen();
    }

}
