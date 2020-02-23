
public class GroundEnemy
{
    private Position _position;
    private Direction _direction;

    public GroundEnemy(Position position, Direction direction)
    {
        _position = position;
        _direction = direction;
    }

    public void Move(Field field)
    {
        var nextPosition = _position;
        switch (_direction)
        {
            case Direction.TopRight:
                nextPosition.X++;
                nextPosition.Y++;
                break;
            case Direction.TopLeft:
                nextPosition.X--;
                nextPosition.Y++;
                break;
            case Direction.BottomRight:
                nextPosition.X++;
                nextPosition.Y--;
                break;
            case Direction.BottomLeft:
                nextPosition.X--;
                nextPosition.Y--;
                break;
        }

        if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.GROUND || field.Grid[nextPosition.X, nextPosition.Y] == Elements.GROUNDENEMY)
        {
            field.Grid[_position.X, _position.Y] = Elements.GROUND;
            field.Grid[nextPosition.X, nextPosition.Y] = Elements.GROUNDENEMY;
            _position = nextPosition;
        }
        else
        {
            Rebound(nextPosition, field);
        }
    }

    private void Rebound(Position nextPosition, Field field)
    {
        if (_direction == Direction.TopRight && field.Grid[_position.X, nextPosition.Y] == Elements.WATER)
            _direction = Direction.BottomRight;
        else
        {
            if (_direction == Direction.TopRight && field.Grid[nextPosition.X, _position.Y] == Elements.WATER)
                _direction = Direction.TopLeft;
            else
            {
                if (_direction == Direction.BottomRight && field.Grid[nextPosition.X, _position.Y] == Elements.WATER)
                    _direction = Direction.BottomLeft;
                else
                {
                    if (_direction == Direction.BottomRight && field.Grid[_position.X, nextPosition.Y] == Elements.WATER)
                        _direction = Direction.TopRight;
                    else
                    {
                        if (_direction == Direction.BottomLeft && field.Grid[_position.X, nextPosition.Y] == Elements.WATER)
                            _direction = Direction.TopLeft;
                        else
                        {
                            if (_direction == Direction.BottomLeft && field.Grid[nextPosition.X, _position.Y] == Elements.WATER)
                                _direction = Direction.BottomRight;
                            else
                            {
                                if (_direction == Direction.TopLeft && field.Grid[nextPosition.X, _position.Y] == Elements.WATER)
                                    _direction = Direction.TopRight;
                                else
                                {
                                    if (_direction == Direction.TopLeft && field.Grid[_position.X, nextPosition.Y] == Elements.WATER)
                                        _direction = Direction.BottomLeft;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


