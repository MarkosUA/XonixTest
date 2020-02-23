using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartBtn()
    {
        SceneController.LoadGameScene();
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
