using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScript : MonoBehaviour
{
    public bool cleanFlg = false;

    private void Update()
    {
        /*if (Input.GetKey(KeyCode.A))
        {
            cleanFlg = true;
            Debug.LogWarning(cleanFlg);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Capacity")
        {
            gameObject.SetActive(false);
            cleanFlg = true;
        }
    }
}
