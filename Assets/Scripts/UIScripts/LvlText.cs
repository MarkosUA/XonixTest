using UnityEngine;
using TMPro;

public class LvlText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private Data _data;

    private void Update()
    {
        _text.text = "Lvl - " + _data.Lvl.ToString();
    }
}
