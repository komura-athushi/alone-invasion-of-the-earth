using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLife : MonoBehaviour
{
    // ScriptableObjectにする予定

    // UI用のScriptableObjectがほしい
    // ハートの間隔
    private float heartPaddingRatio = 1.3f;

    // private float playerHP = DataController.GetGameParam().playerHP; 
    private int playerHP = 5;

    void InitializeDrawLife()
    {
        for (int i = 1; i <= playerHP; i++)
        {
            // ハートの生成
            var heart = Instantiate((GameObject)Resources.Load("UI_Life"));

            // ハートに識別するための命名
            heart.name = "heart" + i;

            // Canvasを親にハートを設置
            heart.transform.SetParent(this.transform, false);

            // ハートをi個分ずらす
            heart.transform.position = new Vector3(heart.transform.position.x + i * heartPaddingRatio, heart.transform.position.y, heart.transform.position.z);
        }
    }

    void DrawLife()
    {
        // テスト用でキー入力を使用しています。
        if (Input.GetKeyDown("u"))
        {
            // テスト用回復
            playerHP++;

            // ハートの生成
            var heart = Instantiate((GameObject)Resources.Load("UI_Life"));

            // ハートに識別するための命名
            heart.name = "heart" + playerHP;

            // Canvasを親にハートを設置
            heart.transform.SetParent(this.transform, false);

            // ハートをi個分ずらす
            heart.transform.position = new Vector3(heart.transform.position.x + playerHP * heartPaddingRatio, heart.transform.position.y, heart.transform.position.z);
        }

        // テスト用でキー入力を使用しています。
        if (Input.GetKeyDown("j"))
        {
            // テスト用ダメージ
            playerHP--;

            // ハートを減らす
            Destroy(GameObject.Find("heart" + (playerHP + 1)));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 最初のハートを描画
        InitializeDrawLife();
    }

    // Update is called once per frame
    // テストでは、FixedUpdateをUpdateにしていました。Fixedだとレスポンスが悪くなるためです。
    void FixedUpdate()
    {
        // playerLifeを常にチェックしてハートの数を更新
        DrawLife();
    }
}
