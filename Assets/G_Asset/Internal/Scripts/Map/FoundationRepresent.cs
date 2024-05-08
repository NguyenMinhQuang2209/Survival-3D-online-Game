using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationRepresent : MonoBehaviour
{
    public static FoundationRepresent instance;
    [SerializeField] private GameObject foundation;
    [SerializeField] private List<MapFoundationItem> foundations = new();
    private Dictionary<Color, GameObject> foundationStore = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < foundations.Count; i++)
        {
            MapFoundationItem item = foundations[i];
            foundationStore[item.color] = item.foundation;
        }
    }
    public GameObject GetFoundation(Color color)
    {
        if (foundationStore.ContainsKey(color))
        {
            return foundationStore[color];
        }
        for (int i = 0; i < foundations.Count; i++)
        {
            MapFoundationItem item = foundations[i];
            foundationStore[item.color] = item.foundation;
            if (item.color == color)
            {
                return item.foundation;
            }
        }
        return foundation;
    }
}
[System.Serializable]
public class MapFoundationItem
{
    public Color color;
    public GameObject foundation;
}