using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourSwitchViewManager : MonoBehaviour
{
    #region Require Values
    public GameObject playerOVRC_Walk;
    public GameObject playerOVRC_Bed;



    public bool player_SitOnBedFlag = false;
    /*
    public GameObject catOVRC_LS;
    public GameObject catOVRC_PB;
    public GameObject catOVRC_Scissors;*/

    /*
    public PlayerInputManager_Stage1_3 playerInputManagerS13;
    public CatInputManager catInputManager;
    */
    #endregion


    public void TourSwitchViewerOnStage1_Player_SitOnBed()
    {
        if (playerOVRC_Walk)
        {
            playerOVRC_Walk.SetActive(false);

            playerOVRC_Bed.SetActive(true);

            player_SitOnBedFlag = true;
        }

    }


    public void TourSwitchViewerOnStage1_Player_ReturnWalk()
    {
        if (player_SitOnBedFlag)
        {
            playerOVRC_Bed.SetActive(false);
            player_SitOnBedFlag = false;

            playerOVRC_Walk.SetActive(true);
        }
    }

        /*
        public void SwitchViewer()
        {
            GameObject ovrc = GameObject.Find("MyOVRPlayerController");

            // Cat move the first point near by Light Stand
            if (ovrc)
            {
                ovrc.SetActive(false);
                playerInputManagerS13.iamCat = true;
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
        */
    }
