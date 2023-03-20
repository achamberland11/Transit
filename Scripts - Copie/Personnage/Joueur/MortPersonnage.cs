using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MortPersonnage : MonoBehaviour
{
    /// <summary>
    /// Ce script gère tout ce qui implique la mort du joueur
    /// Le joueur meurt lorsque les ennemis lui tirent dessus
    /// </summary>

    bool estMort;
    public GameObject panelMort;

    [Header("Son")]
    public AudioSource musiqueJeu;
    public AudioClip sonMort;




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Start is called before the first frame update
    void Start()
    {
        estMort = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estMort)
        {
            if(Input.GetKeyDown(KeyCode.R)) {
                Scene sceneActuel = SceneManager.GetActiveScene();
                int indexScene = sceneActuel.buildIndex;
                SceneManager.LoadScene(indexScene);
            }
        }
    }

    public void Mort()
    {
        MouvementPersonnage.enVie = false;
        musiqueJeu.enabled = false;
        GetComponent<AudioSource>().PlayOneShot(sonMort);
        Time.timeScale = 0f;
        panelMort.SetActive(true);
        estMort = true;
    }
}
