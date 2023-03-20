using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleScript : MonoBehaviour
{
    public GameObject impactTir; // Référence au Prefab à instancier lorsque le tir frappe un objet. (Prefab ParticulesHit)
    GameObject personnage; // Référence au personnage

    private void Start()
    {
        personnage = GameObject.FindGameObjectWithTag("Joueur");

    }



    /// <summary>
    /// Fonction OnCollisionEnter. Gère ce qui se passe lorsqu'une balle touche un objet.
    /// </summary>
    /// <param name="infoCollisions"></param>
    private void OnCollisionEnter(Collision infoCollisions)
    {
        GameObject impactCopie = Instantiate(impactTir);
        impactCopie.transform.position = infoCollisions.GetContact(0).point;
        impactCopie.SetActive(true);
        impactCopie.transform.LookAt(personnage.transform);
        impactCopie.transform.Translate(0f, 0.5f, 0f);
        Destroy(gameObject);
        Destroy(impactCopie, 1.5f);

        // Tue le joueur lorsqu'elle le touche
        if(infoCollisions.gameObject.tag == "Joueur")
        {
            personnage.GetComponent<MortPersonnage>().Mort();
        }
    }
}
