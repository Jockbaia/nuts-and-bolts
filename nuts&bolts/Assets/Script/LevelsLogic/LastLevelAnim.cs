using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelAnim : MonoBehaviour
{
    GameObject player1;
    GameObject player2;

    float stepDuration = 0.3f;

    Vector3 p1Pos;
    Vector3 p1Rot = Vector3.zero;

    void Start()
    {
        player1 = GameObject.Find("Player1");
        player1.transform.Find("Player Camera").parent = null;

        p1Rot = player1.transform.rotation.eulerAngles;
        p1Pos = player1.transform.position;

        player2 = GameObject.Find("Player2");
        player2.transform.Find("Player Camera").parent = null;

        StartCoroutine(StartAnimation());
    }

    void Update()
    {
        Vector3.MoveTowards(player1.transform.position, p1Pos, 0.0f);
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(3f);

        player1.transform.Rotate(new Vector3(0, 90, 0));
        yield return new WaitForSeconds(1.5f);

        player1.transform.Rotate(new Vector3(0, -180, 0));
        yield return new WaitForSeconds(1.5f);

        player1.transform.Rotate(new Vector3(0, 90, 0));
        yield return new WaitForSeconds(stepDuration);

        p1Pos += Vector3.forward;
        yield return new WaitForSeconds(stepDuration);

        p1Pos += Vector3.forward;
        yield return new WaitForSeconds(stepDuration);

        player1.transform.Rotate(new Vector3(0, 90, 0));
        yield return new WaitForSeconds(stepDuration);

        for (int i=0;i<10;i++)
        {
            p1Pos += Vector3.forward;
            yield return new WaitForSeconds(stepDuration);
        }
        


        yield return null;
    }


}
