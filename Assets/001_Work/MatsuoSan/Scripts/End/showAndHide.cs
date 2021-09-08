using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showAndHide : MonoBehaviour
{
    public GameObject showObject;

    public float time = 3.0f;
    private float count = 0;

    void Update()
    {
        count += Time.deltaTime;
        if(time <= count)
        {
            showObject.SetActive(true);
        }
        
    }
}
