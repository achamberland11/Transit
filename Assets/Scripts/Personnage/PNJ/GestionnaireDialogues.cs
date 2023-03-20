using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GestionnaireDialogues : MonoBehaviour
{
    /* Ce script utilise le système de Singleton
     * Le scipt se stock donc lui-même dans la varialbe static instance
     * Les éléments public du script peuvent donc être utilisés plus facilement dans les autres script */
    /// <summary>
    /// Ce script gère tout les dialogues
    /// </summary>

    [Header("Instance")]
    public static GestionnaireDialogues instance;

    [Header("Panel")]
    public GameObject panelMission;
    public GameObject panelDialogue;

    [Header("Textes et Titres")]
    public TextMeshProUGUI nomText;
    public TextMeshProUGUI dialogueText;


    int indexMission;
    string nomPNJ;
    string nomJoueur;
    AudioSource sonPNJ;

    [Header("Caméras")]
    Camera cameraDialoguePNJ;
    public Camera cameraDialoguePerso;
    public Camera cameraPerso;

    Queue<string> phrases = new Queue<string>(); // Queue permet de créer une file d'attente (de string dans ce contexte)




    ////////////////////// APPEL DES FONCTIONS //////////////////////
    
    private void Awake()
    {
        // Envoyer un message d'erreir s'il y a + 1 instance dans la scène
        if (instance != null)
        {
            Debug.LogError("Plus d'une instance du gestionnaire de dialogues (GestionnaireDialogues) dans la scène");
            return;
        }

        instance = this;
    }



    private void Start()
    {
        nomJoueur = PlayerPrefs.GetString("NomUtilisateur");
    }



    /// <summary>
    /// Démarre le dialogue avec le PNJ concerné
    /// </summary>
    /// <param name="nom"></param>
    /// <param name="dialogues"></param>
    public void CommencerDialogue(string nom, string[] dialogues, Camera cameraPNJ, int indexScene, AudioSource sonDialogue)
    {
        dialogueText.text = "";
        panelDialogue.gameObject.GetComponent<Animator>().SetTrigger("dialogueActif");
        Cursor.lockState = CursorLockMode.None;
        MouvementPersonnage.enVie = false; // Jugé mort pour empécher le joueur de se déplacer et de bouger la caméra pendant le dialogue(il n'est pas vrm mort, pas de panique)

        nomPNJ = nom;
        nomText.text = nom;

        sonPNJ = sonDialogue;
        sonPNJ.Play();

        phrases.Clear();

        foreach (string phrase in dialogues)
        {
            phrases.Enqueue(phrase); // Enqueue permet d'ajouter des éléments dans la file d'attente
        }

        indexMission = indexScene;
        cameraDialoguePNJ = cameraPNJ;
        cameraPerso.gameObject.SetActive(false);
        cameraDialoguePNJ.gameObject.SetActive(true);

        Invoke("AfficherDialogue", 0.75f);
    }



    /// <summary>
    /// Affiche le dialogue suivant avec le PNJ
    /// </summary>
    public void AfficherDialogue()
    {
        if (phrases.Count > 0)
        {
            dialogueText.text = "";
            string dialogue = phrases.Dequeue(); // Récupère le prochain élément dans la file d'attente
            StopAllCoroutines(); // Permet d'arrêter l'effet de machine à écrire si le joueur passe au prochain dialogue avant sa fin
            StartCoroutine(EcrireDialogue(dialogue));

            if(dialogue == "...")
            {
                Debug.Log("Ça Marche!!!!!");
            }

            // Change la caméra et le nom de la personne qui parle lorsque le nombre de dialogue restant est pair
            // Une conversation doit donc avoir un nombre de dialogue impair pour que ceci fonctionne
            // Montre le PNJ lorsque c'est pair et le joueur lorsque c'est impair
            if (phrases.Count % 2 == 0)
            {
                nomText.text = nomPNJ;
                sonPNJ.Play();
                cameraDialoguePerso.gameObject.SetActive(false);
                cameraDialoguePNJ.gameObject.SetActive(true);
            }
            else
            {
                nomText.text = nomJoueur;
                sonPNJ.Pause();
                cameraDialoguePerso.gameObject.SetActive(true);
                cameraDialoguePNJ.gameObject.SetActive(false);
            }
        }
        else
        {
            FinDialogue();
            return;
        }
    }



    /// <summary>
    /// Met fin au dialogue en cours
    /// </summary>
    public void FinDialogue()
    {
        panelDialogue.gameObject.GetComponent<Animator>().SetTrigger("dialogueInactif");
        sonPNJ.Pause();
        cameraDialoguePerso.gameObject.SetActive(true);
        cameraDialoguePNJ.gameObject.SetActive(false);
        panelMission.gameObject.SetActive(true);
    }


    /// <summary>
    /// Commence le niveau lié au PNJ en question. Cette fonction est appelé avec un btn en jeu
    /// </summary>
    public void ChargerMission()
    {
        SceneManager.LoadScene(indexMission);
    }



    /// <summary>
    /// Ferme le panel qui propose au joueur de commencer la mission ou non. Cette fonction est appelé avec un btn en jeu
    /// </summary>
    public void FermerPanelMission()
    {
        panelMission.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        MouvementPersonnage.enVie = true;
        cameraPerso.gameObject.SetActive(true);
        cameraDialoguePerso.gameObject.SetActive(false);
        cameraDialoguePNJ.gameObject.SetActive(false);
    }



    /// <summary>
    /// Coroutine qui permet d'écrire le dialogue avec un effet de machine à écrire
    /// </summary>
    /// <param name="phrase"></param>
    /// <returns></returns>
    IEnumerator EcrireDialogue(string phrase)
    {
        dialogueText.text = "";

        foreach (char lettre in phrase.ToCharArray())
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
