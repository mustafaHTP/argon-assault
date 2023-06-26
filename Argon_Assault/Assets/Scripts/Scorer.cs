using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    TextMeshProUGUI _scoreBoard;
    private int _score;

    private void Start()
    {
        _score = 0;
        _scoreBoard = GetComponent<TextMeshProUGUI>();
        if (_scoreBoard is null) Debug.LogWarning("Not found");
        _scoreBoard.text = _score.ToString();
    }

    public void IncreaseScore(int _scoreAmount)
    {
        _score += _scoreAmount;
        _scoreBoard.text = _score.ToString();
        Debug.Log($"Score: {_score}");
    }
}
