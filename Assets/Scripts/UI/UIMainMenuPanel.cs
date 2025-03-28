using UnityEngine;
using UnityEngine.UI;


namespace Arkanoid
{
    public class UIMainMenuPanel : MonoBehaviour
    {
        [SerializeField] Button mainMenuStartGameButton;
        [SerializeField] Button mainMenuExitGameButton;

        private void Start()
        {
            SetButtons();
        }

        private void SetButtons()
        {
            mainMenuStartGameButton.onClick.AddListener(() => GameManager.Instance.NewGame());
            mainMenuExitGameButton.onClick.AddListener(() => GameManager.Instance.ExitGame());
        }
    }
}

