
public class WaterEnemy
{
    private Position _position;
    private Direction _direction;

    public WaterEnemy(Position position, Direction direction)
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

        if (nextPosition.X >= 0 && nextPosition.Y >= 0 && nextPosition.X < field.Width && nextPosition.Y < field.Height)
        {
            if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.WATER || field.Grid[nextPosition.X, nextPosition.Y] == Elements.WATERENEMY)
            {
                field.Grid[_position.X, _position.Y] = Elements.WATER;
                field.Grid[nextPosition.X, nextPosition.Y] = Elements.WATERENEMY;
                _position = nextPosition;
            }
            else
                Rebound(nextPosition, field);
        }
        else
        {
            Rebound(nextPosition, field);
        }
    }

    private void Rebound(Position nextPosition, Field field)
    {
        if (_direction == Direction.TopRight && (nextPosition.Y >= field.Height || field.Grid[_position.X, nextPosition.Y] == Elements.GROUND))
            _direction = Direction.BottomRight;
        else
        {
            if (_direction == Direction.TopRight && (nextPosition.X >= field.Width || field.Grid[nextPosition.X, _position.Y] == Elements.GROUND))
                _direction = Direction.TopLeft;
            else
            {
                if (_direction == Direction.BottomRight && (nextPosition.X >= field.Width || field.Grid[nextPosition.X, _position.Y] == Elements.GROUND))
                    _direction = Direction.BottomLeft;
                else
                {
                    if (_direction == Direction.BottomRight && (nextPosition.Y <= 0 || field.Grid[_position.X, nextPosition.Y] == Elements.GROUND))
                        _direction = Direction.TopRight;
                    else
                    {
                        if (_direction == Direction.BottomLeft && (nextPosition.Y <= 0 || field.Grid[_position.X, nextPosition.Y] == Elements.GROUND))
                            _direction = Direction.TopLeft;
                        else
                        {
                            if (_direction == Direction.BottomLeft && (nextPosition.X <= 0 || field.Grid[nextPosition.X, _position.Y] == Elements.GROUND))
                                _direction = Direction.BottomRight;
                            else
                            {
                                if (_direction == Direction.TopLeft && (nextPosition.X <= 0 || field.Grid[nextPosition.X, _position.Y] == Elements.GROUND))
                                    _direction = Direction.TopRight;
                                else
                                {
                                    if (_direction == Direction.TopLeft && (nextPosition.Y >= field.Height || field.Grid[_position.X, nextPosition.Y] == Elements.GROUND))
                                        _direction = Direction.BottomLeft;
                                    else
                                    {
                                        field.Grid[_position.X, _position.Y] = Elements.WATER;
                                        field.Grid[_position.X + 1, _position.Y] = Elements.WATERENEMY;
                                        _position.X = _position.X + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
