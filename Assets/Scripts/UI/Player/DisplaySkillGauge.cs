using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySkillGauge : MonoBehaviour
{
    // スキルゲージの間隔
    private float skillGaugePaddingRatio = 1.3f;

    // スキルの装備個数 TODO：方針が決まり次第、変数を置き換える。
    static int equipSkill = 2;
    // スキルの付け替えに対応する予定
    Image[] skillGauge = new Image[equipSkill];
    protected Player player;

    // 初期のスキルゲージ表示。
    void InitializeSkillGauge()
    {
        for (int i = 0; i < equipSkill; i++)
        {
            // 0以下は描画しない
            if (i < 0)
            {
                continue;
            }

            // スキルゲージの生成
            var skillGauge = Instantiate((GameObject)Resources.Load("UI_SkillGauge"));

            // スキルゲージに識別するための＋1個分命名
            skillGauge.name = "SkillGauge" + (i + 1);

            // Canvasを親にスキルゲージを設置
            skillGauge.transform.SetParent(this.transform, false);

            // スキルゲージをi個分ずらす
            skillGauge.transform.position = new Vector3(skillGauge.transform.position.x + (i + 1) * skillGaugePaddingRatio, skillGauge.transform.position.y, skillGauge.transform.position.z);
        }

        // スキルゲージの割り当てをし、初期のゲージが満たされている状態にする
        for (int i = 0; i < equipSkill; i++)
        {
            skillGauge[i] = GameObject.Find("SkillGauge" + (i + 1)).GetComponent<Image>();
            skillGauge[i].fillAmount = 1.0f;
        }
    }

    void UseGauge(Image skillGauge)
    {
        // ゲージが満たされていたら１本を消費できるようにする。
        if (skillGauge.fillAmount == 1.0f)
        {
            skillGauge.fillAmount -= 1.0f;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        InitializeSkillGauge();
    }

    // Update is called once per frame
    void Update()
    {
        // テスト用自動回復
        for (int i = 0; i < equipSkill; i++)
        {
            // 所持ポイント/必要ポイントの割合
            skillGauge[i].fillAmount += player.GetSkill1() / player.GetSkill1() / 3.0f * Time.deltaTime;
        }

        // テスト用です。削除予定
        if (Input.GetKeyDown("1"))
        {
            UseGauge(skillGauge[0]);
            Debug.Log("1キーを押しました。スキル1を発動します。");
        }

        // テスト用です。削除予定
        if (Input.GetKeyDown("2"))
        {
            UseGauge(skillGauge[1]);
            Debug.Log("2キーを押しました。スキル2を発動します。");
        }
    }
}
