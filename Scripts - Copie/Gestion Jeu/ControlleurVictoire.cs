using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ControlleurVictoire : MonoBehaviour
{
    /// <summary>
    /// Ce script gère la scène de victoire
    /// </summary>

    [Header("Text d'affichage")]
    public TMP_Text textNomJoueur;
    public TMP_Text textEssaies;
    public TMP_Text textTemps;

    [Header("Scores")]
    public static TimeSpan tempsFinal;
    string nomJoueur;
    int nbrEssaies;
    float temps;

    [Header("Scènes")]
    public int indexSceneIntro;
    public int indexSceneMenu;

    [Header("Musique")]
    public AudioSource musique;



    private void Awake()
    {
        nomJoueur = PlayerPrefs.GetString("NomUtilisateur");
        nbrEssaies = ConteurScore.nbrEssaies;
        temps = ConteurScore.temps;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        tempsFinal = TimeSpan.FromSeconds(temps);
        string tempsText = string.Format("{0:D2}:{1:D2}:{2:D2}", tempsFinal.Minutes, tempsFinal.Seconds, tempsFinal.Milliseconds/10);

        textNomJoueur.text = nomJoueur;
        textEssaies.text = nbrEssaies.ToString();
        textTemps.text = tempsText;

        musique.mute = ControlleurSon.enSourdineMusique;
        musique.volume = ControlleurSon.volumeMusique;
    }



    /// <summary>
    /// Déclenché par un bouton dans la scène de victoire
    /// Renvoie le joueur à la scène d'introduction
    /// </summary>
    public void RetourIntro()
    {
        SceneManager.LoadScene(indexSceneIntro);
    }



    /// <summary>
    /// Déclenché par un bouton dans la scème de victoire
    /// Renvoie le joueur au menu
    /// </summary>
    public void RetourMenu()
    {
        SceneManager.LoadScene(indexSceneMenu);
    }
}
