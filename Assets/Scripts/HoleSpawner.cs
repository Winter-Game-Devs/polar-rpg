using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab to spawn in the Inspector
    public Transform[] spawnPositions; // Array of possible spawn positions
    private int lastIndex = -1;

    public void SpawnObjectAtRandomPosition()
    {
        if (objectToSpawn == null || spawnPositions.Length == 0)
        {
            Debug.LogError("ObjectToSpawn or SpawnPositions is not properly assigned!");
            return;
        }

        // Select a random index from the spawnPositions array
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnPositions.Length);
        } while (randomIndex == lastIndex);

        // Update the last index
        lastIndex = randomIndex;

        // Get the corresponding transform position
        Transform selectedPosition = spawnPositions[randomIndex];

        // Instantiate the object at the selected position
        GameObject newHole = Instantiate(objectToSpawn, selectedPosition.position, Quaternion.identity);
        newHole.GetComponentInChildren<HoleDetector>().hole = false;
        newHole.GetComponentInChildren<HoleDetector>().holeAnim.SetBool("buildHole", false);
        FindObjectOfType<Player>().fishing = false;
        FindObjectOfType<Player>().isFishing = false;
        FindObjectOfType<Player>().isDiggingHole = false;
        FindObjectOfType<Player>().canMove = true;
        FindObjectOfType<Player>().makingHole = false;
        FindObjectOfType<Player>().animator.SetBool("isFishing", false);
        FindObjectOfType<Player>().animator.SetBool("isDigging", false);
    }
}
