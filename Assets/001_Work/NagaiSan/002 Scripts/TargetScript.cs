using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    //Å¶UI
    public bool cleanFlg = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Capacity")
        {
            //Å¶UI
            cleanFlg = true;
            gameObject.SetActive(false);
        }
    }
}
