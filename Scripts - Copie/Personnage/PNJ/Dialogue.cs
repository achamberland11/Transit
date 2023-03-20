using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    /// <summary>
    /// Ce script gère l'activation des dialogues
    /// </summary>

    public int indexScene; // Index de la scène du niveau à charger en lien avec le PNJ
    public Camera cameraPNJ; // Camera attaché au PNJ
    public GameObject affichageTouche; // Affichage de la touche à saisir pour activer le diaologue (e)
    bool detectionDialogue; // Vrai lorsque le joueur est à proximité du PNJ


    [Header("Variables textes")]
    public string nom;
    [TextArea(3, 10)] // Le TextArea permet d'écrire du texte plus long dans l'inspecteur
    public string[] dialogues;// Tableau des différents dialogues du PNJ




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Start is called before the first frame update
    void Start()
    {
        detectionDialogue = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (detectionDialogue)
        {
            AfficherOptionDialogue();
        }
    }



    /// <summary>
    /// Appel la fonction qui débute le dialogue lorsque le joueur appuie sur E
    /// </summary>
    void AfficherOptionDialogue()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioSource sonDialogue = GetComponent<AudioSource>();
            GestionnaireDialogues.instance.CommencerDialogue(nom, dialogues, cameraPNJ, indexScene, sonDialogue);
            affichageTouche.gameObject.SetActive(false);
        }
    }



    private void OnTriggerEnter(Collider collision)
    {
        // Affiche la touche qu'il faut enfoncer pour débuter le dialogue lorsque le joueur est suffisament proche
        if (collision.gameObject.tag == "Joueur")
        {
            detectionDialogue = true;
            affichageTouche.gameObject.SetActive(true);
        }
    }



    private void OnTriggerExit(Collider collision)
    {
        // Ferme le panel qui affiche la touche qu'il faut enfoncer pour débuter le dialogue
        if (collision.gameObject.tag == "Joueur")
        {
            detectionDialogue = false;
            affichageTouche.gameObject.SetActive(false);
        }
    }
}
