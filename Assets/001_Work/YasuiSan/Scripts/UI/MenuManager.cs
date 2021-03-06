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
        // シーン切り替え
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            /*Button.Twoが押されている時の処理*/
            menu.SetActive(false);
        }
    }
}
