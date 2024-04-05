using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _particule;
    [SerializeField]
    private EnemyBehaviour _enemyBehaviour;

    public void Kill()
    {
        Time.timeScale = 0.1f;
        _enemyBehaviour.enabled = false;
        var go = Instantiate(_particule);
        go.transform.position = this.transform.position;
        Destroy(this.gameObject);
        Time.timeScale = 1;
    }
}
