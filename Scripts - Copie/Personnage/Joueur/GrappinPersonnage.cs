using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappinPersonnage : MonoBehaviour
{
    /// <summary>
    /// Ce script g�re tpi ce qui implique le grappin du joueur
    /// </summary>

    [Header("Infos Grappin")]
    public Camera cam; // Cam�ra du joueur
    public Transform grappin; // R�f�rence au transform du empty enfant du grappin pour que la corde soit au bout du grappin
    public GameObject imageAideCible; // Visualisation de la position cibl� par le joueur avec le grappin
    Vector3 positionCible; // Position cibl� par le joueur
    Vector3 positionCibleOffset; // Position � laquelle le joueur est envoy�
    public float distanceMax; // Port�e du grappin
    public float tailleAideCible; // Taille de l'aide � la vis�


    [Header("Son")]
    public AudioClip sonGrappin; // Joue lorsque le grappin est activ�


    [Header("Corde Grappin")]
    public LineRenderer corde; // R�f�rence au line renderer du gameobject grappin


    [Header("Infos Personnages")]
    public MouvementPersonnage refScriptPerso; // Ref�rence au script de mouvement du personnage
    public float vitesseDeplacement; // Vitesse � laquelle le joueur se d�place avec le grappin
    bool enMouvement; // D�finit si le joueur est en mouvement avec le grappin ou non


    [Header("Objets Int�ractifs")]
    /* Variables infos objet */
    public GameObject objetInteractifOriginal; // Gameobject � instancier
    GameObject objetInteractif; // Gameobject cibl� par le grappin
    GameObject objetInteractifMain; // Instantiation du Gameobject objetInteractifOriginal
    public float distanceLancer; // Port�e du lanc�
    bool peutLancerObjet; // D�finit si le joueur peut lancer l'objet en main ou non
    bool objetEnMain; // Pour �viter d'attraper plus d'un objet � la fois

    /* Variables d�placement */
    public float vitesseDeplacementObjet; // Vitesse de d�placement de l'objet lanc�
    bool objetEnMouvement; // D�finit si l'objet a �t� lanc� et est en mouvement ou non




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // Le joueur n'a pas d'objet en main lorsque la partie commence
        objetEnMain = false;
    }



    //// Utiliser Update pour d�tecter les touches
    void Update()
    {
        // V�rifier si le joueur est en vie
        if (MouvementPersonnage.enVie)
        {
            // Le joueur active son grappin lorsqu'il fait clic droit
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                EnvoyerGrappin();
            }
        }

        if (enMouvement)
        {
            // Le grappin se d�sactive lorsque le joueur arrive � destination ou s'il appuie sur ESPACE ou s'il commence un Transit(MAJGauche)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                refScriptPerso.peutBouger = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                corde.enabled = false;
                enMouvement = false;

                // Ajout d'un saut vers le haut lorsque le joueur arrive � destination
                if (Vector3.Distance(transform.position, positionCibleOffset) < 2f)
                {
                    GetComponent<Rigidbody>().AddRelativeForce(0f, 5f, 5f, ForceMode.Impulse);
                }
            }

        }

        // Le joueur lance l'objet en main lorsqu'il fait click gauche
        if (Input.GetKeyDown(KeyCode.Mouse0) && peutLancerObjet)
        {
            LancerObjet(objetInteractifMain);
            peutLancerObjet = false;
        }



        ///////////  Affichage de l'aide � la vis�
        Ray rayonCam = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit infoCollision;
        // Lorsque le joueur vise qqch
        if (Physics.Raycast(grappin.position, rayonCam.direction, out infoCollision, distanceMax))
        {
            GameObject objetCollision = infoCollision.transform.gameObject;
            AfficherAideCible(objetCollision, infoCollision);
        }
        // Lorsque l'aide � la vis� d�tecte qqch
        else if (Physics.SphereCast(grappin.position, tailleAideCible, rayonCam.direction, out infoCollision, distanceMax)
        && !enMouvement && !objetEnMouvement)
        {
            GameObject objetCollision = infoCollision.transform.gameObject;
            AfficherAideCible(objetCollision, infoCollision);
        }
        else
        {
            imageAideCible.SetActive(false);
        }
    }



    //// Utiliser FixedUpdate pour les d�placements
    private void FixedUpdate()
    {
        // D�placer le joueur vers la position cibl� (positionCible)
        if (enMouvement)
        {
            transform.position = Vector3.Lerp(transform.position, positionCibleOffset, vitesseDeplacement * Time.deltaTime / Vector3.Distance(transform.position, positionCible));

            if (Vector3.Distance(transform.position, positionCibleOffset) < 1f)
            {
                refScriptPerso.peutBouger = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                corde.enabled = false;

                // Ajout d'un effet de saut lorsque le joueur arrive � destination
                if (Vector3.Distance(transform.position, positionCibleOffset) < 1f)
                {
                    GetComponent<Rigidbody>().AddRelativeForce(0f, 5f, 5f, ForceMode.Impulse);
                    enMouvement = false;
                }
            }
        }

        // D�placer l'objet cibl� (objetInteractif) vers le joueur
        if (objetEnMouvement)
        {
            objetInteractif.transform.position = Vector3.Lerp(objetInteractif.transform.position, grappin.position, vitesseDeplacementObjet * Time.deltaTime / Vector3.Distance(transform.position, objetInteractif.transform.position));

            // D�sactiver le grappin et attraper l'objet lorsqu'il est suffisamment proche
            if (Vector3.Distance(objetInteractif.transform.position, grappin.position) < 1f)
            {
                AttraperObjet();

                objetEnMouvement = false;
                corde.enabled = false;
                corde.positionCount = 0;
            }
        }
    }



    //// Utiliser LateUpdate pour positionner la corde(LineRenderer) apr�s l'Update pour �viter la latence dans sa position
    private void LateUpdate()
    {
        if (enMouvement)
        {
            corde.SetPosition(0, grappin.position); // Actualiser la position de l'origine de la corde
            corde.SetPosition(1, positionCible);
            corde.positionCount = 2;
        }
        if (objetEnMouvement)
        {
            corde.SetPosition(0, grappin.position); // Actualiser la position de l'origine de la corde
            corde.SetPosition(1, objetInteractif.transform.position); // Actualiser le point d'attache de la corde
            corde.positionCount = 2;
        }
    }



    /// <summary>
    /// Fonction qui envoie le grappin vers l'endroit vis� par le curseur du joueur
    /// </summary>
    void EnvoyerGrappin()
    {
        //Point de r�f�rence pour le grappin (point � atteindre)
        Ray rayonCam = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit infoCollision;

        // V�rifier si le joueur vise une surface
        if (Physics.Raycast(grappin.position, rayonCam.direction, out infoCollision, distanceMax))
        {
            GameObject objetCollision = infoCollision.transform.gameObject;

            //// V�rifier la nature de l'endroit vis�
            if (objetCollision.layer == 6) // Environnement Int�ractif
            {
                positionCible = infoCollision.point;
                positionCibleOffset = new Vector3(positionCible.x, positionCible.y + 3f, positionCible.z);

                // Bloquer les d�placements du joueur pendant le trajet
                refScriptPerso.peutBouger = false;
                refScriptPerso.forceDeplacementHorizontal = 0;
                refScriptPerso.forceDeplacementZ = 0;
                gameObject.GetComponent<Rigidbody>().useGravity = false;

                //Corde du grappin
                corde.enabled = true;
                corde.SetPosition(0, grappin.position);
                corde.SetPosition(1, positionCible);
                GetComponent<AudioSource>().PlayOneShot(sonGrappin);

                enMouvement = true;

            }
            else if (objetCollision.layer == 7 && !objetEnMain) // Objets Int�ractifs
            {
                GetComponent<AudioSource>().PlayOneShot(sonGrappin);
                objetEnMouvement = true;
                objetInteractif = infoCollision.transform.gameObject;

                corde.enabled = true;
                corde.SetPosition(0, grappin.position);
                corde.SetPosition(1, objetInteractif.transform.position);
            }// Sinon, activer l'aide � la viser (en utilisant un Spherecast � la place d'un Raycast)
            else if (Physics.SphereCast(grappin.position, tailleAideCible, rayonCam.direction, out infoCollision, distanceMax))
            {
                objetCollision = infoCollision.transform.gameObject;
                // V�rifier la nature de l'endroit vis�
                if (objetCollision.layer == 6) // Environnement Int�ractif
                {
                    GetComponent<AudioSource>().PlayOneShot(sonGrappin);
                    positionCible = infoCollision.point;
                    positionCibleOffset = new Vector3(positionCible.x, positionCible.y + 3f, positionCible.z);

                    // Bloquer les d�placements du joueur pendant le trajet
                    refScriptPerso.peutBouger = false;
                    refScriptPerso.forceDeplacementHorizontal = 0;
                    refScriptPerso.forceDeplacementZ = 0;
                    gameObject.GetComponent<Rigidbody>().useGravity = false;

                    //Corde du grappin
                    corde.enabled = true;
                    corde.SetPosition(0, grappin.position);
                    corde.SetPosition(1, positionCible);

                    enMouvement = true;

                }
                else if (objetCollision.layer == 7 && !objetEnMain) // Objets Int�ractifs
                {
                    GetComponent<AudioSource>().PlayOneShot(sonGrappin);
                    objetEnMouvement = true;
                    objetInteractif = infoCollision.transform.gameObject;

                    //Corde du grappin
                    corde.enabled = true;
                    corde.SetPosition(0, grappin.position);
                    corde.SetPosition(1, objetInteractif.transform.position);
                    Debug.Log("Objet");
                }
            }
        }// Sinon, activer l'aide � la viser (en utilisant un Spherecast � la place d'un Raycast)
        else if (Physics.SphereCast(grappin.position, tailleAideCible, rayonCam.direction, out infoCollision, distanceMax))
        {
            GameObject objetCollision = infoCollision.transform.gameObject;
            // V�rifier la nature de l'endroit vis�
            if (objetCollision.layer == 6) // Environnement Int�ractif
            {
                GetComponent<AudioSource>().PlayOneShot(sonGrappin);
                positionCible = infoCollision.point;
                positionCibleOffset = new Vector3(positionCible.x, positionCible.y + 3f, positionCible.z);

                // Bloquer les d�placements du joueur pendant le trajet
                refScriptPerso.peutBouger = false;
                refScriptPerso.forceDeplacementHorizontal = 0;
                refScriptPerso.forceDeplacementZ = 0;
                gameObject.GetComponent<Rigidbody>().useGravity = false;

                //Corde du grappin
                corde.enabled = true;
                corde.SetPosition(0, grappin.position);
                corde.SetPosition(1, positionCible);

                enMouvement = true;

            }
            else if (objetCollision.layer == 7 && !objetEnMain) // Objets Int�ractifs
            {
                GetComponent<AudioSource>().PlayOneShot(sonGrappin);
                objetEnMouvement = true;
                objetInteractif = infoCollision.transform.gameObject;

                //Corde du grappin
                corde.enabled = true;
                corde.SetPosition(0, grappin.position);
                corde.SetPosition(1, objetInteractif.transform.position);
            }
        }

    }



    /// <summary>
    /// Fonction qui affiche l'aide � la vis�
    /// </summary>
    /// <param name="objetCible"></param>
    /// <param name="cible"></param>
    void AfficherAideCible(GameObject objetCible, RaycastHit cible)
    {
        // V�rifier la nature de l'endroit vis�
        if (objetCible.layer == 6 || objetCible.layer == 7)  // Environnement Int�ractif
        {
            imageAideCible.SetActive(true);
            imageAideCible.transform.position = cible.transform.position;
            imageAideCible.transform.rotation = cible.transform.rotation;
        }
        else
        {
            imageAideCible.SetActive(false);
        }
    }



    /// <summary>
    /// Fonction qui permet au joueur d'attraper avec le grappin
    /// </summary>
    void AttraperObjet()
    {
        objetEnMain = true;
        objetInteractif.SetActive(false);
        objetInteractifMain = Instantiate(objetInteractifOriginal, objetInteractifOriginal.transform.position, objetInteractifOriginal.transform.rotation);
        objetInteractifMain.transform.parent = cam.transform;
        objetInteractifMain.SetActive(true);

        peutLancerObjet = true;
    }



    /// <summary>
    /// Fonction qui permet au joueur de lancer l'objet en main
    /// </summary>
    /// <param name="objetALancer"></param>
    void LancerObjet(GameObject objetALancer)
    {
        objetEnMain = false;

        Ray rayonCam = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit infoCollision;
        Vector3 directionProjectile = cam.transform.forward;
        
        if(Physics.Raycast(gameObject.transform.position, rayonCam.direction, out infoCollision, Mathf.Infinity))
        {
            directionProjectile = (infoCollision.point - transform.position).normalized;
        }

        Vector3 positionObjetCible = directionProjectile * distanceLancer;
        objetALancer.transform.parent = null;
        objetALancer.tag = "Projectile";
        objetALancer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        objetALancer.GetComponent<Rigidbody>().useGravity = true;
        objetALancer.GetComponent<BoxCollider>().enabled = true;
        objetALancer.gameObject.GetComponent<Rigidbody>().AddForce(positionObjetCible, ForceMode.Impulse);
    }
}