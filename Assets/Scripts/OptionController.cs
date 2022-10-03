using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField] Slider effectVolumeSlider;
    [SerializeField] Slider BGVolumeSlider;

    GameObject BGSound;
    GameObject EffectSound;

    // Start is called before the first frame update
    void Start()
    {
        BGSound = GameObject.Find("BGSound");
        EffectSound = GameObject.Find("EffectSound");
    }

    public void EffectVolume()
    {
        EffectSound.GetComponent<AudioSource>().volume = effectVolumeSlider.value;
    }

    public void BGvolume()
    {
        BGSound.GetComponent<AudioSource>().volume = BGVolumeSlider.value;
    }
}
