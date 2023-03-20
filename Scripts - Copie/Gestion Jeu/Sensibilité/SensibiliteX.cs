using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensibiliteX : MonoBehaviour
{
    Slider indicateurSensibilite;

    private void Awake()
    {
        indicateurSensibilite = gameObject.GetComponent<Slider>();
    }


    private void Start()
    {
        indicateurSensibilite.value = ControlleurSensibilite.sensibiliteX;
    }
}
