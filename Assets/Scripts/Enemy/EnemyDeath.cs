using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _particule;
    [SerializeField]
    private EnemyBehaviour _enemyBehaviour;

    public void Kill()
    {
        _enemyBehaviour.enabled = false;
        _particule.transform.position = this.transform.position;
        Instantiate(_particule);
        Destroy(this.gameObject);
    }
}
