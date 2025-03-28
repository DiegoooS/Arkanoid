using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Arkanoid
{
    public class BonusManager : MonoBehaviour
    {
        public static BonusManager Instance {  get; private set; }

        [field:SerializeField] public float BonusTime { get; private set; } = 15f;
        public int CurrentScoreBonusScaler { get; private set; }
        public int CurrentLengthBonusScaler { get; private set; }
        public int CurrentSpeedBonusScaler { get; private set; }
        [field:SerializeField] public int ScoreBonusScaler { get; private set; } = 2;
        [field: SerializeField] public int LengthBonusScaler { get; private set; } = 2;
        [field: SerializeField] public int SpeedBonusScaler { get; private set; } = 2;
        public int DefaultBonusScaler { get; private set; } = 1;
        public Coroutine ResetDoubleScoreCoroutine { get; private set; }
        public Coroutine ResetDoubleLengthCoroutine { get; private set; }
        public Coroutine ResetDoubleSpeedCoroutine { get; private set; }

        private Player player;

        private void Awake()
        {
            SetInstance();
        }

        private void Start()
        {
            GetPlayerOnTheScene();
            SetBonusScaler(Bonuses.DoubleLength, DefaultBonusScaler);
            SetBonusScaler(Bonuses.DoubleSpeed, DefaultBonusScaler);
            SetBonusScaler(Bonuses.DoubleScore, DefaultBonusScaler);
        }

        public void GetPlayerOnTheScene()
        {
            player = FindAnyObjectByType<Player>();
        }

        private void SetInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            Instance = this;
        }

        public void SetDoubleScore()
        {
            SetBonusScaler(Bonuses.DoubleScore, ScoreBonusScaler);
            UIManager.Instance.SetBonusPanelVisibilityUI(Bonuses.DoubleScore, true);

            if (ResetDoubleScoreCoroutine != null) StopCoroutine(ResetDoubleScoreCoroutine);

            SetBonusResetCoroutine(Bonuses.DoubleScore);
        }

        public void SetDoubleLength()
        {
            SetBonusScaler(Bonuses.DoubleLength, LengthBonusScaler);
            UIManager.Instance.SetBonusPanelVisibilityUI(Bonuses.DoubleLength, true);

            if (ResetDoubleLengthCoroutine != null)
            {
                StopCoroutine(ResetDoubleLengthCoroutine);
                player.ResetPlayerLength();
            } 

            player.SetPlayerLength();

            SetBonusResetCoroutine(Bonuses.DoubleLength);
        }

        public void SetDoubleSpeed()
        {
            SetBonusScaler(Bonuses.DoubleSpeed, SpeedBonusScaler);
            UIManager.Instance.SetBonusPanelVisibilityUI(Bonuses.DoubleSpeed, true);

            if (ResetDoubleSpeedCoroutine != null) StopCoroutine(ResetDoubleSpeedCoroutine);

            SetBonusResetCoroutine(Bonuses.DoubleSpeed);
        }

        public void StopAllBonuses()
        {
            SetBonusScaler(Bonuses.DoubleScore, DefaultBonusScaler);
            SetBonusScaler(Bonuses.DoubleLength, DefaultBonusScaler);
            SetBonusScaler(Bonuses.DoubleSpeed, DefaultBonusScaler);

            if (ResetDoubleLengthCoroutine != null) StopCoroutine(ResetDoubleLengthCoroutine);
            if (ResetDoubleScoreCoroutine != null) StopCoroutine(ResetDoubleScoreCoroutine);
            if (ResetDoubleSpeedCoroutine != null) StopCoroutine(ResetDoubleSpeedCoroutine);

            UIManager.Instance.SetBonusPanelVisibilityUI(Bonuses.DoubleScore, false);
            UIManager.Instance.SetBonusPanelVisibilityUI(Bonuses.DoubleLength, false);
            UIManager.Instance.SetBonusPanelVisibilityUI(Bonuses.DoubleSpeed, false);

            player.ResetPlayerLength();
        }

        public void SetBonusScaler(Bonuses bonusType, int scale)
        {
            if (bonusType == Bonuses.DoubleSpeed) CurrentSpeedBonusScaler = scale;
            if (bonusType == Bonuses.DoubleLength) CurrentLengthBonusScaler = scale;
            if (bonusType == Bonuses.DoubleScore) CurrentScoreBonusScaler = scale;
        }

        public void SetBonusResetCoroutine(Bonuses bonusType)
        {
            if (bonusType == Bonuses.DoubleSpeed) ResetDoubleSpeedCoroutine = StartCoroutine(ResetBonusTimer(Bonuses.DoubleSpeed));
            if (bonusType == Bonuses.DoubleLength) ResetDoubleLengthCoroutine = StartCoroutine(ResetBonusTimer(Bonuses.DoubleLength));
            if (bonusType == Bonuses.DoubleScore) ResetDoubleScoreCoroutine = StartCoroutine(ResetBonusTimer(Bonuses.DoubleScore));
        }

        private IEnumerator ResetBonusTimer(Bonuses bonusType)
        {
            TimeManager.Instance.SetBonusTimer(bonusType);

            yield return new WaitForSeconds(BonusTime);

            if (bonusType == Bonuses.DoubleLength) player.ResetPlayerLength();
            SetBonusScaler(bonusType, DefaultBonusScaler);
            UIManager.Instance.SetBonusPanelVisibilityUI(bonusType, false);
        }
    }
}

