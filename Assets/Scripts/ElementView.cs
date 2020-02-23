using UnityEngine;

public class ElementView : MonoBehaviour
{
    [SerializeField]
    private Sprite _groundSprite;
    [SerializeField]
    private Sprite _groundEnemySprite;
    [SerializeField]
    private Sprite _waterEnemySprite;
    [SerializeField]
    private Sprite _playerSprite;
    [SerializeField]
    private Sprite _waterSprite;
    [SerializeField]
    private Sprite _playerTrackSprite;

    [SerializeField]
    private SpriteRenderer _sr;

    public void UpdateElement(int elementID)
    {
        switch (elementID)
        {
            case Elements.GROUNDENEMY:
                _sr.sprite = _groundEnemySprite;
                break;
            case Elements.WATERENEMY:
                _sr.sprite = _waterEnemySprite;
                break;
            case Elements.PLAYER:
                _sr.sprite = _playerSprite;
                break;
            case Elements.GROUND:
                _sr.sprite = _groundSprite;
                break;
            case Elements.WATER:
                _sr.sprite = _waterSprite;
                break;
            case Elements.TRACK:
                _sr.sprite = _playerTrackSprite;
                break;
        }
    }
}
