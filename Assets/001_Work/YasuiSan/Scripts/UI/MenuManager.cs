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
        // �V�[���؂�ւ�
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            /*Button.Two��������Ă��鎞�̏���*/
            menu.SetActive(false);
        }
    }
}
