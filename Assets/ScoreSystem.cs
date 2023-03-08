using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreSystem : MonoBehaviour{
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private float scoreMultiplier = 5;

    [FormerlySerializedAs("_score")]
    public float score;
    private bool shouldCount = true;

    private void Update(){
        if (!shouldCount) return;
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int EndTimer(){
        shouldCount = false;
        scoreText.text = string.Empty;
        return Mathf.FloorToInt(score);
    }

    public void StartTimer(){
        shouldCount = true;
    }
}