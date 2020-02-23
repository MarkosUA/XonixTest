using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
public class Data : ScriptableObject
{
    public int Hp;
    public int Lvl;
    public int CountOfTheGroundEnemies;
    public int CountOfTheWaterEnemies;
    public int WidthOfTheWater;
    public int WidthOfTheField;
    public int HeightOfTheField;
}
