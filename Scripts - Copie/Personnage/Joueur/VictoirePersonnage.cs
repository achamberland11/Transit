using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoirePersonnage : MonoBehaviour
{
    /// <summary>
    /// Script qui gère le chargement de la scène de victoire
    /// </summary>

    public int indexSceneVictoire; // Index de la scène de victoire
    bool finJeu; // Détermine si le joueur a gagner et ainsi si la partie est terminée




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    private void Awake()
    {
        // La partie n'est pas terminée au début du niveau
        finJeu = false;
    }



    // Update is called once per frame
    void Update()
    {
        // La scène de victoire se charge lorsque la partie est terminée et que le joueur a gagné
        if(finJeu)
        {
            SceneManager.LoadScene(indexSceneVictoire);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        // La partie se termine lorsque le joueur récupère le beigne
        if(collision.gameObject.tag == "ObjetMission")
        {
            finJeu = true;
        }
    }
}
