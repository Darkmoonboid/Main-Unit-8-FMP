using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    [SerializeField] private bool finalNode = false;

    private bool connected;
    // variable for if this is the final node - serielised

    // change connected into a private bool
    // add a new public function for Connect()
    public void Connect()
    {
        // if not connected
            // set connected to true
            // change sprite/colour
            // play a sound
            // if end node
                // do end thing
    }
    // if connected is true and final node is true, load next level
}
