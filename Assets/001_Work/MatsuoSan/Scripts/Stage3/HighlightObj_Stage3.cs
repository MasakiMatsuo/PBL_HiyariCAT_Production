using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObj_Stage3 : MonoBehaviour
{
    public Animator animator;
    public PlayerInputManager_Stage3 inputManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hFlg_Item1 = inputManager.hitFlg_item1;
        bool hFlg_Item2 = inputManager.hitFlg_item2;
        bool hFlg_Item3 = inputManager.hitFlg_item3;


        //Stage2
        if (hFlg_Item1 == true)
        {
            animator.SetBool("ChocolateFlag", true);
        }
        else
        {
            animator.SetBool("ChocolateFlag", false);
        }

        if (hFlg_Item2 == true)
        {
            animator.SetBool("CodeFlag", true);
        }
        else
        {
            animator.SetBool("CodeFlag", false);
        }

        if (hFlg_Item3 == true)
        {
            animator.SetBool("SCFlag", true);
        }
        else
        {
            animator.SetBool("SCFlag", false);
        }
    }
}
