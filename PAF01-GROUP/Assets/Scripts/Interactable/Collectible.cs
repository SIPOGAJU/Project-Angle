using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private ParticleSystem collected;

    private void Update()
    {
        transform.Rotate(new Vector3(45, 45, 45) * Time.deltaTime * rotationSpeed);
    }
    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            if(gameManager.instance != null)
                gameManager.instance.SetCollectibleAmount();

            if(FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("Collectable");

            Instantiate(collected, transform.position, transform.rotation);
            Destroy(this.gameObject); 
        }
    }
}
