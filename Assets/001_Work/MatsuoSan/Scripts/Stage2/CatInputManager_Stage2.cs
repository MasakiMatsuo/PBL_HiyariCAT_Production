using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CatInputManager_Stage2 : MonoBehaviour
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
    public GameObject catMemoStage2_Vase_delight;
    public GameObject catMemoStage2_Vase_cranky;  // ご機嫌斜めな解説モード（ライトスタンド）：UI実装時にコメントアウト外してください

    public GameObject catMemoStage2_Chemical_delight;
    public GameObject catMemoStage2_Chemical_cranky;  // ご機嫌斜めな解説モード（ビニール袋）：UI実装時にコメントアウト外してください
    
    public GameObject catMemoStage2_Door_delight;
    public GameObject catMemoStage2_Door_cranky;  // ご機嫌斜めな解説モード（ハサミ）：UI実装時にコメントアウト外してください

    public GameObject resultMenu;
    public Text resultText;
    private int achievementNum = 0;

    #endregion

    #region Other Scripts
    public SwitchViewManager_Stage2 switchViewManager;
    public PlayerInputManager_Stage2 playerInputManagerS2;
    #endregion

    #region Flags
    private int hasSeenPoints = 0;

    public bool stage2_Vase_Point = false;
    public bool stage2_Chemical_Point = false;
    public bool stage2_Door_Point = false;
    #endregion
    #endregion

    void Start()
    {
        InitApp();
    }

    void InitApp()
    {
        /*Cat Memo -> SetActive(false) */
        catMemoStage2_Vase_delight.SetActive(false);
        catMemoStage2_Vase_cranky.SetActive(false);

        catMemoStage2_Chemical_delight.SetActive(false);
        catMemoStage2_Chemical_cranky.SetActive(false);


        catMemoStage2_Door_delight.SetActive(false);
        catMemoStage2_Door_cranky.SetActive(false);
    }

    public void InitMyCatRay()
    {
        if (playerInputManagerS2.iamCat)
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
        #region Patrol Dangerous Points
        #region On Stage 1
        if (SceneManager.GetActiveScene().name == "004 Stage2") // Need to fix "scene.name" when Finalize
        {
            switch (hasSeenPoints)
            {
                case 0:
                    #region Near LightStand
                    // If player has not never read it, show CatMemo 01
                    if (!stage2_Vase_Point)
                    {
                        if (playerInputManagerS2.Stage2_Vase_Check == true)
                        {
                            catMemoStage2_Vase_delight.SetActive(true);
                        }
                        else
                        {
                            catMemoStage2_Vase_cranky.SetActive(true);
                        }
                    }

                    if (catMemoStage2_Vase_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage2_Vase_Point = true;
                        catMemoStage2_Vase_delight.SetActive(false);
                        catMemoStage2_Vase_cranky.SetActive(false);

                        // Move next point
                        switchViewManager.ViewNextDangerousPoint();
                        hasSeenPoints = 1;
                    }
                    break;
                #endregion

                case 1:
                    #region Near Plastic Bag
                    if (stage2_Vase_Point && !stage2_Chemical_Point)
                    {
                        if (playerInputManagerS2.Stage2_Chemical_Check == true)
                        {
                            catMemoStage2_Chemical_delight.SetActive(true);
                        }
                        else
                        {
                            catMemoStage2_Chemical_cranky.SetActive(true);
                        }
                    }

                    if (catMemoStage2_Chemical_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage2_Chemical_Point = true;
                        catMemoStage2_Chemical_delight.SetActive(false);
                        catMemoStage2_Chemical_cranky.SetActive(false);

                        switchViewManager.ViewNextDangerousPoint();
                        hasSeenPoints = 2;

                    }
                    break;
                #endregion

                case 2:
                    #region Near Scissors
                    if (stage2_Vase_Point && stage2_Chemical_Point && !stage2_Door_Point)
                    {
                        if (playerInputManagerS2.Stage3_Door_Check == true)
                        {
                            catMemoStage2_Door_delight.SetActive(true);
                        }
                        else
                        {
                            catMemoStage2_Door_cranky.SetActive(true);
                        }
                    }

                    if (catMemoStage2_Door_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage2_Door_Point = true;
                        catMemoStage2_Door_delight.SetActive(false);
                        catMemoStage2_Door_cranky.SetActive(false);

                        resultMenu.SetActive(true);
                        achievementNum = Convert.ToInt32(playerInputManagerS2.Stage2_Vase_Check) 
                            + Convert.ToInt32(playerInputManagerS2.Stage2_Chemical_Check)
                            + Convert.ToInt32(playerInputManagerS2.Stage3_Door_Check);
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
                        Debug.Log("Not Done Here...");

                        /* Coming Soon.... */
                        //SceneManager.LoadScene("**Tour Mode**"); // Need to fix "scene.name" when Finalize
                    }

                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("009 EndScene");// Need to fix "scene.name" when Finalize
                    }
                    playerInputManagerS2.iamCat = false;
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
