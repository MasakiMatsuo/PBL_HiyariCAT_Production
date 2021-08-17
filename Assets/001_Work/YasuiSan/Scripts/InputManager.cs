using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
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
    
    #region Cat Values
    public GameObject catRightController;
    public LineRenderer catRayObject;
    #endregion

    public Animator deskAnimation;

    #region UIs
    public GameObject removeB;
    public GameObject pauseMenu;

    public GameObject catMemoStage1_LS_delight;
    //public GameObject catMemoStage1_LS_cranky;  // ご機嫌斜めな解説モード（ライトスタンド）：UI実装時にコメントアウト外してください

    public GameObject catMemoStage1_PB_delight;
    //public GameObject catMemoStage1_PB_cranky;  // ご機嫌斜めな解説モード（ビニール袋）：UI実装時にコメントアウト外してください
    public GameObject catMemoStage1_Scissors_delight;
    //public GameObject catMemoStage1_Ssers_cranky;  // ご機嫌斜めな解説モード（ハサミ）：UI実装時にコメントアウト外してください

    public GameObject resultMenu;

    #endregion

    #region Other Scripts
    public SwitchViewManager switchViewManager;
    public TimerManager timerManager;
    #endregion

    #region Flags
    private bool pFlg = false;
    public bool hitFlg = false; //インタラクティブ可能なオブジェクトにhitしたか

    public bool iamCat = false;
    private int hasSeenPoints = 0;

    public bool stage1_LS = false;
    public bool stage1_PB = false;
    public bool stage1_Scissors = false;
    #endregion
    #endregion

    void Start()
    {
        InitApp();
    }

    void Update()
    {
        InitMyRay();

        if (!iamCat)
        {
            PlayerMode();
        }
        else
        {
            CatMode();
        }
    }

    public void InitApp()
    {
        #region Open Desk Capacity when Start 
        deskAnimation.SetBool("Touch", true);
        #endregion

        #region Initialize UIs
        removeB.SetActive(false);
        pauseMenu.SetActive(false);

        catMemoStage1_LS_delight.SetActive(false);
        #endregion
    }

    public void CatMode()
    {
        #region Close Desk
        deskAnimation.SetBool("Touch", false);
        #endregion

        #region Patrol Dangerous Points
        #region On Stage 1
        if (SceneManager.GetActiveScene().name == "003 TestEnvStage1_Ver1.0") // Need to fix "scene.name" when Finalize
        {
            switch (hasSeenPoints)
            {
                case 0:
                    #region Near LightStand
                    // If player has not never read it, show CatMemo
                    if (!stage1_LS)
                    {
                        catMemoStage1_LS_delight.SetActive(true);
                    }

                    if (catMemoStage1_LS_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_LS = true;
                        catMemoStage1_LS_delight.SetActive(false);

                        // Move next point
                        switchViewManager.ViewNextDangerousPoint();
                        //return;
                        hasSeenPoints = 1;

                    }
                    break;

                #endregion

                case 1:
                    #region Near Plastic Bag
                    if (stage1_LS && !stage1_PB)
                    {
                        catMemoStage1_PB_delight.SetActive(true);
                    }

                    if (catMemoStage1_PB_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_PB = true;
                        catMemoStage1_PB_delight.SetActive(false);

                        switchViewManager.ViewNextDangerousPoint();
                        hasSeenPoints = 2;

                    }
                    break;
                #endregion

                case 2:
                    #region Near Scissors
                    if (stage1_LS && stage1_PB && !stage1_Scissors)
                    {
                        catMemoStage1_Scissors_delight.SetActive(true);
                    }

                    if (catMemoStage1_Scissors_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_Scissors = true;
                        catMemoStage1_Scissors_delight.SetActive(false);

                        resultMenu.SetActive(true);
                        hasSeenPoints = -1;

                    }
                    break;
                #endregion

                default:
                    break;
            }


            /*
            #region Near LightStand
            // If player has not never read it, show CatMemo
            if (!stage1_LS)
            {
                catMemoStage1_LS_delight.SetActive(true);
            }

            if (catMemoStage1_LS_delight && OVRInput.GetDown(OVRInput.RawButton.A))
            {
                stage1_LS = true;
                catMemoStage1_LS_delight.SetActive(false);

                // Move next point
                switchViewManager.ViewNextDangerousPoint();
                //return;
            }
            #endregion
            #region Near Plastic Bag
            if (stage1_LS && !stage1_PB)
            {
                catMemoStage1_PB_delight.SetActive(true);
            }
            
            if (catMemoStage1_PB_delight && OVRInput.GetDown(OVRInput.RawButton.B))
            {
                stage1_PB = true;
                catMemoStage1_PB_delight.SetActive(false);

                switchViewManager.ViewNextDangerousPoint();
            }
            #endregion
            #region Near Scissors
            if (stage1_LS && stage1_PB && !stage1_Scissors)
            {
                catMemoStage1_Scissors_delight.SetActive(true);
            }

            if (catMemoStage1_Scissors_delight && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                stage1_Scissors = true;
                catMemoStage1_Scissors_delight.SetActive(false);

                resultMenu.SetActive(true);
            }
            #endregion
            */

            //Debug用プレイヤーの視点に戻る→Non Activeなオブジェクトには戻れない（戻す方法はあるがそこに時間をかけるべきでない）
            //switchViewManager.ForDebugComeBackPlayerView();
        }
        #endregion
        /*
        else if (SceneManager.GetActiveScene().name == "【※上記を参考にSceneの名前を書いてください。】")
        {
            if (!read)
                {
                    【表示したいメモ名】.SetActive(true);
                }

                if (OVRInput.GetDown(OVRInput.RawButton.A))
                {
                    read = true;
                    【表示したいメモ名】.SetActive(false);
                }
        }*/
        #endregion

        PointingInteractionOnCatMode();
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

    public void InitMyRay()
    {
        if (!iamCat)
        {
            #region Create Start Point of Ray (Player)
            // Create Vertex(0:Start, 1:End point)
            rayObject.SetVertexCount(2);

            // Set Vertex0 (Start point == position in RightController)
            rayObject.SetPosition(0, playerRightController.transform.position);
            #endregion
        }
        else
        {
            #region Create Start Point of Ray (Cat)
            // Create Vertex(0:Start, 1:End point)
            catRayObject.SetVertexCount(2);

            // Set Vertex0 (Start point == position in RightController)
            catRayObject.SetPosition(0, catRightController.transform.position);
            #endregion
        }
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
                    if (tagName == "Tutorial")
                    {
                        SceneManager.LoadScene("002 testStage00");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Play" || tagName == "Next00")
                    {
                        SceneManager.LoadScene("003 TestEnvStage1_Ver1.0");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Next01")
                    {
                        SceneManager.LoadScene("004 testStage02");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Next02")
                    {
                        SceneManager.LoadScene("005 testStage03");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Retry")
                    {
                        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    }
                    else if (tagName == "ReturnTitle")
                    {
                        SceneManager.LoadScene("001 testTitle");// Need to fix "scene.name" when Finalize
                    }
                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("006 testEndScene");// Need to fix "scene.name" when Finalize
                    }

                    #region Debug Button NextStage
                    else if (tagName == "Debug_NextStage")
                    {
                        if (SceneManager.GetActiveScene().name == "003 TestEnvStage1_Ver1.0")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("004 testStage02");// Need to fix "scene.name" when Finalize
                        }
                        else if (SceneManager.GetActiveScene().name == "004 testStage02")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("005 testStage03");// Need to fix "scene.name" when Finalize
                        }
                        else if (SceneManager.GetActiveScene().name == "002 testStage00")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("003 TestEnvStage1_Ver1.0");// Need to fix "scene.name" when Finalize
                        }
                    }
                    #endregion

                    #endregion
                    #region EndGame
                    else if (tagName == "End")
                    {
                        Application.Quit();
                    }
                    #endregion
                    #region Debug Cat Mode
                    else if (tagName == "Debug_CatMode")
                    {
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
                        //hitFlg = true;
                        hit.collider.transform.parent = playerRightController.transform;
                        
                        break;
                    }
                    /*if (tagName != "Target")
                    {
                        hitFlg = false;
                    }*/
                    
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
                        #region Remove LightStand when STAGE01
                        if (SceneManager.GetActiveScene().name == "003 PrototypeStage1") // Need to fix "scene.name" when Finalize
                        {
                            GameObject _LS001 = GameObject.Find("LightStand001");

                            _LS001.SetActive(false);
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

            foreach (var hit in hits)
            {

                string tagName2 = hit.collider.tag;
                if (tagName2 == "Target")
                {
                    hitFlg = true;

                }
            }
            //hitFlg = true;


        }
        // When the RHandTrigger is released
        else
        {
            // Remove the Echo of Ray (Player)
            rayObject.SetPosition(1, playerRightController.transform.position + playerRightController.transform.forward * 0.0f);

            hitFlg = false;

            #region Release Bug Fix(uncompleted) , this is the processing when the RHandTrigger is released while grabbing an object
            if (playerRightController.transform.GetChild(3).gameObject)
            {
                MyReleaseObject();
            }
            //Avoid Error (it's not effective...)
            else { }
            #endregion
            
        }

        //////ハイライト関連
        ///
    }

    public void PointingDeskCapacity()
    {
        // Get Status in "Touch" (True or False)
        bool nowTransDeskCap = deskAnimation.GetBool("Touch");

        // Open Desk (with decsion area)
        if (!nowTransDeskCap)
        {
            deskAnimation.SetBool("Touch", true);
        }
        // Close Desk (with decsion area)
        else
        {
            deskAnimation.SetBool("Touch", false);
        }
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


    public void PointingInteractionOnCatMode()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            #region Set Vertex1 and With of Ray (Cat)
            // Set Vertex1 (End point == position is 100m in front of RightController)
            catRayObject.SetPosition(1, catRightController.transform.position + catRightController.transform.forward * 100.0f);

            // Set Width of Ray (This is 2 Demention)
            catRayObject.SetWidth(0.01f, 0.01f);
            #endregion

            #region Create Ray, this is same scale to Line Renderer (Cat)
            RaycastHit[] hits;
            hits = Physics.RaycastAll(catRightController.transform.position, catRightController.transform.forward * 100.0f);
            #endregion

            // Selecting
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                foreach (var hit in hits)
                {
                    string tagName = hit.collider.tag;

                    #region Debug Button NextStage
                    if (tagName == "Debug_NextStage")
                    {
                        if (SceneManager.GetActiveScene().name == "003 TestEnvStage1_Ver1.0")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("004 testStage02");// Need to fix "scene.name" when Finalize
                        }
                        else if (SceneManager.GetActiveScene().name == "004 testStage02")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("005 testStage03");// Need to fix "scene.name" when Finalize
                        }
                        else if (SceneManager.GetActiveScene().name == "002 testStage00")// Need to fix "scene.name" when Finalize
                        {
                            SceneManager.LoadScene("003 TestEnvStage1_Ver1.0");// Need to fix "scene.name" when Finalize
                        }
                    }
                    #endregion
                    else if (tagName == "PlayAgain")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }

                    else if (tagName == "TourHere")
                    {
                        Debug.Log("Not Done Here...");
                    }

                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("006 testEndScene");// Need to fix "scene.name" when Finalize
                    }
                }
            }
        }
        else
        {
            // Remove the Echo of Ray
            catRayObject.SetPosition(1, catRightController.transform.position + catRightController.transform.forward * 0.0f);
        }

    }
    
}
