using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Image menu;

    void Update()
    {
        ChangeStartMenu();
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
}
