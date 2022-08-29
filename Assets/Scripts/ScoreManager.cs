using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int score;

    public int IncreaseScore() => score++;
}
