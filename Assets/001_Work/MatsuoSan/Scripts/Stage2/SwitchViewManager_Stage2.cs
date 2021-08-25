using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchViewManager_Stage2 : MonoBehaviour
{
    #region Require Values
    public GameObject catOVRC_Vase;
    public GameObject catOVRC_Chemical;
    public GameObject catOVRC_Door;

    public PlayerInputManager_Stage2 playerInputManager;
    public CatInputManager_Stage2 catInputManager;
    #endregion

    public void SwitchViewer()
    {
        GameObject ovrc = GameObject.Find("MyOVRPlayerController");

        // Cat move the first point near by Light Stand
        if (ovrc)
        {
            ovrc.SetActive(false);
            playerInputManager.iamCat = true;
            catOVRC_Vase.SetActive(true);
        }
    }

    public void ViewNextDangerousPoint()
    {
        // Cat move the second point near by Plastic Bag
        if (catInputManager.stage2_Vase_Point && !catInputManager.stage2_Chemical_Point)
        {
            catOVRC_Vase.SetActive(false);
            catOVRC_Chemical.SetActive(true);
        }

        // Cat move the third point near by Scissors
        if (catInputManager.stage2_Vase_Point && catInputManager.stage2_Chemical_Point && !catInputManager.stage2_Door_Point)
        {
            catOVRC_Chemical.SetActive(false);
            catOVRC_Door.SetActive(true);
        }
    }

}
