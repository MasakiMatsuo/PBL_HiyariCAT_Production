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

    public Animator tableAnimation;

    public bool iamCat = false;

    #region UIs
    public GameObject pauseMenu;
    public GameObject sitOnBedB;
    //public GameObject sitOnChairB;
    #endregion

    #region Other Scripts
    public CatInputManager catInputManager;
    public TourSwitchViewManager tourSVM;
    //public TimerManager timerManager;
    #endregion
    
    #region Flags
    private bool pFlg = false;
    private bool sobFlg = false;

    #endregion
    #endregion

    void Start()
    {
        InitApp();
    }

    void Update()
    {
        if (!iamCat)
        {
            InitMyPlayerRay();
            PlayerMode();
        }
        else
        {
            #region Create Start Point of Ray (Cat)
            catInputManager.InitMyCatRay();
            #endregion
            CatMode();
        }

        if (tourSVM.player_SitOnBedFlag)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.Y))
            {
                Debug.Log("Press Y");
                tourSVM.TourSwitchViewerOnStage1_Player_ReturnWalk();
            }
        }

    }

    public void InitApp()
    {
        #region Open Desk Capacity when Start 
        tableAnimation.SetBool("Touch", true);
        #endregion
        
        #region Initialize UIs
        pauseMenu.SetActive(false);
        sitOnBedB.SetActive(false);
        #endregion

        iamCat = false;
    }

    public void CatMode()
    {
        #region Close Desk
        tableAnimation.SetBool("Touch", false);
        #endregion

        catInputManager.CatMode();
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

    public void PlayerMode()
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
                    #endregion
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
                            sitOnBedB.SetActive(true);
                            sobFlg = true;
                        }
                        else
                        {
                            sitOnBedB.SetActive(false);
                            sobFlg = false;
                        }
                    }
                    if (tagName == "SitOnBed")
                    {
                        tourSVM.TourSwitchViewerOnStage1_Player_SitOnBed();
                    }
                    #endregion // SitOnBed
                    #endregion // Tour Mode Interaction
                    
                }
            }
        }
        // When the RHandTrigger is released
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);
        }
    }

    public void PointingDeskCapacity()
    {
        // Get Status in "Touch" (True or False)
        bool nowTransDeskCap = tableAnimation.GetBool("Touch");

        #region Open / Close Desk (with Decision Area)
        if (!nowTransDeskCap)
        {
            tableAnimation.SetBool("Touch", true);
        }
        else
        {
            tableAnimation.SetBool("Touch", false);
        }
        #endregion
    }

}
