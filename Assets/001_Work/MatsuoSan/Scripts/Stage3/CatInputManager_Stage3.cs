using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class CatInputManager_Stage3 : MonoBehaviour
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
    public GameObject catMemo_Item1_delight;
    public GameObject catMemo_Item1_cranky;

    public GameObject catMemo_Item2_delight;
    public GameObject catMemo_Item2_cranky;

    public GameObject catMemo_Item3_delight;
    public GameObject catMemo_Item3_cranky;

    //Stage2


    //Stage3

    public GameObject resultMenu;
    public Text resultText;
    private int achievementNum = 0;
    #endregion

    #region Other Scripts
    public SwitchViewManager_Stage3 switchViewManager;
    public PlayerInputManager_Stage3 playerInputManagerS3;
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
        catMemo_Item1_delight.SetActive(false);
        catMemo_Item1_cranky.SetActive(false);

        catMemo_Item2_delight.SetActive(false);
        catMemo_Item2_cranky.SetActive(false);

        catMemo_Item3_delight.SetActive(false);
        catMemo_Item3_cranky.SetActive(false);
    }

    public void InitMyCatRay()
    {
        if (playerInputManagerS3.iamCat)
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

            catMemo_Item3_delight.SetActive(true);

            if (catMemo_Item3_delight && OVRInput.GetDown(OVRInput.RawButton.A))
            {
                stage1_Scissors_Point = true;
                catMemo_Item3_delight.SetActive(false);

                //resultMenu.SetActive(true);

            }
        }

        #region Patrol Dangerous Points
        #region On Stage 1
        if (SceneManager.GetActiveScene().name == "005 Stage3") // Need to fix "scene.name" when Finalize
        {
            switch (hasSeenPoints)
            {
                case 0:
                    #region Near LightStand
                    // If player has not never read it, show CatMemo 01
                    if (!stage1_LS_Point)
                    {
                        //catMemo_Item1_delight.SetActive(true);
                        if (playerInputManagerS3.Stage3_Chocolate_Check == true)
                        {
                            catMemo_Item1_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Item1_cranky.SetActive(true);
                        }
                    }

                    if (catMemo_Item1_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_LS_Point = true;

                        catMemo_Item1_delight.SetActive(false);
                        catMemo_Item1_cranky.SetActive(false);

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
                        //catMemo_Item2_delight.SetActive(true);

                        //Are there any interactive objects left?
                        if (playerInputManagerS3.Stage3_Code_Check == true)
                        {
                            catMemo_Item2_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Item2_cranky.SetActive(true);
                        }
                    }

                    if (catMemo_Item2_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_PB_Point = true;

                        catMemo_Item2_delight.SetActive(false);
                        catMemo_Item1_cranky.SetActive(false);


                        switchViewManager.ViewNextDangerousPoint();
                        hasSeenPoints = 2;

                    }
                    break;
                #endregion

                case 2:
                    #region Near Scissors
                    if (stage1_LS_Point && stage1_PB_Point && !stage1_Scissors_Point)
                    {
                        //catMemo_Item3_delight.SetActive(true);
                        if (playerInputManagerS3.Stage3_SC_Check == true)
                        {
                            catMemo_Item3_delight.SetActive(true);

                        }
                        else
                        {
                            catMemo_Item3_cranky.SetActive(true);
                        }
                    }

                    if (catMemo_Item3_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        stage1_Scissors_Point = true;

                        catMemo_Item3_delight.SetActive(false);
                        catMemo_Item3_cranky.SetActive(false);

                        resultMenu.SetActive(true);
                        achievementNum = Convert.ToInt32(playerInputManagerS3.Stage3_Chocolate_Check) + Convert.ToInt32(playerInputManagerS3.Stage3_Code_Check)
                            + Convert.ToInt32(playerInputManagerS3.Stage3_SC_Check);
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
        else if (SceneManager.GetActiveScene().name == "?y?????L???Q?l??Scene?????O?????????????????B?z")
        {
            if (!read)
                {
                    ?y?\???????????????z.SetActive(true);
                }

                if (OVRInput.GetDown(OVRInput.RawButton.A))
                {
                    read = true;
                    ?y?\???????????????z.SetActive(false);
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
                        //SceneManager.LoadScene("006 TourStage1_Human");
                        /* Coming Soon.... */
                        //SceneManager.LoadScene("**Tour Mode**"); // Need to fix "scene.name" when Finalize
                    }

                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("009 EndScene");// Need to fix "scene.name" when Finalize
                    }
                    playerInputManagerS3.iamCat = false;
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
