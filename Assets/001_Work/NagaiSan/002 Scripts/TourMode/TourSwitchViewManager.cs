using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourSwitchViewManager : MonoBehaviour
{
    #region Require Values
    public GameObject playerOVRC_Walk;
    public GameObject playerOVRC_Pos1;
    public GameObject playerOVRC_Pos2;

    public bool player_SitOnPos1Flag = false;
    public bool player_SitOnPos2Flag = false;
    /*
    public GameObject catOVRC_LS;
    public GameObject catOVRC_PB;
    public GameObject catOVRC_Scissors;*/

    /*
    public PlayerInputManager_Stage1_3 playerInputManagerS13;
    public CatInputManager catInputManager;
    */
    #endregion

    // Player sits on Bed.
    public void TourSwitchViewerOnStage1_Player_SitOnBed()
    {
        if (playerOVRC_Walk)
        {
            playerOVRC_Walk.SetActive(false);
            playerOVRC_Pos1.SetActive(true);

            player_SitOnPos1Flag = true;
        }
    }

    // Player sits on Chair.
    public void TourSwitchViewerOnStage1_Player_SitOnChair()
    {
        if (playerOVRC_Walk)
        {
            playerOVRC_Walk.SetActive(false);
            playerOVRC_Pos2.SetActive(true);

            player_SitOnPos2Flag = true;
        }
    }

    // Player returns walking.
    public void TourSwitchViewerOnStage1_Player_ReturnWalk()
    {
        #region Player sit on Pos1(Bed).
        if (player_SitOnPos1Flag)
        {
            playerOVRC_Pos1.SetActive(false);
            player_SitOnPos1Flag = false;

            playerOVRC_Walk.SetActive(true);
        }
        #endregion
        #region Player sit on Pos2(Human:Chair, Cat:Table).
        if (player_SitOnPos2Flag)
        {
            playerOVRC_Pos2.SetActive(false);
            player_SitOnPos2Flag = false;

            playerOVRC_Walk.SetActive(true);
        }
        #endregion
    }

    // Cat sits on Bed.
    public void TourSwitchViewerOnStage1_Cat_SitOnBed()
    {
        if (playerOVRC_Walk)
        {
            playerOVRC_Walk.SetActive(false);
            playerOVRC_Pos1.SetActive(true);

            player_SitOnPos1Flag = true;
        }
    }

    // Cat sits on Table.
    public void TourSwitchViewerOnStage1_Cat_SitOnTable()
    {
        if (playerOVRC_Walk)
        {
            playerOVRC_Walk.SetActive(false);
            playerOVRC_Pos2.SetActive(true);

            player_SitOnPos2Flag = true;
            Debug.Log($"playerOVRC_Pos2 is {playerOVRC_Pos2}");
        }
    }

}
