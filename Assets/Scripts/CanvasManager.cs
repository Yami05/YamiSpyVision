using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    //[SerializeField] private GameObject shootingPanel;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject NextLevel;

    private GameEvents gameEvents;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.instance;
        gameEvents = GameEvents.instance;

        gameEvents.Score += () => score.text = "Score:" + scoreManager.score.ToString();
        gameEvents.NextLevel += () => NextLevel.SetActive(true);
    }
    public void NextLevelInıt() => SceneManager.LoadScene("SampleScene");
}
