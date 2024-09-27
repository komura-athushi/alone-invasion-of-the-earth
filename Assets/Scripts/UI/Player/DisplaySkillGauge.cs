using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySkillGauge : MonoBehaviour
{
    // スキルゲージの間隔
    private float skillGaugePaddingRatio = 1.5f;
    // スキルの装備個数 TODO：方針が決まり次第、変数を置き換える。
    int maxSkillNumber;

    // 描画にするためのコンポーネント
    Image[] skillGauge;

    // 現在のゲージの溜まっている値
    int[] currentSkillGauge;

    // ゲージを使用する際に必要な値
    int[] requiredSkillGauge;

    protected Player player;

    // 初期のスキルゲージ表示。
    void InitializeSkillGauge()
    {
        for (int i = 0; i < skillGauge.Length; i++)
        {
            // 0以下は描画しない
            if (i < 0)
            {
                continue;
            }

            // スキルゲージの生成
            var uiSkillGauge = Instantiate((GameObject)Resources.Load("UI_SkillGauge"));

            // スキルゲージに識別するための＋1個分命名
            uiSkillGauge.name = "SkillGauge" + (i + 1);

            // Canvasを親にスキルゲージを設置
            uiSkillGauge.transform.SetParent(this.transform, false);

            // スキルゲージをi個分ずらす
            uiSkillGauge.transform.position = new Vector3(uiSkillGauge.transform.position.x + (i + 1) * skillGaugePaddingRatio, uiSkillGauge.transform.position.y, uiSkillGauge.transform.position.z);
        }

        // スキルゲージの割り当てをし、初期のゲージが満たされている状態にする
        for (int i = 0; i < skillGauge.Length; i++)
        {
            skillGauge[i] = GameObject.Find("SkillGauge" + (i + 1)).GetComponent<Image>();
            skillGauge[i].fillAmount = 0.0f;
        }
    }

    // テスト用自動回復
    void TestSkillGaugeHeal()
    {
        for (int i = 0; i < skillGauge.Length; i++)
        {
            if (currentSkillGauge[i] < requiredSkillGauge[i])
            {
                currentSkillGauge[i]++;
            }
        }
    }

    void DrawSkillGauge()
    {
        for (int i = 0; i < skillGauge.Length; i++)
        {
            // 所持ポイント/必要ポイントの割合
            skillGauge[i].fillAmount = (float)currentSkillGauge[i] / (float)requiredSkillGauge[i];
        }
    }

    void UseSkillGauge(int skillNumber)
    {
        // ゲージが満たされていたら１本を消費できるようにする。
        if (currentSkillGauge[skillNumber] == requiredSkillGauge[skillNumber])
        {
            currentSkillGauge[skillNumber] = 0;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        maxSkillNumber = DataController.GetPlayerParam().MaxSkillNumber;

        skillGauge = new Image[maxSkillNumber];
        currentSkillGauge = new int[maxSkillNumber];
        requiredSkillGauge = new int[maxSkillNumber];

        for (int i = 0; i < skillGauge.Length; i++)
        {
            // i番目のスキルゲージを取得
            currentSkillGauge[i] = DataController.GetPlayerParam().SkillGauge;

            // i番目のスキルに必要なゲージを取得
            requiredSkillGauge[i] = DataController.GetPlayerParam().RequiredSkillGauge;
        }

        // スキルゲージのスポーンと初期化
        InitializeSkillGauge();
    }

    // Update is called once per frame
    void Update()
    {
        // テスト用のゲージ自動回復です。Merge時に削除予定
        TestSkillGaugeHeal();

        // スキルゲージの描画更新
        DrawSkillGauge();


        // テスト用です。Merge時に削除予定
        if (Input.GetKeyDown("1"))
        {
            UseSkillGauge(0);
            Debug.Log("1キーを押しました。スキル1を発動します。");
        }

        // テスト用です。Merge時に削除予定
        if (Input.GetKeyDown("2"))
        {
            UseSkillGauge(1);
            Debug.Log("2キーを押しました。スキル2を発動します。");
        }
    }
}
