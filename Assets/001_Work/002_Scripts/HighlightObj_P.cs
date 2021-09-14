using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighlightObj_P : MonoBehaviour
{
    #region Require Values
    public Animator animator;

    #region Other Scripts
    public PlayerInputManager_P playerInputManager_P;
    #endregion // Other Scripts

    #region Flags
    public bool hitObj01 = default;
    public bool hitObj02 = default;
    public bool hitObj03 = default;
    #endregion // Flags
    #endregion // Require Values

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Appropriate objects are assigned by playerInputManager.
        hitObj01 = playerInputManager_P.hitFlg_item1;
        hitObj02 = playerInputManager_P.hitFlg_item2;
        hitObj03 = playerInputManager_P.hitFlg_item3;

        if (hitObj01 == true)
        {
            animator.SetBool("HitObjFlg01", true);
        }
        else
        {
            animator.SetBool("HitObjFlg01", false);
        }

        if (hitObj02 == true)
        {
            animator.SetBool("HitObjFlg02", true);
        }
        else
        {
            animator.SetBool("HitObjFlg02", false);
        }

        if (hitObj03 == true)
        {
            animator.SetBool("HitObjFlg03", true);
        }
        else
        {
            animator.SetBool("HitObjFlg03", false);
        }
    }

}