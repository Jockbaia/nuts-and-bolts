using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotPowers : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip clipSwitchPower;
    public AudioClip clipXrayOn;

    // UI Power-swap logic
    public static event EventHandler PowerSwitched;

    public IDictionary<PowerSelector, bool> powers = new Dictionary<PowerSelector, bool>(); //string = name = key, bool = 1 available 0 not available

    public struct components
    {
        public int Larm;
        public int Rarm;
        public int view;
        public int legs;
        public int rocket;
        public int bolts;

        /* 8
         * 0: push/pull
         * Larm 1-2: extend, 3: push/pull heavier obj
         * Rarm 1-2-3: magnetic, 4: destroy numberpad (-3 Rarm)
         * view 0-1-2: normal, 3: x-ray
         * legs 1-2: up 3-4: down 5: up/down + move
         * rocket: 0-1-2: no, 3: yes
          */

    }

    public components _components;

    public enum PowerSelector
    {
        PushPull, ArmExtend, PushPullHeavy, Magnetic, DestroyPad, Xray, Up, Down, UpDownMove, Rocket
    }

    public PowerSelector selectedPower;

    private int powersIndex = 0;

    public void OnSwitchPower(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;
        if (this.GetComponent<HandleNumpadNav>().padOpen) return;
        if (GetComponent<PlayerLogic>().isExtendingArm) return;

        if (context.action.triggered)
        {
            checkPowers();
            switchPower();
            audioSrc.PlayOneShot(clipSwitchPower);
            if (selectedPower == PowerSelector.Xray) audioSrc.PlayOneShot(clipXrayOn);
            PowerSwitched?.Invoke(this, EventArgs.Empty);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();      
    }

    // Update is called once per frame
    void Update()
    {
        checkPowers(); //check and update powers

        if (!powers[selectedPower])
        {
            switchPower();
            PowerSwitched?.Invoke(this, EventArgs.Empty);
        }
    }

    public void checkPowers()
    {
        powers[PowerSelector.ArmExtend] = _components.Larm > 0;       
        powers[PowerSelector.PushPullHeavy] = _components.Larm > 4;       
        powers[PowerSelector.Magnetic] = _components.Rarm > 0;
        powers[PowerSelector.DestroyPad] = _components.Rarm > 3;
        powers[PowerSelector.Xray] = _components.view > 2;
        powers[PowerSelector.Up] = _components.legs > 0;
        powers[PowerSelector.Down] = _components.legs > 2;
        powers[PowerSelector.UpDownMove] = _components.legs > 4;
        powers[PowerSelector.Rocket] = _components.rocket > 2;
    }

    public void switchPower()
    {
        List<PowerSelector> keys = new List<PowerSelector>(powers.Keys);

        ResetArms();

        for (int i = 0; i < keys.Count; i++)
        {
            powersIndex = (powersIndex + 1) % keys.Count;
            if (powers[keys[powersIndex]] == true)
            {
                if (keys[powersIndex] == PowerSelector.PushPullHeavy) continue;
                if (keys[powersIndex] == PowerSelector.UpDownMove) continue;
                if (keys[powersIndex] == PowerSelector.Down && powers[PowerSelector.UpDownMove]) continue;

                if (keys[powersIndex] == PowerSelector.PushPull && powers[PowerSelector.PushPullHeavy])
                {
                    selectedPower = PowerSelector.PushPullHeavy;
                }
                else if ((keys[powersIndex] == PowerSelector.Up || keys[powersIndex] == PowerSelector.Down) && powers[PowerSelector.UpDownMove])
                {
                    selectedPower = PowerSelector.UpDownMove;
                }
                else
                {
                    selectedPower = keys[powersIndex];
                }

                
                return;
            }
        }
    }

    void ResetArms()
    {
        transform.Find("Model/Arm_Right").localRotation = Quaternion.Euler(0, 70, 0);
        transform.Find("Model/Arm_Left").localRotation = Quaternion.Euler(0, -70, 0);
    }

    private void initialize()
    {
        if (gameObject.name == "Player1")
        {
            _components.Larm = SceneLoader._componentsP1.Larm;
            _components.Rarm = SceneLoader._componentsP1.Rarm;
            _components.view = SceneLoader._componentsP1.view;
            _components.legs = SceneLoader._componentsP1.legs;
            _components.rocket = SceneLoader._componentsP1.rocket;
            _components.bolts = SceneLoader._componentsP1.bolts;
        }
        else
        {
            _components.Larm = SceneLoader._componentsP2.Larm;
            _components.Rarm = SceneLoader._componentsP2.Rarm;
            _components.view = SceneLoader._componentsP2.view;
            _components.legs = SceneLoader._componentsP2.legs;
            _components.rocket = SceneLoader._componentsP2.rocket;
            _components.bolts = SceneLoader._componentsP2.bolts;
        }

        powers.Add(PowerSelector.PushPull, true);
        powers.Add(PowerSelector.ArmExtend, _components.Larm > 0);     
        powers.Add(PowerSelector.PushPullHeavy, _components.Larm > 4);     
        powers.Add(PowerSelector.Magnetic, _components.Rarm > 0);     
        powers.Add(PowerSelector.DestroyPad, _components.Rarm > 3);     
        powers.Add(PowerSelector.Xray, _components.view > 2);
        powers.Add(PowerSelector.Up, _components.legs > 0);
        powers.Add(PowerSelector.Down, _components.legs > 2);
        powers.Add(PowerSelector.UpDownMove, _components.legs > 4);
        powers.Add(PowerSelector.Rocket, _components.rocket > 2);

        if (powers[PowerSelector.PushPullHeavy])
            selectedPower = PowerSelector.PushPullHeavy;
        else
            selectedPower = PowerSelector.PushPull;
        PowerSwitched?.Invoke(this, EventArgs.Empty);
    }
}
