using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void onPlayClick()
    {
        PlayerPrefs.SetInt("usedContinue", 0);
        SceneManager.LoadScene("LevelDesign");
    }

    public void onStoryClick()
    {
        SceneManager.LoadScene("Story");
    }

    public void onQuitClick()
    {
        Application.Quit(0);
    }

    public void onReturnClick()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void onContinueClick()
    {
        PlayerPrefs.SetInt("usedContinue", 1);
        SceneManager.LoadScene("LevelDesign");
    }
}
