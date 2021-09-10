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

    private bool sflag = false;

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
            Debug.LogWarning("TV");

            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                if (!sflag)
                {
                    tvImage.enabled = true;
                    sflag = true;
                }
                else
                {
                    tvImage.enabled = false;
                    sflag = false;
                }
                
            }
            if(OVRInput.GetDown(OVRInput.RawButton.B))
            {
                //vImage.enabled = false;
            }

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                id = id < screens.Length - 1 ? id + 1 : 0;
                s_Image.sprite = screens[id];
            }

            tvText.SetActive(true);
        }

        if (!tourPIM.tFlgAct)
        {
            //tvImage.enabled = false;
        }

        if (tourPIM.tFlg)
        {
            //tvText.SetActive(true);
        }

        if (!tourPIM.tFlg)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                tvImage.enabled = false;
            }

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
            
            controller1.SetActive(true);
            controller2.SetActive(false);
        }

        if (!tourPIM.trFlgAct)
        {
            controller1.SetActive(false);
            controller2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScreens();
        RemoteController();
    }
}
