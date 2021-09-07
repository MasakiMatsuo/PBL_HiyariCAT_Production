using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion // Other Scripts

    #endregion // Require Values

    void Start()
    {
        InitTimerMemo();
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
            #region CountDownTimer
            totalTime -= Time.deltaTime;
            seconds = (int)totalTime;
            #endregion // CountDownTimer

            #region Display Memo
            #region  Left 2:00
            if (seconds >= 115)
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
            #endregion // Display Memo
        }
        #endregion // Player Mode (Display Timer)
    }

    IEnumerator TimerEnd()
    {
        timeEndMemo.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        timeEndMemo.SetActive(false);
        playerInputManager_P.CheckRemoving();
        switchViewManager_P.SwitchViewer();
    }
}