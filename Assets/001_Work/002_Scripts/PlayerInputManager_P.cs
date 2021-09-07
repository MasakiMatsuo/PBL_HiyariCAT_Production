using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager_P : MonoBehaviour
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
    public CatInputManager_P catInputManager_P;
    public SwitchViewManager_P switchViewManager_P;
    #endregion // Other Scripts

    #region Flags
    private bool pFlg = false;
    private bool rFlg = false;

    public bool dangerPos01_Check = default;
    public bool dangerPos02_Check = default;
    public bool dangerPos03_Check = default;

    public bool lightObj01 = false;
    public bool lightObj02 = false;
    public bool heavyObj = false;

    //Å¶UI
    //ÉXÉeÅ[ÉW1
    public bool hitFlg_item1 = false;
    public bool hitFlg_item2 = false;
    public bool hitFlg_item3 = false;

    #endregion // Flags

    #region Audio
    public AudioSource audioData;
    public AudioSource audioAnime;
    #endregion // Audio

    // Cat Model
    //public GameObject catModel;  // Need to fix after add on her model on all stages.

    #endregion // Require Values

    void Start()
    {
        InitApp();
    }

    void Update()
    {
        // Forced Initialize
        if (OVRInput.Get(OVRInput.RawButton.B) && OVRInput.Get(OVRInput.RawButton.X) && OVRInput.Get(OVRInput.RawButton.Y) && OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            SceneManager.LoadScene("001 Title");
        }

        if (!iamCat)
        {
            InitMyPlayerRay();

            // When Stage 0
            if (SceneManager.GetActiveScene().name == "002 Stage0")
            {
                TutorialPlayerMode();
            }

            // When Stage 1 or Stage 3
            else
            {
                PlayerMode();
            }
        }
        else
        {
            CatMode();
        }
    }

    public void InitApp()
    {
        #region Initialized Eliminate Mode
        capacityAnimation.SetBool("Touch", true);
        //catModel.SetActive(true);  // "Need to fix"
        #endregion // Open Desk Capacity when Start 

        #region Initialize UIs
        removeB.SetActive(false);
        pauseMenu.SetActive(false);
        #endregion // Initialize UIs

        iamCat = false;
    }

    public void CatMode()
    {
        #region Initialized Cat Mode
        capacityAnimation.SetBool("Touch", false);
        //catModel.SetActive(false);  // "Need to fix"
        #endregion // Initialized Cat Mode

        #region Create Start Point of Ray (Cat)
        catInputManager_P.InitMyCatRay();
        #endregion // Create Start Point of Ray (Cat)

        locomotionManager.SetActive(false);
        catInputManager_P.CatMode();
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
                        SceneManager.LoadScene("009 EndScene");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "AbortThisStage")
                    {
                        if (SceneManager.GetActiveScene().name == "002 Stage0")
                        {
                            switchViewManager_P.SwitchViewerOnStage0();
                        }
                        else
                        {
                            CheckRemoving();
                            switchViewManager_P.SwitchViewer();
                        }
                    }

                    /*Remove soon "Debug Button"*/
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
                        switchViewManager_P.SwitchViewer();
                    }
                    #endregion // Checking dangerous items & Debug Cat Mode
                    #endregion // Menu Pointing

                    #region Interaction of Capacity
                    if (tagName == "Capacity")
                    {
                        PointingCapacity();
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
                    if (tagName == "HeavyTarget")
                    {
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

                            //Remove Audio
                            //audioData.Play();

                            _B001.SetActive(false);
                            //removeB.SetActive(false);
                        }
                        #endregion // Remove LightStand when STAGE 0
                        #region Remove LightStand when STAGE 1
                        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _LS001 = GameObject.Find("LightStand001");

                            //Remove Audio
                            //audioData.Play();

                            _LS001.SetActive(false);
                            //removeB.SetActive(false);
                        }
                        #endregion // Remove LightStand when STAGE 1
                        // Does Stage 2 Need?
                        /*#region Remove Items when STAGE 2
                        if (SceneManager.GetActiveScene().name == "004 Stage2") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _001 = GameObject.Find("****");
                            GameObject _002 = GameObject.Find("****");
                            GameObject _003 = GameObject.Find("****");

                            _001.SetActive(false);
                            removeB.SetActive(false);
                        }
                        #endregion*/ // Remove Items when STAGE 2

                        #region Remove StorageCabinet001 when STAGE3
                        if (SceneManager.GetActiveScene().name == "005 Stage3") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _SC001 = GameObject.Find("StorageCabinet001");

                            //Remove Audio
                            //audioData.Play();

                            _SC001.SetActive(false);
                            //removeB.SetActive(false);
                        }
                        #endregion // Remove StorageCabinet001 when STAGE3

                        //Remove Audio
                        audioData.Play();

                        removeB.SetActive(false);
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

            //When Ray is not hitting anything.
            if (hits.Length == 0)
            {
                hitFlg_item1 = false;
                hitFlg_item2 = false;
                hitFlg_item3 = false;
            }

            //Highlight Judge
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
                            lightObj01 = true;
                        }
                        if (lightObjNam == "OfficeKnife001")
                        {
                            lightObj02 = true;
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
                        lightObj01 = false;
                        lightObj02 = false;
                        heavyObj = false;
                    }
                }
                #endregion // HighLighting on Stage 0
                #region HighLighting on Stage 1
                else if (SceneManager.GetActiveScene().name == "003 Stage1")
                {
                    if (lightTagName == "Target")
                    {
                        if (lightObjNam == "Bag001")
                        {
                            hitFlg_item1 = true;
                        }
                        else if (lightObjNam == "Scissors001")
                        {
                            hitFlg_item2 = true;
                        }
                    }
                    else if (lightTagName == "HeavyTarget")
                    {
                        hitFlg_item3 = true; //Need?
                        /*if (lightObjNam == "LightStand001")
                        {
                            hitFlg_item3 = true;
                        }*/
                    }
                }
                #endregion // HighLighting on Stage 1
                #region HighLighting on Stage 3
                if (SceneManager.GetActiveScene().name == "005 Stage3")
                {
                    if (lightTagName == "Target")
                    {
                        if (lightObjNam == "Chocolate001")
                        {
                            hitFlg_item1 = true;
                        }
                        else if (lightObjNam == "Code001")
                        {
                            hitFlg_item2 = true;
                        }
                    }
                    else if (lightTagName == "HeavyTarget")
                    {
                        //hitFlg_item3 = true; //Need?
                        if (lightObjNam == "StorageCabinet001")
                        {
                            hitFlg_item3 = true;
                        }
                    }
                }
                #endregion // HighLighting on Stage 3

            }
        }
        // When the RHandTrigger is released
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);

            //all highlight flag are falsed
            hitFlg_item1 = false;
            hitFlg_item2 = false;
            hitFlg_item3 = false;

            lightObj01 = false;
            lightObj02 = false;
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
        GameObject pos01 = default;
        GameObject pos02 = default;
        GameObject pos03 = default;

        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
        {
            #region ÅyStage 1ÅzChecking that dangerous items have been removed.
            pos01 = GameObject.Find("LightStand001");
            pos02 = GameObject.Find("Bag001");
            pos03 = GameObject.Find("Scissors001");
            #endregion // ÅyStage 1ÅzChecking that dangerous items have been removed.
        }
        else if (SceneManager.GetActiveScene().name == "005 Stage3") // Need to fix "scene.name" when Finalize
        {
            pos01 = GameObject.Find("Chocolate001");
            pos02 = GameObject.Find("Code001");
            pos03 = GameObject.Find("StorageCabinet001");
        }

            #region Checking
            if (pos01)
        {
            dangerPos01_Check = false;
        }
        else
        {
            dangerPos01_Check = true;
        }
        if (pos02)
        {
            dangerPos02_Check = false;
        }
        else
        {
            dangerPos02_Check = true;
        }
        if (pos03)
        {
            dangerPos03_Check = false;
        }
        else
        {
            dangerPos03_Check = true;
        }
        #endregion // Checking
    }

    public void PointingCapacity()
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

        //Audio
        audioAnime.Play();
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