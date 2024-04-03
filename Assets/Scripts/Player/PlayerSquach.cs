using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerSquach : MonoBehaviour
{
    private bool isSlamming;
    private PlayerInputHandler _handler;

    private void Awake()
    {
        _handler = GetComponent<PlayerInputHandler>();
        _handler.Slam += OnSlam;
    }

    void OnSlam(InputAction.CallbackContext ctx)
    {
        isSlamming = true;
    }

    // Start is called before the first frame update
    void SlamSquach()
    {
        var scale = transform.localScale;
        var position = transform.position;

        _handler.SendMessage("StopInput");

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(scale.x * 1.5f, scale.y * 0.5f, 1), 0.1f));
        seq.Insert(0, transform.DOMove(new Vector3(position.x, position.y - scale.y * 0.25f, position.z), 0.1f));
        seq.Append(transform.DOScale(scale, 0.13f));
        seq.Insert(0.1f, transform.DOMove(position, 0.13f)).OnComplete(() => _handler.SendMessage("StartInput"));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSlamming)
        {
            isSlamming = false;
            SlamSquach();
        }
    }
}
