using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Hole> holes;
    public int minHoles = 1;
    public int maxHoles = 3;

    void Start()
    {
        holes = new List<Hole>();
        RetrieveMapHoles();
    }

    void OpenRandomHoles(int count)
    {
        // Copy holes list to avoid modifying the original list
        List<Hole> copyHoles = new List<Hole>(this.holes);
        for (int i = 0; i < count; i++) {
            // Get random index
            int randomIndex = Random.Range(0, copyHoles.Count);
            // Open the hole
            copyHoles[randomIndex].OpenHole();
            // Remove the hole from the list
            copyHoles.RemoveAt(randomIndex);
        }
    }

    int GetRandomNumberOfHoles()
    {
        return Random.Range(minHoles, maxHoles + 1);
    }

    // Get list of all the holes on the map
    void RetrieveMapHoles()
    {
        Hole[] holesInScene = FindObjectsOfType<Hole>();
        foreach (Hole hole in holesInScene) {
            holes.Add(hole);
        }
    }
}
