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

        Debug.Log(this.transform.position);
        Debug.Log(_particule.transform.position);
        Destroy(this.gameObject);
    }
}
