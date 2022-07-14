using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandleNumpadNav : MonoBehaviour
{
    private string[,] padM = { { "1", "2", "3" },
                               { "4", "5", "6" },
                               { "7", "8", "9" },
                          { "delete", "0", "enter" }};

    private string[] DisHigh = { "1", "1" };

    public bool padOpen = false;

    PadManager correct;

    GameObject pad;
    private int i = 0;
    private int j = 0;


    float cooldown = 0f;

    GameObject door;
    private float minDist = 2f;
    private float distance;
    GameObject player;

    public bool destroyNumPad_1 = false;
    public bool destroyNumPad_2 = false;

    bool specialAction = false;

    public void OnSpecialAction(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;
        if (pad.activeSelf) return;
        specialAction = context.action.triggered;
    }

    private void Start()
    {
        pad = GameObject.Find(this.gameObject.name == "Player1" ? "PadCanvas_1" : "PadCanvas_2");
        door = GameObject.Find(this.gameObject.name == "Player1" ? "P1Map/DoorP1" : "P2Map/DoorP2");
        player = GameObject.Find(this.gameObject.name == "Player1" ? "Player1" : "Player2");
        correct = pad.GetComponent<PadManager>();
        pad.SetActive(false);
        correct.OnPassCorrect += Correct_OnPassCorrect;
    }

    private void Update()
    {
        distance = Vector3.Distance(door.transform.position, player.transform.position);

        if (transform.GetComponent<RobotPowers>().selectedPower.ToString() == "DestroyPad" &&
            distance <= minDist &&
            specialAction)
        {
            if (!destroyNumPad_1 && !correct.isOpen1)
            {
                if (this.gameObject.name == "Player1")
                {
                    destroyNumPad_1 = true;

                    StartCoroutine(HackingSound());

                    // Damage
                    GetComponent<RobotPowers>()._components.Rarm -= 3;

                    // Camera Shake
                    ManageCoop.player1.camera.GetComponent<CameraFollow>().enabled = false;
                    StartCoroutine(TargetFollower.Shake(ManageCoop.player1.camera, 0.15f, 0.4f));

                    // Damage sound
                    player.GetComponent<PlayerLogic>().audioSrc.PlayOneShot(player.GetComponent<PlayerLogic>().clipDamage);

                    // Open Door
                    correct.displayText.text = "OK";
                    correct.animator.SetBool("isOpen", true);

                    StartCoroutine(correct.StopDoor());

                    correct.isOpen1 = true;
                }
            }

            if (!destroyNumPad_2 && !correct.isOpen2)
            {
                if (this.gameObject.name == "Player2")
                {
                    destroyNumPad_2 = true;

                    StartCoroutine(HackingSound());

                    // Damage
                    GetComponent<RobotPowers>()._components.Rarm -= 3;

                    // Camera Shake
                    ManageCoop.player2.camera.GetComponent<CameraFollow>().enabled = false;
                    StartCoroutine(TargetFollower.Shake(ManageCoop.player2.camera, 0.15f, 0.4f));

                    // Damage sound
                    player.GetComponent<PlayerLogic>().audioSrc.PlayOneShot(player.GetComponent<PlayerLogic>().clipDamage);

                    // Open Door
                    correct.displayText.text = "OK";
                    correct.animator.SetBool("isOpen", true);

                    StartCoroutine(correct.StopDoor());

                    correct.isOpen2 = true;
                }
            }
        }
    }

    IEnumerator HackingSound()
    {
        var src = GetComponent<PlayerLogic>().audioSrc;
        var clip = GetComponent<PlayerLogic>().clipDestroyNum;
        src.PlayOneShot(clip);
        yield return new WaitForSeconds(1.8f);
    }

    private void Correct_OnPassCorrect(object sender, EventArgs e)
    {
        padOpen = false;
        pad.SetActive(padOpen);
        correct.OnPassCorrect -= Correct_OnPassCorrect;

        correct.audioSrc.PlayOneShot(correct.clipOk);
    }

    public void OnInteractNumpad(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        if (context.action.triggered && distance < minDist)
        {
            if (padOpen)
            {
                padOpen = false;
            }
            else
            {
                padOpen = true;
                UpdateButtonHighlight(DisHigh);
            }
            pad.SetActive(padOpen);
        }
    }

    public void OnNavigation(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;
        if (!padOpen) return;

        if (cooldown < 0.005)
        {
            cooldown += Time.deltaTime;
            return;
        }
        cooldown = 0;

        if (context.control.displayName == "w" || context.control.displayName == "i")
        {
            if (i == 0)
            {
                i = 3;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
            else
            {
                i--;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }

        }
        else if (context.control.displayName == "a" || context.control.displayName == "j")
        {
            if (j == 0)
            {
                j = 2;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
            else
            {
                j--;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
        }
        else if (context.control.displayName == "s" || context.control.displayName == "k")
        {
            if (i == 3)
            {
                i = 0;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);

            }
            else
            {
                i++;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
        }
        else if (context.control.displayName == "d" || context.control.displayName == "l")
        {
            if (j == 2)
            {
                j = 0;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
            else
            {
                j++;
                DisHigh[0] = DisHigh[1];
                DisHigh[1] = padM[i, j];
                UpdateButtonHighlight(DisHigh);
            }
        }
    }

    public void OnEnterNum(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        if (context.action.triggered && padOpen)
        {
            if (padM[i, j] == "delete")
                pad.GetComponent<PadManager>().takeNumber("cancel");
            else
                pad.GetComponent<PadManager>().takeNumber(padM[i, j]);
        }
    }

    void UpdateButtonHighlight(string[] array)
    {
        pad.transform.Find("pad").Find(array[0]).GetComponent<Button>().interactable = true;
        pad.transform.Find("pad").Find(array[1]).GetComponent<Button>().interactable = false;
        correct.audioSrc.PlayOneShot(correct.clipNavigate);
    }
}
