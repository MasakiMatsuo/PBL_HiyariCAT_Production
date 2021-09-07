using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchViewManager_P : MonoBehaviour
{
    #region Require Values
    public GameObject catOVRC_Pos01;
    public GameObject catOVRC_Pos02;
    public GameObject catOVRC_Pos03;

    public PlayerInputManager_P playerInputManager_P;
    public CatInputManager_P catInputManager_P;
    #endregion

    public void SwitchViewer()
    {
        GameObject ovrc = GameObject.Find("MyOVRPlayerController");

        // Cat move the first point near by Light Stand
        if (ovrc)
        {
            ovrc.SetActive(false);
            playerInputManager_P.iamCat = true;
            catOVRC_Pos01.SetActive(true);
        }
    }

    public void ViewNextDangerousPoint()
    {
        // Cat move the second point near by Pos2
        if (catInputManager_P.pos01_ReadFlag && !catInputManager_P.pos02_ReadFlag)
        {
            catOVRC_Pos01.SetActive(false);
            catOVRC_Pos02.SetActive(true);
        }

        // Cat move the third point near by Pos3
        if (catInputManager_P.pos01_ReadFlag && catInputManager_P.pos02_ReadFlag && !catInputManager_P.pos03_ReadFlag)
        {
            catOVRC_Pos02.SetActive(false);
            catOVRC_Pos03.SetActive(true);
        }
    }

    public void SwitchViewerOnStage0()
    {
        GameObject ovrc = GameObject.Find("TutorialMyOVRPlayerController");

        // Cat move the first point near by Light Stand
        if (ovrc)
        {
            ovrc.SetActive(false);
            playerInputManager_P.iamCat = true;
            catOVRC_Pos03.SetActive(true);
        }
    }
}
