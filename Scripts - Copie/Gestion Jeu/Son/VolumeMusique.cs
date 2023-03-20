using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMusique : MonoBehaviour
{
    Slider volume;

    private void Awake()
    {
        volume = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        volume.value = ControlleurSon.volumeMusique;
    }
}
