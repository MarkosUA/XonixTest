using UnityEngine.SceneManagement;

public static class SceneController
{
    private static string _gameSceneIndex = "GameScene";

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }
}
