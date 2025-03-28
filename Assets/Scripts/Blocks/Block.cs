using UnityEngine;

namespace Arkanoid
{
    public abstract class Block : MonoBehaviour
    {
        public abstract int MaxHealth {  get; set; }
        public abstract int CurrentHealth { get; set; }

        private void OnCollisionEnter2D(Collision2D collision) => TakeDamage();

        private void Start()
        {
            SetCurrentHealth();
        }

        public void SetCurrentHealth()
        {
            CurrentHealth = MaxHealth;
        }
     
        public virtual void TakeDamage()
        {
            CurrentHealth--;

            if (CurrentHealth <= 0)
            {
                this.gameObject.SetActive(false);
                BlockManager.Instance.ReduceBlockPool();
                ScoreManager.Instance.AddScore(1);
            }
        }
    }
}
