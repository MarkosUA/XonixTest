using UnityEngine;

public class FieldView : MonoBehaviour
{
    private FieldController _fieldController;
    private Field _field;

    private float _timer;
    private int _numberOfTheGround;

    private ElementView[,] _elementViews;
    [SerializeField]
    private ElementView _prefab;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private Data _data;
    [SerializeField]
    private CameraController _cameraController;
    [SerializeField]
    private SliderProgress _sliderProgress;

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _pauseBtn;
    [SerializeField]
    private GameObject _continueBtn;
    [SerializeField]
    private GameObject _nextLvlBtn;
    [SerializeField]
    private GameObject _tryAgainBtn;

    public bool Pause { get; set; }
    public float TimerToEnd { get; private set; }

    private void Awake()
    {
        _data.WidthOfTheField = Screen.width / _data.SizeCoef;
        _data.HeightOfTheField = Screen.height / _data.SizeCoef;

        Coef();

        _field = new Field(width: _data.WidthOfTheField, height: _data.HeightOfTheField);
        _elementViews = new ElementView[_field.Width, _field.Height];
        _fieldController = new FieldController(_field, _playerView, _data);

        _cameraController.CenterCamera(_field);

        _timer = _data.TimerValue;
        TimerToEnd = _data.TimeToEndLevel;

        FieldCreation();
    }

    private void Update()
    {
        if (!Pause)
        {
            if (_data.Hp > 0)
            {
                if (TimerToEnd >= 0)
                {
                    if (_timer <= 0)
                    {
                        if (_fieldController.GroundEnemies.Count > 0)
                        {
                            if (_fieldController.NumberOfTheRepaintedElements <= _numberOfTheGround - _fieldController.NumberOfTheRepaintedElements)
                            {
                                _fieldController.UpdateField();
                                UpdateFieldView();
                                _timer = _data.TimerValue;
                                _sliderProgress.SliderChange(_fieldController.NumberOfTheRepaintedElements, _numberOfTheGround / 2);
                            }
                            else
                                Win();
                        }
                        else
                            Win();
                    }
                    else
                    {
                        _timer -= Time.deltaTime;
                    }
                    TimerToEnd -= Time.deltaTime;
                }
            }
            else
            {
                Lose();
            }
        }
    }

    private void UpdateFieldView()
    {
        for (var x = 0; x < _field.Width; x++)
        {
            for (var y = 0; y < _field.Height; y++)
            {
                _elementViews[x, y].UpdateElement(_field.Grid[x, y]);
            }
        }
    }

    private void FieldCreation()
    {
        for (int x = 0; x < _field.Width; x++)
        {
            for (int y = 0; y < _field.Height; y++)
            {
                if (x < _data.WidthOfTheWater || y < _data.WidthOfTheWater || x >= _field.Width - _data.WidthOfTheWater || y >= _field.Height - _data.WidthOfTheWater)
                {
                    var element = _elementViews[x, y] = Instantiate(_prefab);
                    _field.Grid[x, y] = Elements.WATER;
                    element.transform.position = new Vector3(x, y, z: 0);
                }
                else
                {
                    var element = _elementViews[x, y] = Instantiate(_prefab);
                    _field.Grid[x, y] = Elements.GROUND;
                    element.transform.position = new Vector3(x, y, z: 0);
                    _numberOfTheGround++;
                }
            }
        }
    }

    private void Coef()
    {
        var currentSize = Screen.height / 2 / _data.SizeCoef;
        _cameraController.SizeOfTheCamera(currentSize);
    }

    private void Lose()
    {
        Pause = true;
        _pauseBtn.SetActive(false);
        _panel.SetActive(true);
        _continueBtn.SetActive(false);
        _nextLvlBtn.SetActive(false);
        _tryAgainBtn.SetActive(true);
    }

    private void Win()
    {
        Pause = true;
        _pauseBtn.SetActive(false);
        _panel.SetActive(true);
        _continueBtn.SetActive(false);
        _nextLvlBtn.SetActive(true);
        _tryAgainBtn.SetActive(false);
    }
}
