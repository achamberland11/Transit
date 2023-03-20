using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SauvegardeDonnees : MonoBehaviour
{
    /// <summary>
    /// Sauvegarde le nom que l'utilisateur a choisi
    /// </summary>
    /// <param name="nom"></param>
    public static void SauvegardeUtilisateur(string nom)
    {
        PlayerPrefs.SetString("NomUtilisateur", nom);
    }


    /// <summary>
    /// Sauvegarde les préférences sonnores de l'utilisateur
    /// </summary>
    /// <param name="volumeGeneral"></param>
    public static void SauvegardeSon(float volumeGeneral, float volumeMusique)
    {
        PlayerPrefs.SetFloat("VolumeGeneral", volumeGeneral);
        PlayerPrefs.SetFloat("VolumeMusique", volumeMusique);
    }


    /// <summary>
    /// Sauvegarde les préférences de sensibilité X de l'utilisateur
    /// </summary>
    /// <param name="sensibiliteX"></param>
    public static void SauvegardeSensibiliteX(float sensibiliteX)
    {
        PlayerPrefs.SetFloat("SensibiliteX", sensibiliteX);
    }

    /// <summary>
    /// Sauvegarde les préférences de sensibilité X de l'utilisateur
    /// </summary>
    /// <param name="sensibiliteY"></param>
    public static void SauvegardeSensibiliteY(float sensibiliteY)
    {
        PlayerPrefs.SetFloat("SensibiliteY", sensibiliteY);
    }
}
