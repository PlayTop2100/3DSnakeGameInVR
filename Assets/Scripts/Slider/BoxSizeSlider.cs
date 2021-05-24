using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSizeSlider : MonoBehaviour
{
    [SerializeField]
    Text sldrTxt;

    public void OnChange()
    {
        float val = GetComponent<Slider>().value;
        GameManager.boxSize = val;
        sldrTxt.text = val.ToString();
    }
}
