using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace Arkanoid
{
    public class ScoreManager : MonoBehaviour
    {
        private int score;
        public static ScoreManager Instance { get; private set; }

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

        public void AddScore(int amount)
        {
            score += amount * BonusManager.Instance.CurrentScoreBonusScaler;
            UIManager.Instance.UpdateScoreUI(score);
        }

        public void ResetScore()
        {
            score = 0;
            UIManager.Instance.UpdateScoreUI(score);
        }

        public int GetScore()
        {
            return score;
        }
    }
}

