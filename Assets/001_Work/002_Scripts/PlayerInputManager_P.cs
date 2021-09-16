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

    #region Animation and Models
    public Animator capacityAnimation;
    public GameObject catModel;
    #endregion // Animation and Models

    #region UIs
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject[] removeBs = new GameObject[1];
    #endregion // UIs

    #region Other Scripts
    public GameObject tutorialManager;
    public GameObject locomotionManager;
    public CatInputManager_P catInputManager_P;
    public SwitchViewManager_P switchViewManager_P;
    #endregion // Other Scripts

    #region Flags
    public bool iamCat = false;

    private bool pFlg = false;
    private bool rFlg = false;

    public bool countDownStart = false;

    public bool dangerPos01_Check = default;
    public bool dangerPos02_Check = default;
    public bool dangerPos03_Check = default;

    public bool hitFlg_item1 = false;
    public bool hitFlg_item2 = false;
    public bool hitFlg_item3 = false;
    #region Stage 2 Bools
    public bool rFlg_Vase = false;
    public bool rFlg_Chemical = false;
    public bool rFlg_Door = false;

    public int objIndex = -1;
    #endregion // Stage 2 Bools
    #endregion // Flags

    #region Audio
    public AudioSource audioRemove;
    public AudioSource audioAnime;
    #endregion // Audio
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
                if (!countDownStart && OVRInput.GetDown(OVRInput.RawButton.A))
                {
                    startMenu.SetActive(false);
                    countDownStart = true;
                }
                else
                {
                    PlayerMode();
                }
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
        if (SceneManager.GetActiveScene().name == "004 Stage2")
        {
            capacityAnimation.SetBool("Touch", false);
        }
        else
        {
            capacityAnimation.SetBool("Touch", true);
        }
        #endregion // Initialized Eliminate Mode

        #region Initialize UIs
        pauseMenu.SetActive(false);

        for (int i = 0; i < removeBs.Length; i++)
        {
            removeBs[i].SetActive(false);
        }

        // Stage1~3
        if (SceneManager.GetActiveScene().name != "002 Stage0")
        {
            startMenu.SetActive(true);
            catModel.SetActive(true);
        }
        #endregion // Initialize UIs

        iamCat = false;
    }

    public void CatMode()
    {
        #region Initialized Cat Mode
        if (SceneManager.GetActiveScene().name != "004 Stage2")
        {
            capacityAnimation.SetBool("Touch", false);
        }
        #endregion // Initialized Cat Mode

        if (SceneManager.GetActiveScene().name != "002 Stage0")
        {
            HitFlagFalser();
            catModel.SetActive(false);
        }

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
                    string objName = hit.collider.name;

                    #region Scene Transition
                    if (tagName == "Quit")
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

                        for (int i = 0; i < removeBs.Length; i++)
                        {
                            removeBs[i].SetActive(false);
                        }
                    }
                    #endregion //Scene Transition

                    #region Interaction of Capacity
                    if (tagName == "Capacity")
                    {
                        if (SceneManager.GetActiveScene().name != "004 Stage2")
                        {
                            PointingCapacity();
                            break;
                        }
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
                        #region Remove Button ON/OFF Without Stage 2
                        if (!rFlg)
                        {
                            removeBs[0].SetActive(true); ;
                            rFlg = true;
                        }
                        else
                        {
                            removeBs[0].SetActive(false);
                            rFlg = false;
                        }
                        #endregion // Remove Button ON/OFF Without Stage 2
                    }
                    #region Remove Button ON/OFF on Stage2

                    if (SceneManager.GetActiveScene().name == "004 Stage2")
                    {
                        if (tagName == "HeavyTarget_Vase")
                        {
                            if (!rFlg_Vase)
                            {
                                removeBs[0].SetActive(true);
                                rFlg_Vase = true;
                            }
                            else
                            {
                                removeBs[0].SetActive(false);
                                rFlg_Vase = false;
                            }
                        }
                        if (tagName == "HeavyTarget_Chemical")
                        {
                            if (!rFlg_Chemical)
                            {
                                removeBs[1].SetActive(true);
                                rFlg_Chemical = true;
                            }
                            else
                            {
                                removeBs[1].SetActive(false);
                                rFlg_Chemical = false;
                            }
                        }
                        if (tagName == "Capacity")
                        {
                            if (!capacityAnimation.GetBool("Touch"))
                            {
                                if (!rFlg_Door)
                                {
                                    removeBs[2].SetActive(true);
                                    removeBs[3].SetActive(true);
                                    rFlg_Door = true;
                                }
                                else
                                {
                                    removeBs[2].SetActive(false);
                                    removeBs[3].SetActive(false);
                                    rFlg_Door = false;
                                }
                            }
                        }
                    }
                    #endregion // Remove Button ON/OFF on Stage2
                    #endregion // Print "Remove" Button for tag.name == "HeavyTarget"

                    if (tagName == "Remove")
                    {
                        #region Remove Barrel when STAGE 0
                        if (SceneManager.GetActiveScene().name == "002 Stage0") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _B001 = GameObject.Find("Barrel001");
                            _B001.SetActive(false);
                        }
                        #endregion // Remove LightStand when STAGE 0
                        #region Remove LightStand when STAGE 1
                        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _LS001 = GameObject.Find("LightStand001");
                            _LS001.SetActive(false);
                        }
                        #endregion // Remove LightStand when STAGE 1
                        #region Remove StorageCabinet001 when STAGE3
                        if (SceneManager.GetActiveScene().name == "005 Stage3") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _SC001 = GameObject.Find("StorageCabinet001");
                            _SC001.SetActive(false);
                        }
                        #endregion // Remove StorageCabinet001 when STAGE3

                        #region After Remove Process
                        if (SceneManager.GetActiveScene().name != "004 Stage2")
                        {
                            //Remove Audio
                            audioRemove.Play();

                            removeBs[0].SetActive(false);
                        }
                        #endregion // After Remove Process
                    }

                    #region Remove HeavyItems when STAGE 2
                    if (SceneManager.GetActiveScene().name == "004 Stage2") // Need to fix "scene.name" when Finalize
                    {
                        GameObject obj = default;

                        #region Search condition
                        if (tagName == "Remove_Vase")
                        {
                            obj = GameObject.Find("Vase001");
                            objIndex = 0;
                        }
                        else if (tagName == "Remove_Chemical")
                        {
                            obj = GameObject.Find("Chemicals001v2");
                            objIndex = 1;
                        }
                        else if (tagName == "Remove_Door")
                        {
                            PointingCapacity();
                            objIndex = 2;
                        }
                        #endregion // Search condition
                        #region Remove Vase or Chemicals
                        if (objIndex == 0 || objIndex == 1)
                        {
                            //Remove Audio
                            audioRemove.Play();

                            obj.SetActive(false);
                        }
                        #endregion // Remove Vase or Chemicals

                        objIndex = Remove_RemoveButtons(objIndex);
                    }
                    #endregion // Remove HeavyItems when STAGE 2
                    #endregion // Interaction of HeavyTarget
                }
                #region Catching Object's gravity is false;
                if (SceneManager.GetActiveScene().name != "004 Stage2")
                {
                    GameObject go = playerRightController.transform.GetChild(3).gameObject;
                    go.GetComponent<Rigidbody>().useGravity = false;
                }
                #endregion // Catching Object's gravity is false;
            }
            // Release Object
            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
            {
                MyReleaseObject();
            }
            
            //When Ray is not hitting anything.
            if (hits.Length == 0)
            {
                HitFlagFalser();
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
                            hitFlg_item1 = true;
                        }
                        if (lightObjNam == "OfficeKnife001")
                        {
                            hitFlg_item2 = true;
                        }
                    }
                    else if (lightTagName == "HeavyTarget")
                    {
                        if (lightObjNam == "Barrel001")
                        {
                            hitFlg_item3 = true;
                        }
                    }
                    else
                    {
                        HitFlagFalser();
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
                        hitFlg_item3 = true;
                    }
                    else
                    {
                        HitFlagFalser();
                    }
                }
                #endregion // HighLighting on Stage 1
                #region HighLighting on Stage 2
                
                if (SceneManager.GetActiveScene().name == "004 Stage2")
                {
                    if (lightTagName == "HeavyTarget_Vase")
                    {
                        hitFlg_item1 = true;
                    }
                    if (lightTagName == "HeavyTarget_Chemical")
                    {
                        hitFlg_item2 = true;
                    }
                    if (lightTagName == "Capacity")
                    {
                        if (!capacityAnimation.GetBool("Touch"))
                        {
                            hitFlg_item3 = true;
                        }
                    }
                }
                
                #endregion // HighLighting on Stage 2
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
                        hitFlg_item3 = true;
                    }
                    else
                    {
                        HitFlagFalser();
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
            HitFlagFalser();

            #region Bug Fix (When Player released the RHandTrigger while holding an object, the process of leaving the Laser Pointer is performed.) without Stage2
            if (SceneManager.GetActiveScene().name != "004 Stage2")
            {
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
            }
            #endregion // Bug Fix (When Player released the RHandTrigger while holding an object, the process of leaving the Laser Pointer is performed.) without Stage2
        }
    }

    public void CheckRemoving()
    {
        #region Initialize Values
        GameObject pos01 = default;
        GameObject pos02 = default;
        GameObject pos03 = default;
        bool pos03_bool = default;
        #endregion // Initialize Values

        #region Stage1: Find the active objects
        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
        {
            pos01 = GameObject.Find("LightStand001");
            pos02 = GameObject.Find("Bag001");
            pos03 = GameObject.Find("Scissors001");
        }
        #endregion // Stage1: Find the active objects
        #region Stage2: Find the active objects

        else if (SceneManager.GetActiveScene().name == "004 Stage2") // Need to fix "scene.name" when Finalize
        {
            pos01 = GameObject.Find("Vase001");
            pos02 = GameObject.Find("Chemicals001v2");
            pos03_bool = capacityAnimation.GetBool("Touch");
        }
        #endregion // Stage2: Find the active objects
        #region Stage3: Find the active objects
        else if (SceneManager.GetActiveScene().name == "005 Stage3") // Need to fix "scene.name" when Finalize
        {
            pos01 = GameObject.Find("Chocolate001");
            pos02 = GameObject.Find("Code001");
            pos03 = GameObject.Find("StorageCabinet001");
        }
        #endregion // Stage3: Find the active objects

        #region Checking
        if (SceneManager.GetActiveScene().name != "004 Stage2")
        {
            Checking(pos01, pos02, pos03);
        }
        else
        {
            CheckingOnStage2(pos01, pos02, pos03_bool);
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

    public void Checking(GameObject p01, GameObject p02, GameObject p03)
    {
        #region pos01
        if (p01)
                dangerPos01_Check = false;
        else
                dangerPos01_Check = true;
        #endregion // pos01
        #region pos02
        if (p02)
                dangerPos02_Check = false;
        else
                dangerPos02_Check = true;
        #endregion // pos02
        #region pos03
        if (p03)
                dangerPos03_Check = false;
        else
                dangerPos03_Check = true;
        #endregion // pos03
    }
    
    public void CheckingOnStage2(GameObject p01, GameObject p02, bool p03_b)
    {
        #region pos01
        if (p01)
            dangerPos01_Check = false;
        else
            dangerPos01_Check = true;
        #endregion // pos01
        #region pos02
        if (p02)
            dangerPos02_Check = false;
        else
            dangerPos02_Check = true;
        #endregion // pos02
        #region pos03
        if (!p03_b)
            dangerPos03_Check = false;
        else
            dangerPos03_Check = true;
        #endregion // pos03
    }

    public int Remove_RemoveButtons(int index)
    {
        if (SceneManager.GetActiveScene().name == "004 Stage2")
        {
            if (0 <= index && index <= 2)
            {
                removeBs[index].SetActive(false);

                if (index == 2)
                {
                    removeBs[index + 1].SetActive(false);
                }
                index = -1;
            }
        }
        return index;
    }
    
    public void HitFlagFalser()
    {
        hitFlg_item1 = false;
        hitFlg_item2 = false;
        hitFlg_item3 = false;
    }

    public void TutorialPlayerMode()
    {
        PointingInteraction();

        // PauseMenu (Player Clears Tutorial and Press "X")
        if (tutorialManager.GetComponent<TutorialManager_P>().endTutorialFlag && OVRInput.GetDown(OVRInput.RawButton.X))
        {
            PauseMenu();
        }

        tutorialManager.GetComponent<TutorialManager_P>().GuidanceApp();
    }
}