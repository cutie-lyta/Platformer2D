using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _confettis;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var go = Instantiate(_confettis);
            go.transform.position = this.transform.position + Vector3.up;
        }
    }
}
