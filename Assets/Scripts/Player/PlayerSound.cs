using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    AudioClip teleport;

    [SerializeField]
    AudioClip fall;

    [SerializeField]
    AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        PlayerMain.Instance.Teleport.Teleport += () =>
        {
            source.clip = teleport;
            source.loop = false;
            source.Play();

        };

        PlayerMain.Instance.Slam.Slamming += () =>
        {
            source.clip = explosion;
            source.loop = false;
            source.Play();
        };

        PlayerMain.Instance.Input.Slam += (ctx) =>
        {
            if (ctx.performed)
            {
                source.clip = fall;
                source.loop = false;
                source.Play();
            }
        };
    }
}
