using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager_Stage2 : MonoBehaviour
{
    #region Require Values
    #region UI Display text
    public GameObject timeLimitImage_Last2m;
    public GameObject timeLimitImage_Last1m;
    public GameObject timeLimitImage_Last30s;
    public GameObject timeEndMemo;
    #endregion // UI Display text

    #region Other Scripts
    public PlayerInputManager_Stage2 playerInputManagerS2;
    public CatInputManager_Stage2 catInputManager;
    public SwitchViewManager_Stage2 switchViewManager;
    #endregion // Other Scripts

    // This value is the time limit.
    public float totalTime = 120f;

    // This Value is Initial. This number can be any non-negative number.
    int seconds = 99999;
    #endregion // Require Values

    void Start()
    {
        InitTimerMemo();
        playerInputManagerS2.GetComponent<PlayerInputManager_Stage2>();
    }

    void Update()
    {
        // Cat Mode
        if (playerInputManagerS2.iamCat == true)
        {
            RemoveTimer();
        }

        // Player Mode
        else
        {
            CountDownTimer();
        }
    }

    void InitTimerMemo()
    {
        timeLimitImage_Last2m.SetActive(false);
        timeLimitImage_Last1m.SetActive(false);
        timeLimitImage_Last30s.SetActive(false);
        timeEndMemo.SetActive(false);
    }

    public void RemoveTimer()
    {
        #region On Cat Mode (Remove Timer)
        seconds = 0;
        totalTime = 0;
        #endregion // On Cat Mode (Remove Timer)
    }

    public void CountDownTimer()
    {
        #region Player Mode (Display Timer)
        if (seconds <= 0)
        {
            RemoveTimer();
        }
        else
        {
            if (playerInputManagerS2.countDownStart)
            {
                #region CountDownTimer
                totalTime -= Time.deltaTime;
                seconds = (int)totalTime;
                #endregion // CountDownTimer
            }


            // Time limit 2:00
            if (seconds >= 115)
            {
                timeLimitImage_Last2m.SetActive(true);
            }
            else
            {
                timeLimitImage_Last2m.SetActive(false);
            }

            // Time limit 1:00
            if (60 >= seconds && seconds >= 55)
            {
                timeLimitImage_Last1m.SetActive(true);
            }
            else
            {
                timeLimitImage_Last1m.SetActive(false);
            }

            // Time limit 0:30
            if (30 >= seconds && seconds >= 25)
            {
                timeLimitImage_Last30s.SetActive(true);
            }
            else
            {
                timeLimitImage_Last30s.SetActive(false);
            }

            if (seconds <= 0)
            {
                StartCoroutine(TimerEnd());
            }
        }
        #endregion // Player Mode (Display Timer)
    }


    IEnumerator TimerEnd()
    {
        timeEndMemo.SetActive(true);
        playerInputManagerS2.pauseMenu.SetActive(false);

        //Before moving to cat mode delate removeB masaki
        playerInputManagerS2.removeB_Vase.SetActive(false);
        playerInputManagerS2.removeB_Chemical.SetActive(false);
        playerInputManagerS2.removeB_Door.SetActive(false);
        playerInputManagerS2.removeB_Door2.SetActive(false);

        yield return new WaitForSeconds(3.5f);

        timeEndMemo.SetActive(false);
        playerInputManagerS2.CheckRemoving();
        switchViewManager.SwitchViewer();

    }
}
