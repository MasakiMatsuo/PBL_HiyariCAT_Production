using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveHeavyObj : MonoBehaviour
{
    public GameObject removeMessage;
    public CleanUpMenu_P cleanUpMenu_P;

    public bool removeHeavyObjFlag01 = false;

    void Start()
    {
        removeHeavyObjFlag01 = false;
    }

    void OnDisable()
    {
        removeHeavyObjFlag01 = true;
    }
}
