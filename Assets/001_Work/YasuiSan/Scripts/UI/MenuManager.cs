using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    void Update()
    {
        ChangeStartMenu();
    }

    void ChangeStartMenu()
    {
        // ƒV[ƒ“Ø‚è‘Ö‚¦
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            /*Button.Two‚ª‰Ÿ‚³‚ê‚Ä‚¢‚é‚Ìˆ—*/
            menu.SetActive(false);
        }
    }
}
