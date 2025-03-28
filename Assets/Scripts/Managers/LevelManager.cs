using System;
using UnityEngine;

namespace Arkanoid
{
    public class LevelManager : MonoBehaviour
    {
        private int currentLevel = 0;
        [SerializeField] private int blocksRowsToSpawn = 1;
        [SerializeField] private int blocksInRowsToSpawn = 5;
        [SerializeField] private int levelToWin = 5;
        [SerializeField] private int spawnRowOnRound = 3;
        [SerializeField] private int spawnExtraBlocksInRowOnRound = 5;


        public static LevelManager Instance { get; private set; }

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

        public void ResetLevelProparties()
        {
            currentLevel = 0;
            blocksRowsToSpawn = 1;
            blocksInRowsToSpawn = 5;
        }

        public void NextLevel()
        {
            SetCurrentLevel();

            if(CheckIfWinLevel()) GameManager.Instance.EndGame(true);

            if (GameManager.Instance.IsGameOver) return;

            SetLevelProparties();

            UIManager.Instance.SetLevelAmountText(currentLevel);
            UIManager.Instance.PlayLevelamountTextAnimation();

            BlockManager.Instance.GenerateLevel(blocksRowsToSpawn, blocksInRowsToSpawn);
        }

        private void SetLevelProparties()
        {
            if (currentLevel % spawnRowOnRound == 0)
            {
                blocksRowsToSpawn++;
            }
            if (currentLevel % spawnExtraBlocksInRowOnRound == 0)
            {
                blocksInRowsToSpawn += 2;
            }
        }

        private void SetCurrentLevel()
        {
            currentLevel++;
        }

        private bool CheckIfWinLevel()
        {
            return currentLevel >= levelToWin;
        }
    }
}

