using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    Vector3 cameraAddPos;

    // Start is called before the first frame update
    void Start()
    {
        // Playerタグのついたオブジェクトをターゲットにする
        player = GameObject.FindGameObjectWithTag("Player");
        // 最初に座標更新しておく
        CameraUpdate();
        cameraAddPos = DataController.GetGameParam().CameraAddPos;
    }
    void Update()
    {
        // プレイヤーがいないなら何もしない
        if (player == null)
        {
            return;
        }
        CameraUpdate();
    }
    void CameraUpdate()
    {
        // カメラの位置を設定
        transform.position = player.transform.position + cameraAddPos;
    }
}
