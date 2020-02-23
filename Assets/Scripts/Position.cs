
public struct Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        var otherPosition = (Position)obj;

        if (otherPosition.X != X) return false;
        if (otherPosition.Y != Y) return false;

        return true;
    }

    public override int GetHashCode()
    {
        return X + Y;
    }
}
