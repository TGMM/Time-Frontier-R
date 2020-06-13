using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {

        public void PlayGame()
        {
            //All the scenes have to be loaded to the build settings 
            SceneManager.LoadScene(2);
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        public void EasterEgg()
        {
            SceneManager.LoadScene("EasterEgg");
        }

        public void Back()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}