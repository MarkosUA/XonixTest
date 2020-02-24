using UnityEngine;
using TMPro;

public class TimerTxt : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private FieldView _fieldView;

    private void Update()
    {
        _text.text = Mathf.Round(_fieldView.TimerToEnd).ToString();
    }
}
