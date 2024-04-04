using UnityEngine;
using UnityEngine.VFX;

public class Explosion : MonoBehaviour, IPoolable
{
    public bool InUse { get; private set; }

    private VisualEffect _explosionEffect;

    // Start is called before the first frame update
    void Awake()
    {
        _explosionEffect = GetComponent<VisualEffect>();
    }

    public void Use(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        InUse = false;

        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale/2;

        _explosionEffect.Play();

        Invoke("Unuse", 1f);
    }

    public void Unuse()
    {
        InUse = false;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
