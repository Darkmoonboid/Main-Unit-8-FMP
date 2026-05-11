using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Load_levels : MonoBehaviour
{
    [SerializeField] 
    private string requiredTag = "Line";
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(requiredTag))
        {
            Scene_Controller.instance.NextLevel();
        }
    }
}