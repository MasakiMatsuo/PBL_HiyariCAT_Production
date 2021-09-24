using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveMassage : MonoBehaviour
{
    public bool loopCount = true;
    float time = 0.0f;

    GameObject item1 = default;
    GameObject item2 = default;

    public GameObject cleanMenu1;
    public GameObject cleanMenu2;
    public GameObject cleanMenu3;

    void Update()
    {
        //Stage1
        if (SceneManager.GetActiveScene().name == "003 Stage1")
        {
            item1 = GameObject.Find("LightStand001");

            if(!item1)
            {
                if (loopCount)
                {
                    cleanMenu1.SetActive(true);
                }

                time += Time.deltaTime;
                if (time >= 5.0)
                {
                    cleanMenu1.SetActive(false);
                    time = 0.0f;
                }

                loopCount = false;
            }
        }
        
        //Stage2
        if (SceneManager.GetActiveScene().name == "004 Stage2")
        {
            item1 = GameObject.Find("Vase001");
            item2 = GameObject.Find("Chemicals001v2");

            if (!item1)
            {
                if (loopCount)
                {
                    cleanMenu1.SetActive(true);
                }
                time += Time.deltaTime;
                if (time >= 5.0)
                {
                    cleanMenu1.SetActive(false);
                    time = 0.0f;
                }
                loopCount = false;
            }
            if (!item2)
            {
                if (loopCount)
                {
                    cleanMenu2.SetActive(true);
                }
                time += Time.deltaTime;
                if (time >= 5.0)
                {
                    cleanMenu2.SetActive(false);
                    time = 0.0f;
                }
                loopCount = false;
            }
        }
        
        //Stage3
        if (SceneManager.GetActiveScene().name == "005 Stage3")
        {
            item1 = GameObject.Find("StorageCabinet001");

            if (!item1)
            {
                if (loopCount)
                {
                    cleanMenu1.SetActive(true);
                }
                time += Time.deltaTime;
                if (time >= 5.0)
                {
                    cleanMenu1.SetActive(false);
                    time = 0.0f;
                }
                loopCount = false;
            }
        }
    }
}
