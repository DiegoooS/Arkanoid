using System;
using UnityEngine;

namespace Arkanoid
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 direction;

        [field: SerializeField] public int Speed { get; private set; } = 3;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            SetDirection();
            rb.linearVelocity = direction * (Speed * BonusManager.Instance.CurrentSpeedBonusScaler);
        }

        private void SetDirection()
        {
            direction = new(Input.GetAxisRaw("Horizontal"), rb.linearVelocity.y);
        }

        public Vector2 GetDirection()
        {
            return direction;
        }
    }
}

