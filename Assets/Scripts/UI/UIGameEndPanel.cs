using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Arkanoid
{
    public class UIGameEndPanel : MonoBehaviour
    {
        [SerializeField] Button gameEndTryAgainButton;
        [SerializeField] Button gameEndExitGameButton;
        [SerializeField] Button gameEndExitToMainMenuButton;
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text timeText;

        private void Start()
        {
            SetButtons();
        }

        private void SetButtons()
        {
            gameEndTryAgainButton.onClick.AddListener(() => GameManager.Instance.NewGame());
            gameEndExitGameButton.onClick.AddListener(() => GameManager.Instance.ExitGame());
            gameEndExitToMainMenuButton.onClick.AddListener(() => GameManager.Instance.ExitToMainMenu());
        }

        public void SetPanelToWin()
        {
            titleText.text = "YOU WON!";
        }

        public void SetPanelToLost()
        {
            titleText.text = "GAME OVER";
        }

        public void SetTimeText(string time)
        {
            timeText.text = $"YOUR TIME: {time}";
        }

        public void SetScoreText(int score)
        {
            scoreText.text = $"YOUR SCORE: {score.ToString()}";
        }
    }
}


