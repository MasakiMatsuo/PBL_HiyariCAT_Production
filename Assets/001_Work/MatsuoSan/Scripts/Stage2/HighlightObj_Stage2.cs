using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObj_Stage2 : MonoBehaviour
{
    public Animator animator;
    public PlayerInputManager_Stage2 inputManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hFlg2_1 = inputManager.hitFlg2_1;
        bool hFlg2_2 = inputManager.hitFlg2_2;
        bool hFlg2_3 = inputManager.hitFlg2_3;


        //Stage2
        if (hFlg2_1 == true)
        {
            animator.SetBool("HitObjFlg01", true);
        }
        else
        {
            animator.SetBool("HitObjFlg01", false);
        }

        if (hFlg2_2 == true)
        {
            animator.SetBool("HitObjFlg02", true);
        }
        else
        {
            animator.SetBool("HitObjFlg02", false);
        }

        if (hFlg2_3 == true)
        {
            animator.SetBool("HitObjFlg03", true);
        }
        else
        {
            animator.SetBool("HitObjFlg03", false);
        }
    }
}