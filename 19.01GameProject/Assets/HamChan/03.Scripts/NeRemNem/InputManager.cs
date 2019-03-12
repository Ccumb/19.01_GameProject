using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Neremnem.Tools
{
    public class InputManager : MonoBehaviour
    {
        public string PlayerID = "Player1";
        public enum MovementControls { Joystick, Arrows }

        public MovementControls MovementControl = MovementControls.Joystick;

        public bool SmoothMovement = true;
        public Vector2 Threshold = new Vector2(0.1f, 0.4f);

        public NRMInput.Button Skill1Button { get; protected set; }
        public NRMInput.Button Skill2Button { get; protected set; }
        public NRMInput.Button Skill3Button { get; protected set; }
        public NRMInput.Button Skill4Button { get; protected set; }
        public NRMInput.Button DashButton { get; protected set; }
        public NRMInput.Button AttackButton { get; protected set; }
        public NRMInput.Button PauseButton { get; protected set; }
        public NRMInput.Button InventoryButton { get; protected set; }
        public NRMInput.Button UseItemButton { get; protected set; }
        public NRMInput.Button SwapItemButton { get; protected set; }
        public Vector2 PrimaryMovement { get { return mPrimaryMovement; } }

        protected List<NRMInput.Button> ButtonList;
        protected Vector2 mPrimaryMovement = Vector2.zero;
        protected string mAxisHorizontal;
        protected string mAxisVertical;
        protected string mAxisSecondaryHorizontal;
        protected string mAxisSecondaryVertical;
        
        protected virtual void Start()
        {
            InitializeButtons();
            InitializeAxis();
        }
        
        protected virtual void InitializeButtons()
        {
            ButtonList = new List<NRMInput.Button>();
            ButtonList.Add(DashButton = new NRMInput.Button(PlayerID, "Dash", DashButtonDown, DashButtonPressed, DashButtonUp));
            ButtonList.Add(AttackButton = new NRMInput.Button(PlayerID, "Attack", AttackButtonDown, AttackButtonPressed, AttackButtonUp));
            ButtonList.Add(PauseButton = new NRMInput.Button(PlayerID, "Pause", PauseButtonDown, PauseButtonPressed, PauseButtonUp));
            ButtonList.Add(Skill1Button = new NRMInput.Button(PlayerID, "Skill1", Skill1ButtonDown, Skill1ButtonPressed, Skill1ButtonUp));
            ButtonList.Add(Skill2Button = new NRMInput.Button(PlayerID, "Skill2", Skill2ButtonDown, Skill2ButtonPressed, Skill2ButtonUp));
            ButtonList.Add(Skill3Button = new NRMInput.Button(PlayerID, "Skill3", Skill3ButtonDown, Skill3ButtonPressed, Skill3ButtonUp));
            ButtonList.Add(Skill4Button = new NRMInput.Button(PlayerID, "Skill4", Skill4ButtonDown, Skill4ButtonPressed, Skill4ButtonUp));
            ButtonList.Add(InventoryButton = new NRMInput.Button(PlayerID, "Inventory", InventoryButtonDown, InventoryButtonPressed, InventoryButtonUp));
            ButtonList.Add(UseItemButton = new NRMInput.Button(PlayerID, "UseItem", UseItemButtonDown, UseItemButtonPressed, UseItemButtonUp));
            ButtonList.Add(SwapItemButton = new NRMInput.Button(PlayerID, "SwapItem", SwapItemButtonDown, SwapItemButtonPressed, SwapItemButtonUp));
           
        }
        
        protected virtual void InitializeAxis()
        {
            mAxisHorizontal = PlayerID + "_Horizontal";
            mAxisVertical = PlayerID + "_Vertical";
            mAxisSecondaryHorizontal = PlayerID + "_SecondaryHorizontal";
            mAxisSecondaryVertical = PlayerID + "_SecondaryVertical";
        }
        
        protected virtual void LateUpdate()
        {
            ProcessButtonStates();
        }
        
        protected virtual void Update()
        {
            SetMovement();
            GetInputButtons();
        }
        
        protected virtual void GetInputButtons()
        {
            foreach (NRMInput.Button button in ButtonList)
            {
                if (Input.GetButton(button.ButtonID))
                {
                    button.TriggerButtonPressed();
                }
                if (Input.GetButtonDown(button.ButtonID))
                {
                    button.TriggerButtonDown();
                }
                if (Input.GetButtonUp(button.ButtonID))
                {
                    button.TriggerButtonUp();
                }
            }
        }
        
        public virtual void ProcessButtonStates()
        {
            // for each button, if we were at ButtonDown this frame, we go to ButtonPressed. If we were at ButtonUp, we're now Off
            foreach (NRMInput.Button button in ButtonList)
            {
                if (button.State.CurrentState == NRMInput.EButtonStates.Down)
                {
                    button.State.ChangeState(NRMInput.EButtonStates.Pressed);
                }
                if (button.State.CurrentState == NRMInput.EButtonStates.Up)
                {
                    button.State.ChangeState(NRMInput.EButtonStates.Off);
                }
            }
        }
        
        public virtual void SetMovement()
        {
            if (SmoothMovement)
            {

                mPrimaryMovement.x = Input.GetAxis(mAxisHorizontal);
                Debug.Log(mPrimaryMovement);

                mPrimaryMovement.y = Input.GetAxis(mAxisVertical);
            }
            else
            {
                mPrimaryMovement.x = Input.GetAxisRaw(mAxisHorizontal);
                mPrimaryMovement.y = Input.GetAxisRaw(mAxisVertical);
            }

        }
    
        public virtual void SetMovement(Vector2 movement)
        {
            mPrimaryMovement.x = movement.x;
            mPrimaryMovement.y = movement.y;

        }
        
        public virtual void SetHorizontalMovement(float horizontalInput)
        {
            mPrimaryMovement.x = horizontalInput;

        }
        
        public virtual void SetVerticalMovement(float verticalInput)
        {
            mPrimaryMovement.y = verticalInput;

        }

        public virtual void AttackButtonDown() { AttackButton.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void AttackButtonPressed() { AttackButton.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void AttackButtonUp() { AttackButton.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void DashButtonDown() { DashButton.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void DashButtonPressed() { DashButton.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void DashButtonUp() { DashButton.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void PauseButtonDown() { PauseButton.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void PauseButtonPressed() { PauseButton.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void PauseButtonUp() { PauseButton.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void Skill1ButtonDown() { Skill1Button.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void Skill1ButtonPressed() { Skill1Button.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void Skill1ButtonUp() { Skill1Button.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void Skill2ButtonDown() { Skill2Button.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void Skill2ButtonPressed() { Skill2Button.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void Skill2ButtonUp() { Skill2Button.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void Skill3ButtonDown() { Skill3Button.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void Skill3ButtonPressed() { Skill3Button.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void Skill3ButtonUp() { Skill3Button.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void Skill4ButtonDown() { Skill4Button.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void Skill4ButtonPressed() { Skill4Button.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void Skill4ButtonUp() { Skill4Button.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void InventoryButtonDown() { InventoryButton.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void InventoryButtonPressed() { InventoryButton.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void InventoryButtonUp() { InventoryButton.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void UseItemButtonDown() { UseItemButton.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void UseItemButtonPressed() { UseItemButton.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void UseItemButtonUp() { UseItemButton.State.ChangeState(NRMInput.EButtonStates.Up); }

        public virtual void SwapItemButtonDown() { SwapItemButton.State.ChangeState(NRMInput.EButtonStates.Down); }
        public virtual void SwapItemButtonPressed() { SwapItemButton.State.ChangeState(NRMInput.EButtonStates.Pressed); }
        public virtual void SwapItemButtonUp() { SwapItemButton.State.ChangeState(NRMInput.EButtonStates.Up); }

    }
}
