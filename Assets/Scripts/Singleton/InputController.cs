using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 処理順番を早くする？
// https://qiita.com/No2DGameNoLife/items/d1497a2f98f95a5194ac
[DefaultExecutionOrder(-2)]
public class InputController : MonoBehaviour
{
    public enum EnKey{
        enKey_PlayerLeft,
        enKey_PlayerRight,
        enKey_PlayerUp,
        enKey_PlayerDown,
        enKey_PlayerJump,
        enKey_Decision,
        enKey_Attack
    };
    //シングルトン
    private static InputController instance = null;

    public static InputController GetInstance()
    {
        if (instance == null) {
                var previous = FindObjectOfType (typeof(InputController));
                if (previous) 
                {
                    Debug.LogWarning ("Initialized twice. Don't use InputController in the scene hierarchy.");
                    instance = (InputController)previous;
                }
                else 
                {
                    var go = new GameObject ("InputController");
                    instance = go.AddComponent<InputController> ();
                    DontDestroyOnLoad (go);
                }
            }
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool GetIsInputKey(EnKey enKey)
    {
        bool result = false;
        switch(enKey)
        {
            case EnKey.enKey_PlayerLeft:
                result = Input.GetKey(KeyCode.A);
                break;
            case EnKey.enKey_PlayerRight:
                result = Input.GetKey(KeyCode.D);
                break;
            case EnKey.enKey_PlayerUp:
                result = Input.GetKey(KeyCode.W);
                break;
            case EnKey.enKey_PlayerDown:
                result = Input.GetKey(KeyCode.S);
                break;
            case EnKey.enKey_PlayerJump:
                result = Input.GetKey(KeyCode.Space);
                break;
            case EnKey.enKey_Decision:
                result = Input.GetKey(KeyCode.Return);
                break;
        }
        return result;
    }
    public bool GetIsDownKey(EnKey enKey)
    {
                bool result = false;
        switch(enKey)
        {
            case EnKey.enKey_PlayerLeft:
                result = Input.GetKeyDown(KeyCode.A);
                break;
            case EnKey.enKey_PlayerRight:
                result = Input.GetKeyDown(KeyCode.D);
                break;
            case EnKey.enKey_PlayerJump:
                result = Input.GetKeyDown(KeyCode.Space);
                break;
            case EnKey.enKey_Decision:
                result = Input.GetKeyDown(KeyCode.Return);
                break;
                case EnKey.enKey_Attack:
                result = Input.GetMouseButtonDown(0);
                break;
        }
        return result;
    }
}
