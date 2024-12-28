using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlideArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Object entered: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the area");
            other.GetComponent<Player>().slide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Object exited: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the area");
            other.GetComponent<Player>().slide = false;
        }
    }

    

}
