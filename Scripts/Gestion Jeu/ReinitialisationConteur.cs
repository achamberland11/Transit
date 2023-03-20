using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinitialisationConteur : MonoBehaviour
{
    /// <summary>
    /// Ce script r�initialise les conteurs lorque le joueur revient � la sc�ne d'intro
    /// </summary>

    private void Awake()
    {
        // R�initialiser le conteur d'essaies lorsque le joueur revient � la sc�ne d'intro
        ConteurScore.nbrEssaies = 0;
        ConteurScore.temps = 0;
        ConteurScore.tempsAffichage = ConteurScore.tempsAffichage * 0;
    }
}
