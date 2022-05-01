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

        List<List<char>> room = ReadLevelFile();

        for (int z = room.Count-1; z >= 0; z--)
        {
            for (int x = 0; x < room[0].Count; x++)
            {
                Vector3 tilePosition = new Vector3(x, 0, z);

                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;

                if (room[z][x] == '1')
                {
                    Vector3 tallboxPosition = new Vector3(x, 1, z);
                    Transform newTallbox = Instantiate(tallboxPrefab, tallboxPosition, Quaternion.Euler(Vector3.zero)) as Transform;
                    newTallbox.parent = mapHolder;
                }
            }
        }
    }

    List<List<char>> ReadLevelFile()
    {
        List<List<char>> room = new List<List<char>>();

        string[] rows = level.text.Split('\n');

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
