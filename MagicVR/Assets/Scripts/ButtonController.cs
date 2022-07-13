using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public void FirstLevel()
    {
        SceneManager.LoadScene("scene 1");
    }

    public void Quit()
    {
        Debug.Log("Game is closing");
        Application.Quit();
    }

    public void PlaySound()
    {

    }

}

