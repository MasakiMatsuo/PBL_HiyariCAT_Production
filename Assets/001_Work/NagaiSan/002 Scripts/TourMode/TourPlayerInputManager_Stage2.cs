using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TourPlayerInputManager_Stage2 : MonoBehaviour
{
    /// CAUTION !!! /////////////////////////////////////////////////
    /// You shold search "Need to fix" before Finalize.
    /// If you can find it, we still have some things to fix 
    /// CAUTION !!! /////////////////////////////////////////////////
    
    #region Require Values
    #region Player Values
    public GameObject playerRightController;
    public LineRenderer rayObject;
    #endregion

    public Animator capacityAnimation;

    #region UIs
    public GameObject pauseMenu;
    public GameObject sitOnPos1B;
    public GameObject sitOnPos2B;

    public GameObject sitOnPos1S;
    public GameObject sitOnPos1D;

    public GameObject message1;
    public GameObject message2;
    public GameObject message3;
    #endregion

    #region Other Scripts
    public TourSwitchViewManager tourSVM;
    #endregion
    
    #region Flags
    private bool pFlg = false;
    private bool sobFlg = false;
    private bool socFlg = false;

    private bool sosFlg = false;
    private bool sodFlg = false;

    private bool mFlg1 = false;
    private bool mFlg2 = false;
    private bool mFlg3 = false;

    //Å¶UI
    public bool hitFlgD2_1 = false;
    public bool hitFlgD2_2 = false;
    public bool hitFlgD2_3 = false;

    #endregion
    #endregion

    void Start()
    {
        InitApp();
    }

    void Update()
    {
        InitMyPlayerRay();
        TourMode();

        if (tourSVM.player_SitOnPos1Flag || tourSVM.player_SitOnPos2Flag)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.Y))
            {
                ReturnToPosition();
            }
        }

    }

    public void InitApp()
    {
        
        #region Close Desk Capacity when Start 
        capacityAnimation.SetBool("Touch", false);
        #endregion
        
        #region Initialize UIs
        pauseMenu.SetActive(false);
        sitOnPos1B.SetActive(false);
        sitOnPos2B.SetActive(false);

        sitOnPos1S.SetActive(false);
        #endregion
    }

    public void InitMyPlayerRay()
    {
        #region Create Start Point of Ray (Player)
        // Create Vertex(0:Start, 1:End point)
        rayObject.SetVertexCount(2);

        // Set Vertex0 (Start point == position in RightController)
        rayObject.SetPosition(0, playerRightController.transform.position);
        #endregion
    }

    public void TourMode()
    {
        // PauseMenu
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            PauseMenu();
        }
        PointingInteraction();
    }

    public void PauseMenu()
    {
        if (!pFlg)
        {
            pauseMenu.SetActive(true);
            pFlg = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            pFlg = false;
        }
    }

    public void PointingInteraction()
    {
        // Launch Ray
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            #region Set Vertex1 and With of Ray (Player)
            // Set Vertex1 (End point == position is 100m in front of RightController)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 100.0f);

            // Set Width of Ray (This is 2 Demention)
            rayObject.SetWidth(0.01f, 0.01f);
            #endregion

            #region Create Ray, this is same scale to Line Renderer (Player)
            RaycastHit[] hits;
            hits = Physics.RaycastAll(playerRightController.transform.position, playerRightController.transform.forward * 100.0f);
            #endregion

            // Selecting
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                foreach (var hit in hits)
                {
                    string tagName = hit.collider.tag;

                    #region Menu Pointing
                    #region Scene Transition
                    if (tagName == "Next00")
                    {
                        SceneManager.LoadScene("003 Stage1");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Next01")
                    {
                        SceneManager.LoadScene("004 Stage2");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Next02")
                    {
                        SceneManager.LoadScene("005 Stage3");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Retry")
                    {
                        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    }
                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("008 EndScene");// Need to fix "scene.name" when Finalize
                    }

                    #region Debug Cat Mode
                    else if (tagName == "Debug_CatMode")
                    {
                        SceneManager.LoadScene("006 TourStage1_Cat");// Need to fix "scene.name" when Finalize
                    }
                    #endregion // Debug Cat Mode

                    #region Debug Human Mode
                    else if (tagName == "Debug_HumanMode")
                    {
                        SceneManager.LoadScene("006 TourStage1_Human");// Need to fix "scene.name" when Finalize
                    }
                    #endregion // Debug Human Mode

                    #region Debug Button NextStage
                    else if (tagName == "Debug_NextStage")
                    {
                        if (SceneManager.GetActiveScene().name == "003 Stage1")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("004 Stage2");// Need to fix "scene.name" when Finalize
                        }
                        else if (SceneManager.GetActiveScene().name == "004 Stage2")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("005 Stage3");// Need to fix "scene.name" when Finalize
                        }
                        else if (SceneManager.GetActiveScene().name == "002 Stage0")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("003 Stage1");// Need to fix "scene.name" when Finalize
                        }
                    }
                    #endregion // Debug Button NextStage
                    #endregion // Scene Transition
                    #endregion // Menu Pointing

                    #region Interaction of Capacity
                    if (tagName == "Capacity")
                    {
                        PointingDeskCapacity();
                        break;
                    }
                    #endregion

                    #region Tour Mode Interaction

       

                    #region SitOnShelf
                    if (tagName == "Stage2_Shelf")
                    {
                        if (!sosFlg)
                        {
                            sitOnPos1S.SetActive(true);
                            sosFlg = true;
                        }
                        else
                        {
                            sitOnPos1S.SetActive(false);
                            sosFlg = false;
                        }
                    }
                    if (tagName == "Stage2_Shelf")
                    {
                        sitOnPos1S.SetActive(false);
                        sosFlg = false;
                        tourSVM.TourSwitchViewerOnStage2_Cat_SitOnShelf();
                    }
                    #endregion // SitOnShelf

                    #region Doma
                    if (tagName == "Stage2_Doma")
                    {
                        if (!sodFlg)
                        {
                            sitOnPos1D.SetActive(true);
                            sodFlg = true;
                        }
                        else
                        {
                            sitOnPos1D.SetActive(false);
                            sodFlg = false;
                        }
                    }
                    if (tagName == "Stage2_Doma")
                    {
                        sitOnPos1D.SetActive(false);
                        sodFlg = false;
                        tourSVM.TourSwitchViewerOnStage2_Cat_SitOnDoma();
                    }
                    #endregion // Doma

                    #region DoorActmessage
                    if (tagName == "Door_Message1")
                    {
                        if (!mFlg1)
                        {
                            message1.SetActive(true);
                            mFlg1 = true;
                        }
                        else
                        {
                            message1.SetActive(false);
                            mFlg1 = false;
                        }
                    }
                    if (tagName == "Door_Message2")
                    {
                        if (!mFlg2)
                        {
                            message2.SetActive(true);
                            mFlg2 = true;
                        }
                        else
                        {
                            message2.SetActive(false);
                            mFlg2 = false;
                        }
                    }
                    if (tagName == "Door_Message3")
                    {
                        if (!mFlg3)
                        {
                            message3.SetActive(true);
                            mFlg3 = true;
                        }
                        else
                        {
                            message3.SetActive(false);
                            mFlg3 = false;
                        }
                    }

                    #endregion //Door Act message

                    #endregion // Tour Mode Interaction
                }
            }

            //Å¶UI Highlight Object
            foreach (var hit in hits)
            {
                string lightTagName = hit.collider.tag;
                string lightObjNam = hit.collider.name;

                if (lightTagName == "Door_Message1")
                {
                    if (lightObjNam == "Door001_part001")
                    {
                        hitFlgD2_1 = true;
                    }
                }

                if (lightTagName == "Door_Message2")
                {
                    if (lightObjNam == "Door001_part")
                    {
                        hitFlgD2_2 = true;
                    }
                }
                if (lightTagName == "Door_Message3")
                {
                    if (lightObjNam == "Door001_part002")
                    {
                        hitFlgD2_3 = true;
                    }
                }
            }
        }
        // When the RHandTrigger is released
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);

            //Å¶UI Flae all highlight false
            hitFlgD2_1 = false;
            hitFlgD2_2 = false;
            hitFlgD2_3 = false;
        }
    }

    public void PointingDeskCapacity()
    {
        // Get Status in "Touch" (True or False)
        bool nowTransDeskCap = capacityAnimation.GetBool("Touch");

        #region Open / Close Desk (with Decision Area)
        if (!nowTransDeskCap)
        {
            capacityAnimation.SetBool("Touch", true);
        }
        else
        {
            capacityAnimation.SetBool("Touch", false);
        }
        #endregion
    }

    public void ReturnToPosition()
    {
        sitOnPos1S.SetActive(false);
        sosFlg = false;

        tourSVM.TourSwitchViewerOnStage2_Player_ReturnWalk();
            
    }

}
