using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltsManager : MonoBehaviour
{
    void Update()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position + new Vector3(0f, 0.5f, 0f), 0.01f);
        if (coll.Length > 1)
        {
            foreach (Collider c in coll)
            {
                if (c.gameObject.tag == "Player")
                {
                    c.gameObject.GetComponent<RobotPowers>()._components.bolts++;
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine(boltAnimation(this.gameObject, 0.5f, false, c.gameObject));
                }
            }
            
        }
    }

    /*void OnTriggerEnter(Collider collider)
    {
        Debug.Log("test");
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<RobotPowers>()._components.bolts++;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(boltAnimation(this.gameObject, 0.5f, false, collider.gameObject));     
        }       
    }*/

    IEnumerator boltAnimation(GameObject animatedObj, float duration, bool secondTime, GameObject player)
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / duration;
        Vector3 startPos = animatedObj.transform.position;     
        Vector3 endPos1 = animatedObj.transform.position + new Vector3(0f, 3f, 0f);       
        Vector3 endPos2 = player.transform.position + new Vector3(0f, 1.5f, 0f); ;

        if (!secondTime)
        {
            while (ratio < 1f)
            {
                elapsedTime += Time.deltaTime;
                ratio = elapsedTime / duration;
                animatedObj.transform.position = Vector3.Lerp(startPos, endPos1, ratio);
                animatedObj.transform.Rotate(0f, 4f, 0f);
                //animatedObj.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                yield return null;
            }
            StartCoroutine(boltAnimation(animatedObj, 0.5f, true, player));
        }
        if (secondTime)
        {
            while (ratio < 1f)
            {
                elapsedTime += Time.deltaTime;
                ratio = elapsedTime / duration;
                animatedObj.transform.position = Vector3.Lerp(startPos, endPos2, ratio);
                animatedObj.transform.Rotate(0f, 4f, 0f);
                //animatedObj.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                yield return null;
            }
            //suono moneta presa o simili
            var clip = player.GetComponent<PlayerLogic>().clipBoltObtained;
            player.GetComponent<PlayerLogic>().audioSrc.PlayOneShot(clip);
            Destroy(animatedObj.gameObject);
        }
    }

}
