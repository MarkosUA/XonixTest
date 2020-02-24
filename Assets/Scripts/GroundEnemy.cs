
public class GroundEnemy
{
    private Direction _direction;
    private PlayerController _playerController;

    public Position Position { get; private set; }

    public GroundEnemy(Position position, Direction direction, PlayerController playerController)
    {
        Position = position;
        _direction = direction;
        _playerController = playerController;
    }

    public void Move(Field field)
    {
        var nextPosition = Position;
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
            field.Grid[Position.X, Position.Y] = Elements.GROUND;
            field.Grid[nextPosition.X, nextPosition.Y] = Elements.GROUNDENEMY;
            Position = nextPosition;
        }
        else
        {
            if (field.Grid[nextPosition.X, nextPosition.Y] == Elements.TRACK)
                _playerController.Damage(field);
            else
                Rebound(nextPosition, field);
        }
    }

    public void Die(Field field)
    {
        field.Grid[Position.X, Position.Y] = Elements.WATER;
    }

    private void Rebound(Position nextPosition, Field field)
    {
        if (_direction == Direction.TopRight && field.Grid[Position.X, nextPosition.Y] == Elements.WATER)
            _direction = Direction.BottomRight;
        else
        {
            if (_direction == Direction.TopRight && field.Grid[nextPosition.X, Position.Y] == Elements.WATER)
                _direction = Direction.TopLeft;
            else
            {
                if (_direction == Direction.BottomRight && field.Grid[nextPosition.X, Position.Y] == Elements.WATER)
                    _direction = Direction.BottomLeft;
                else
                {
                    if (_direction == Direction.BottomRight && field.Grid[Position.X, nextPosition.Y] == Elements.WATER)
                        _direction = Direction.TopRight;
                    else
                    {
                        if (_direction == Direction.BottomLeft && field.Grid[Position.X, nextPosition.Y] == Elements.WATER)
                            _direction = Direction.TopLeft;
                        else
                        {
                            if (_direction == Direction.BottomLeft && field.Grid[nextPosition.X, Position.Y] == Elements.WATER)
                                _direction = Direction.BottomRight;
                            else
                            {
                                if (_direction == Direction.TopLeft && field.Grid[nextPosition.X, Position.Y] == Elements.WATER)
                                    _direction = Direction.TopRight;
                                else
                                {
                                    if (_direction == Direction.TopLeft && field.Grid[Position.X, nextPosition.Y] == Elements.WATER)
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


