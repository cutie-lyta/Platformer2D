using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The pool of all IPoolable objects
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<GameObject, List<IPoolable>> AllThePools;

    // Local Use
    private GameObject _newObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        AllThePools = new Dictionary<GameObject, List<IPoolable>>();
    }

    /// <summary>
    /// Get an Available GameObject depending on the given prefab. Creates a new list for the prefab if there isn't one already
    /// </summary>
    /// <param name="prefab">The given prefab</param>
    /// <returns></returns>
    public GameObject Get(GameObject prefab)
    {
        // If the Diectionnary doesn't have a list for this prefab, create one
        if (!AllThePools.ContainsKey(prefab))
        {
            AllThePools.Add(prefab, new List<IPoolable>());
        }

        return GetAvailable(prefab);
    }

    /// <summary>
    /// Get an available prefab from the prefab list. Instanciates a new prefabs if there aren't any
    /// </summary>
    /// <param name="prefab">The given prefab</param>
    /// <returns>An available GameObject</returns>
    private GameObject GetAvailable(GameObject prefab)
    {
        for (int i = 0; i < AllThePools[prefab].Count; i++)
        {
            // If the IPoolable is not used
            if (!AllThePools[prefab][i].InUse)
            {
                return AllThePools[prefab][i].GetGameObject();
            }
        }

        return CreateNewObject(prefab);
    }

    /// <summary>
    /// Instanciates a new Gameobject based on the given prefab, then add it to the correct list
    /// </summary>
    /// <param name="prefab">The given prefab</param>
    /// <returns>The GameObject</returns>
    private GameObject CreateNewObject(GameObject prefab)
    {
        _newObject = Instantiate(prefab, transform.position, Quaternion.identity);

        AllThePools[prefab].Add(_newObject.GetComponent<IPoolable>());

        return _newObject;
    }
}
