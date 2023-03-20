using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlleurSensibilite : MonoBehaviour
{
    [Header("Sensibilités")]
    [Range(0.5f, 10f)]
    public static float sensibiliteX;
    [Range(0.5f, 10f)]
    public static float sensibiliteY;

    [Header("Glissières")]
    public Slider glissiereX;
    public Slider glissiereY;




    // Start is called before the first frame update
    void Awake()
    {
        if(!PlayerPrefs.HasKey("SensibiliteX") || !PlayerPrefs.HasKey("SensibiliteY"))
        {
            sensibiliteX = glissiereX.value;
            sensibiliteY = glissiereY.value;

            SauvegardeDonnees.SauvegardeSensibiliteX(sensibiliteX);
            SauvegardeDonnees.SauvegardeSensibiliteY(sensibiliteY);
        }
        else
        {
            sensibiliteX = PlayerPrefs.GetFloat("SensibiliteX");
            sensibiliteY = PlayerPrefs.GetFloat("SensibiliteY");
        }
    }



    /// <summary>
    /// Change la sensibilité X
    /// </summary>
    /// <param name="sensibilite"></param>
    public void AjusterSensibiliteX(float sensibilite)
    {
        sensibiliteX = sensibilite;
        SauvegardeDonnees.SauvegardeSensibiliteX(sensibiliteX);
    }



    /// <summary>
    /// Change la sensibilité Y
    /// </summary>
    /// <param name="sensibilite"></param>
    public void AjusterSensibiliteY(float sensibilite)
    {
        sensibiliteY = sensibilite;
        SauvegardeDonnees.SauvegardeSensibiliteY(sensibiliteY);
    }
}
