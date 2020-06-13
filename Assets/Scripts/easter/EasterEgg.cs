using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    public void Regresa()
    {
        //All the scenes have to be loaded to the build settings 
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().Stop("easter");
        FindObjectOfType<AudioManager>().Play("menu");
    }
}
