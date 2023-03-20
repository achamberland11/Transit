using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConteurFPS : MonoBehaviour
{
    public static bool actif;
    float nbrFPS;
    public TextMeshProUGUI conteurText;

    // Start is called before the first frame update
    void Start()
    {
        nbrFPS = 1f / Time.deltaTime;
        InvokeRepeating("ConterFPS", 1f, 1f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            actif = !actif;
        }

        if(actif)
        {
            conteurText.enabled = true;
        }
        else
        {
            conteurText.enabled = false;
        }
    }

    void ConterFPS()
    {
        nbrFPS = 1f / Time.deltaTime;
        nbrFPS = (int)Mathf.Floor(nbrFPS);
        conteurText.text = nbrFPS.ToString();
    }
}
