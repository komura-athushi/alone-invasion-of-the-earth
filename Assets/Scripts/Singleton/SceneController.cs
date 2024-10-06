using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance = null;
    public enum EnScene{
        enScene_Game,
        enScene_Title
    };
    private EnScene enScene = EnScene.enScene_Game;

    public static SceneController GetInstance()
    {
        if (instance == null) {
                var previous = FindObjectOfType (typeof(SceneController));
                if (previous) 
                {
                    Debug.LogWarning ("Initialized twice. Don't use InputController in the scene hierarchy.");
                    instance = (SceneController)previous;
                }
                else 
                {
                    var go = new GameObject ("SceneController");
                    instance = go.AddComponent<SceneController> ();
                    DontDestroyOnLoad (go);
                }
            }
        return instance;
    }
    public void Update()
    {
        switch(enScene)
        {
            case EnScene.enScene_Game:
                if(InputController.GetInstance().GetIsDownKey(InputController.EnKey.enKey_Decision))
                {
                    // 次のシーンを非同期で読み込み
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
                    enScene = EnScene.enScene_Game;
                }
                break;
            case EnScene.enScene_Title:
                if(InputController.GetInstance().GetIsDownKey(InputController.EnKey.enKey_Decision))
                {
                    // 次のシーンを非同期で読み込み
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("NextScene");
                    enScene = EnScene.enScene_Title;
                }
            break;
        }
    }
}
 