using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript_P : MonoBehaviour
{
    //UI
    public bool cleanFlg = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Capacity")
        {
            gameObject.SetActive(false);
            cleanFlg = true;
        }
    }
}
