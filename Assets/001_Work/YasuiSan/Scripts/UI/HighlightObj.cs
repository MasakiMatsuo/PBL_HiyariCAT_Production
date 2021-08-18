using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObj : MonoBehaviour
{    
    public Animator animator;
    public PlayerInputManager_Stage1_3 inputManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hFlg1_1 = inputManager.hitFlg1_1;
        bool hFlg1_2 = inputManager.hitFlg1_2;
        bool hFlg1_3 = inputManager.hitFlg1_3;


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
}