using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanUpMenu : MonoBehaviour
{
    public TargetScript[] targetScript;
    public GameObject cleanMenu1;
    public GameObject cleanMenu2;

    float life_time = 10.0f;
    float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        PrintCleanMenu();
    }

    void PrintCleanMenu()
    {
        for (int i = 0; i <= targetScript.Length; i++)
        {
            bool cFlg = targetScript[i].cleanFlg;

            if (targetScript[i].name == "Bag001")
            {
                if (cFlg == true)
                {
                    cleanMenu1.SetActive(true);

                    time += Time.deltaTime;
                    if (time >= life_time)
                    {
                        targetScript[i].cleanFlg = false;
                        time = 0.0f;
                    }
                }
                else
                {
                    cleanMenu1.SetActive(false);
                }
            }
            else if (targetScript[i].name == "Scissors001")
            {
                if (cFlg == true)
                {
                    cleanMenu2.SetActive(true);

                    time += Time.deltaTime;
                    if (time >= life_time)
                    {
                        targetScript[i].cleanFlg = false;
                        time = 0.0f;
                    }
                }
                else
                {
                    cleanMenu2.SetActive(false);
                }
            }
        }
    }
}

