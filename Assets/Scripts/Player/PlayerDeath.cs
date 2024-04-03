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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0.0f;
            _redScreen.DOFade(80.0f, 0.5f).SetUpdate(true);
            _redScreen.DOFade(0.0f, 0.5f).SetUpdate(true).onComplete = () =>
            {
                // Yoann met le screen shake stp
                _particule.transform.position = this.transform.position;
                _spriteRenderer.DOFade(0.0f, 0.1f).SetUpdate(true);
                StartCoroutine(WaitForLoad());
                Time.timeScale = 1.0f;
                Instantiate(_particule);
            };
        }
    }

    public IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }
}
