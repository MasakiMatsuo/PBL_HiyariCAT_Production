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
        // �V�[���؂�ւ�
        if (OVRInput.Get(OVRInput.Button.One))
        {
            /*Button.Two��������Ă��鎞�̏���*/
            menu.enabled = false;
        }
    }
}
