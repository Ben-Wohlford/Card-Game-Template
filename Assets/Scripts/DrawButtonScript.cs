using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawButtonScript : MonoBehaviour
{
    public bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress(){;
        isClicked = true;
        Debug.Log("Draw Card");
    }
}