using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{



     public void RestartToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }




    // Start is called before the first frame update
    void Start()
    {

        Invoke("RestartToMenu", 3);

   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
