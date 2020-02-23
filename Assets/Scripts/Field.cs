
public class Field
{
    public int[,] Grid { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public Field(int width, int height)
    {
        Width = width;
        Height = height;

        Grid = new int[width, height];
    }
}
