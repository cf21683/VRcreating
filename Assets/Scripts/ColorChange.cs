using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public Slider sliderR, sliderG, sliderB;
    public Renderer target;
    void Start()
    {
        sliderR.onValueChanged.AddListener(delegate{OnColorChange();});
        sliderG.onValueChanged.AddListener(delegate{OnColorChange();});
        sliderB.onValueChanged.AddListener(delegate{OnColorChange();});
    }

    
    void OnColorChange()
    {
        target.material.color = new Color(sliderR.value, sliderG.value, sliderB.value);
    }
}
