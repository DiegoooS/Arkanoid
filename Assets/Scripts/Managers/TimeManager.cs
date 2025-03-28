using UnityEngine;


namespace Arkanoid
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        private float timeSinceLevelStart;
        private bool countTime = false;
        private string currentTimeString;

        private float scoreBonusTimer;
        private float speedBonusTimer;
        private float lengthBonusTimer;
        private float scoreBonusStartTime;
        private float speedBonusStartTime;
        private float lengthBonusStartTime;

        private int seconds;
        private int minutes = 0;

        private void Awake()
        {
            SetInstance();
        }

        private void Update()
        {
            SetBonusTime(scoreBonusStartTime, Bonuses.DoubleScore, ref scoreBonusTimer);
            SetBonusTime(speedBonusStartTime, Bonuses.DoubleSpeed, ref speedBonusTimer);
            SetBonusTime(lengthBonusStartTime, Bonuses.DoubleLength, ref lengthBonusTimer);
        }

        private void SetInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            Instance = this;
        }

        public void SetCountTime()
        {
            countTime = !countTime;
            ResetTimer();
        }

        public void SetTimer()
        {
            if (!countTime) return;

            timeSinceLevelStart += Time.deltaTime;
            seconds = Mathf.RoundToInt(timeSinceLevelStart) % 60;
            minutes = Mathf.RoundToInt(timeSinceLevelStart) / 60;
            currentTimeString = $"{minutes.ToString("D2")}:{seconds.ToString("D2")}";
            UIManager.Instance.SetCurrentTimeText(currentTimeString);
        }

        private void ResetTimer()
        {
            timeSinceLevelStart = 0;
            minutes = 0;
        }

        public string GetCurrentTimeString()
        {
            return currentTimeString;
        }

        public void SetBonusTimer(Bonuses bonusType)
        {
            if (bonusType == Bonuses.DoubleSpeed) speedBonusStartTime = Time.time;
            if (bonusType == Bonuses.DoubleLength) lengthBonusStartTime = Time.time;
            if (bonusType == Bonuses.DoubleScore) scoreBonusStartTime = Time.time;
        }

        private void SetBonusTime(float bonusStartTime, Bonuses bonus, ref float bonusTimer)
        {
            if (Time.time - bonusStartTime < BonusManager.Instance.BonusTime)
            {
                bonusTimer = BonusManager.Instance.BonusTime - (Time.time - bonusStartTime);
                UIManager.Instance.SetBonusTimerUI(bonus, Mathf.FloorToInt(bonusTimer).ToString("D2"));
            }
        }
    }
}

