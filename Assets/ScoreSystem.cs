using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour{
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private float scoreMultiplier = 5;

    public float _score;
    private bool _shouldCount = true;

    private void Update(){
        if (!_shouldCount) return;
        _score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(_score).ToString();
    }

    public int EndTimer(){
        _shouldCount = false;
        scoreText.text = string.Empty;
        return Mathf.FloorToInt(_score);
    }

    public void StartTimer(){
        _shouldCount = true;
    }
}