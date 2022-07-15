using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    GameObject player;
    GameObject model;
    GameObject child;

    public bool up = false;
    public bool down = false;
    public bool firstTime = true;

    public bool hit = false;

    private void Start()
    {
        player = GameObject.Find(this.gameObject.name == "Player1" ? "Player1" : "Player2");       
    }

    private void Update()
    {
        CheckMovements();
        DownLegs();
        UpLegs();
    }


    private void CheckMovements()
    {

        if (hit)
        {
            hit = false;
            if (up == true)
            {               
                model = this.transform.Find("Model").gameObject;
                child = model.transform.Find("Legs").gameObject;
                child = child.transform.Find("Bottom").gameObject;
                StartCoroutine(zeroPosition(model, 0.2f, 0, child));
            }
            else if (down == true)
            {                
                model = this.transform.Find("Model").gameObject;
                child = model.transform.Find("Legs").gameObject;
                StartCoroutine(zeroPosition(model, 0.2f, 0, child));
            }            
        }

        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "UpDownMove" && transform.GetComponent<PlayerLogic>().specialAction)
        {
            
            if (up == true)
            {
                //se premo shift scendo a normale
                model = this.transform.Find("Model").gameObject;
                child = model.transform.Find("Legs").gameObject;
                child = child.transform.Find("Bottom").gameObject;
                StartCoroutine(zeroPosition(model, 0.2f, 0, child));
            }
            else if (down == true)
            {
                //se premo shfit torno altezza normale
                model = this.transform.Find("Model").gameObject;
                child = model.transform.Find("Legs").gameObject;
                StartCoroutine(zeroPosition(model, 0.2f, 0, child));
            }
            else if (down == false && up == false)
            {
                 
                if (firstTime) //se premo shift vado up prima volta
                {
                    model = this.transform.Find("Model").gameObject;
                    child = model.transform.Find("Legs").gameObject;
                    child = child.transform.Find("Bottom").gameObject;
                    StartCoroutine(legsUpDown(model, 0.2f, 0.18f, child, "up"));
                    
                }
                else if (!firstTime) //se premo shift vado down seconda volta
                {
                    model = this.transform.Find("Model").gameObject;
                    child = model.transform.Find("Legs").gameObject;
                    StartCoroutine(legsUpDown(model, 0.2f, -0.2f, child, "down"));
                }             
            }

        }  

        if(up && down) //can't be up and down same time
        {
            up = false;
            down = false;
        }

        if ((up == false && down == false) || (transform.GetComponent<RobotPowers>()._components.legs > 4)) //ok, quando posso muovermi.
        {
            transform.GetComponent<PlayerLogic>().canMove = true;
        }
        else
        {
            transform.GetComponent<PlayerLogic>().canMove = false;
        }

    }
    private void DownLegs()
    {
        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "Down" && transform.GetComponent<RobotPowers>()._components.legs > 2
            && down == false && transform.GetComponent<PlayerLogic>().specialAction)
        {        
           
            model = this.transform.Find("Model").gameObject;
            child = model.transform.Find("Legs").gameObject;
            StartCoroutine(legsUpDown(model, 0.2f, -0.2f, child, "down"));      
                    
        }
        else if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "Down" && down == true 
            && transform.GetComponent<PlayerLogic>().specialAction)
        {
            
            model = this.transform.Find("Model").gameObject;
            child = model.transform.Find("Legs").gameObject;
            StartCoroutine(zeroPosition(model, 0.2f, 0, child));
        }
        else if (down == true && transform.GetComponent<RobotPowers>()._components.legs < 3)
        {
            model = this.transform.Find("Model").gameObject;
            child = model.transform.Find("Legs").gameObject;
            StartCoroutine(zeroPosition(model, 0.2f, 0, child));           
        }
    }

    private void UpLegs()
    {
        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "Up" && transform.GetComponent<RobotPowers>()._components.legs > 0
            && up == false && transform.GetComponent<PlayerLogic>().specialAction)
        {         
            model = this.transform.Find("Model").gameObject;
            child = model.transform.Find("Legs").gameObject;
            child = child.transform.Find("Bottom").gameObject;
            StartCoroutine(legsUpDown(model, 0.2f, 0.18f, child, "up"));          
            
        }
        else if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "Up" && up == true
            && transform.GetComponent<PlayerLogic>().specialAction)
        {
            
            model = this.transform.Find("Model").gameObject;
            child = model.transform.Find("Legs").gameObject;
            child = child.transform.Find("Bottom").gameObject;
            StartCoroutine(zeroPosition(model, 0.2f, 0, child));                
            
        }
        else if (up == true && transform.GetComponent<RobotPowers>()._components.legs < 1)
        {
            model = this.transform.Find("Model").gameObject;
            child = model.transform.Find("Legs").gameObject;
            child = child.transform.Find("Bottom").gameObject;
            StartCoroutine(zeroPosition(model, 0.2f, 0, child));
            
        }
    }
    
    IEnumerator legsUpDown(GameObject animatedObj, float duration, float valueUD, GameObject child, string updown)
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / duration;
        Vector3 startPos = animatedObj.transform.position;
        Vector3 endPos1 = animatedObj.transform.position + new Vector3(0f, valueUD, 0f);
        Vector3 legsPos = child.transform.position;


        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            animatedObj.transform.position = Vector3.Lerp(startPos, endPos1, ratio);
            child.transform.position = legsPos;         
            yield return null;
        }

        if (updown == "up")
        {
            up = true;
        }else if (updown == "down")
        {
            down = true;
        }

        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "UpDownMove"  && updown == "up")
        {
            firstTime = false;
        }
        else if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "UpDownMove" && updown == "down")
        {
            firstTime = true;
        }
    }

    IEnumerator zeroPosition(GameObject animatedObj, float duration, float Ypos, GameObject child)
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / duration;
        Vector3 startPos = animatedObj.transform.position;
        Vector3 endPos1 = animatedObj.transform.position;
        Vector3 legsPos = child.transform.position; 

        endPos1.y = Ypos;

        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            animatedObj.transform.position = Vector3.Lerp(startPos, endPos1, ratio);
            child.transform.position = new Vector3(child.transform.position.x, legsPos.y, child.transform.position.z);
            yield return null;
        }
      
        up = false;
        down = false;

        transform.GetComponent<PlayerLogic>().canMove = true;

    }

}
