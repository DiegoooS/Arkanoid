using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Arkanoid
{
    public class UIGamePanel : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreAmountText;
        [SerializeField] TMP_Text levelText;
        [SerializeField] TMP_Text currentTimeText;
        [SerializeField] TMP_Text scoreBonusTimerText;
        [SerializeField] TMP_Text lengthBonusTimerText;
        [SerializeField] TMP_Text speedBonusTimerText;
        [SerializeField] TMP_Text tipText;
        [SerializeField] GameObject scoreBonusPanel;
        [SerializeField] GameObject lengthBonusPanel;
        [SerializeField] GameObject speedBonusPanel;

        public void UpdateScoreUI(int amount)
        {
            scoreAmountText.text = amount.ToString();
        }

        public void SetLevelAmountText(int amount)
        {
            levelText.text = $"LEVEL {amount}";
        }

        public void PlayLevelamountTextAnimation()
        {
            levelText.GetComponent<Animator>().Play("ShowAndHideText", 0, 0f);
        }

        public void SetTimeText(string time)
        {
            currentTimeText.text = time;
        }

        public void SetBonusTimerText(Bonuses bonusType, string time)
        {
            if (bonusType == Bonuses.DoubleSpeed) speedBonusTimerText.text = time;
            if (bonusType == Bonuses.DoubleLength) lengthBonusTimerText.text = time;
            if (bonusType == Bonuses.DoubleScore) scoreBonusTimerText.text = time;
        }

        public void SetBonusPanelVisibility(Bonuses bonusType, bool isActive)
        {
            if (bonusType == Bonuses.DoubleSpeed) speedBonusPanel.SetActive(isActive);
            if (bonusType == Bonuses.DoubleLength) lengthBonusPanel.SetActive(isActive);
            if (bonusType == Bonuses.DoubleScore) scoreBonusPanel.SetActive(isActive);
        }

        public void SetTipTextVisibility(bool isVisible)
        {
            tipText.gameObject.SetActive(isVisible);
        }
    }
}

