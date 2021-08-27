using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript_Stage3 : MonoBehaviour
{
    //Å¶UI
    public bool cleanFlg = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError($"Masaki Test : Object name is {other.name} !");
        if (other.tag == "Capacity")
        {
            //Å¶UI
            cleanFlg = true;
            gameObject.SetActive(false);
        }
    }
}

