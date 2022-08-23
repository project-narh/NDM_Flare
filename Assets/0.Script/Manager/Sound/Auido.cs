using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Auido : MonoBehaviour
{
    public Slider slider;
    public Slider slider2;

    private void Start()
    {
        slider2.maxValue = 0;
        slider2.minValue = -40;
        slider.maxValue = 0;
        slider.minValue = -40;
    }

    public void Audio_BGM()
    {
        float sound = slider.value;
        SoundManager.Instance.Set_BGM(sound);
    }

    public void Audio_SFX()
    {
        float sound = slider2.value;
        SoundManager.Instance.Set_SFX(sound);
    }
}
