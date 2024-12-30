using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igloo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Just running code");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player found the house");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }
}
