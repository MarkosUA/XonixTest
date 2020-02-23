using System.Collections.Generic;
using UnityEngine;

public class FieldController
{
    private Field _field;
    private PlayerController _playerController;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private Data _data;

    private List<GroundEnemy> _groundEnemies;
    private List<WaterEnemy> _waterEnemies;

    public FieldController(Field field, PlayerView playerView, Data data)
    {
        _field = field;
        _playerView = playerView;
        _data = data;
        AddPlayer(new Position(x: 1, y: 1));
        AddEnemies(_data.CountOfTheGroundEnemies, _data.CountOfTheWaterEnemies);
    }

    public void UpdateField()
    {
        foreach (var groundEnemy in _groundEnemies)
        {
            groundEnemy.Move(_field);
        }
        foreach (var waterEnemy in _waterEnemies)
        {
            waterEnemy.Move(_field);
        }
        _playerController.Move(_field);
    }

    public void AddEnemies(int numberOfGroundEnemies, int numberOfWaterEnemies)
    {
        _groundEnemies = new List<GroundEnemy>();
        _waterEnemies = new List<WaterEnemy>();

        for (int i = 0; i < numberOfGroundEnemies; i++)
        {
            AddGroundEnemy(new Position(x: Random.Range(_data.WidthOfTheWater, _field.Width - _data.WidthOfTheWater), y: Random.Range(_data.WidthOfTheWater, _field.Height - _data.WidthOfTheWater)));
        }

        for (int i = 0; i < numberOfWaterEnemies; i++)
        {
            AddWaterEnemy(new Position(x: SelectPositionForWaterEnemy(_field.Width), y: SelectPositionForWaterEnemy(_field.Height)));
        }
    }

    private void AddGroundEnemy(Position position)
    {
        _field.Grid[position.X, position.Y] = Elements.GROUNDENEMY;
        var groundEnemy = new GroundEnemy(position, SelectDirection());
        _groundEnemies.Add(groundEnemy);
    }

    private void AddWaterEnemy(Position position)
    {
        _field.Grid[position.X, position.Y] = Elements.WATERENEMY;
        var waterEnemy = new WaterEnemy(position, SelectDirection());
        _waterEnemies.Add(waterEnemy);
    }

    private void AddPlayer(Position position)
    {
        _field.Grid[position.X, position.Y] = Elements.PLAYER;
        _playerModel = new PlayerModel(Direction.NoMove);
        _playerController = new PlayerController(position, _playerModel);
        _playerView.PlayerModel = _playerModel;
    }

    private Direction SelectDirection()
    {
        var rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                return Direction.TopRight;
            case 2:
                return Direction.TopLeft;
            case 3:
                return Direction.BottomRight;
            default:
                return Direction.BottomLeft;
        }
    }

    private int SelectPositionForWaterEnemy(int maxValue)
    {
        var rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                return 0;
            case 2:
                return 1;
            case 3:
                return maxValue - 1;
            case 4:
                return maxValue - 2;
            default:
                return 1;
        }
    }
}
