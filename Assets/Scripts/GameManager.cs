using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameStates gameStates;

    private List<GameObject> bullets = new List<GameObject>();

    private void Start()
    {
        gameStates = GameStates.Meg1;
    }


    public void AddBullet(GameObject go) => bullets.Add(go);

    public void RemoveBullet(GameObject go) => bullets.Remove(go);

    public void SetState(GameStates game) => gameStates = game;

    public bool CheckState(GameStates gameState) => gameState == gameStates;

    public int GetBulletsCount() => bullets.Count;

}
