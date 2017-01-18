using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : Entity {

    public void StartGame () {
        SceneManager.LoadScene("Stage1");
    }

    public void Help () {
        SceneManager.LoadScene("Help");
    }

    public void Menu () {
        SceneManager.LoadScene("Menu");
    }

    public void Quit () {
        print("Quit");
        Application.Quit();
    }
}
