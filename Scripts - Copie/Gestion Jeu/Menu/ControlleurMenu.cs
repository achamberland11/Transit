using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlleurMenu : MonoBehaviour
{
    /// <summary>
    /// Ce script gère le menu principal
    /// </summary>

    [Header("Panels Principaux")]
    public GameObject panelCommencerJeu;
    public GameObject panelOptions;
    public GameObject panelCredit;
    public GameObject panelQuitter;

    [Header("Panels Options")]
    public GameObject panelCommandes;
    public GameObject panelSon;

    [Header("Nom Utilisateur")]
    public TMP_InputField champText;
    public static string nomJoueur;




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }



    /// <summary>
    /// Un panel qui permet au joueur de choisir son nom et de commencer la partie s'ouvre lorsqu'il appuie sur Jouer
    /// </summary>
    public void OuvrirPanelCommencerJeu()
    {
        panelCommencerJeu.SetActive(true);
        panelQuitter.SetActive(false);
        panelCredit.SetActive(false);
        panelOptions.SetActive(false);
        panelCommandes.SetActive(false);
        panelSon.SetActive(false);
    }



    /// <summary>
    /// Le panel qui permet au joueur de choisir son nom et de commencer la partie se ferme lorsqu'il appuie sur Fermer
    /// </summary>
    public void FermerPanelCommencerJeu()
    {
        panelCommencerJeu.SetActive(false);
    }



    /// <summary>
    /// Commence la partie si le joueur appuie sur jouer et qu'il avait déjà décidé son nom
    /// Ou lorsqu'il appuie sur confirmer
    /// Le joueur ne peut commencer la partie tant qu'il n'a pas entré son nom
    /// </summary>
    public void CommencerJeu()
    {
        if (champText.text == "" && !PlayerPrefs.HasKey("NomUtilisateur"))
        {

        }
        else
        {
            nomJoueur = champText.text;
            SauvegardeDonnees.SauvegardeUtilisateur(nomJoueur);
            SceneManager.LoadScene(1);
        }
    }



    /// <summary>
    /// Ouvre le panel des options lorsque le joueur appui sur Option
    /// </summary>
    public void OuvrirPanelOptions()
    {
        panelOptions.SetActive(true);
        panelQuitter.SetActive(false);
        panelCredit.SetActive(false);
        panelCommencerJeu.SetActive(false);
        panelCommandes.SetActive(false);
        panelSon.SetActive(false);
    }



    /// <summary>
    /// Ferme le panel des options lorsque le joueur appuie sur Fermer
    /// </summary>
    public void FermerPanelOptions()
    {
        panelOptions.SetActive(false);
    }



    /// <summary>
    /// Ouvre le panel des commandes lorsque le joueur appui sur Commandes
    /// </summary>
    public void OuvrirPanelCommande()
    {
        panelCommandes.SetActive(true);
        panelOptions.SetActive(false);
        panelCredit.SetActive(false);
        panelQuitter.SetActive(false);
        panelCommencerJeu.SetActive(false);
    }



    /// <summary>
    /// Ferme le panel des commandes lorsque le joueur appuie sur Fermer
    /// </summary>
    public void FermerPanelCommande()
    {
        panelCommandes.SetActive(false);
        panelOptions.SetActive(true);
    }



    /// <summary>
    /// Ouvre le panel son lorsque le joueur appui sur Son
    /// </summary>
    public void OuvrirPanelSon()
    {
        panelSon.SetActive(true);
        panelOptions.SetActive(false);
        panelCredit.SetActive(false);
        panelQuitter.SetActive(false);
        panelCommencerJeu.SetActive(false);
    }



    /// <summary>
    /// Ferme le panel du son lorsque le joueur appuie sur Fermer
    /// </summary>
    public void FernerPanelSon()
    {
        panelSon.SetActive(false);
        panelOptions.SetActive(true);
    }



    /// <summary>
    /// Ouvre le panel qui permet de quitter la partie lorsque le joueur appuie sur Quitter
    /// </summary>
    public void OuvrirPanelCredit()
    {
        panelCredit.SetActive(true);
        panelOptions.SetActive(false);
        panelQuitter.SetActive(false);
        panelCommencerJeu.SetActive(false);
        panelCommandes.SetActive(false);
        panelSon.SetActive(false);
    }



    /// <summary>
    /// Ferme le panel qui permet de quitter la partie lorsque le joueur appuie sur Fermer
    /// </summary>
    public void FermerPanelCredit()
    {
        panelCredit.SetActive(false);
    }



    /// <summary>
    /// Ouvre le panel qui permet de quitter la partie lorsque le joueur appuie sur Quitter
    /// </summary>
    public void OuvrirPanelQuitter()
    {
        panelQuitter.SetActive(true);
        panelOptions.SetActive(false);
        panelCredit.SetActive(false);
        panelCommencerJeu.SetActive(false);
        panelCommandes.SetActive(false);
        panelSon.SetActive(false);
    }



    /// <summary>
    /// Ferme le panel qui permet de quitter la partie lorsque le joueur appuie sur Fermer
    /// </summary>
    public void FermerPanelQuitter()
    {
        panelQuitter.SetActive(false);
    }



    /// <summary>
    /// Ferme l'application lorsque le joueur appuie sur OUI
    /// </summary>
    public void QuitterApplication()
    {
        Application.Quit();
    }
}
