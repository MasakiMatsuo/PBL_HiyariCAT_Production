using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript_Stage3 : MonoBehaviour
{
    //��UI
    public bool cleanFlg = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError($"Masaki Test : Object name is {other.name} !");
        if (other.tag == "Capacity")
        {
            //��UI
            cleanFlg = true;
            gameObject.SetActive(false);
        }
    }
}

