using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    public float multiplier = 1.4f;
    public float duration = 4f;

    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        
        //apply eftect on the targeted player
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.health *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //lasts for a certain amount of seconds
        yield return new WaitForSeconds(duration);

        //reverse the effect on the targeted player
        stats.health /= multiplier;
        
        //destoy the power up object from scene
        Destroy(gameObject);

        Debug.Log("Power up picked up!");
    }
}
