using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    //[SerializeField] float speed = 20;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }

    /* void Update()
     {
         transform.position = Vector3.Lerp(this.transform.position,target.position + offset, Time.deltaTime * speed);
     }*/
}
