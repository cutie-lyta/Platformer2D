using DG.Tweening;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _particule;
    [SerializeField]
    private EnemyBehaviour _enemyBehaviour;

    public void Kill()
    {
        Time.timeScale = 0.2f;
        var go = Instantiate(_particule);
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<EnemyBehaviour>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        _enemyBehaviour.enabled = false;
        go.transform.position = this.transform.position;
        DOTween.To(()=> Time.timeScale, x => Time.timeScale = x, 1, 0.2f).onComplete += ()=>
            {
                Destroy(this.gameObject);
            };
    }
}
