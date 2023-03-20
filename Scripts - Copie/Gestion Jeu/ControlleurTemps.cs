using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleurTemps : MonoBehaviour
{
    /// <summary>
    /// Ce script remet le temps à vitesse normal lorsque la scène reprend
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }
}
