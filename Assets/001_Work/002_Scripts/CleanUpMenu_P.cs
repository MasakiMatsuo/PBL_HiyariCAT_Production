using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CleanUpMenu_P : MonoBehaviour
{
    public TargetScript_P[] targetScript_P;
    public GameObject cleanMenu1;
    public GameObject cleanMenu2;

    public GameObject removeHeavyObj;

    public bool officeKnifeRemoveFlag = false;

    float life_time = 3.0f;
    float time = 0.0f;

    void Start()
    {
        InitCleanMenu();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "004 Stage2")
        {
            PrintCleanMenu();
        }
        
        if (removeHeavyObj.GetComponent<RemoveHeavyObj>().removeHeavyObjFlag01)
        {
            StartCoroutine(DisplayRemoveMessage());
        }
    }

    void InitCleanMenu()
    {
        officeKnifeRemoveFlag = false;
        cleanMenu1.SetActive(false);
        cleanMenu2.SetActive(false);
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

    public IEnumerator DisplayRemoveMessage()
    {
        if (SceneManager.GetActiveScene().name != "004 Stage2")
        {
            removeHeavyObj.GetComponent<RemoveHeavyObj>().removeMessage.SetActive(true);

            yield return new WaitForSeconds(3.0f);
            removeHeavyObj.GetComponent<RemoveHeavyObj>().removeMessage.SetActive(false);
        }
    }

    public IEnumerator DisplayRemoveMessageOnStage2(int oIndex)
    {
        if (oIndex == 0)
        {
            cleanMenu1.SetActive(true);

            yield return new WaitForSeconds(3.0f);
            cleanMenu1.SetActive(false);
        }
        else if(oIndex == 1)
        {
            cleanMenu2.SetActive(true);

            yield return new WaitForSeconds(3.0f);
            cleanMenu2.SetActive(false);
        }
        else if (oIndex == 2)
        {
            removeHeavyObj.GetComponent<RemoveHeavyObj>().removeMessage.SetActive(true);

            yield return new WaitForSeconds(3.0f);
            removeHeavyObj.GetComponent<RemoveHeavyObj>().removeMessage.SetActive(false);
        }
    }
}

