using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLife : MonoBehaviour
{
    float heartPaddingRatio = 1.3f;
    int playerlife = 5;

    void InitializeDrawLife()
    {
        for (int i = 1; i <= playerlife; i++)
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
        if (Input.GetKeyDown("u"))
        {
            // テスト用回復
            playerlife++;

            // ハートの生成
            var heart = Instantiate((GameObject)Resources.Load("UI_Life"));

            // ハートに識別するための命名
            heart.name = "heart" + playerlife;

            // Canvasを親にハートを設置
            heart.transform.SetParent(this.transform, false);

            // ハートをi個分ずらす
            heart.transform.position = new Vector3(heart.transform.position.x + playerlife * heartPaddingRatio, heart.transform.position.y, heart.transform.position.z);
        }

        if (Input.GetKeyDown("j"))
        {
            // テスト用ダメージ
            playerlife--;

            // ハートを減らす
            Destroy(GameObject.Find("heart" + (playerlife + 1)));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 最初のハートを描画
        InitializeDrawLife();
    }

    // Update is called once per frame
    // テスト用でFixedUpdateをUpdateにしています。Fixedだとレスポンスが悪くなるためです。
    void Update()
    {
        // playerLifeを常にチェックしてハートの数を更新
        DrawLife();
    }
}
