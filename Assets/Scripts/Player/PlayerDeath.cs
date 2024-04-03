using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _particule;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.SendMessage("Kill");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0.0f;

            Time.timeScale = 1.0f;
            _particule.transform.position = this.transform.position;
            Instantiate(_particule);
        }
    }
}
