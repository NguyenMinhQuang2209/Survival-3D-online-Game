using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private List<MapFoundation> foundations = new();
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject foundation;
    [SerializeField] private Texture2D noiseMap;
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int mapHeight = 100;
    [SerializeField] private int offsetX = 0;
    [SerializeField] private int offsetY = 0;
    [SerializeField] private int depth = 20;
    [SerializeField] private Vector3 foundationScale = new();
    [SerializeField] private Vector3 foundationSize = new();
    private Dictionary<string, float> mapHeightStore = new();
    private void Start()
    {
        ReadNoiseMap();
    }
    public void ReadNoiseMap()
    {
        int width = noiseMap.width;
        int height = noiseMap.height;

        width = width > mapWidth ? mapWidth : width;
        height = height > mapHeight ? mapHeight : height;

        string key = width + "-" + height;

        Color[] pixels = noiseMap.GetPixels();

        for (int i = offsetX; i < width + offsetX; i++)
        {
            for (int j = offsetY; j < height + offsetY; j++)
            {
                int index = i * width + j;

                float grayScaleValue = pixels[index].grayscale;
                int foundationValue = (int)Mathf.Ceil((float)grayScaleValue * depth) + 1;
                for (int z = 0; z < foundationValue; z++)
                {

                    Vector3 pos = new(foundationSize.x * (i - offsetX), foundationSize.y * z, foundationSize.z * (j - offsetY));
                    SpawnFoundation(pos);
                }
            }
        }
        mapHeightStore[key] = height;
    }
    public void SpawnFoundation(Vector3 pos)
    {
        GameObject foundationItem = Instantiate(foundation, pos, Quaternion.identity, spawnParent);
        foundationItem.transform.localScale = foundationScale;
    }
}
[System.Serializable]
public class MapFoundation
{
    public FoundationName name;
    public GameObject foundation;
}