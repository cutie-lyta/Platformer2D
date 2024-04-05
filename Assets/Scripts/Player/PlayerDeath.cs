using DG.Tweening;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _particule;
    [SerializeField]
    private Image _redScreen;
    private SpriteRenderer _spriteRenderer;

    private bool _invincible;

    private void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.SendMessage("Kill");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_invincible)
        {
            PlayerMain.Instance.Input.SendMessage("StopInput");
            Time.timeScale = 0;
            _redScreen.DOFade(10.0f, 0.5f).SetUpdate(true);
            _redScreen.DOFade(0.0f, 0.5f).SetUpdate(true).onComplete = () =>
            {
                var go = Instantiate(_particule);
                go.transform.position = this.transform.position;
                _spriteRenderer.DOFade(0.0f, 0.1f).SetUpdate(true);
                Time.timeScale = 0.45f;
                Destroy(PlayerMain.Instance);
                StartCoroutine(WaitForLoad());
            };
        }
    }

    public IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1;
    }

    public void Invincibility()
    {
        _invincible = true;
    }

    public void UnInvincibility()
    {
        _invincible = false;
    }
}
