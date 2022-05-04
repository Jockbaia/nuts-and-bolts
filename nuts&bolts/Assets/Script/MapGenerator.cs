using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public TextAsset level;

    public Transform tilePrefab;
    public Transform tallboxPrefab;

    [Range(0, 1)]
    public float outlinePercent;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    void Start()
    {
        
    } 

    private void GameManagerOnGameStateChanged(GameState obj)
    {
        if (obj == GameState.Level0)
        {
            GenerateMap();
        }
    }

    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int z = GameManager.instance.room.Count-1; z >= 0; z--)
        {
            for (int x = 0; x < GameManager.instance.room[0].Count; x++)
            {
                Vector3 tilePosition = new Vector3(x, 0, z);

                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;

                if (GameManager.instance.room[z][x] == '1') // TallBox
                {
                    Vector3 tallboxPosition = new Vector3(x, 1, z);
                    Transform newTallbox = Instantiate(tallboxPrefab, tallboxPosition, Quaternion.Euler(Vector3.zero)) as Transform;
                    newTallbox.parent = mapHolder;
                    // Scripts
                    newTallbox.gameObject.AddComponent<PushBox>();
                    //newTallbox.gameObject.AddComponent<DragBox>();
                    // Move point
                    Transform newTallboxMovePoint = new GameObject("Box Move Point").transform;
                    newTallboxMovePoint.transform.position = newTallbox.transform.position;
                    newTallboxMovePoint.parent = newTallbox;
                }
            }
        }
    }
}
