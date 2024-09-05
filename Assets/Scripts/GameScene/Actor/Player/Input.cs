using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController{
    public class PlayerInput
    {
        public Vector2 InputVector => inputVector;
        public bool IsJumping { get => isJumping; set => isJumping = value; }
        private Vector2 inputVector;
        private bool isJumping;
        private float xInput;
        private float yInput;

        public void HandleInput()
        {
            // Reset input
            xInput = 0;
            yInput = 0;

            if (InputController.GetInstance().GetIsInputKey(InputController.EnKey.enKey_PlayerRight))
            {
                xInput++;
            }
            if (InputController.GetInstance().GetIsInputKey(InputController.EnKey.enKey_PlayerLeft))
            {
                xInput--;
            }
            if (InputController.GetInstance().GetIsInputKey(InputController.EnKey.enKey_PlayerUp))
            {
                yInput++;
            }
            if (InputController.GetInstance().GetIsInputKey(InputController.EnKey.enKey_PlayerDown))
            {
                yInput--;
            }

            inputVector = new Vector2(xInput, yInput);
            isJumping = InputController.GetInstance().GetIsInputKey(InputController.EnKey.enKey_PlayerJump);
        }
    }
}
