using System;
using UnityEngine;

namespace Arkanoid
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Ball ball;

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();

                ball.transform.SetParent(null);
                ballRb.simulated = true;
                ballRb.linearVelocity = new Vector2(ballRb.linearVelocity.x, Vector2.up.y * ball.Speed);
                UIManager.Instance.SetTipTextVisibility(false);
                Destroy(this);
            }
        }
    }
}

