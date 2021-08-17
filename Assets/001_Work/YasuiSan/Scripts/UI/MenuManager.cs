using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Image menu;

    public TargetScript targetScript1;
    //public TargetScript targetScript2;
    public GameObject cleanMenu;

    public float life_time = 30.0f;
    float time = 0f;

    void Update()
    {
        ChangeStartMenu();
        //PrintCleanMenu();
    }

    void ChangeStartMenu()
    {
        // ƒV[ƒ“Ø‚è‘Ö‚¦
        if (OVRInput.Get(OVRInput.Button.One))
        {
            /*Button.Two‚ª‰Ÿ‚³‚ê‚Ä‚¢‚é‚Ìˆ—*/
            menu.enabled = false;
        }
    }

    void PrintCleanMenu()
    {
        Text score_text = cleanMenu.GetComponent<Text>();
        if (targetScript1.name == "Bag001")
        {
            score_text.text = "Clean up completed\n    (Plastic bag)";
        }
        else if (targetScript1.name == "Scissors001")
        {
            score_text.text = "Clean up completed\n    (Scissors)";
        }

        bool cFlg = targetScript1.cleanFlg;
        if (cFlg == true)
        {
            cleanMenu.SetActive(true);

            time += Time.deltaTime;
            if (time > life_time)
            {
                targetScript1.cleanFlg = false;
                time = 0f;
            }
        }
        else
        {
            cleanMenu.SetActive(false);
        }
    }
}
