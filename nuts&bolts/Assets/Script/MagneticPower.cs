using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticPower : MonoBehaviour
{
    PlayerLogic player;
    RobotPowers powers;

    float time = 0f;

    void Start()
    {
        player = transform.GetComponent<PlayerLogic>();
        powers = transform.gameObject.GetComponent<RobotPowers>();
    }

    void Update()
    {
        if (player.specialAction && powers.selectedPower == RobotPowers.PowerSelector.Magnetic)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.GetComponent<MagneticMove>() != null)
                {
                    Transform box = hit.collider.transform;
                    Transform boxMP = box.GetComponent<MagneticMove>().movePoint;

                    if (Vector3.Distance(boxMP.position, box.position) == 0f
                        && Vector3.Distance(transform.position, boxMP.position) > 1.5f
                        && Vector3.Distance(player.movePoint.position, transform.position) == 0f)
                    {
                        if (transform.rotation.eulerAngles.y == 0f)
                        {
                            boxMP.position += new Vector3(0, 0, -1);
                        }
                        else if (transform.rotation.eulerAngles.y == 90f)
                        {
                            boxMP.position += new Vector3(-1, 0, 0);
                        }
                        else if (transform.rotation.eulerAngles.y == 180f)
                        {
                            boxMP.position += new Vector3(0, 0, 1);
                        }
                        else if (transform.rotation.eulerAngles.y == 270f)
                        {
                            boxMP.position += new Vector3(1, 0, 0);
                        }

                        if (time <= 0)
                        {
                            time = player.GetComponent<PlayerLogic>().clipMagnetic.length;
                            player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<PlayerLogic>().clipMagnetic);
                        }
                        time -= Time.deltaTime;


                    }
                }
            }
        }
        else
        {
            time = 0f;
        }
    }
}
