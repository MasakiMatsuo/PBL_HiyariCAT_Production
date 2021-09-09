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

    public GameObject remoteText1;
    public GameObject remoteText2;
    public GameObject tvText;

    public GameObject controller1;
    public GameObject controller2;
    public GameObject controller1S;
    public GameObject controller2S;

    bool sflag;

    public TourPlayerInputManager tourPIM;

    // Start is called before the first frame update
    void Start()
    {
        sflag = false;
        s_Image = GetComponent<Image>();
        tvImage.enabled = false;

        remoteText1.SetActive(true);
        remoteText2.SetActive(false);
        tvText.SetActive(false);

        //controller2.SetActive(false);


    }

    void ChangeScreens()
    {
        //if (Input.GetKeyDown(KeyCode.A)OVRInput.GetDown(OVRInput.RawButton.A))

        if (tourPIM.tFlg)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.B) || tvImage.enabled == false)
            {
                tvImage.enabled = true;
            }
            else if(OVRInput.GetDown(OVRInput.RawButton.B) || tvImage.enabled == true)
            {
                tvImage.enabled = false;
            }

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                id = id < screens.Length - 1 ? id + 1 : 0;
                s_Image.sprite = screens[id];
            }
        }

        if (!tourPIM.tFlgAct)
        {
            tvImage.enabled = false;
        }

        if (tourPIM.tFlg)
        {
            tvText.SetActive(true);
        }

        if (!tourPIM.tFlg)
        {
            tvText.SetActive(false);

        }
    }

    void RemoteController()
    {
        
        if (tourPIM.trFlg)
        {
            remoteText1.SetActive(false);
            remoteText2.SetActive(true);
        }

        if (!tourPIM.trFlg)
        {
            remoteText1.SetActive(true);
            remoteText2.SetActive(false);
        }

        if (tourPIM.trFlgAct)
        {
            controller1.SetActive(false);
            controller2.SetActive(true);

            controller1S.SetActive(true);
            controller2S.SetActive(false);
        }

        if (!tourPIM.trFlgAct)
        {
            controller1.SetActive(true);
            controller2.SetActive(false);

            controller1S.SetActive(false);
            controller2S.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScreens();
        RemoteController();
    }
}
