using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickup : MonoBehaviour
{
    [SerializeField] int value;

    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddFlame(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
