using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager_P : MonoBehaviour
{
    #region Require Values
    #region Display Objects
    public GameObject timeLimit_L2M;
    public GameObject timeLimit_L1M;
    public GameObject timeLimit_LHalfM;
    public GameObject timeEndMemo;
    #endregion // Display Objects

    #region Values
    // Time Limit Value.
    public float totalTime = 120f;

    // Initial Value. This number can be any non-negative number.
    int seconds = 99999;
    #endregion // Values

    #region Other Scripts
    public SwitchViewManager_P switchViewManager_P;
    public PlayerInputManager_P playerInputManager_P;
    public TeleportTargetHandler _TTH;
    #endregion // Other Scripts

    //Audio CatSE
    public AudioSource audioCat;

    #endregion // Require Values

    void Start()
    {
        InitTimerMemo();
        playerInputManager_P.GetComponent<PlayerInputManager_P>();
    }

    void Update()
    {
        // Cat Mode
        if (playerInputManager_P.iamCat == true)
        {
            ResetTimer();
        }
        // Player Mode
        else
        {
            CountDownTimer();
        }
    }

    void InitTimerMemo()
    {
        timeLimit_L2M.SetActive(false);
        timeLimit_L1M.SetActive(false);
        timeLimit_LHalfM.SetActive(false);
        timeEndMemo.SetActive(false);
    }

    void ResetTimer()
    {
        #region On Cat Mode (Remove Timer)
        seconds = 0;
        totalTime = 0;
        #endregion // On Cat Mode (Remove Timer)
    }

    void CountDownTimer()
    {
        #region Player Mode (Display Timer)
        if (seconds <= 0)
        {
            ResetTimer();
        }
        else
        {
            if (playerInputManager_P.countDownStart)
            {
                #region CountDownTimer
                totalTime -= Time.deltaTime;
                seconds = (int)totalTime;
                #endregion // CountDownTimer
            }

            #region Display Memo
            #region  Left 2:00
            if (120 > seconds && seconds >= 115)
            {
                timeLimit_L2M.SetActive(true);
            }
            else
            {
                timeLimit_L2M.SetActive(false);
            }
            #endregion //  Left 2:00

            #region  Left 1:00
            if (60 >= seconds && seconds >= 55)
            {
                timeLimit_L1M.SetActive(true);
            }
            else
            {
                timeLimit_L1M.SetActive(false);
            }
            #endregion //  Left 1:00

            #region  Left 0:30
            if (30 >= seconds && seconds >= 25)
            {
                timeLimit_LHalfM.SetActive(true);
            }
            else
            {
                timeLimit_LHalfM.SetActive(false);
            }
            #endregion //  Left 0:30

            #region  Timer End
            if (seconds <= 0)
            {
                StartCoroutine(TimerEnd());
            }
            #endregion // Timer End

            //Play Audio
            if (120 > seconds && seconds >= 119)
            {
                audioCat.Play();
            }
            else if(60 >= seconds && seconds >= 59)
            {
                audioCat.Play();
            }
            else if (30 >= seconds && seconds >= 29)
            {
                audioCat.Play();
            }
            else if (seconds <= 0)
            {
                audioCat.Play();
            }

            #endregion // Display Memo
        }
        #endregion // Player Mode (Display Timer)
    }

    IEnumerator TimerEnd()
    {
        timeEndMemo.SetActive(true);
        playerInputManager_P.pauseMenu.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            for (int i = 0; i < 4; i++)
            {
                playerInputManager_P.removeBs[i].SetActive(false);
            }
        }
        else
        {
            playerInputManager_P.removeBs[0].SetActive(false);
        }

        _TTH.AimCollisionLayerMask = 0;
        yield return new WaitForSeconds(3.5f);

        timeEndMemo.SetActive(false);
        playerInputManager_P.CheckRemoving();
        switchViewManager_P.SwitchViewer();
    }
}
