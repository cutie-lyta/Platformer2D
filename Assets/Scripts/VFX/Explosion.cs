using UnityEngine;
using UnityEngine.VFX;

public class Explosion : MonoBehaviour, IPoolable
{
    public bool InUse { get; private set; }

    private VisualEffect _explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        _explosionEffect = GetComponent<VisualEffect>();
    }

    public void Use(Vector3 position, Quaternion rotation)
    {
        InUse = false;
        _explosionEffect.Play();
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
