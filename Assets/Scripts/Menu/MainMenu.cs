using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame () 
    {
        //All the scenes have to be loaded to the build settings 
        SceneManager.LoadScene("MapGenTest");
        FindObjectOfType<AudioManager>().Stop("menu");
        FindObjectOfType<AudioManager>().Play("Theme");
    }
 
    public void QuitGame ()
    {
        Debug.Log ("quit");
        Application.Quit();
    }

    public void EasterEgg()
    {
        FindObjectOfType<AudioManager>().Stop("menu");
        FindObjectOfType<AudioManager>().Play("easter");
        SceneManager.LoadScene("EasterEgg");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().Stop("easter");
        FindObjectOfType<AudioManager>().Play("menu");
    }
}