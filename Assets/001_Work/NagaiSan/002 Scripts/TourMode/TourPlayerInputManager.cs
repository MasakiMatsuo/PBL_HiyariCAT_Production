using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TourPlayerInputManager : MonoBehaviour
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
    #endregion

    #region Other Scripts
    public TourSwitchViewManager tourSVM;
    #endregion
    
    #region Flags
    private bool pFlg = false;
    private bool sobFlg = false;
    private bool socFlg = false;

    public bool tFlg = false;
    public bool tFlgAct = false;
    public bool trFlg = false;
    public bool trFlgAct = false;

    private bool mFlg1 = false;
    private bool mFlg2 = false;
    private bool mFlg3 = false;

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
                        SceneManager.LoadScene("009 EndScene");// Need to fix "scene.name" when Finalize
                    }

                    #region Cat Mode
                    else if (tagName == "CatMode")
                    {
                        if (SceneManager.GetActiveScene().name == "006 TourStage1_Human")
                        {
                            SceneManager.LoadScene("006 TourStage1_Cat");// Need to fix "scene.name" when Finalize
                        }

                        if (SceneManager.GetActiveScene().name == "008 TourStage3_Human")// 本番環境に移行後シーン名修正
                        {
                            SceneManager.LoadScene("008 TourStage3_Cat");
                        }
                    }

                    /*
                    #region Debug Cat Mode
                    else if (tagName == "Debug_CatMode")
                    {
                        SceneManager.LoadScene("006 TourStage1_Cat");// Need to fix "scene.name" when Finalize
                    }
                    #endregion // Debug Cat Mode
                    */
                    #endregion // Cat Mode

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

                    #region SitOnBed
                    if (tagName == "Bed_Stage1")
                    {
                        if (!sobFlg)
                        {
                            sitOnPos1B.SetActive(true);
                            sitOnPos2B.SetActive(false);
                            sobFlg = true;
                        }
                        else
                        {
                            sitOnPos1B.SetActive(false);
                            sobFlg = false;
                        }
                    }
                    if (tagName == "SitOnBed")
                    {
                        SittingNow();
                        sobFlg = false;
                        tourSVM.TourSwitchViewerOnStage1_Player_SitOnBed();
                    }
                    #endregion // SitOnBed
                    #region SitOnChair
                    if (tagName == "Chair_Stage1")
                    {
                        if (!socFlg)
                        {
                            sitOnPos2B.SetActive(true);
                            sitOnPos1B.SetActive(false);
                            socFlg = true;
                        }
                        else
                        {
                            sitOnPos2B.SetActive(false);
                            socFlg = false;
                        }
                    }
                    if (tagName == "SitOnChair")
                    {
                        SittingNow();
                        socFlg = false;
                        tourSVM.TourSwitchViewerOnStage1_Player_SitOnChair();
                    }
                    #endregion // SitOnChair

                    #region SitOnSofa
                    if (SceneManager.GetActiveScene().name == "008 TourStage3_Human")// 本番環境に移行後シーン名修正
                    {
                        if (!trFlgAct) {
                            if (tagName == "Sofa_Stage3")
                            {
                                if (!sobFlg)
                                {
                                    sitOnPos1B.SetActive(true);
                                    sitOnPos2B.SetActive(false);
                                    sobFlg = true;
                                }
                                else
                                {
                                    sitOnPos1B.SetActive(false);
                                    sobFlg = false;
                                }
                            }
                            if (tagName == "SitOnSofa")
                            {
                                SittingNow();
                                sobFlg = false;
                                tourSVM.TourSwitchViewerOnStage1_Player_SitOnBed();
                            }
                        }
                    }
                    #endregion // SitOnSofa

                    #region TVRemoteActive
                    if (tagName == "TVRemote")
                    {
                        if (!trFlgAct)
                        {
                            trFlgAct = true;
                        }
                        
                    }
                    #endregion // TVRemoteActive

                    #endregion // Tour Mode Interaction
                }
            }

            if (hits.Length == 0)
            {
                trFlg = false;
                tFlg = false;
                //trFlgAct = false;
            }

            foreach (var hit in hits)
            {
                string tagNameHit = hit.collider.tag;

                #region TV
                if (tagNameHit == "TV")
                {
                    if (!tFlg)
                    {
                        tFlg = true;
                    }
                }
                else if (tagNameHit == "TVRemote")
                {
                    if (!trFlg)
                    {
                        trFlg = true;
                    }                    
                }else
                {
                    tFlg = false;

                }
                #endregion // TV

            }
        }
        // When the RHandTrigger is released
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);

            trFlg = false;
            tFlg = false;
            trFlgAct = false;
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
        sitOnPos1B.SetActive(false);
        sobFlg = false;
        sitOnPos2B.SetActive(false);
        socFlg = false;
        tourSVM.TourSwitchViewerOnStage1_Player_ReturnWalk();
            
    }

    public void SittingNow()
    {
        sitOnPos1B.SetActive(false);
        sitOnPos2B.SetActive(false);
    }

}
