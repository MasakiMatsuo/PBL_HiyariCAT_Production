using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager_Stage2 : MonoBehaviour
{
    public Text timerText;

    //UI Display text
    public GameObject timeLimitImage_Last2m;
    public GameObject timeLimitImage_Last1m;
    public GameObject timeLimitImage_Last30s;

    //Other Scripts
    public PlayerInputManager_Stage2 playerInputManagerS2;
    public CatInputManager_Stage2 catInputManager;
    public SwitchViewManager_Stage2 switchViewManager;

    // This value is the time limit.
    public float totalTime = 120f;

    // This Value is Initial. This number can be any non-negative number.
    int seconds = 99999;

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

    public void RemoveTimer()
    {
        #region On Cat Mode (Remove Timer)
        seconds = 0;
        timerText.color = new Color(1f, 0f, 0f);
        totalTime = 0;
        #endregion
    }

    public void CountDownTimer()
    {
        #region Player Mode (Display Timer)
        if (seconds <= 0)
        {
            seconds = 0;

            /*This setting is my idea, it can change*/
            timerText.color = new Color(1f, 0f, 0f);

            totalTime = 0;
        }
        else
        {
            totalTime -= Time.deltaTime;

            seconds = (int)totalTime;
            timerText.text = seconds.ToString();

            //*UI
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
                //Before moving to cat mode delate removeB masaki
                playerInputManagerS2.removeB_Vase.SetActive(false);
                playerInputManagerS2.removeB_Chemical.SetActive(false);
                playerInputManagerS2.removeB_Door.SetActive(false);
                playerInputManagerS2.removeB_Door2.SetActive(false);

                playerInputManagerS2.CheckRemoving();
                switchViewManager.SwitchViewer();
            }

        }
        #endregion
    }
}
