using Arkanoid;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform playerSpawn;
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; } = false;

    private Player spawnedPlayer;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.MainMenuUI();
    }

    public void NewGame()
    {
        SetIsGameOver(false);
        ScoreManager.Instance.ResetScore();
        UIManager.Instance.StartGameUI();
        TimeManager.Instance.SetCountTime();
        LevelManager.Instance.NextLevel(); 
        SpawnPlayer();
        BonusManager.Instance.GetPlayerOnTheScene();
        AudioManager.Instance.PlayGameSoundtrack();
        Cursor.visible = false;
        UIManager.Instance.SetTipTextVisibility(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        UIManager.Instance.MainMenuUI();
    }

    public void EndGame(bool isWin)
    {
        if (IsGameOver) return;

        UIManager.Instance.SetScoreText(ScoreManager.Instance.GetScore());
        UIManager.Instance.SetTimeText(TimeManager.Instance.GetCurrentTimeString());
        DespawnPlayer();
        SetIsGameOver(true);
        BlockManager.Instance.DespawnBlocks();
        UIManager.Instance.EndGameUI(isWin);
        LevelManager.Instance.ResetLevelProparties();
        UIManager.Instance.SetLevelAmountText(0);
        TimeManager.Instance.SetCountTime();
        BonusManager.Instance.StopAllBonuses();
        AudioManager.Instance.StopGameSoundtrack();
        Cursor.visible = true;
    }

    private void SetIsGameOver (bool isGameOver)
    {
        IsGameOver = isGameOver;
    }

    private void SpawnPlayer()
    {
        spawnedPlayer = Instantiate(this.player);
        spawnedPlayer.transform.position = playerSpawn.position;
    }

    private void DespawnPlayer()
    {
        Destroy(spawnedPlayer.gameObject);

        Ball ball = FindAnyObjectByType<Ball>();

        if (ball == null) return;

        Destroy(ball.gameObject);
    }

    private void Update()
    {
        TimeManager.Instance.SetTimer();
    }
}
