using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObj_Tour : MonoBehaviour
{    
    public Animator animator;
    public TourPlayerInputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ステージ1
        bool hFlg1_1 = inputManager.hitFlgD2_1;
        bool hFlg1_2 = inputManager.hitFlgD2_2;
        bool hFlg1_3 = inputManager.hitFlgD2_3;


        //Stage1
        if (hFlg1_1 == true)
        {
            animator.SetBool("highlightFlgD1", true);
        }
        else
        {
            animator.SetBool("highlightFlgD1", false);
        }

        if (hFlg1_2 == true)
        {
            animator.SetBool("highlightFlgD2", true);
        }
        else
        {
            animator.SetBool("highlightFlgD2", false);
        }

        if (hFlg1_3 == true)
        {
            animator.SetBool("highlightFlgD3", true);
        }
        else
        {
            animator.SetBool("highlightFlgD3", false);
        }
    }
}