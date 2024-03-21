using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public Slider sliderR, sliderG, sliderB;
    public Button closeButton;
    public GameObject canvas;
    public Renderer target;
    void Start()
    {
        sliderR.onValueChanged.AddListener(delegate{OnColorChange();});
        sliderG.onValueChanged.AddListener(delegate{OnColorChange();});
        sliderB.onValueChanged.AddListener(delegate{OnColorChange();});
        closeButton.onClick.AddListener(CloseCanvas);
    }

    
    void OnColorChange()
    {
        target.material.color = new Color(sliderR.value, sliderG.value, sliderB.value);
    }

    void CloseCanvas(){
        canvas.SetActive(false);
    }

    public void SetRenderer(Renderer renderer){
        target = renderer;
    }
}
