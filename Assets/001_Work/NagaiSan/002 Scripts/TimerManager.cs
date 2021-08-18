using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Require Values
    public Text timerText;
    public PlayerInputManager_Stage1_3 playerInputManagerS13;
    public CatInputManager catInputManager;

    //Å¶UI Display text
    public GameObject timeLimitImage1;
    public GameObject timeLimitImage2;
    public GameObject timeLimitImage3;

    // This value is the time limit.
    public float totalTime = 120f;

    // This Value is Initial. This number can be any non-negative number.
    int seconds = 99999;
    #endregion

    void Update()
    {
        // Cat Mode
        if (playerInputManagerS13.iamCat == true)
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
            //timerText.text = seconds.ToString();

            //*UI
            // Time limit 2:00
            if (seconds >= 115)
            {
                timeLimitImage1.SetActive(true);
            }
            else
            {
                timeLimitImage1.SetActive(false);
            }

            // Time limit 1:00
            if (60 >= seconds && seconds >= 55)
            {
                timeLimitImage2.SetActive(true);
            }
            else
            {
                timeLimitImage2.SetActive(false);
            }

            // Time limit 0:30
            if (30 >= seconds && seconds >= 25)
            {
                timeLimitImage3.SetActive(true);
            }
            else
            {
                timeLimitImage3.SetActive(false);
            }

        }
        #endregion
    }
}
