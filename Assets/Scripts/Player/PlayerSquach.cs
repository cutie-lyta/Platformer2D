using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerSquach : MonoBehaviour
{
    private bool isSlamming;

    private void Awake()
    {
        PlayerMain.Instance.Input.Slam += OnSlam;
<<<<<<< Updated upstream
=======
        PlayerMain.Instance.Teleport.Teleport += OnPlayerFaitBouinpe;
        _scale = transform.localScale;
>>>>>>> Stashed changes
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

        PlayerMain.Instance.Input.SendMessage("StopInput");

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(scale.x * 1.5f, scale.y * 0.5f, 1), 0.1f));
        seq.Insert(0, transform.DOMove(new Vector3(position.x, position.y - scale.y * 0.25f, position.z), 0.1f));
        seq.Append(transform.DOScale(scale, 0.13f));
        seq.Insert(0.1f, transform.DOMove(position, 0.13f)).OnComplete(() => PlayerMain.Instance.Input.SendMessage("StartInput"));

    }

<<<<<<< Updated upstream
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSlamming)
=======
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
>>>>>>> Stashed changes
        {
            isSlamming = false;
            SlamSquach();
        }
    }
}
