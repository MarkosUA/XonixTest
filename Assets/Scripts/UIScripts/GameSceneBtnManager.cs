using UnityEngine;

public class GameSceneBtnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _continueBtn;
    [SerializeField]
    private GameObject _nextLvlBtn;
    [SerializeField]
    private GameObject _pauseBtn;
    [SerializeField]
    private GameObject _tryAgainBtn;
    [SerializeField]
    FieldView _fieldView;
    [SerializeField]
    Data _data;

    public void PauseBtn()
    {
        _fieldView.Pause = true;
        _panel.SetActive(true);
        if (!_continueBtn.activeSelf)
            _continueBtn.SetActive(true);
        if (_nextLvlBtn.activeSelf)
            _nextLvlBtn.SetActive(false);
        if (_tryAgainBtn.activeSelf)
            _tryAgainBtn.SetActive(false);
        _pauseBtn.SetActive(false);
    }

    public void ContinueBtn()
    {
        _pauseBtn.SetActive(true);
        _panel.SetActive(false);
        _fieldView.Pause = false;
    }

    public void NextLevel()
    {
        _data.Lvl++;
        _data.CountOfTheWaterEnemies++;
        _data.CountOfTheGroundEnemies += 2;
        _data.Hp = _data.HpValue;
        SceneController.LoadGameScene();
    }

    public void TryAgainBtn()
    {
        _data.Hp = _data.HpValue;
        SceneController.LoadGameScene();
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

}
