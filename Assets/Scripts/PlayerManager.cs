using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float scaleSpeed = .5f;
    [SerializeField] float minSize = 0.0f;
    [SerializeField] float candleScale = 0f;
    [SerializeField] float moveSpeed = .5f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();

        transform.localScale -= Vector3.up * Time.deltaTime * scaleSpeed;

        if (this.transform.localScale.y <= minSize)
        {
            scaleSpeed = 0;
        }
    }

    void Move()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * moveSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Candle"))
        {
            transform.localScale += Vector3.up * candleScale;
            Destroy(other.gameObject);
        }
    }
}
