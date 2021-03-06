using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchViewManager_Stage3 : MonoBehaviour
{
    #region Require Values
    public GameObject catOVRC_LS;
    public GameObject catOVRC_PB;
    public GameObject catOVRC_Scissors;

    public PlayerInputManager_Stage3 playerInputManagerS3;
    public CatInputManager_Stage3 catInputManager;
    #endregion

    public void SwitchViewer()
    {
        GameObject ovrc = GameObject.Find("MyOVRPlayerController");

        // Cat move the first point near by Light Stand
        if (ovrc)
        {
            ovrc.SetActive(false);
            playerInputManagerS3.iamCat = true;
            catOVRC_LS.SetActive(true);
        }
    }

    public void ViewNextDangerousPoint()
    {
        // Cat move the second point near by Plastic Bag
        if (catInputManager.stage1_LS_Point && !catInputManager.stage1_PB_Point)
        {
            catOVRC_LS.SetActive(false);
            catOVRC_PB.SetActive(true);
        }

        // Cat move the third point near by Scissors
        if (catInputManager.stage1_LS_Point && catInputManager.stage1_PB_Point && !catInputManager.stage1_Scissors_Point)
        {
            catOVRC_PB.SetActive(false);
            catOVRC_Scissors.SetActive(true);
        }
    }


    public void SwitchViewerOnStage0()
    {
        GameObject ovrc = GameObject.Find("TutorialMyOVRPlayerController");

        // Cat move the first point near by Light Stand
        if (ovrc)
        {
            ovrc.SetActive(false);
            playerInputManagerS3.iamCat = true;
            catOVRC_Scissors.SetActive(true);
        }
    }

}
