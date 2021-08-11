using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Require Values
    public Text timerText;
    public PlayerInputManager playerInputManager;
    public CatInputManager catInputManager;

    // This value is the time limit.
    public float totalTime = 120f;

    // This Value is Initial. This number can be any non-negative number.
    int seconds = 99999;
    #endregion

    void Update()
    {
        // Cat Mode
        if (playerInputManager.iamCat == true)
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
        }
        #endregion
    }
}
