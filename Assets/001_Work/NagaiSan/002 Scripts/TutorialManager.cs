using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    #region Require Values
    public GameObject[] tutorialObjects = new GameObject[3];


    #region UIs
    public GameObject pauseMenu;
    public GameObject tutorialEndScreen;

    // Texts
    public GameObject[] guideTexts = new GameObject[34];

    // Images
    public GameObject[] guideImages = new GameObject[8];
    #endregion // UIs

    #region OtherScripts
    public PlayerInputManager_Stage1_3 playerIMS1_3;
    public CleanUpMenu cleanUpMenu;
    #endregion // OtherScripts

    #region Flags
    private int readNum = 0;
    public bool endTutorialFlag = false;
    #endregion // Flags

    #endregion // Require Values


    void Start()
    {
        InitAppOnStage0();
    }

    /*
     * Creating now...
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "002 Stage0" && cleanUpMenu.officeKnifeRemoveFlag)
        {
            tutorialEndScreen.SetActive(true);
        }
        else
        {
            tutorialEndScreen.SetActive(false);
        }

        if (playerIMS1_3.iamCat)
        {
            cleanUpMenu.officeKnifeRemoveFlag = false;
        }
    }
    */

    public void InitAppOnStage0()
    {
        if (SceneManager.GetActiveScene().name == "002 Stage0")
        {
            playerIMS1_3.GetComponent<PlayerInputManager_Stage1_3>();
            cleanUpMenu.GetComponent<CleanUpMenu>();

            for (int i = 0; i < tutorialObjects.Length; i++)
            {
                tutorialObjects[i].SetActive(false);
            }
            for (int i = 0; i < guideTexts.Length; i++)
            {
                guideTexts[i].SetActive(false);
            }
            
            for (int i = 0; i < guideImages.Length; i++)
            {
                guideImages[i].SetActive(false);
            }

            if (pauseMenu)
            {
                pauseMenu.SetActive(false);
            }

            readNum = 0;
        }
    }

    public void GuideTexts_Welcome_to_No1()
    {
        bool readDone = false;

        switch (readNum)
        {
            #region case 0
            case 0:
                guideImages[0].SetActive(true);
                guideTexts[readNum].SetActive(true);
                readDone = true;
                break;
            #endregion // case 0
            #region case 1~34
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
            case 33:
            case 34:
                if (OVRInput.GetDown(OVRInput.RawButton.A) && !readDone)
                {
                    #region Display Media
                    if (readNum == 1){}
                    else if(1 < readNum && readNum <= 8)
                    {
                        guideImages[0].SetActive(false);
                        guideImages[1].SetActive(true);
                    }
                    else if (8 < readNum && readNum <= 11)
                    {
                        guideImages[1].SetActive(false);
                        guideImages[2].SetActive(true);
                    }
                    else if (readNum == 12)
                    {
                        guideImages[2].SetActive(false);
                        guideImages[3].SetActive(true);
                    }
                    else if (readNum == 13)
                    {
                        guideImages[3].SetActive(false);
                        guideImages[4].SetActive(true);
                    }
                    else if (readNum == 14 || readNum == 15)
                    {
                        guideImages[4].SetActive(false);
                        guideImages[5].SetActive(true);
                    }
                    else if (15 < readNum && readNum <= 17)
                    {
                        guideImages[5].SetActive(false);
                        guideImages[6].SetActive(true);
                    }
                    else if (17 < readNum && readNum <= 27)
                    {
                        guideImages[6].SetActive(false);
                        guideImages[7].SetActive(true);
                    }
                    #endregion // Display Media

                    // Release Tutorial Objects: Handgun and Barrel
                    if (readNum == 26)
                    {
                        for (int i = 0; i < tutorialObjects.Length -1; i++)
                        {
                            tutorialObjects[i].SetActive(true);
                        }
                    }

                    #region Switch the message to be displayed
                    guideTexts[readNum -1].SetActive(false);

                    if (0 < readNum && readNum < 34)
                    {
                        guideTexts[readNum].SetActive(true);
                    }
                    #endregion // Switch the message to be displayed
                    readDone = true;

                    #region Open Operate Pause Menu
                    if (readDone && readNum == 32)
                    {
                        endTutorialFlag = true;
                    }
                    #endregion // Open Operate Pause Menu

                    #region Closing Guide Texts and Release a Tutorial Target: Cutter knife
                    if (readDone && readNum == 34)
                    {
                        GameObject messages = GameObject.Find("Messages");
                        messages.SetActive(false);

                        readNum = -1;
                        readDone = false;

                        tutorialObjects[2].SetActive(true);
                    }
                    #endregion // Closing Guide Texts and Release a Tutorial Target: Cutter knife
                }

                break;
            #endregion // case 1~34

            default:
                break;
        }

        if (readDone)
        {
            readDone = false;
            readNum++;
        }

    }

}
