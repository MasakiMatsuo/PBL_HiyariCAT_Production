using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CleanUpMenu_P : MonoBehaviour
{
    public TargetScript_P[] targetScript_P;
    public GameObject cleanMenu1;
    public GameObject cleanMenu2;

    public bool officeKnifeRemoveFlag = false;

    float life_time = 3.0f;
    float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        PrintCleanMenu();
    }

    void PrintCleanMenu()
    {
        for (int i = 0; i < targetScript_P.Length; i++)
        {
            bool cFlg = targetScript_P[i].cleanFlg;

            if (SceneManager.GetActiveScene().name == "002 Stage0")
            {
                if (targetScript_P[i].name == "Handgun001")
                {
                    GenerateCM1(cFlg, i);
                }
                else if (targetScript_P[i].name == "OfficeKnife001")
                {
                    GenerateCM2(cFlg, i);
                }
                else { officeKnifeRemoveFlag = false; }
            }

            if (targetScript_P[i].name == "Bag001")
            {
                if (cFlg == true)
                {
                    cleanMenu1.SetActive(true);

                    time += Time.deltaTime;
                    if (time >= life_time)
                    {
                        targetScript_P[i].cleanFlg = false;
                        time = 0.0f;
                    }
                }
                else
                {
                    cleanMenu1.SetActive(false);
                }
            }
            else if (targetScript_P[i].name == "Scissors001")
            {
                if (cFlg == true)
                {
                    cleanMenu2.SetActive(true);

                    time += Time.deltaTime;
                    if (time >= life_time)
                    {
                        targetScript_P[i].cleanFlg = false;
                        time = 0.0f;
                    }
                }
                else
                {
                    cleanMenu2.SetActive(false);
                }
            }
            else if (targetScript_P[i].name == "Chocolate001")
            {
                if (cFlg == true)
                {
                    cleanMenu1.SetActive(true);

                    time += Time.deltaTime;
                    if (time >= life_time)
                    {
                        targetScript_P[i].cleanFlg = false;
                        time = 0.0f;
                    }
                }
                else
                {
                    cleanMenu1.SetActive(false);
                }
            }
            else if (targetScript_P[i].name == "Code001")
            {
                if (cFlg == true)
                {
                    cleanMenu2.SetActive(true);

                    time += Time.deltaTime;
                    if (time >= life_time)
                    {
                        targetScript_P[i].cleanFlg = false;
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


    public void GenerateCM1(bool cF, int cnt)
    {
        if (cF)
        {
            cleanMenu1.SetActive(true);
            DisplayTextTime(cnt);
        }
        else
        {
            cleanMenu1.SetActive(false);
        }
    }

    public void GenerateCM2(bool cF, int cnt)
    {
        if (cF)
        {
            cleanMenu2.SetActive(true);
            DisplayTextTime(cnt);
            officeKnifeRemoveFlag = true;
        }
        else
        {
            cleanMenu2.SetActive(false);
        }
    }

    public void DisplayTextTime(int c)
    {
        time += Time.deltaTime;

        if (time >= life_time)
        {
            targetScript_P[c].cleanFlg = false;
            time = 0.0f;
        }
    }

}

