using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    private PlayerInputHandler _handler;

    private VisualEffect _vfx;

    private bool isSlamming = false;

    void Awake()
    {
        _handler = GetComponent<PlayerInputHandler>();
        _handler.Slam += (ctx) => { isSlamming = true; };

        _vfx = GetComponentInChildren<VisualEffect>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isSlamming)
        {
            _vfx.Play();
            isSlamming = false;
        }
    }
}
