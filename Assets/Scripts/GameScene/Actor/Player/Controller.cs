using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PlayerController{
    abstract public class IController
    {
        protected Player player;
        protected PlayerInput input;
        public  virtual void Enter(Player player, PlayerInput input) { this.player = player; this.input = input; }
        public abstract void Execute();
        public virtual IController ChangeState()
        {
            if(player.IsGrounded() && input.IsJumping) { return new JumpController(); }
            Vector2 inputVector = input.InputVector;
            if( Mathf.Abs(inputVector.x) > 0.1f ) { return new MoveGroundController(); }
            return new IdleController();
        }
        public abstract void Exit();
    }

    public class IdleController: IController
    {
        float gravity;
        public  override void Enter(Player player, PlayerInput input) 
        {
            this.player = player; 
            this.input = input;
            gravity = DataController.GetGameParam().Gravity; 
        }
        public override IController ChangeState() 
        {


            return base.ChangeState();
        }
        public override void Execute()
        {
            Vector2 velocity = Vector2.zero;
            velocity.y -= gravity;
            player.MovePlayer(velocity);
        }

        public override void Exit()
        {

        }
    }

    public class MoveGroundController: IController
    {
        float moveSpeed;
        float gravity;
        public override void Enter(Player player, PlayerInput input) 
        {   
            this.player = player; 
            this.input = input; 
            moveSpeed = player.GetMoveSpeed();
            gravity = player.GetGravity();
        }
        public override IController ChangeState() 
        {
            return base.ChangeState();
        }
        public override void Execute()
        {
            Move();
        }
        void Move()
        {
            Vector2 inputVector = input.InputVector;
            inputVector.x *= moveSpeed;
            Vector2 velocity = player.GetVelocity();
            velocity.x = inputVector.x;
            velocity.y -= gravity;
            if(player.IsGrounded())
            {
                velocity.y = 0.0f;
            }
            player.MovePlayer(velocity);
        }
        public override void Exit()
        {

        }
    }
    public class JumpController: IController
    {
        float moveSpeed;
        float gravity;
        public float inputJumpTimer = 0.0f;
        bool isJumping = false;
        float jumpPower = 0.0f;
        public override void Enter(Player player, PlayerInput input) 
        {
            this.player = player; 
            this.input = input; 
            moveSpeed = player.GetMoveSpeed();
            gravity = player.GetGravity();
        }
        public override IController ChangeState() 
        {
            if(!isJumping || !player.IsGrounded()) { return this; }
            return base.ChangeState();
        }
        public override void Execute()
        {
            CountTimer();
            Move();
        }
        void CountTimer()
        {
            if (isJumping)
            {
                return;
            }
            inputJumpTimer += Time.deltaTime;

            if(!input.IsJumping)
            {
                if(inputJumpTimer <= DataController.GetPlayerParam().SmallJumpTime)
                {
                    jumpPower = DataController.GetPlayerParam().SmallJumpPower;
                    Jump();
                    return;
                }
            }
            if(inputJumpTimer > DataController.GetPlayerParam().SmallJumpTime)
            {
                jumpPower = DataController.GetPlayerParam().BigJumpPower;
                Jump();
                return;
            }
        }
        void Jump()
        {
            Vector2 velocity = player.GetVelocity();
            velocity.y = jumpPower;
            player.MovePlayer(velocity);
            isJumping = true;
        }
        void Move()
        {
            if(!isJumping)
            {
                return;
            }

            Vector2 inputVector = input.InputVector;
            inputVector.x *= moveSpeed * DataController.GetPlayerParam().ReduceMoveSpeedForJumping;
            Vector2 velocity = player.GetVelocity();
            velocity.x *= DataController.GetPlayerParam().AirResistanceForJumping;
            velocity.x += inputVector.x;
            if(velocity.x >= moveSpeed)
            {
                velocity.x = moveSpeed;
            }
            else if(velocity.x <= -moveSpeed)
            {
                velocity.x = -moveSpeed;
            }
            velocity.y -= gravity;
            player.MovePlayer(velocity);
        }
        public override void Exit()
        {

        }
    }
}
