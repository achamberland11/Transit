using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
    [Header("NavAgent et IA")]
    public Transform joueur;
    NavMeshAgent navAgent;
    public bool peutBouger;
    public float distanceDetection;
    public float distanceArret;
    public float detectionAttaque;
    public bool peutTirer;
    public float delaiTir;

    [Header("Tir")]
    public GameObject balle;
    public GameObject particuleBalle;
    public float vitesseBalle;
    public AudioClip sonTir;

    [Header("Mort")]
    public ParticleSystem particuleDestruction;
    public GameObject fusil;
    public GameObject cheveux;
    public GameObject corps;
    public AudioClip sonMort;




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        peutBouger = true;

        // L'appel de la fonction de tir de l'ennemi est continuellement
        // Mais celle-ci s'éxécute seulement si la variable bool peutTirer est à true
        InvokeRepeating("TirerFusil", 2f, delaiTir);
    }



    // Update is called once per frame
    void Update()
    {

    }



    private void FixedUpdate()
    {
        float distanceJoueur = Vector3.Distance(joueur.position, gameObject.transform.position);
        RaycastHit infoCollision;
        bool raycast = Physics.Raycast(transform.position, gameObject.transform.forward, out infoCollision, detectionAttaque);
        Debug.DrawRay(transform.position, gameObject.transform.forward * detectionAttaque, Color.red);

        //// L'ennemi se déplace vers le joueur s'il est suffisament proche et S'arrête lorsqu'il est trop proche
        // En mouvement vers le joueur
        if (peutBouger && distanceJoueur < distanceDetection && distanceJoueur > distanceArret)
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(joueur.position);
            gameObject.GetComponent<Animator>().SetBool("EnMouvement", true);
            gameObject.GetComponent<Animator>().SetBool("EnCombat", false);
            if(raycast)
            {
                // Tir sur le joueur lorsqu'il est dans sa ligne de mire
                if (infoCollision.transform.gameObject.tag == "Joueur")
                {
                    peutTirer = true;
                }
            }
        }
        // Arrêt
        else
        {
            navAgent.isStopped = true;
            // Lorsqu'il est proche du joueur
            if(distanceDetection >= distanceArret && peutBouger)
            {
                gameObject.GetComponent<Animator>().SetBool("EnCombat", true);
                gameObject.GetComponent<Animator>().SetBool("EnMouvement", false);
                gameObject.transform.LookAt(joueur.position);
                if (raycast)
                {
                    // Tir sur le joueur lorsqu'il est dans sa ligne de mire
                    if (infoCollision.transform.gameObject.tag == "Joueur")
                    {
                        peutTirer = true;
                    }
                }
            }
            // Lorsqu'il est loin du joueur
            else
            {
                gameObject.GetComponent<Animator>().SetBool("EnMouvement", false);
                gameObject.GetComponent<Animator>().SetBool("EnCombat", false);
            }
        }
    }

    
    /// <summary>
    /// Fonction qui fait en sorte que l'ennemi tir sur le joueur
    /// Est appelé continuellement
    /// S'exécute seulement si la variable peutTirer est à true
    /// </summary>
    void TirerFusil()
    {
        if(peutTirer)
        {
            GetComponent<AudioSource>().PlayOneShot(sonTir);
            GameObject nouvelleBalle = Instantiate(balle, balle.transform.position, balle.transform.rotation); // La balle prend la position et la rotation de son original
            nouvelleBalle.SetActive(true);
            nouvelleBalle.transform.LookAt(joueur);
            nouvelleBalle.GetComponent<Rigidbody>().velocity = nouvelleBalle.transform.forward * vitesseBalle;
        }
        else
        {
            return;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Joueur" && TransitPerso.enTransit)
        {
            FracasserEnnemi();
        }

        // L'ennemi peut mourri par les projectiles seulement si le projectile en question a une velocité y supérieur à 0
        if(collision.gameObject.tag == "Projectile")
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.y > 0)
            {
                FracasserEnnemi();
            }
        }
    }



    /// <summary>
    /// Fait jouer le système de particule de mort de l'ennemi
    /// Détruit l'ennemi ensuite
    /// </summary>
    public void FracasserEnnemi()
    {
        GetComponent<AudioSource>().PlayOneShot(sonMort, 1.75f);
        peutBouger = false;
        peutTirer = false;
        particuleDestruction.Play();
        corps.SetActive(false);
        cheveux.GetComponent<Rigidbody>().useGravity = true;
        cheveux.GetComponent<MeshCollider>().enabled = true;
        fusil.GetComponent<Rigidbody>().useGravity = true;
        fusil.GetComponent<MeshCollider>().enabled = true;
        Destroy(gameObject, 3.25f);
    }
}