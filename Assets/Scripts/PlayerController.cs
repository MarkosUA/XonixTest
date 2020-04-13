using System.Collections.Generic;
using System.Linq;

public class PlayerController
{
    private Position _position;
    private Position _returnPosition;
    private int _realElement;
    private PlayerModel _playerModel;
    private Data _data;
    private FieldController _fieldController;

    private List<Position> _trackPositions;

    public PlayerController(Position position, PlayerModel playerModel, Data data, FieldController fieldController)
    {
        _position = position;
        _playerModel = playerModel;
        _trackPositions = new List<Position>();
        _data = data;
        _fieldController = fieldController;
    }

    public void Damage(Field field)
    {
        _data.Hp--;
        field.Grid[_position.X, _position.Y] = Elements.GROUND;
        field.Grid[_returnPosition.X, _returnPosition.Y] = Elements.PLAYER;
        _realElement = Elements.WATER;
        ClearTrack(field, _trackPositions);
        _trackPositions.Clear();
        _position = _returnPosition;
    }

    public void Move(Field field)
    {
        var nextPosition = _position;
        switch (_playerModel.PlayerDirection)
        {
            case Direction.Top:
                nextPosition.Y++;
                break;
            case Direction.Right:
                nextPosition.X++;
                break;
            case Direction.Bottom:
                nextPosition.Y--;
                break;
            case Direction.Left:
                nextPosition.X--;
                break;
            case Direction.NoMove:
                break;
        }

        if (nextPosition.X >= 0 && nextPosition.Y >= 0 && nextPosition.X < field.Width && nextPosition.Y < field.Height)
        {
            if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.WATER && _realElement == Elements.WATER)
            {
                field.Grid[_position.X, _position.Y] = Elements.WATER;
                field.Grid[nextPosition.X, nextPosition.Y] = Elements.PLAYER;
                _realElement = Elements.WATER;
                _position = nextPosition;
            }
            else
            {
                if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.WATER && _realElement == Elements.GROUND)
                {
                    field.Grid[_position.X, _position.Y] = Elements.TRACK;
                    field.Grid[nextPosition.X, nextPosition.Y] = Elements.PLAYER;
                    _realElement = Elements.WATER;
                    _trackPositions.Add(_position);

                    var zone1 = CountingZones(field, new Position(_position.X + 1, _position.Y));
                    var zone2 = CountingZones(field, new Position(_position.X - 1, _position.Y));
                    var zone3 = CountingZones(field, new Position(_position.X, _position.Y - 1));
                    var zone4 = CountingZones(field, new Position(_position.X, _position.Y + 1));

                    _position = nextPosition;

                    var zone = ZoneSelection(new List<List<Position>> { zone1, zone2, zone3, zone4 });
                    PaintingZone(field, zone, _trackPositions);
                    _fieldController.DeletedEnemies(zone);
                    _trackPositions.Clear();
                }
                else
                {
                    if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.GROUND && _realElement == Elements.WATER)
                    {
                        field.Grid[_position.X, _position.Y] = Elements.WATER;
                        field.Grid[nextPosition.X, nextPosition.Y] = Elements.PLAYER;
                        _realElement = Elements.GROUND;
                        _returnPosition = _position;
                        _position = nextPosition;
                    }
                    else
                    {
                        if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.GROUND && _realElement == Elements.GROUND)
                        {
                            field.Grid[_position.X, _position.Y] = Elements.TRACK;
                            field.Grid[nextPosition.X, nextPosition.Y] = Elements.PLAYER;
                            _realElement = Elements.GROUND;
                            _trackPositions.Add(_position);
                            _position = nextPosition;
                        }
                        else
                        {
                            if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.GROUNDENEMY || field.Grid[nextPosition.X, nextPosition.Y] == Elements.WATERENEMY)
                            {
                                _data.Hp--;
                            }
                            else
                            {
                                if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.TRACK)
                                {
                                    _data.Hp--;
                                    field.Grid[_position.X, _position.Y] = Elements.GROUND;
                                    field.Grid[_returnPosition.X, _returnPosition.Y] = Elements.PLAYER;
                                    _realElement = Elements.WATER;
                                    ClearTrack(field, _trackPositions);
                                    _trackPositions.Clear();
                                    _position = _returnPosition;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            _playerModel.PlayerDirection = Direction.NoMove;
        }
    }

    private List<Position> CountingZones(Field field, Position position)
    {
        if (field.Grid[position.X, position.Y] != Elements.GROUND &&
            field.Grid[position.X, position.Y] != Elements.GROUNDENEMY) return null;

        var result = new List<Position>();
        result.Add(position);

        var tempField = new bool[field.Width, field.Height];
        tempField[position.X, position.Y] = true;

        int addedPositionsCount;

        do
        {
            addedPositionsCount = 0;

            for (var x = 0; x < field.Width; x++)
            {
                for (var y = 0; y < field.Height; y++)
                {
                    if (tempField[x, y]) continue;

                    if (field.Grid[x, y] != Elements.GROUND &&
                        field.Grid[x, y] != Elements.GROUNDENEMY) continue;

                    if (x > 0)
                    {
                        if (tempField[x - 1, y])
                        {
                            tempField[x, y] = true;
                            result.Add(new Position(x, y));
                            addedPositionsCount++;
                        }
                    }

                    if (x < field.Width - 1)
                    {
                        if (tempField[x + 1, y])
                        {
                            tempField[x, y] = true;
                            result.Add(new Position(x, y));
                            addedPositionsCount++;
                        }
                    }

                    if (y > 0)
                    {
                        if (tempField[x, y - 1])
                        {
                            tempField[x, y] = true;
                            result.Add(new Position(x, y));
                            addedPositionsCount++;
                        }
                    }

                    if (y < field.Height - 1)
                    {
                        if (tempField[x, y + 1])
                        {
                            tempField[x, y] = true;
                            result.Add(new Position(x, y));
                            addedPositionsCount++;
                        }
                    }
                }
            }
        } while (addedPositionsCount > 0);

        return result;
    }

    private List<Position> ZoneSelection(List<List<Position>> positionLists)
    {
        positionLists.RemoveAll(list => list == null);
        var minLength = positionLists.Min(x => x.Count);

        return positionLists.Find(x => x.Count == minLength);
    }

    private void PaintingZone(Field field, List<Position> zone, List<Position> track)
    {
        for (int i = 0; i < track.Count; i++)
        {
            field.Grid[track[i].X, track[i].Y] = Elements.WATER;
        }

        for (int i = 0; i < zone.Count; i++)
        {
            field.Grid[zone[i].X, zone[i].Y] = Elements.WATER;
        }
    }

    private void ClearTrack(Field field, List<Position> track)
    {
        for (int i = 0; i < track.Count; i++)
        {
            field.Grid[track[i].X, track[i].Y] = Elements.GROUND;
        }
    }
}

