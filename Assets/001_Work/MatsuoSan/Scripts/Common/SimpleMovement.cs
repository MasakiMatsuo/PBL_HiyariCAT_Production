using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Vector3 scale, initialPos;
    [SerializeField]
    private Vector3 velocityScals, sizeScale;
    [SerializeField]
    private float velocityMove = 1f, sizeMove = 10f;

    void Start()
    {
        //scale = new Vector3(2f, 0.5f, 0.3f);
        //scale = Vector3.one;
        scale = transform.localScale;

        initialPos = transform.position;
    }

    void Update()
    {
        Scale();
        Move();
    }

    void Scale()
    {
        scale.x += Mathf.Sin(Time.time * velocityScals.x) * Time.deltaTime * sizeScale.x;
        scale.y += Mathf.Sin(Time.time * velocityScals.y) * Time.deltaTime * sizeScale.y;
        scale.z += Mathf.Sin(Time.time * velocityScals.z) * Time.deltaTime * sizeScale.z;
        transform.localScale = scale;
    }

    void Move()
    {
        transform.Translate(Vector3.forward * Mathf.Sin(Time.time * velocityMove) * Time.deltaTime * sizeMove);
        //transform.Translate(Vector3.up * Mathf.Sin(Time.time * velocityMove) * Time.deltaTime * sizeMove);
    }
}
