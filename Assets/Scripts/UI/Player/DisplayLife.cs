using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void IncreaseLife(int healHP)
    {
        for (int i = playerHP - healHP; i < playerHP; i++)
        {
            // 0以下は描画しない
            if (i < 0)
            {
                continue;
            }

            // ハートの生成
            var heart = Instantiate((GameObject)Resources.Load("UI_Life"));

            // ハートに識別するための＋1個分命名
            heart.name = "heart" + (i + 1);

            // Canvasを親にハートを設置
            heart.transform.SetParent(this.transform, false);

            // ハートをi個分ずらす
            heart.transform.position = new Vector3(heart.transform.position.x + (i + 1) * heartPaddingRatio, heart.transform.position.y, heart.transform.position.z);
        }
    }

    void DecreaseLife(int damageHP)
    {
        for (int i = playerHP + damageHP; i > playerHP; i--)
        {
            // ハートを減らす
            Destroy(GameObject.Find("heart" + i));
            if (i < 0)
            {
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 最初のハートを描画
        InitializeDrawLife();
    }

    // Update is called once per frame
    void Update()
    {
        // playerHPが増減するたびに呼び出すようにする。

        // テスト用でキー入力を使用しています。
        if (Input.GetKeyDown("u"))
        {
            // nポイント回復させる。
            int healHP = 2;
            playerHP += healHP;

            // 回復した差分をintで渡してください。
            IncreaseLife(healHP);
        }

        // テスト用でキー入力を使用しています。
        if (Input.GetKeyDown("j"))
        {
            // nポイントダメージを受ける。
            int damageHP = 2;
            playerHP -= damageHP;

            // ダメージを受けた差分をintで渡してください。
            DecreaseLife(damageHP);
        }
    }
}
