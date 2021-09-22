using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveHeavyObj : MonoBehaviour
{
    public GameObject removeMessage;

    void OnDisable()
    {
        StartCoroutine(DisplayRemoveMessage());
    }

    IEnumerator DisplayRemoveMessage()
    {
        removeMessage.SetActive(true);

        yield return new WaitForSeconds(3.0f);
        removeMessage.SetActive(false);
    }
}
