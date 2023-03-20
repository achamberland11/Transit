using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GestionnaireDialogues : MonoBehaviour
{
    /* Ce script utilise le syst�me de Singleton
     * Le scipt se stock donc lui-m�me dans la varialbe static instance
     * Les �l�ments public du script peuvent donc �tre utilis�s plus facilement dans les autres script */
    /// <summary>
    /// Ce script g�re tout les dialogues
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

    [Header("Cam�ras")]
    Camera cameraDialoguePNJ;
    public Camera cameraDialoguePerso;
    public Camera cameraPerso;

    Queue<string> phrases = new Queue<string>(); // Queue permet de cr�er une file d'attente (de string dans ce contexte)




    ////////////////////// APPEL DES FONCTIONS //////////////////////
    
    private void Awake()
    {
        // Envoyer un message d'erreir s'il y a + 1 instance dans la sc�ne
        if (instance != null)
        {
            Debug.LogError("Plus d'une instance du gestionnaire de dialogues (GestionnaireDialogues) dans la sc�ne");
            return;
        }

        instance = this;
    }



    private void Start()
    {
        nomJoueur = PlayerPrefs.GetString("NomUtilisateur");
    }



    /// <summary>
    /// D�marre le dialogue avec le PNJ concern�
    /// </summary>
    /// <param name="nom"></param>
    /// <param name="dialogues"></param>
    public void CommencerDialogue(string nom, string[] dialogues, Camera cameraPNJ, int indexScene, AudioSource sonDialogue)
    {
        dialogueText.text = "";
        panelDialogue.gameObject.GetComponent<Animator>().SetTrigger("dialogueActif");
        Cursor.lockState = CursorLockMode.None;
        MouvementPersonnage.enVie = false; // Jug� mort pour emp�cher le joueur de se d�placer et de bouger la cam�ra pendant le dialogue(il n'est pas vrm mort, pas de panique)

        nomPNJ = nom;
        nomText.text = nom;

        sonPNJ = sonDialogue;
        sonPNJ.Play();

        phrases.Clear();

        foreach (string phrase in dialogues)
        {
            phrases.Enqueue(phrase); // Enqueue permet d'ajouter des �l�ments dans la file d'attente
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
            string dialogue = phrases.Dequeue(); // R�cup�re le prochain �l�ment dans la file d'attente
            StopAllCoroutines(); // Permet d'arr�ter l'effet de machine � �crire si le joueur passe au prochain dialogue avant sa fin
            StartCoroutine(EcrireDialogue(dialogue));

            if(dialogue == "...")
            {
                Debug.Log("�a Marche!!!!!");
            }

            // Change la cam�ra et le nom de la personne qui parle lorsque le nombre de dialogue restant est pair
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
    /// Commence le niveau li� au PNJ en question. Cette fonction est appel� avec un btn en jeu
    /// </summary>
    public void ChargerMission()
    {
        SceneManager.LoadScene(indexMission);
    }



    /// <summary>
    /// Ferme le panel qui propose au joueur de commencer la mission ou non. Cette fonction est appel� avec un btn en jeu
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
    /// Coroutine qui permet d'�crire le dialogue avec un effet de machine � �crire
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
