using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour{
    [SerializeField]
    private GameObject gameOverDisplay;

    [SerializeField]
    private AsteroidSpawner asteroidSpawner;

    [SerializeField]
    private TMP_Text gameOverText;

    [SerializeField]
    private ScoreSystem scoreSystem;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Button continueButton;

    public void EndGame(){
        asteroidSpawner.enabled = false;
        var finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Your Score: {finalScore}";
        gameOverDisplay.gameObject.SetActive(true);
    }

    public void PlayAgain(){
        SceneManager.LoadScene("Scene_Main");
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("Scene_Menu");
    }

    public void ContinueButton(){
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
    }

    public void ContinueGame(){
        scoreSystem.StartTimer();
        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        asteroidSpawner.enabled = true;
        gameOverDisplay.gameObject.SetActive(false);
    }
}