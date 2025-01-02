using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igloo : MonoBehaviour
{
    public int inventory;

    public int Inventory {
        get {return inventory;}
        private set {inventory=value;}
    }

    public void SetInventory(int value) {
        inventory = value;
    }

    public bool isBeingRobbed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player found the house");
            inventory += other.GetComponent<Player>().FishDelivered();

            if (inventory >= 100) EndGame();
        }

        if (other.CompareTag("Villain"))
        {
            isBeingRobbed = true;

            if (inventory >= 100) EndGame();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {isBeingRobbed = false;}

    private void EndGame()
    {
    }
}
