using Arkanoid;
using UnityEngine;

namespace Arkanoid
{
    public class DeathWall : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Ball>() != null)
            {
                GameManager.Instance.EndGame(false);
            }
        }
    }
}

