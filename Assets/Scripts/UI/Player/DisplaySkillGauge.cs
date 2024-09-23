using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySkillGauge : MonoBehaviour
{
    // ハートの間隔
    private float skillGaugePaddingRatio = 1.3f;

    int skillGaugeNumber = 2;
    // スキルの付け替えに対応する予定
    Image skillGauge1;
    Image skillGauge2;
    protected Player player;

    // 初期のスキルゲージ表示。
    void InitializeSkillGauge()
    {
        for (int i = 0; i < skillGaugeNumber; i++)
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
    }

    void UseGauge(Image skillGauge)
    {
        // ゲージが満たされていたら消費するようにする
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
        skillGauge1 = GameObject.Find("SkillGauge1").GetComponent<Image>();
        skillGauge2 = GameObject.Find("SkillGauge2").GetComponent<Image>();
        skillGauge1.fillAmount = 1.0f;
        skillGauge2.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // テスト用自動回復
        skillGauge1.fillAmount += player.GetSetSkill1 / player.GetSetSkill1 / 3.0f * Time.deltaTime;
        skillGauge2.fillAmount += player.GetSetSkill1 / player.GetSetSkill1 / 3.0f * Time.deltaTime;


        // テスト用です。削除予定
        if (Input.GetKeyDown("1"))
        {
            UseGauge(skillGauge1);
        }

        // テスト用です。削除予定
        if (Input.GetKeyDown("2"))
        {
            UseGauge(skillGauge2);
        }

    }
}
