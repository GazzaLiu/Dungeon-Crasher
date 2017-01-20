using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : Entity {

    public GUIStyle btnStyle_Play;
    public GUIStyle btnStyle_Options;
    public GUIStyle btnStyle_Quit;
    public GUIStyle btnStyle_Main_Menu;

    private float btnWidth_1 = 165 * 2.6f;
    private float btnHeight_1 = 50 * 2.6f;
    private float btnWidth_2 = 160 * 2.6f;
    private float btnHeight_2 = 38 * 2.6f;

    void OnGUI () {
        if (SceneManager.GetActiveScene().name == "Menu") {
            if (GUI.Button(new Rect(Screen.width / 2 - btnWidth_1 / 2, 360, btnWidth_1, btnHeight_1), "", btnStyle_Play)) {
                SceneManager.LoadScene("Stage1");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - btnWidth_1 / 2, 490, btnWidth_1, btnHeight_1), "", btnStyle_Options)) {
                SceneManager.LoadScene("Options");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - btnWidth_1 / 2, 610, btnWidth_1, btnHeight_1), "", btnStyle_Quit)) {
                print("Quit");
                Application.Quit();
            }
        }
        if (SceneManager.GetActiveScene().name == "Options") {
            if (GUI.Button(new Rect(Screen.width / 2 - btnWidth_2 / 2, 570, btnWidth_2, btnHeight_2), "", btnStyle_Main_Menu)) {
                SceneManager.LoadScene("Menu");
            }
        }
        if (SceneManager.GetActiveScene().name == "PlayerWin") {
            if (GUI.Button(new Rect(Screen.width / 2 - btnWidth_2 / 2, 570, btnWidth_2, btnHeight_2), "", btnStyle_Main_Menu)) {
                SceneManager.LoadScene("Menu");
            }
        }
        if (SceneManager.GetActiveScene().name == "EnemyWin") {
            if (GUI.Button(new Rect(Screen.width / 2 - btnWidth_2 / 2, 570, btnWidth_2, btnHeight_2), "", btnStyle_Main_Menu)) {
                SceneManager.LoadScene("Menu");
            }
        }
    }

}
