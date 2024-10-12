using System;
using UnityEngine;

public class DataController
{
    private const String dataPath = "Data/";
    EnemyParam enemyParam;
    GameParam gameParam;
    PlayerParam playerParam;
    UIParam uiParam;

    //シングルトン
    private static DataController instance = null;

    private DataController()
    {
        // Resourcesで読み込むときはAssets/Resources/フォルダ配下に配置する必要有
        playerParam = Resources.Load(dataPath + "PlayerParam") as PlayerParam;
        gameParam = Resources.Load(dataPath + "GameParam") as GameParam;
        enemyParam = Resources.Load(dataPath + "EnemyParam") as EnemyParam;
        uiParam = Resources.Load(dataPath + "UIParam") as UIParam;
    }
    public static DataController GetInstance()
    {
        if (instance == null)
        {
            instance = new DataController();
        }
        return instance;
    }
    public static PlayerParam GetPlayerParam()
    {
        return GetInstance().playerParam;
    }
    public static EnemyParam GetEnemyParam()
    {
        return GetInstance().enemyParam;
    }
    public static GameParam GetGameParam()
    {
        return GetInstance().gameParam;
    }
    public static UIParam GetUIParam()
    {
        return GetInstance().uiParam;
    }
}
