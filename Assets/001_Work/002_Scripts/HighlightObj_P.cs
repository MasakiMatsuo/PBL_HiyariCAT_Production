using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighlightObj_P : MonoBehaviour
{
    public Animator animator;
    public PlayerInputManager_P playerInputManager_P;

    /*MN_Add_Start*/
    #region Flags
    public bool lightObjFlg01 = default;
    public bool lightObjFlg02 = default;
    public bool heavyObjFlg = default;
    #endregion
    /*MN_Add_End*/

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*MN_Add_Start*/
        // Stage 0
        if (SceneManager.GetActiveScene().name == "002 Stage0")
        {
            HighLightingOnStage0();
        }
        /*MN_Add_End*/

        //ステージ1
        bool hFlg1_1 = playerInputManager_P.hitFlg_item1;
        bool hFlg1_2 = playerInputManager_P.hitFlg_item2;
        bool hFlg1_3 = playerInputManager_P.hitFlg_item3;

        //Stage1
        if (hFlg1_1 == true)
        {
            animator.SetBool("highlightFlg", true);
        }
        else
        {
            animator.SetBool("highlightFlg", false);
        }

        if (hFlg1_2 == true)
        {
            animator.SetBool("ScissorsFlg", true);
        }
        else
        {
            animator.SetBool("ScissorsFlg", false);
        }

        if (hFlg1_3 == true)
        {
            animator.SetBool("LightFlg", true);
        }
        else
        {
            animator.SetBool("LightFlg", false);
        }

        //Stage2


        //Stage3
    }

    /*MN_Add_Start*/
    public void HighLightingOnStage0()
    {
        lightObjFlg01 = playerInputManager_P.lightObj01;
        lightObjFlg02 = playerInputManager_P.lightObj02;
        heavyObjFlg = playerInputManager_P.heavyObj;

        if (lightObjFlg01 == true)
        {
            animator.SetBool("LightObj01", true);
        }
        else if (lightObjFlg02 == true)
        {
            animator.SetBool("LightObj02", true);
        }
        else
        {
            animator.SetBool("LightObj01", false);
            animator.SetBool("LightObj02", false);
        }

        if (heavyObjFlg == true)
        {
            animator.SetBool("HeavyObj01", true);
        }
        else
        {
            animator.SetBool("HeavyObj01", false);
        }
    }
    /*MN_Add_End*/


}