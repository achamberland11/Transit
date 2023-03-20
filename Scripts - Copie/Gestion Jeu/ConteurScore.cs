using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ConteurScore : MonoBehaviour
{
    public static int nbrEssaies = 0;
    public static float temps = 0;
    public static TimeSpan tempsAffichage;
    public TextMeshProUGUI textEssaies;
    public TextMeshProUGUI textTemps;




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    private void Start()
    {
        nbrEssaies++;
        textEssaies.text = nbrEssaies.ToString();
    }

    private void Update()
    {
        // Le conteur revient à zero lorsque la partie recommence
        temps = Time.timeSinceLevelLoad;


        tempsAffichage = TimeSpan.FromSeconds(temps);
        string tempsText = string.Format("{0:D2}:{1:D2}:{2:D2}", tempsAffichage.Minutes, tempsAffichage.Seconds, tempsAffichage.Milliseconds / 10);

        textTemps.text = tempsText;
    }
}
