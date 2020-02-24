using UnityEngine;
using UnityEngine.UI;

public class SliderProgress : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public void SliderChange(float presentValue, float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = presentValue;
    }
}
