using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObj : MonoBehaviour
{    
    public Animator animator;

    public InputManager inputManager;

    bool hFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hFlg = inputManager.hitFlg;

        if (hFlg == true)
        {
            if (gameObject.layer == 11)
            {
                animator.SetBool("highlightFlg", true);
            }

            if (gameObject.layer == 12)
            {
                animator.SetBool("ScissorsFlg", true);
            }

        }else
        {
            if (gameObject.layer == 11)
            {
                animator.SetBool("highlightFlg", false);
            }

            if (gameObject.layer == 12)
            {
                animator.SetBool("ScissorsFlg", false);
            }
        }
    }
}
