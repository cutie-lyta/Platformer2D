using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerMovement Movement { get; private set; }
    public PlayerInputHandler Input { get; private set; }

    public static PlayerMain Instance;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        Movement = GetComponent<PlayerMovement>();
        Input = GetComponent<PlayerInputHandler>();
    }
}
