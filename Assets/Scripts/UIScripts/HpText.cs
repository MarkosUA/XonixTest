using UnityEngine;
using TMPro;

public class HpText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private Data _data;

    private void Update()
    {
        _text.text = "HP - " + _data.Hp.ToString();
    }
}
