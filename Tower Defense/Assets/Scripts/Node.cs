using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private Color startColor;
    private Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
        Debug.Log("we are seeing the mouse");

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
