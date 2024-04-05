using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerSquach : MonoBehaviour
{
    private bool isSlamming;
    private Vector3 _scale;
    private int _framecounter;

    private void Awake()
    {
        PlayerMain.Instance.Input.Slam += OnSlam;
        PlayerMain.Instance.Teleport.Teleport += OnPlayerFaitBouinpe;
        _scale = transform.localScale;
    }

    void OnSlam(InputAction.CallbackContext ctx)
    {
        isSlamming = true;
        _framecounter = 0;
    }

    // Start is called before the first frame update
    void SlamSquach()
    {
        var position = transform.position;

        PlayerMain.Instance.Input.SendMessage("StopInput");

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(_scale.x * 1.5f, _scale.y * 0.5f, 1), 0.1f));
        seq.Insert(0, transform.DOMove(new Vector3(position.x, position.y - _scale.y * 0.25f, position.z), 0.1f));
        seq.Append(transform.DOScale(_scale, 0.13f));
        seq.Insert(0.1f, transform.DOMove(position, 0.13f))
            .OnComplete(() => { PlayerMain.Instance.Input.SendMessage("StartInput"); transform.localScale = _scale; })
            .OnKill(() => { transform.localScale = _scale; });
        seq.onUpdate += () => { transform.rotation = Quaternion.Euler(0, 0, 0); };

    }

    /// <summary>
    /// Fait le bouinpe
    /// </summary>
    void OnPlayerFaitBouinpe()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(_scale, 0.30f).SetEase(Ease.InOutBack);
    }

    private void FixedUpdate()
    {
        if (PlayerMain.Instance.Movement.IsGrounded && isSlamming && _framecounter > 4)
        {
            isSlamming = false;
            SlamSquach();
        }
        _framecounter++;
    }
}
