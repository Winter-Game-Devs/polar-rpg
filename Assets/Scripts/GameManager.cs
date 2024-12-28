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

    void Update()
    {
        
    }

    void OpenRandomHoles(int count)
    {
        for (int i = 0; i < count; i++) {
            int randomIndex = Random.Range(0, holes.Count);

            if (holes[randomIndex].isOpen)
            {
                // Try again
                i--;
            }
            else
            {
                holes[randomIndex].OpenHole();
            }
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
