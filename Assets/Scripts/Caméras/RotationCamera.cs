using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    /// <summary>
    /// Ce script gère la rotation verticale de la caméra du joueur
    /// </summary>

    public float rotationYMax;
    public float rotationYMin;

    [Range(0.5f, 10f)]
    float vitesseRotation = ControlleurSensibilite.sensibiliteY;
    float positionXSouris;

    float rotationVerticalCamera;

    public MouvementPersonnage refScriptPerso;




    ////////////////////// APPEL DES FONCTIONS //////////////////////

    // Update is called once per frame
    private void Update()
    {
        if (MouvementPersonnage.enVie)
        {
            positionXSouris = Input.GetAxis("Mouse Y");
            vitesseRotation = ControlleurSensibilite.sensibiliteY;
        }
    }



    void FixedUpdate()
    {
        if (MouvementPersonnage.enVie)
        {
            rotationVerticalCamera -= positionXSouris * vitesseRotation;
            rotationVerticalCamera = Mathf.Clamp(rotationVerticalCamera, rotationYMin, rotationYMax);

            transform.localEulerAngles = new Vector3(rotationVerticalCamera, 0f, 0f);
        }
    }
}
