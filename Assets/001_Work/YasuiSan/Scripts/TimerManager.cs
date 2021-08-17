using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Require Values
    public Text timerText1;
    public Text timerText2;
    public Text timerText3;
    public InputManager inputManager;

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
        if (inputManager.iamCat == true)
        {
            #region On Cat Mode (Remove Timer)
            seconds = 0;
            timerText1.color = new Color(1f, 0f, 0f);
            totalTime = 0;

            //This process may be unnecessary. -> "timerText.gameObject.SetActive(false);"
            //because "MyOVRPlayerController" will be false anyway on Cat Mode. As a result, it does the same thing as this process.
            timerText1.gameObject.SetActive(false);

            #endregion
        }

        else
        {
            Debug.LogWarning(seconds);
            #region Player Mode (Display Timer)
            if (seconds <= 0)
            {
                seconds = 0;
                timerText1.color = new Color(1f, 0f, 0f);
                totalTime = 0;
            }
            else
            {
                totalTime -= Time.deltaTime;
                seconds = (int)totalTime;

                if ( seconds >= 115)
                {
                   
                    timerText1.gameObject.SetActive(true);
                    //timeLimitImage1.SetActive(true);
                }else
                {
                    timeLimitImage1.SetActive(false);
                }

                if (60 >= seconds && seconds >= 55)
                {
                    
                    timerText2.gameObject.SetActive(true);
                    timeLimitImage2.SetActive(true);
                }
                else
                {
                    timeLimitImage2.SetActive(false);
                }

                if (30 >= seconds && seconds >= 25)
                {
                    
                    timerText3.gameObject.SetActive(true);
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
}
