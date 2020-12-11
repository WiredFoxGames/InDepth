using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boostbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBooster(float booster)
    {
        slider.maxValue = booster;
        slider.value = booster;
    }
   
    public void SetBooster(float booster)
    {
        slider.value = booster;
    }
}
