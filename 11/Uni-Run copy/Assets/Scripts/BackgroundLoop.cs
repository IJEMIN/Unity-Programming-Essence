using UnityEngine;
using System.Collections;

public class BackgroundLoop : MonoBehaviour 
{
    
    private BoxCollider2D groundCollider;       //This stores a reference to the collider attached to the Ground.
    private float width;       //A float to store the x-axis length of the collider2D attached to the Ground GameObject.

    //Awake is called before Start.
    private void Awake ()
    {

    }

    //Update runs once per frame
    private void Update()
    {

    }

    //Moves the object this script is attached to right in order to create our looping background effect.
    private void Reposition()
    {

    }
}