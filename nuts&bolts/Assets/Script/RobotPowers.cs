using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotPowers : MonoBehaviour
{
    // UI Power-swap logic
    public static event EventHandler PowerSwitched;

    public IDictionary<PowerSelector, bool> powers = new Dictionary<PowerSelector, bool>(); //string = name = key, bool = 1 available 0 not available

    public struct components
    {
        public int Larm; // 0: push/pull, 1-2: extend, 3-4: push/pull heavier obj, 5: more extend 
        public int Rarm; // 0: push/pull, 1-2: magnetic, 3-4: magnetic with heavier obj, 5: destroy numberpad (-2 Rarm)
        public int view; // 0: miope, 1-2: normal, 3: x-ray 
        public int legs; //0: normal, 1-2: up (not movements), 3-4: down with movements, 5: up and movements
        public int rocket; //0,1,2: no, 3: yes
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
        PushPull, ArmExtend, Magnetic, DestroyPad, Xray, Up, Down, Rocket
    }

    public PowerSelector selectedPower;

    private int powersIndex = 0;

    public void OnSwitchPower(InputAction.CallbackContext context)
    {
        if (PlayerLogic.menuOpen) return;

        bool tabPressed = context.action.triggered;

        if (tabPressed)
        {
            checkPowers();
            switchPower();

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
    }

    public void checkPowers()
    {
        powers[PowerSelector.ArmExtend] = _components.Larm > 0;       
        powers[PowerSelector.Magnetic] = _components.Rarm > 0;
        powers[PowerSelector.DestroyPad] = _components.Rarm > 3;
        powers[PowerSelector.Xray] = _components.view > 2;
        powers[PowerSelector.Up] = _components.legs > 0;
        powers[PowerSelector.Down] = _components.legs > 2;
        powers[PowerSelector.Rocket] = _components.rocket > 2;
    }

    public void switchPower()
    {
        List<PowerSelector> keys = new List<PowerSelector>(powers.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            powersIndex = (powersIndex + 1) % keys.Count;
            if (powers[keys[powersIndex]] == true)
            {
                selectedPower = keys[powersIndex];
                return;
            }
        }
    }

    private void initialize()
    {
        _components.Larm = 5;
        _components.Rarm = 5;
        _components.legs = 5;
        _components.view = 5;
        _components.rocket = 5;
        _components.bolts = 5;

        powers.Add(PowerSelector.PushPull, true);
        powers.Add(PowerSelector.ArmExtend, _components.Larm > 0);     
        powers.Add(PowerSelector.Magnetic, _components.Rarm > 0);     
        powers.Add(PowerSelector.DestroyPad, _components.Rarm > 3);     
        powers.Add(PowerSelector.Xray, _components.view > 2);
        powers.Add(PowerSelector.Up, _components.legs > 0);
        powers.Add(PowerSelector.Down, _components.legs > 2);
        powers.Add(PowerSelector.Rocket, _components.rocket > 2);

        selectedPower = PowerSelector.PushPull;
    }
}
