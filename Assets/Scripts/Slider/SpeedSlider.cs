using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    [SerializeField]
    Text sldrTxt;

    public void OnChange()
    {
        float val = GetComponent<Slider>().value;
        GameManager.speed = val;
        sldrTxt.text = val.ToString();
    }
}
