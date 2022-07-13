using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool leftTrigger, rightTrigger;

    [SerializeField] ActionBasedController leftController, rightController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftTrigger = leftController.activateAction.action.ReadValue<bool>();
        rightTrigger = rightController.activateAction.action.ReadValue<bool>();

        if (leftTrigger && rightTrigger)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
