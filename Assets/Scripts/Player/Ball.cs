using UnityEngine;

namespace Arkanoid
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] int playerHitDirectionScaler = 1;
        private int currentPlayerHitDirectionScaler;
        private AudioSource audioSource;
        [field: SerializeField] public int Speed { get; private set; } = 5;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            SetAudioSource();
        }

        private void SetAudioSource()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            currentPlayerHitDirectionScaler = playerHitDirectionScaler;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                rb.linearVelocity += playerMovement.GetDirection() * currentPlayerHitDirectionScaler;

                // Preventing the ball's direction from being straigth up
                if (currentPlayerHitDirectionScaler == playerHitDirectionScaler ) currentPlayerHitDirectionScaler++;
            }

            audioSource.Play();
        }
    }
}

