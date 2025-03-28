using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Arkanoid
{
    public class HeavyBlock : Block
    {
        [field:SerializeField] public override int MaxHealth { get; set; } = 3;
        public override int CurrentHealth { get; set; }

        private string[] bonuses = {Bonuses.DoubleScore.ToString(), Bonuses.DoubleLength.ToString(), Bonuses.DoubleSpeed.ToString()};

        public override void TakeDamage()
        {
            base.TakeDamage();

            if (CurrentHealth > 0) return;

            ActivateBonus();
            SetCurrentHealth();
        }

        private void ActivateBonus()
        {
            string randomBonus = bonuses[UnityEngine.Random.Range(0, bonuses.Length)];

            switch (randomBonus)
            {
                case "DoubleScore":
                    BonusManager.Instance.SetDoubleScore();
                    break;
                case "DoubleLength":
                    BonusManager.Instance.SetDoubleLength();
                    break;
                case "DoubleSpeed":
                    BonusManager.Instance.SetDoubleSpeed();
                    break;
            }
        }
    }
}
