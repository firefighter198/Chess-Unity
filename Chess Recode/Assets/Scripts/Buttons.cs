using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button buttonRestart1, buttonRestart2;
    
    private bool restart1 = false, restart2 = false;
    
    public void ButtonRestart1()
    {
        restart1 = !restart1;
        if (restart1)
        {
            buttonRestart1.GetComponent<Image>().color = new Color(.1f, .5f, .1f, .2f);
        }
        else
        {
            buttonRestart1.GetComponent<Image>().color = new Color(.1f, .1f, .1f, .2f);
        }
        RestartRequest();
    }

    public void ButtonRestart2()
    {
        restart2 = !restart2;
        if (restart2)
        {
            buttonRestart2.GetComponent<Image>().color = new Color(.1f, .5f, .1f, .2f);
        }
        else
        {
            buttonRestart2.GetComponent<Image>().color = new Color(.1f, .1f, .1f, .2f);
        }
        RestartRequest();
    }

    private void RestartRequest()
    {
        if (restart1 && restart2)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
