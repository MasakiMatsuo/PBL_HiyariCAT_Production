using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTVScreen : MonoBehaviour
{
    private Image s_Image;
    //public List<Image> screens = new List<Image>();
    public Sprite[] screens;
    private int id = 0;
    public Image tvImage;

    bool sflag;

    public TourPlayerInputManager tourPIM;

    // Start is called before the first frame update
    void Start()
    {
        sflag = false;
        s_Image = GetComponent<Image>();
        //tvImage.SetActive(false);
        tvImage.enabled = false;


    }

    void ChangeScreens()
    {
        //if (Input.GetKeyDown(KeyCode.A)OVRInput.GetDown(OVRInput.RawButton.A))

        if (tourPIM.tFlg)
        {
            tvImage.enabled = true;

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                id = id < screens.Length - 1 ? id + 1 : 0;
                s_Image.sprite = screens[id];
            }
        }

        if (!tourPIM.tFlg)
        {
            tvImage.enabled = false;
        }

        /*
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            id = id < screens.Length - 1 ? id + 1 : 0;
            s_Image.sprite = screens[id];
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScreens();
    }
}
