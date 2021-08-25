using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager_Stage1_3 : MonoBehaviour
{
    /// CAUTION !!! /////////////////////////////////////////////////
    /// You shold search "Need to fix" before Finalize.
    /// If you can find it, we still have some things to fix 
    /// CAUTION !!! /////////////////////////////////////////////////

    #region Require Values
    #region Player Values
    public GameObject playerRightController;
    public LineRenderer rayObject;
    #endregion // Player Values

    public Animator capacityAnimation;

    public bool iamCat = false;

    #region UIs
    public GameObject removeB;
    public GameObject pauseMenu;
    #endregion // UIs

    #region Other Scripts
    public GameObject tutorialManager;
    public GameObject locomotionManager;
    public CatInputManager catInputManager;
    public SwitchViewManager switchViewManager;
    #endregion // Other Scripts

    #region Flags
    private bool pFlg = false;
    private bool rFlg = false;

    public bool Stage1_LS_Check = default;
    public bool Stage1_PB_Check = default;
    public bool Stage1_Scissors_Check = default;

    public bool lightObj = false;
    public bool heavyObj = false;

    //Å¶UI
    //ÉXÉeÅ[ÉW1
    public bool hitFlg1_1 = false;
    public bool hitFlg1_2 = false;
    public bool hitFlg1_3 = false;

    #endregion // Flags
    #endregion // Require Values

    void Start()
    {
        InitApp();
    }

    void Update()
    {
        // When Stage 0
        if (SceneManager.GetActiveScene().name == "002 Stage0")// Need to fix "scene.name" when Finalize
        {
            if (!iamCat)
            {
                InitMyPlayerRay();
                TutorialPlayerMode();
            }
        }
        // When Stage 1 or Stage 3
        else
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
                #endregion // Create Start Point of Ray (Cat)
                CatMode();
            }
        }

    }

    public void InitApp()
    {
        #region Open Desk Capacity when Start 
        capacityAnimation.SetBool("Touch", true);
        #endregion // Open Desk Capacity when Start 

        #region Initialize UIs
        removeB.SetActive(false);
        pauseMenu.SetActive(false);
        #endregion // Initialize UIs

        iamCat = false;
    }

    public void CatMode()
    {
        #region Close Desk
        capacityAnimation.SetBool("Touch", false);
        #endregion // Close Desk

        locomotionManager.SetActive(false);

        catInputManager.CatMode();


    }

    public void InitMyPlayerRay()
    {
        #region Create Start Point of Ray (Player)
        // Create Vertex(0:Start, 1:End point)
        rayObject.SetVertexCount(2);

        // Set Vertex0 (Start point == position in RightController)
        rayObject.SetPosition(0, playerRightController.transform.position);
        #endregion // Create Start Point of Ray (Player)
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
            #endregion // Set Vertex1 and With of Ray (Player)

            #region Create Ray, this is same scale to Line Renderer (Player)
            RaycastHit[] hits;
            hits = Physics.RaycastAll(playerRightController.transform.position, playerRightController.transform.forward * 100.0f);
            #endregion // Create Ray, this is same scale to Line Renderer (Player)

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
                    else if (tagName == "AbortThisStage")
                    {
                        CheckRemoving();
                        switchViewManager.SwitchViewer();
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
                    #endregion // Debug Button NextStage

                    #endregion //Scene Transition

                    #region Checking dangerous items & Debug Cat Mode
                    else if (tagName == "Debug_CatMode")
                    {
                        CheckRemoving();
                        switchViewManager.SwitchViewer();
                    }
                    #endregion // Checking dangerous items & Debug Cat Mode
                    #endregion // Menu Pointing

                    #region Interaction of Capacity
                    if (tagName == "Capacity")
                    {
                        PointingDeskCapacity();
                        break;
                    }
                    #endregion // Interaction of Capacity

                    #region Interaction of Target
                    if (tagName == "Target")
                    {
                        hit.collider.transform.parent = playerRightController.transform;
                        break;
                    }
                    #endregion //Interaction of Target

                    #region Interaction of HeavyTarget
                    #region Print "Remove" Button for tag.name == "HeavyTarget"
                    /*if (tagName == "HeavyTarget")
                    {
                        removeB.SetActive(true);
                    }*/
                    if (tagName == "HeavyTarget")
                    {
                        //Debug.LogWarning(rFlg);
                        if (!rFlg)
                        {
                            removeB.SetActive(true); ;
                            rFlg = true;
                        }
                        else
                        {
                            removeB.SetActive(false);
                            rFlg = false;
                        }
                    }
                    #endregion // Print "Remove" Button for tag.name == "HeavyTarget"

                    if (tagName == "Remove")
                    {
                        #region Remove Barrel when STAGE 0
                        if (SceneManager.GetActiveScene().name == "002 Stage0") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _B001 = GameObject.Find("Barrel001");

                            _B001.SetActive(false);
                            removeB.SetActive(false);
                        }
                        #endregion // Remove LightStand when STAGE 0
                        #region Remove LightStand when STAGE 1
                        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _LS001 = GameObject.Find("LightStand001");

                            _LS001.SetActive(false);
                            removeB.SetActive(false);
                        }
                        #endregion // Remove LightStand when STAGE 1
                        // Does Stage 2 Need?
                        #region Remove Items when STAGE 2
                        if (SceneManager.GetActiveScene().name == "004 Stage2") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _001 = GameObject.Find("****");
                            GameObject _002 = GameObject.Find("****");
                            GameObject _003 = GameObject.Find("****");

                            _001.SetActive(false);
                            removeB.SetActive(false);
                        }
                        #endregion // Remove Items when STAGE 2
                    }
                    #endregion // Interaction of HeavyTarget
                }
                #region Catching Object's gravity is false;
                GameObject go = playerRightController.transform.GetChild(3).gameObject;
                go.GetComponent<Rigidbody>().useGravity = false;
                #endregion //Catching Object's gravity is false;
            }
            // Release Object
            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
            {
                MyReleaseObject();
            }

            //Å¶UI Highlight Object
            foreach (var hit in hits)
            {
                string lightTagName = hit.collider.tag;
                string lightObjNam = hit.collider.name;

                #region HighLighting on Stage 0
                if (SceneManager.GetActiveScene().name == "002 Stage0")
                {
                    if (lightTagName == "Target")
                    {
                        if (lightObjNam == "Handgun001")
                        {
                            lightObj = true;
                        }
                    }
                    else if (lightTagName == "HeavyTarget")
                    {
                        if (lightObjNam == "Barrel001")
                        {
                            heavyObj = true;
                        }
                    }
                    else
                    {
                        lightObj = false;
                        heavyObj = false;
                    }
                }
                #endregion // HighLighting on Stage 0

                else if (SceneManager.GetActiveScene().name == "003 Stage1")
                {
                    if (lightTagName == "Target")
                    {
                        if (lightObjNam == "Bag001")
                        {
                            hitFlg1_1 = true;
                        }
                        else if (lightObjNam == "Scissors001")
                        {
                            hitFlg1_2 = true;
                        }
                    }
                    else if (lightTagName == "HeavyTarget")
                    {
                        hitFlg1_3 = true;
                        if (lightObjNam == "LightStand001")
                        {
                            hitFlg1_3 = true;
                        }
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
            hitFlg1_1 = false;
            hitFlg1_2 = false;
            hitFlg1_3 = false;

            lightObj = false;
            heavyObj = false;

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
            #endregion // Bug Fix (When Player released the RHandTrigger while holding an object, the process of leaving the Laser Pointer is performed.)
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
                Stage1_LS_Check = false;
            }
            else
            {
                Stage1_LS_Check = true;
            }
            if (PB)
            {
                Stage1_PB_Check = false;
            }
            else
            {
                Stage1_PB_Check = true;
            }
            if (Scissors)
            {
                Stage1_Scissors_Check = false;
            }
            else
            {
                Stage1_Scissors_Check = true;
            }
            #endregion // Checking
            #endregion // ÅyStage 1ÅzChecking that dangerous items have been removed.

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
        #endregion // Open / Close Desk (with Decision Area)
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

    public void TutorialPlayerMode()
    {
        PointingInteraction();

        // PauseMenu (Player Clears Tutorial and Press "X")
        if (tutorialManager.GetComponent<TutorialManager>().endTutorialFlag && OVRInput.GetDown(OVRInput.RawButton.X))
        {
            PauseMenu();
        }

        tutorialManager.GetComponent<TutorialManager>().GuideTexts_Welcome_to_No1();


    }

}