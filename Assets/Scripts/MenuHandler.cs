using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public TMP_InputField playerNameInput;

    public void StartGame()
    {
        if (playerNameInput.text == "")
        {
            Debug.Log("No player name entered");
            return;
        }

        SceneManager.LoadScene("main");
        ScoreManager.Instance.name = playerNameInput.text;
        ScoreManager.Instance.SaveScore(0, playerNameInput.text);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        //Don't forget since this use editor code, need to add "using UnityEditor" at the top and wrap it between #if
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}