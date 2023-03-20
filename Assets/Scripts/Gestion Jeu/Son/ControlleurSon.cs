using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlleurSon : MonoBehaviour
{
    [Header("Son Général")]
    [Range(0f, 1f)]
    public static float volumeGeneral;
    public static bool enSourdineGeneral = false;
    public Slider glissiereGeneral;
    public Toggle sourdineGeneral;

    [Header("Musique")]
    GameObject[] objetsMusique;
    AudioSource musique; 
    [Range(0f, 1f)]
    public static float volumeMusique;
    public static bool enSourdineMusique = false;
    public Slider glissiereMusique;
    public Toggle sourdineMusique;



    private void Awake()
    {
        objetsMusique = GameObject.FindGameObjectsWithTag("Musique");

        if (!PlayerPrefs.HasKey("VolumeGeneral"))
        {
            volumeGeneral = 1;
            volumeMusique = 1;
            SauvegardeDonnees.SauvegardeSon(volumeGeneral, volumeMusique);
        }
        else
        {
            volumeGeneral = PlayerPrefs.GetFloat("VolumeGeneral");
            volumeMusique = PlayerPrefs.GetFloat("VolumeMusique");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (enSourdineGeneral)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = volumeGeneral;
        }

        foreach (GameObject objetMusique in objetsMusique)
        {
            musique = objetMusique.GetComponent<AudioSource>();

            musique.mute = enSourdineMusique;
            musique.volume = volumeMusique;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enSourdineGeneral)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = volumeGeneral;
        }

        foreach (GameObject objetMusique in objetsMusique)
        {
            musique = objetMusique.GetComponent<AudioSource>();

            musique.mute = enSourdineMusique;
            musique.volume = volumeMusique;
        }
    }



    /// <summary>
    /// Change le volume général
    /// </summary>
    /// <param name="volume"></param>
    public void ChangerVolumeGeneral(float volume)
    {
        volumeGeneral = volume;

        SauvegardeDonnees.SauvegardeSon(volumeGeneral, volumeMusique);
    }



    /// <summary>
    /// Met en sourdine et inversement pour le son
    /// </summary>
    /// <param name="sourdine"></param>
    public void AlternerSourdineGeneral(bool sourdine)
    {
        enSourdineGeneral = sourdine;
    }



    /// <summary>
    /// Change le volume de la musique
    /// </summary>
    /// <param name="volume"></param>
    public void ChangerVolumeMusique(float volume)
    {
        volumeMusique = volume;

        SauvegardeDonnees.SauvegardeSon(volumeGeneral, volumeMusique);
    }



    /// <summary>
    /// Met en sourdine et inversement pour la musique
    /// </summary>
    /// <param name="sourdine"></param>
    public void AlternerSourdineMusique(bool sourdine)
    {
        enSourdineMusique = sourdine;
    }
}