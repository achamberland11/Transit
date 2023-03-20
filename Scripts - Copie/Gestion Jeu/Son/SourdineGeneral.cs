using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourdineGeneral : MonoBehaviour
{
    Toggle sourdine;

    private void Awake()
    {
        sourdine = gameObject.GetComponent<Toggle>();
    }

    private void Start()
    {
        sourdine.isOn = ControlleurSon.enSourdineGeneral;
    }
}