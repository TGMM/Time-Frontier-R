using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Regresa();
        }
    }

    public void Regresa()
    {
        //All the scenes have to be loaded to the build settings 
        SceneManager.LoadScene("MainMenu");

        var audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null) return;

        audioManager.Stop("easter");
        audioManager.Play("menu");
    }
}
