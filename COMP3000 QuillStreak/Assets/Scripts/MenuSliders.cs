using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSliders : MonoBehaviour
{
    private Text text;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
    }

    public void changedValue() 
    {
        text.text = slider.value + "/" + slider.maxValue;
    }
}
