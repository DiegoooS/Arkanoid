using UnityEngine;

namespace Arkanoid
{
    public class NormalBlock : Block
    {
        [field: SerializeField] public override int MaxHealth { get; set; } = 1;
        public override int CurrentHealth { get; set; }

        public override void TakeDamage()
        {
            base.TakeDamage();

            if (CurrentHealth > 0) return;

            SetCurrentHealth();
        }
    }
}
