using UnityEngine;

public class FieldView : MonoBehaviour
{
    private FieldController _fieldController;
    private Field _field;

    private float _timer = 6f;

    private ElementView[,] _elementViews;
    [SerializeField]
    private ElementView _prefab;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private Data _data;

    private void Awake()
    {
        _field = new Field(width: _data.WidthOfTheField, height: _data.HeightOfTheField);
        _elementViews = new ElementView[_field.Width, _field.Height];
        _fieldController = new FieldController(_field, _playerView, _data);

        FieldCreation();
    }

    private void Update()
    {
        if( _timer <= 0)
        {
            _fieldController.UpdateField();
            UpdateFieldView();
            _timer = 6f;
        }
        else
        {
            _timer--;
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
                }
            }
        }
    }
}
