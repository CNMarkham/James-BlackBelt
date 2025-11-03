using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensSetting : MonoBehaviour
{
    public Slider sensSlider;
    // Start is called before the first frame update
    void Start()
    {
        sensSlider.value = PlayerPrefs.GetFloat("Sensitivity", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensSlider.value);
    }
}
