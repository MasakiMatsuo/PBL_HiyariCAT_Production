using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    #region Require Values
    #region UIs
    // Texts
    //public List<GameObject> guideTexts;
    public GameObject[] guideTexts = new GameObject[9];

    // Images
    //public List<GameObject> guideImages;
    public GameObject[] guideImages = new GameObject[2];
    #endregion // UIs

    private int readNum = 0;
    public bool endTutorialFlag = false;

    #endregion // Require Values


    void Start()
    {
        InitAppOnStage0();
    }

    public void InitAppOnStage0()
    {
        if (SceneManager.GetActiveScene().name == "002 Stage0")
        {
            /*
            guideTexts = new List<GameObject>();
            guideImages = new List<GameObject>();
            */
            for (int i = 0; i < guideTexts.Length; i++)
            {
                guideTexts[i].SetActive(false);
            }
            
            for (int i = 0; i < guideImages.Length; i++)
            {
                guideImages[i].SetActive(false);
            }
            
            readNum = 0;
        }
    }

    public void GuideTexts_Welcome_to_No1()
    {
        bool readDone = false;

        switch (readNum)
        {
            case 0:
                guideImages[0].SetActive(true);
                guideTexts[readNum].SetActive(true);
                readDone = true;
                break;

            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                if (OVRInput.GetDown(OVRInput.RawButton.A) && !readDone)
                {
                    if (readNum == 1){}
                    else
                    {
                        guideImages[0].SetActive(false);
                        guideImages[1].SetActive(true);
                    }

                    guideTexts[readNum -1].SetActive(false);
                    guideTexts[readNum].SetActive(true);
                    readDone = true;
                }
                break;

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
