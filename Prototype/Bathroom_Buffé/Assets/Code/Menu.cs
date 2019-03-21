using UnityEngine;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviour
{ 
    public void StartGameButton()
    {
        SceneManager.LoadScene("SampleScene");

    }
        public void ControlsButton()
    {
        SceneManager.LoadScene("ControlsScene");
       
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
