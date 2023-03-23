using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitPerso : MonoBehaviour
{
    /************************* TRANSIT : Sorte de téléportation sur une courte distance
     * vers l'endroit où regarde le joueur *******************/
    /// <summary>
    /// Ce scipt gère tout ce qui implique la capacité Transit du personnage
    /// </summary>

    [Header("Infos Transit")]
    public Camera cam; // Caméra FPS
    Vector3 positionCible; // Position cible du Transit
    public float delaiRecharge;  // Délai de recharge entre chaque Transit
    public float pourcentageTransit; // Le joueur ne peut plus exécuter son Transi lorsque ce pourcentage est insuffisant (lorsque son mana est insuffisant)
    public float pourcentageTransitMax; // Mana maximum
    public float pourcentageTransitActuel; // Mana actuel
    public GameObject imagePourcentageTransit; // Représentation visuel du mana du joueur

    [Header("Variables Déplacement")]
    public MouvementPersonnage refScriptPerso; // Référence du scipt de mouvement du personnage
    public float distanceMax; // Portée du transit (125)
    public float distanceMaxY; // Effet de saut vers le haut lorsque le Transit est activé
    public static bool enTransit; // Détermine si le joueur est en cours de transit ou non
    bool peutTransit; // Détermine si le joueur peut éxécuter son Transit ou non

    [Header("Variables Son")]
    public AudioClip sonTransit; // Joue lorsque le joueur éxécute son Transit
    public AudioClip sonMana; // Joue lorsque le mana du joueur est insuffisant et qu'il ne peut faire son transit




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Start is called before the first frame update
    void Start()
    {
        peutTransit = true;
        enTransit = false;
    }



    //// Utiliser Update pour détecter les touches
    void Update()
    {
        pourcentageTransit = pourcentageTransitActuel / pourcentageTransitMax;
        imagePourcentageTransit.GetComponent<Image>().fillAmount = pourcentageTransit;

        // Augmenter le mana du Transit tant qu'il n'est pas au max
        if(pourcentageTransitActuel < pourcentageTransitMax)
        {
            pourcentageTransitActuel += 0.025f;
        }

        // Vérifier si le joueur a suffisamment de mana pour faire un Transit
        if (pourcentageTransitActuel >= 15f)
        {
            peutTransit = true;
        }
        else
        {
            peutTransit = false;
        }

        if (MouvementPersonnage.enVie)
        {
            // Activer Transit
            if (Input.GetKeyDown(KeyCode.LeftShift) && peutTransit)
            {
                GetComponent<AudioSource>().PlayOneShot(sonTransit);
                ActiverTransit();
                enTransit = true;
                peutTransit = false;
            }
            // Jouer le sonMana lorsque le joueur essai d'éxécuter son Transit mais n'a pas suffisamment de Mana
            if(Input.GetKeyDown(KeyCode.LeftShift) && !peutTransit)
            {
                GetComponent<AudioSource>().PlayOneShot(sonMana);
            }
        }

    }



    private void FixedUpdate()
    {
        if (enTransit)
        {
            Invoke("RechargerTransit", delaiRecharge);
            Invoke("DesactiverTransit", 0.25f);
        }
    }



    /// <summary>
    /// Fonction qui active le transit
    /// </summary>
    void ActiverTransit()
    {
        Ray rayonCam = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit infoCollision;
        pourcentageTransitActuel -= 15f;

        //if (raycast)
        //{
        //    // Vérifier si un mur est devant le joueur pour éviter qu'il ne passe à travers
        //    if (Vector3.Distance(transform.position, infoCollision.transform.position) < distanceMaxY)
        //    {
        //        positionCible = infoCollision.point;
        //    }
        //    else
        //    {
        //        positionCible = rayonCam.direction * distanceMax + gameObject.transform.up * distanceMaxY;
        //    }
        //}
        //else
        //{
        //    positionCible = rayonCam.direction * distanceMax + gameObject.transform.up * distanceMaxY;
        //}

        positionCible = rayonCam.direction * distanceMax + gameObject.transform.up * distanceMaxY;

        if (Physics.Raycast(transform.position, transform.forward, out infoCollision, 10f))
        {
            GameObject objetCollision = infoCollision.transform.gameObject;
            if(objetCollision.tag == "Environnement")
            {
                positionCible = infoCollision.point - transform.position;
            }
        }

        GetComponent<Rigidbody>().AddForce(positionCible, ForceMode.Impulse);
    }



    /// <summary>
    /// Fonction qui désactive le Transit (tant que enTransit == true, le joueur peut éliminer les ennemis)
    /// </summary>
    void DesactiverTransit()
    {
        enTransit = false;
    }



    /// <summary>
    /// Fonction qui permet de réactiver le Transit
    /// </summary>
    void RechargerTransit()
    {
        peutTransit = true;
    }
}