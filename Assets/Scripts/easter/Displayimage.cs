using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayimage : MonoBehaviour
{
    [SerializeField] private Image custom;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            custom.enabled = true;
        }
    }

void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            custom.enabled =false;
        }
    }
}
