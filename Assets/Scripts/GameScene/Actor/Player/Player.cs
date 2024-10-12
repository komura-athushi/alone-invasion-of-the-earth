using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

using PlayerController;
public class Player : MonoBehaviour
{
    int hp;
    int maxSkillNumber;
    int[] currentSkillGauge;
    int[] requiredSkillGauge;
    float moveSpeed;
    float jumpPower;
    float gravity;
    Vector2 velocity = Vector2.zero;
    bool isAttacking = false;
    bool isZeroGraviting = false;
    bool isDamaged = false;
    CharacterController2D characterController2D;
    IController currentController;
    IController beforeController;
    PlayerInput input;
    //プロパティ
    public int GetSetHp { get { return hp; } set { hp = value; } }
    public int MaxSkillNumber() { return maxSkillNumber; }
    public int[] CurrentSkillGauge() { return currentSkillGauge; }
    public int[] RequiredSkillGauge() { return requiredSkillGauge; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetGravity() { return gravity; }
    public bool IsAttacking() { return isAttacking; }
    public bool IsZeroGraviting() { return isZeroGraviting; }
    public bool IsDamaged() { return isDamaged; }
    public bool IsGrounded() { return characterController2D.IsGrounded(); }
    public Vector2 GetVelocity() { return characterController2D.GetVelocity(); }

    // Start is called before the first frame update
    void Awake()
    {
        hp = DataController.GetPlayerParam().Hp;
        maxSkillNumber = DataController.GetPlayerParam().MaxSkillNumber;
        currentSkillGauge = DataController.GetPlayerParam().CurrentSkillGauge;
        requiredSkillGauge = DataController.GetPlayerParam().RequiredSkillGauge;
        moveSpeed = DataController.GetPlayerParam().MoveSpeed;
        gravity = DataController.GetGameParam().Gravity;
        // 自身にアタッチされているCharacterControllerを取得する
        characterController2D = GetComponent<CharacterController2D>();
        SceneController.GetInstance();
        input = new PlayerInput();
        beforeController = new IdleController();
        beforeController.Enter(this, input);
        currentController = beforeController;
    }
    void FixedUpdate()
    {
        ExecuteController();
    }
    public void MovePlayer(Vector2 velocity)
    {
        this.velocity = velocity;
        characterController2D.Move(velocity);
    }

    void ExecuteController()
    {
        input.HandleInput();
        currentController.Execute();
        IController newController = currentController.ChangeState();
        if (!ReferenceEquals(currentController, newController))
        {
            currentController.Exit();
            beforeController = currentController;
            currentController = newController;
            currentController.Enter(this, input);
        }
    }
}
