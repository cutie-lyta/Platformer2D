using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject _exploPrefab;

    void Awake()
    {
        PlayerMain.Instance.Slam.Slamming += MakeExplosion;
    }

    private void MakeExplosion(int exploStrength)
    {
        Explosion explo = ObjectPool.Instance.Get(_exploPrefab).GetComponent<Explosion>();

        explo.Use(transform.position, Quaternion.Euler(0, 0, 0), new Vector3(exploStrength, exploStrength, exploStrength));
    }
}
