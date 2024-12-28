using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void OpenHole()
    {
        isOpen = true;
    }

    public void CloseHole()
    {
        isOpen = false;
    }
}
