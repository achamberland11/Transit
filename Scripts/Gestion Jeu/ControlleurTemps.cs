using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleurTemps : MonoBehaviour
{
    /// <summary>
    /// Ce script remet le temps � vitesse normal lorsque la sc�ne reprend
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }
}
