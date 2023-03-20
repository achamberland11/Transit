using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensibiliteY : MonoBehaviour
{
    Slider indicateurSensibilité;

    private void Awake()
    {
        indicateurSensibilité = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        indicateurSensibilité.value = ControlleurSensibilite.sensibiliteY;
    }
}
