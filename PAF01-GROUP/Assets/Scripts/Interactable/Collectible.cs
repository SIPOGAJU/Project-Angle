using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;

    private void Update()
    {
        transform.Rotate(new Vector3(45, 45, 45) * Time.deltaTime * rotationSpeed);
    }
    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            gameManager.instance.SetCollectibleAmount();
            FindObjectOfType<AudioManager>().Play("Collectable");
            Destroy(this.gameObject); 
        }
    }

}
