using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    private Material _default;

    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _default = _spriteRenderer.material;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        _spriteRenderer.material = _material;
        DOTween.To(() => _spriteRenderer.material.GetFloat("_Intensity"), x => _spriteRenderer.material.SetFloat("_Intensity", x), 0.0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _spriteRenderer.material = _default;
    }

}
