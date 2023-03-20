using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinitialisationConteur : MonoBehaviour
{
    /// <summary>
    /// Ce script réinitialise les conteurs lorque le joueur revient à la scène d'intro
    /// </summary>

    private void Awake()
    {
        // Réinitialiser le conteur d'essaies lorsque le joueur revient à la scène d'intro
        ConteurScore.nbrEssaies = 0;
        ConteurScore.temps = 0;
        ConteurScore.tempsAffichage = ConteurScore.tempsAffichage * 0;
    }
}
