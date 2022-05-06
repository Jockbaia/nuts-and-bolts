using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public TextAsset levelFile;

    public Transform tilePrefab;
    public Transform tallboxPrefab;

    public List<List<char>> room;

    void Awake()
    {
        ReadLevelFile();
    }

    void Start()
    {
        GenerateMap();
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

    List<List<char>> ReadLevelFile()
    {
        List<List<char>> room = new List<List<char>>();

        string[] rows = levelFile.text.Split('\n');

        foreach (string row in rows)
        {
            string[] cols = row.Split(',');
            List<char> tmp = new List<char>();
            foreach (string c in cols)
            {
                tmp.Add(c.ToCharArray()[0]);
            }
            room.Add(tmp);
        }
        room.Reverse();
        return room;
    }
}
