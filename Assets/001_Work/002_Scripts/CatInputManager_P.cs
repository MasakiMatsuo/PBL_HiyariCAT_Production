using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CatInputManager_P : MonoBehaviour
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
    // Cat Memo
    public GameObject catMemo_Pos01_delight;
    public GameObject catMemo_Pos01_cranky;

    public GameObject catMemo_Pos02_delight;
    public GameObject catMemo_Pos02_cranky;

    public GameObject catMemo_Pos03_delight;
    public GameObject catMemo_Pos03_cranky;

    public GameObject resultMenu;
    public Text resultText;
    private int achievementNum = 0;
    #endregion

    #region Other Scripts
    public SwitchViewManager_P switchViewManager_P;
    public PlayerInputManager_P playerInputManager_P;
    #endregion

    #region Flags
    private int hasSeenPoints = 0;

    public bool pos01_ReadFlag = false;
    public bool pos02_ReadFlag = false;
    public bool pos03_ReadFlag = false;
    #endregion
    #endregion

    void Start()
    {
        InitApp();
    }

    void InitApp()
    {
        #region Cat Memo Initialized
        catMemo_Pos01_delight.SetActive(false);
        catMemo_Pos01_cranky.SetActive(false);

        catMemo_Pos02_delight.SetActive(false);
        catMemo_Pos02_cranky.SetActive(false);

        catMemo_Pos03_delight.SetActive(false);
        catMemo_Pos03_cranky.SetActive(false);
        #endregion // Cat Memo Initialized
        resultMenu.SetActive(false);
    }

    public void InitMyCatRay()
    {
        if (playerInputManager_P.iamCat)
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
        #region On Stage 0
        if (SceneManager.GetActiveScene().name == "002 Stage0")
        {
            #region For Tutorial Setting
            pos01_ReadFlag = true;
            pos02_ReadFlag = true;

            #region Display CatMemo
            if (!pos03_ReadFlag)
            {
                catMemo_Pos03_delight.SetActive(true);
            }
            else
            {
                catMemo_Pos03_delight.SetActive(false);
            }
            #endregion // Display CatMemo
            #endregion // For Tutorial Setting

            if (catMemo_Pos03_delight && OVRInput.GetDown(OVRInput.RawButton.A))
            {
                pos03_ReadFlag = true;
                hasSeenPoints = -1;

                resultMenu.SetActive(true);
            }
        }
        #endregion // On Stage 0

        #region Patrol Dangerous Points
        #region On Stage 1
        if (SceneManager.GetActiveScene().name == "003 Stage1") // Need to fix "scene.name" when Finalize
        {
            switch (hasSeenPoints)
            {
                case 0:
                    #region Near LightStand
                    // If player has not never read it, show CatMemo 01
                    if (!pos01_ReadFlag)
                    {
                        #region catMemo_Pos01.SetActive(true);
                        if (playerInputManager_P.dangerPos01_Check == true)
                        {
                            catMemo_Pos01_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Pos01_cranky.SetActive(true);
                        }
                        #endregion // catMemo_Pos01.SetActive(true);
                    }

                    if (catMemo_Pos01_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        #region After read catMemo_Pos01
                        pos01_ReadFlag = true;

                        catMemo_Pos01_delight.SetActive(false);
                        catMemo_Pos01_cranky.SetActive(false);

                        // Move next point
                        switchViewManager_P.ViewNextDangerousPoint();
                        hasSeenPoints = 1;
                        #endregion // After read catMemo_Pos01
                    }
                    break;
                #endregion // Near LightStand

                case 1:
                    #region Near Plastic Bag
                    if (pos01_ReadFlag && !pos02_ReadFlag)
                    {
                        #region catMemo_Pos02.SetActive(true);
                        if (playerInputManager_P.dangerPos02_Check == true)
                        {
                            catMemo_Pos02_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Pos02_cranky.SetActive(true);
                        }
                        #endregion // catMemo_Pos02.SetActive(true);
                    }

                    if (catMemo_Pos02_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        #region After read catMemo_Pos02
                        pos02_ReadFlag = true;

                        catMemo_Pos02_delight.SetActive(false);
                        catMemo_Pos02_cranky.SetActive(false);

                        switchViewManager_P.ViewNextDangerousPoint();
                        hasSeenPoints = 2;
                        #endregion // After read catMemo_Pos02
                    }
                    break;
                #endregion // Near Plastic Bag

                case 2:
                    #region Near Scissors
                    if (pos01_ReadFlag && pos02_ReadFlag && !pos03_ReadFlag)
                    {
                        #region catMemo_Pos03.SetActive(true);
                        if (playerInputManager_P.dangerPos03_Check == true)
                        {
                            catMemo_Pos03_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Pos03_cranky.SetActive(true);
                        }
                        #endregion // catMemo_Pos03.SetActive(true);
                    }

                    if (catMemo_Pos03_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        #region After read catMemo_Pos03
                        pos03_ReadFlag = true;

                        catMemo_Pos03_delight.SetActive(false);
                        catMemo_Pos03_cranky.SetActive(false);

                        resultMenu.SetActive(true);
                        achievementNum = Convert.ToInt32(playerInputManager_P.dangerPos01_Check) + Convert.ToInt32(playerInputManager_P.dangerPos02_Check)
                            + Convert.ToInt32(playerInputManager_P.dangerPos03_Check);
                        resultText.text = achievementNum + "/3";

                        hasSeenPoints = -1;
                        #endregion // After read catMemo_Pos03
                    }
                    break;
                #endregion // Near Scissors

                default:
                    break;
            }
        }
        #endregion // On Stage 1
        #region On Stage 3
        if (SceneManager.GetActiveScene().name == "005 Stage3") // Need to fix "scene.name" when Finalize
        {
            switch (hasSeenPoints)
            {
                case 0:
                    #region Near Chocolate
                    // If player has not never read it, show CatMemo 01
                    if (!pos01_ReadFlag)
                    {
                        #region catMemo_Pos01.SetActive(true);
                        if (playerInputManager_P.dangerPos01_Check == true)
                        {
                            catMemo_Pos01_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Pos01_cranky.SetActive(true);
                        }
                        #endregion // catMemo_Pos01.SetActive(true);
                    }

                    if (catMemo_Pos01_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        #region After read catMemo_Pos01
                        pos01_ReadFlag = true;

                        catMemo_Pos01_delight.SetActive(false);
                        catMemo_Pos01_cranky.SetActive(false);

                        // Move next point
                        switchViewManager_P.ViewNextDangerousPoint();
                        hasSeenPoints = 1;
                        #endregion // After read catMemo_Pos01
                    }
                    break;
                #endregion // Near Chocolate

                case 1:
                    #region Near Plastic Bag
                    if (pos01_ReadFlag && !pos02_ReadFlag)
                    {
                        #region catMemo_Pos02.SetActive(true);
                        if (playerInputManager_P.dangerPos02_Check == true)
                        {
                            catMemo_Pos02_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Pos02_cranky.SetActive(true);
                        }
                        #endregion // catMemo_Pos02.SetActive(true);
                    }

                    if (catMemo_Pos02_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        #region After read catMemo_Pos02
                        pos02_ReadFlag = true;

                        catMemo_Pos02_delight.SetActive(false);
                        catMemo_Pos01_cranky.SetActive(false);

                        switchViewManager_P.ViewNextDangerousPoint();
                        hasSeenPoints = 2;
                        #endregion // After read catMemo_Pos02
                    }
                    break;
                #endregion // Near Plastic Bag

                case 2:
                    #region Near Storage Cabinet
                    if (pos01_ReadFlag && pos02_ReadFlag && !pos03_ReadFlag)
                    {
                        #region catMemo_Pos03.SetActive(true);
                        if (playerInputManager_P.dangerPos03_Check == true)
                        {
                            catMemo_Pos03_delight.SetActive(true);
                        }
                        else
                        {
                            catMemo_Pos03_cranky.SetActive(true);
                        }
                        #endregion // catMemo_Pos03.SetActive(true);
                    }

                    if (catMemo_Pos03_delight && OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        #region After read catMemo_Pos03
                        pos03_ReadFlag = true;

                        catMemo_Pos03_delight.SetActive(false);
                        catMemo_Pos03_cranky.SetActive(false);

                        resultMenu.SetActive(true);
                        achievementNum = Convert.ToInt32(playerInputManager_P.dangerPos01_Check) + Convert.ToInt32(playerInputManager_P.dangerPos02_Check)
                            + Convert.ToInt32(playerInputManager_P.dangerPos03_Check);
                        resultText.text = achievementNum + "/3";

                        hasSeenPoints = -1;
                        #endregion // After read catMemo_Pos03
                    }
                    break;
                #endregion // Near Storage Cabinet

                default:
                    break;
            }
        }
        #endregion // On Stage 3
        #endregion // Patrol Dangerous Points

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

            #region Remove Ray when point on Target of forgotten to remove.
            foreach (var hit in hits)
            {
                string tagName = hit.collider.tag;
                if (tagName == "Target")
                {
                    // Remove Ray
                    catRayObject.SetPosition(1, catRightController.transform.position + catRightController.transform.forward * 0.0f);
                }
            }
            #endregion // Remove Ray when point on Target of forgotten to remove.

            // Selecting
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                foreach (var hit in hits)
                {
                    string tagName = hit.collider.tag;
                    #region Scene Transitions
                    /*Remove soon "Debug"*/
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

                    else if (tagName == "TourHere")
                    {
                        if (SceneManager.GetActiveScene().name == "003 Stage1")
                        {
                            SceneManager.LoadScene("006 TourStage1_Human");
                        }
                        /* Coming soon....
                        else if (SceneManager.GetActiveScene().name == "005 Stage3")
                        {
                            SceneManager.LoadScene("008 TourStage3_Human");
                        }
                        */
                    }

                    else if (tagName == "Quit")
                    {
                        SceneManager.LoadScene("009 EndScene");// Need to fix "scene.name" when Finalize
                    }
                    playerInputManager_P.iamCat = false;
                    #endregion // Scene Transitions
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
