using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TourCatInputManager : MonoBehaviour
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

    public PlayerInputManager_Stage1_3 playerIM;
    #endregion

    #region Flags
    private bool pFlg = false;
    private bool sobFlg = false;
    private bool socFlg = false;

    private bool sosFlg = false;
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

        if (tourSVM.player_SitOnPos1Flag || tourSVM.player_SitOnPos2Flag )
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

                    #region Interaction of Target
                    if (tagName == "Target")
                    {
                        Debug.LogWarning("hit");
                        hit.collider.transform.parent = playerRightController.transform;
                        break;
                    }
                    #endregion

                    #region SitOnBed
                    if (tagName == "Bed_Stage1")
                    {
                        if (!sobFlg)
                        {
                            sitOnPos1B.SetActive(true);
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
                        sitOnPos1B.SetActive(false);
                        sobFlg = false;
                        tourSVM.TourSwitchViewerOnStage1_Cat_SitOnBed();
                    }
                    #endregion // SitOnBed
                    #region SitOnChair
                    if (tagName == "Table_Stage1")
                    {
                        if (!socFlg)
                        {
                            sitOnPos2B.SetActive(true);
                            socFlg = true;
                        }
                        else
                        {
                            sitOnPos2B.SetActive(false);
                            socFlg = false;
                        }
                    }
                    if (tagName == "SitOnTable")
                    {
                        sitOnPos2B.SetActive(false);
                        socFlg = false;
                        tourSVM.TourSwitchViewerOnStage1_Cat_SitOnTable();
                    }
                    #endregion // SitOnChair

                    #endregion // Tour Mode Interaction
                }
            }

            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
            {
                MyReleaseObject();
            }
        }
        // When the RHandTrigger is released
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);

            #region Bug Fix (When Player released the RHandTrigger while holding an object, the process of leaving the Laser Pointer is performed.)
            int checkNG = 3;
            int childCheck = playerRightController.transform.childCount - 1;

            if (childCheck != checkNG)
            {
                return;
            }
            else
            {
                MyReleaseObject();
            }
            #endregion
        }
    }

    public void MyReleaseObject()
    {
        #region Deselect Object's gravity is true;
        GameObject go = playerRightController.transform.GetChild(3).gameObject;
        go.GetComponent<Rigidbody>().useGravity = true;
        #endregion // Deselect Object's gravity is true;

        #region Child Objects relased
        for (int i = 0; i < playerRightController.transform.childCount; i++)
        {
            var child = playerRightController.transform.GetChild(i);
            if (child.tag == "Target")
            {
                child.parent = null;
            }
        }
        #endregion // Child Objects relased
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

}
