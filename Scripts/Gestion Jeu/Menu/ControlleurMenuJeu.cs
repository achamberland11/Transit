using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlleurMenuJeu : MonoBehaviour
{
    /// <summary>
    /// Ce script gère le menu en jeu
    /// </summary>

    [Header("Variable Canvas Menu")]
    public GameObject menu;
    bool menuOuvert; //À true lorsque le menu est ouvert

    [Header("Variables Menu")]
    public GameObject panelOptions;
    public GameObject panelQuitter;

    [Header("Panels Options")]
    public GameObject panelCommandes;
    public GameObject panelSon;



    ////////////////////// APPEL DES FONCTIONS //////////////////////

    private void Start()
    {
        menuOuvert = false;
    }


    // Update is called once per frame
    void Update()
    {
        menu.SetActive(menuOuvert);

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            menuOuvert = !menuOuvert;
        }

        if(menuOuvert)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(MouvementPersonnage.enVie) 
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    /// <summary>
    /// Ouvre le panel des options lorsque le joueur appui sur Option
    /// </summary>
    public void FermerMenu()
    {
        menuOuvert = false;
        panelOptions.SetActive(false);
        panelQuitter.SetActive(false);
    }



    /// <summary>
    /// Ouvre le panel des options lorsque le joueur appui sur Option
    /// </summary>
    public void OuvrirPanelOptions()
    {
        panelOptions.SetActive(true);
        panelQuitter.SetActive(false);
        panelCommandes.SetActive(false);
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
        panelQuitter.SetActive(false);
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
        panelQuitter.SetActive(false);
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
    public void OuvrirPanelQuitter()
    {
        panelQuitter.SetActive(true);
        panelOptions.SetActive(false);
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
