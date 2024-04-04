using UnityEngine;

/// <summary>
/// Implemented by objects that can be used in an Object Pool
/// </summary>
public interface IPoolable
{
    /// <summary>
    /// If the Poolable is used or not
    /// </summary>
    public bool InUse { get; }

    /// <summary>
    /// Uses the Poolable
    /// </summary>
    /// <param name="position"></param>
    public void Use(Vector3 position, Quaternion rotation);

    /// <summary>
    /// Unuses the Poolable
    /// </summary>
    /// <param name="position"></param>
    public void Unuse();

    /// <summary>
    /// Gets the GameObject of the Poolable
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject();
}