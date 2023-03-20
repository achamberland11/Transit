using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsPersonnage : MonoBehaviour
{
    /// <summary>
    /// Ce script gère toutes les animations du joueur
    /// </summary>

    [Header("Déplacements")]
    float forceDeplacementZ;
    float forceDeplacementHorizontal;
    bool peutBouger;
    float ajoutGravite;
    float hauteurSaut;
    bool auSol;


    // Update is called once per frame
    void Update()
    {
        forceDeplacementZ = Input.GetAxis("Vertical");
        forceDeplacementHorizontal = Input.GetAxis("Horizontal");


        // Animation Course Avant/Arrière
        if (forceDeplacementZ > 0.01f)
        {
            GetComponent<Animator>().SetBool("EnMouvementAvant", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnMouvementAvant", false);
        }

        if (forceDeplacementZ < -0.01f)
        {
            GetComponent<Animator>().SetBool("EnMouvementArriere", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnMouvementArriere", false);
        }


        // Animation Course Latéral
        if (forceDeplacementHorizontal > 0.01f)
        {
            GetComponent<Animator>().SetBool("EnMouvementLateralGauche", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnMouvementLateralGauche", false);
        }
        if (forceDeplacementHorizontal < -0.01f)
        {
            GetComponent<Animator>().SetBool("EnMouvementLateralDroite", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnMouvementLateralDroite", false);
        }



        // Animation Saut
        RaycastHit infoCollision;
        auSol = Physics.SphereCast(transform.position + new Vector3(0f, 0.5f, 0f), 0.25f, -Vector3.up, out infoCollision, 0.8f);


        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            print("Saut");
            GetComponent<Animator>().SetBool("EnSaut", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnSaut", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && auSol && (forceDeplacementHorizontal != 0 || forceDeplacementZ != 0))
        {
            GetComponent<Animator>().SetBool("EnSautEnMouvement", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnSautEnMouvement", false);
        }
    }
}
