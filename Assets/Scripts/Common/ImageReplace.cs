using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ImageType
{
    Image,
    SpriteRenderer,
}
public class ImageReplace : MonoBehaviour
{
    public ImageType Type;

    private Image ImgConponent;
    private SpriteRenderer SRConponent;

    public Sprite SR;

    private void Start()
    {
        if (Type == ImageType.Image)
        {
            ImgConponent = this.GetComponent<Image>();
        }
        else if (Type == ImageType.SpriteRenderer)
        {
            SRConponent = this.GetComponent<SpriteRenderer>();
        }
    }

    public void Replace()
    {
        if (Type == ImageType.Image)
        {
            ImgConponent.sprite = SR;
        }
        else if (Type == ImageType.SpriteRenderer)
        {
            SRConponent.sprite = SR;
        }
    }
}
