using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Arkanoid
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UIGameEndPanel gameEndPanel;
        [SerializeField] UIMainMenuPanel mainMenuPanel;
        [SerializeField] UIGamePanel gamePanel;

        private GameObject activePanel;

        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            setInstance();
        }

        private void setInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            Instance = this;
        }

        public void MainMenuUI()
        {
            SetCurrentUI(mainMenuPanel.gameObject, activePanel);
        }

        public void StartGameUI()
        {
            SetCurrentUI(gamePanel.gameObject, activePanel);
        }

        public void EndGameUI(bool isWin)
        { 
            if (isWin)
                GameWinUI();
            else
                GameOverUI();    
        }

        private void GameOverUI()
        {
            gameEndPanel.SetPanelToLost();
            SetCurrentUI(gameEndPanel.gameObject, activePanel);
        }

        private void GameWinUI()
        {
            gameEndPanel.SetPanelToWin();
            SetCurrentUI(gameEndPanel.gameObject, activePanel);
        }

        public void SetCurrentUI(GameObject openPanel, GameObject closePanel)
        {
            openPanel.SetActive(true);
            if (closePanel != null) closePanel.SetActive(false);

            activePanel = openPanel;
        }

        public void UpdateScoreUI(int amount)
        {
            gamePanel.UpdateScoreUI(amount);
        }

        public void SetLevelAmountText(int amount)
        {
            gamePanel.SetLevelAmountText(amount);
        }

        public void PlayLevelamountTextAnimation()
        {
            gamePanel.PlayLevelamountTextAnimation();
        }

        public void SetCurrentTimeText(string time)
        {
            gamePanel.SetTimeText(time);
        }

        public void SetTimeText(string time)
        {
            gameEndPanel.SetTimeText(time);
        }

        public void SetScoreText(int score)
        {
            gameEndPanel.SetScoreText(score);
        }

        public void SetBonusTimerUI(Bonuses bonusType, string time)
        {
            gamePanel.SetBonusTimerText(bonusType, time);
        }

        public void SetBonusPanelVisibilityUI(Bonuses bonusType, bool isActive)
        {
            gamePanel.SetBonusPanelVisibility(bonusType, isActive);
        }

        public void SetTipTextVisibility(bool isVisible)
        {
            gamePanel.SetTipTextVisibility(isVisible);
        }
    }
}

