using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuChange : MonoBehaviour
{
    [SerializeField]
    private Image menu;
    // Update is called once per frame
    void Update()
    {
        ChangeMenu();
        //Debug.Log(mCount);
    }

    void ChangeMenu()
    {
        // ƒV[ƒ“Ø‚è‘Ö‚¦
        if (OVRInput.Get(OVRInput.Button.One))
        {
            /*Button.Two‚ª‰Ÿ‚³‚ê‚Ä‚¢‚é‚Ìˆ—*/
            menu.enabled = false;
        }
    }

}
