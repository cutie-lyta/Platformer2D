using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    [SerializeField]
    private VisualEffect _effectSpawn;
    private Material _default;

    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        var rate = 4.0f;
        _effectSpawn.playRate = rate;
    }
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _default = _spriteRenderer.material;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        _effectSpawn.Play();
        _spriteRenderer.material = _material;
        DOTween.To(() => _spriteRenderer.material.GetFloat("_Intensity"), x => _spriteRenderer.material.SetFloat("_Intensity", x), 0.0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _effectSpawn.Stop();
        _spriteRenderer.material = _default;
    }

}
