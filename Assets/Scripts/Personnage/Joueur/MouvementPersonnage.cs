using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouvementPersonnage : MonoBehaviour
{
    /// <summary>
    /// Ce script gère tout les déplacements du joueur
    /// </summary>

    [Header("Déplacements")]
    public float vitesseDeplacement;
    public float forceDeplacementZ;
    public float forceDeplacementHorizontal;
    public bool peutBouger;
    public float ajoutGravite;
    public float hauteurSaut;
    float forceSaut;
    // Contraintes déplacement
    bool auSol;
    public static bool enVie;

    [Header("Caméra")]
    public Camera cam;
    [Range(0.5f, 10f)]
    float vitesseRotation = ControlleurSensibilite.sensibiliteX;
    float forceRotation;




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        enVie = true;

        /*GameObject instanceObjetInteractif = Instantiate(objetInteractif, objetInteractif.transform.position, objetInteractif.transform.rotation);
        instanceObjetInteractif.SetActive(true);*/
    }



    //// Utiliser Update pour détecter les touches
    void Update()
    {
        if (enVie)
        {
            peutBouger = true;
            forceRotation = Input.GetAxis("Mouse X") * vitesseRotation;
            vitesseRotation = ControlleurSensibilite.sensibiliteX;
        }
        else
        {
            peutBouger = false;
        }

        if (peutBouger)
        {
            forceDeplacementZ = Input.GetAxis("Vertical") * vitesseDeplacement;
            forceDeplacementHorizontal = Input.GetAxis("Horizontal") * vitesseDeplacement;

            RaycastHit infoCollision;
            auSol = Physics.SphereCast(transform.position + new Vector3(0f, 0.5f, 0f), 0.25f, -Vector3.up, out infoCollision, 0.8f);

            if (Input.GetKeyDown(KeyCode.Space) && auSol)
            {
                forceSaut = hauteurSaut;
            }
        }
    }



    // Utiliser FixedUpdate pour appliquer les forces
    private void FixedUpdate()
    {

        if (auSol)
        {
            GetComponent<Rigidbody>().AddRelativeForce(forceDeplacementHorizontal, forceSaut, forceDeplacementZ, ForceMode.VelocityChange);
        }
        else
        {
            GetComponent<Rigidbody>().AddRelativeForce(forceDeplacementHorizontal, ajoutGravite, forceDeplacementZ, ForceMode.VelocityChange);
        }

        forceSaut = 0;

        transform.Rotate(0f, forceRotation, 0f);
    }



    // Détecter les collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sol Mort")
        {
            enVie = false;
            forceDeplacementHorizontal = 0;
            forceDeplacementZ = 0;
            forceRotation = 0;
            Invoke("RecommencerNiveau", 2f);
        }
    }

    // Fonction qui recommence la scène actuelle
    private void RecommencerNiveau()
    {
        Scene sceneActuelle = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneActuelle.name);
    }
}
