using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager_Stage2 : MonoBehaviour
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

    public Animator doorAnimation;

    public bool iamCat = false;

    #region UIs
    public GameObject removeB;
    public GameObject pauseMenu;
    #endregion

    #region Other Scripts
    public CatInputManager catInputManager;
    public SwitchViewManager switchViewManager;
    #endregion

    #region Flags
    private bool pFlg = false;

    public bool Stage2_Vase_Check = default;
    public bool Stage2_Chemical_Check = default;
    public bool Stage3_Door_Check = default;
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


    }

    public void InitApp()
    {
        #region Open Desk Capacity when Start 
        doorAnimation.SetBool("Touch", true);
        #endregion

        #region Initialize UIs
        removeB.SetActive(false);
        pauseMenu.SetActive(false);
        #endregion

        iamCat = false;
    }

    public void CatMode()
    {
        #region Close Desk
        doorAnimation.SetBool("Touch", false);
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
                    #endregion

                    #region Checking dangerous items & Debug Cat Mode
                    else if (tagName == "Debug_CatMode")
                    {
                        CheckRemoving();
                        switchViewManager.SwitchViewer();
                    }
                    #endregion
                    #endregion

                    #region Interaction of Capacity
                    if (tagName == "Capacity")
                    {
                        PointingDeskCapacity();
                        break;
                    }
                    #endregion

                    #region Interaction of Target
                    if (tagName == "Target")
                    {
                        hit.collider.transform.parent = playerRightController.transform;
                        break;
                    }
                    #endregion
                    #region Interaction of HeavyTarget
                    #region Print "Remove" Button for tag.name == "HeavyTarget"
                    if (tagName == "HeavyTarget")
                    {
                        removeB.SetActive(true);
                    }
                    #endregion

                    if (tagName == "Remove")
                    {
                        #region Remove LightStand when STAGE 1
                        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _LS001 = GameObject.Find("LightStand001");

                            _LS001.SetActive(false);
                            removeB.SetActive(false);
                        }
                        #endregion
                        #region Remove Items when STAGE 2
                        if (SceneManager.GetActiveScene().name == "004 Stage2") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _Vase001 = GameObject.Find("Vase001");
                            GameObject _Chemical002 = GameObject.Find("Chemicals001v2");

                            _Vase001.SetActive(false);
                            _Chemical002.SetActive(false);
                            removeB.SetActive(false);
                        }
                        #endregion
                    }
                    #endregion
                }
                #region Catching Object's gravity is false;
                GameObject go = playerRightController.transform.GetChild(3).gameObject;
                go.GetComponent<Rigidbody>().useGravity = false;
                #endregion
            }
            // Release Object
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


    public void CheckRemoving()
    {
        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
        {
            #region ÅyStage 1ÅzChecking that dangerous items have been removed.
            GameObject LS = GameObject.Find("LightStand001");
            GameObject PB = GameObject.Find("Bag001");
            GameObject Scissors = GameObject.Find("Scissors001");

            #region Checking
            if (LS)
            {
                Stage2_Vase_Check = false;
            }
            else
            {
                Stage2_Vase_Check = true;
            }
            if (PB)
            {
                Stage2_Chemical_Check = false;
            }
            else
            {
                Stage2_Chemical_Check = true;
            }
            if (Scissors)
            {
                Stage3_Door_Check = false;
            }
            else
            {
                Stage3_Door_Check = true;
            }
            #endregion
            #endregion

        }
    }

    public void PointingDeskCapacity()
    {
        // Get Status in "Touch" (True or False)
        bool nowTransDeskCap = doorAnimation.GetBool("Touch");

        #region Open / Close Desk (with Decision Area)
        if (!nowTransDeskCap)
        {
            doorAnimation.SetBool("Touch", true);
        }
        else
        {
            doorAnimation.SetBool("Touch", false);
        }
        #endregion
    }

    public void MyReleaseObject()
    {
        #region Deselect Object's gravity is true;
        GameObject go = playerRightController.transform.GetChild(3).gameObject;
        go.GetComponent<Rigidbody>().useGravity = true;
        #endregion

        #region Child Objects relased
        for (int i = 0; i < playerRightController.transform.childCount; i++)
        {
            var child = playerRightController.transform.GetChild(i);
            if (child.tag == "Target")
            {
                child.parent = null;
            }
        }
        #endregion
    }


}

