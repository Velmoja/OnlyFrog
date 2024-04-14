using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class UIManager : MonoBehaviour
{
    public void Exit() 
    {
        Application.Quit();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Main");
    }


}
