using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoirePersonnage : MonoBehaviour
{
    /// <summary>
    /// Script qui g�re le chargement de la sc�ne de victoire
    /// </summary>

    public int indexSceneVictoire; // Index de la sc�ne de victoire
    bool finJeu; // D�termine si le joueur a gagner et ainsi si la partie est termin�e




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    private void Awake()
    {
        // La partie n'est pas termin�e au d�but du niveau
        finJeu = false;
    }



    // Update is called once per frame
    void Update()
    {
        // La sc�ne de victoire se charge lorsque la partie est termin�e et que le joueur a gagn�
        if(finJeu)
        {
            SceneManager.LoadScene(indexSceneVictoire);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        // La partie se termine lorsque le joueur r�cup�re le beigne
        if(collision.gameObject.tag == "ObjetMission")
        {
            finJeu = true;
        }
    }
}
