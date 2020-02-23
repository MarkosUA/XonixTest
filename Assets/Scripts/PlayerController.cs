using System.Collections.Generic;
using UnityEngine;


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

                    var zone1 = new List<Position>();
                    var zone2 = new List<Position>();
                    var zone3 = new List<Position>();
                    var zone4 = new List<Position>();

                    CountingZones(field, new Position(_position.X + 1, _position.Y), zone1);
                    CountingZones(field, new Position(_position.X - 1, _position.Y), zone2);
                    CountingZones(field, new Position(_position.X, _position.Y + 1), zone3);
                    CountingZones(field, new Position(_position.X, _position.Y - 1), zone4);

                    _position = nextPosition;

                    var zone = ZoneSelection(zone1, zone2, zone3, zone4);
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

    private void CountingZones(Field field, Position position, List<Position> list) //TODO: Fix alloc.
    {
        if (field.Grid[position.X, position.Y] != Elements.GROUND && field.Grid[position.X, position.Y] != Elements.GROUNDENEMY)
            return;
        if (list.Contains(position))
            return;

        list.Add(position);

        CountingZones(field, new Position(position.X + 1, position.Y), list);
        CountingZones(field, new Position(position.X - 1, position.Y), list);
        CountingZones(field, new Position(position.X, position.Y + 1), list);
        CountingZones(field, new Position(position.X, position.Y - 1), list);
    }

    private List<Position> ZoneSelection(List<Position> zone1, List<Position> zone2, List<Position> zone3, List<Position> zone4)
    {
        if (zone1.Count > 0 && zone1.Count <= zone2.Count || zone1.Count > 0 && zone1.Count <= zone3.Count || zone1.Count > 0 && zone1.Count <= zone4.Count)
            return zone1;
        else
        {
            if (zone2.Count > 0 && zone2.Count <= zone1.Count || zone2.Count > 0 && zone2.Count <= zone3.Count || zone2.Count > 0 && zone2.Count <= zone4.Count)
                return zone2;
            else
            {
                if (zone3.Count > 0 && zone3.Count <= zone1.Count || zone3.Count > 0 && zone3.Count <= zone2.Count || zone3.Count > 0 && zone3.Count <= zone4.Count)
                    return zone3;
                else
                {
                    if (zone4.Count > 0 && zone4.Count <= zone1.Count || zone4.Count > 0 && zone4.Count <= zone2.Count || zone4.Count > 0 && zone4.Count <= zone3.Count)
                        return zone4;
                    else
                    {
                        return null;
                    }
                }
            }
        }
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

