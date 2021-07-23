using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickup : MonoBehaviour
{
    [SerializeField] int value;

    public GameObject pickupEffect;
    public AudioClip pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddFlame(value);

            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.5f); 

            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
