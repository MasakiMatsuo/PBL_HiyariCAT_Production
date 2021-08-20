using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatInputManager : MonoBehaviour
{
    /// CAUTION !!! /////////////////////////////////////////////////
    /// You shold search "Need to fix" before Finalize.
    /// If you can find it, we still have some things to fix 
    /// CAUTION !!! /////////////////////////////////////////////////

    #region Require Values
    #region Cat Values
    public GameObject catRightController;
    public LineRenderer catRayObject;
    #endregion

    #region UIs
    //Stage1
    public GameObject catMemoStage1_LS_delight;
    public GameObject catMemoStage1_LS_cranky;  // ご機嫌斜めな解説モード（ライトスタンド）：UI実装時にコメントアウト外してください

    public GameObject catMemoStage1_PB_delight;
    public GameObject catMemoStage1_PB_cranky;  // ご機嫌斜めな解説モード（ビニール袋）：UI実装時にコメントアウト外してください

    public GameObject catMemoStage1_Scissors_delight;
    public GameObject catMemoStage1_Ssers_cranky;  // ご機嫌斜めな解説モード（ハサミ）：UI実装時にコメントアウト外してください

    //Stage2


    //Stage3

    public GameObject resultMenu;
    public Text resultText;
    private int achievementNum = 0;
    #endregion

    #region Other Scripts
    public SwitchViewManager switchViewManager;
    public PlayerInputManager_Stage1_3 playerInputManagerS13;
    #endregion

    #region Flags
    private int hasSeenPoints = 0;

    public bool stage1_LS_Point = false;
    public bool stage1_PB_Point = false;
    public bool stage1_Scissors_Point = false;
    #endregion
    #endregion

    void Start()
    {
        InitApp();
    }

    void InitApp()
    {
        /*Cat Memo -> SetActive(false) */
        catMemoStage1_LS_delight.SetActive(false);
        catMemoStage1_LS_cranky.SetActive(false);

        catMemoStage1_PB_delight.SetActive(false);
        catMemoStage1_PB_cranky.SetActive(false);

        catMemoStage1_Scissors_delight.SetActive(false);
        catMemoStage1_Ssers_cranky.SetActive(false);
    }

    public void InitMyCatRay()
    {
        if (playerInputManagerS13.iamCat)
        {
            #region Create Start Point of Ray (Cat)
            // Create Vertex(0:Start, 1:End point)
            catRayObject.SetVertexCount(2);

            // Set Vertex0 (Start point == position in RightController)
            catRayObject.SetPosition(0, catRightController.transform.position);
            #endregion
        }
    }

    public void CatMode()
    {
        if (SceneManager.GetActiveScene().name == "002 Stage0")
        {
            stage1_LS_Point = true;
            stage1_PB_Point = true;

            catMemoStage1_Scissors_delight.SetActive(true);

            if (catMemoStage1_Scissors_delight && OVRInput.GetDown(OVRInput.RawButton.A))
            {
                stage1_Scissors_Point = true;
                catMemoStage1_Scissors_delight.SetActive(false);

                //resultMenu.SetActive(true);

            }
        }

        #region Patrol Dangerous Points
        #region On Stage 1
        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
        {
            switch (hasSeenPoints)
            {
                case 0:
                    #region Near LightStand
                    // If player has not never read it, show CatMemo 01
                    if (!stage1_LS_Point)
                    {
                        //catMemoStage1_LS_delight.SetActive(true);
                        if (playerInputManagerS13.Stage1_LS_Check == true)
                        {
                            catMemoStage1_LS_delight.SetActive(true);
                        }
                        else
                        {
                            catMemoStage1_LS_cranky.SetActive(true);
                        }
                    }

                    if (catMemoStage1_LS_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_LS_Point = true;

                        catMemoStage1_LS_delight.SetActive(false);
                        catMemoStage1_LS_cranky.SetActive(false);

                        // Move next point
                        switchViewManager.ViewNextDangerousPoint();
                        hasSeenPoints = 1;
                    }
                    break;
                #endregion

                case 1:
                    #region Near Plastic Bag
                    if (stage1_LS_Point && !stage1_PB_Point)
                    {
                        //catMemoStage1_PB_delight.SetActive(true);

                        //Are there any interactive objects left?
                        if (playerInputManagerS13.Stage1_PB_Check == true)
                        {
                            catMemoStage1_PB_delight.SetActive(true);
                        }
                        else
                        {
                            catMemoStage1_PB_cranky.SetActive(true);
                        }
                    }

                    if (catMemoStage1_PB_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_PB_Point = true;

                        catMemoStage1_PB_delight.SetActive(false);
                        catMemoStage1_LS_cranky.SetActive(false);


                        switchViewManager.ViewNextDangerousPoint();
                        hasSeenPoints = 2;

                    }
                    break;
                #endregion

                case 2:
                    #region Near Scissors
                    if (stage1_LS_Point && stage1_PB_Point && !stage1_Scissors_Point)
                    {
                        //catMemoStage1_Scissors_delight.SetActive(true);
                        if (playerInputManagerS13.Stage1_Scissors_Check == true)
                        {
                            catMemoStage1_Scissors_delight.SetActive(true);
                           
                        }
                        else
                        {
                            catMemoStage1_Ssers_cranky.SetActive(true);
                        }
                    }

                    if (catMemoStage1_Scissors_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_Scissors_Point = true;

                        catMemoStage1_Scissors_delight.SetActive(false);
                        catMemoStage1_Ssers_cranky.SetActive(false);

                        resultMenu.SetActive(true);
                        achievementNum = Convert.ToInt32(playerInputManagerS13.Stage1_LS_Check) + Convert.ToInt32(playerInputManagerS13.Stage1_PB_Check) 
                            + Convert.ToInt32(playerInputManagerS13.Stage1_Scissors_Check); 
                        resultText.text = achievementNum + "/3";

                        hasSeenPoints = -1;

                    }
                    break;
                #endregion

                default:
                    break;
            }
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
                    else if (tagName == "PlayAgain")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }

                    //
                    else if (tagName == "TourHere")
                    {
                        SceneManager.LoadScene("006 TourStage1_Human");
                        /* Coming Soon.... */
                        //SceneManager.LoadScene("**Tour Mode**"); // Need to fix "scene.name" when Finalize
                    }

                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("008 EndScene");// Need to fix "scene.name" when Finalize
                    }
                    playerInputManagerS13.iamCat = false;
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
