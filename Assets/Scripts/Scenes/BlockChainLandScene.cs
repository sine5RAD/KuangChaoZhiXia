using KCGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * 描述：区块链大陆场景
 * 作者：sine5RAD
 */
public class BlockChainLandScene : SceneBase
{
    private Player player;
    public BlockChainLandScene(int floor, float difficulty, float difficultyGap)
    {
        DungonConfig.Instance.floor = floor;
        DungonConfig.Instance.difficulty = difficulty;
        DungonConfig.Instance.difficultyGap = difficultyGap;
    }

    public override void EnterScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        base.EnterScene(scene, loadSceneMode);
        Debug.Log(SceneManager.GetActiveScene().name);
        GameObject.Find("Grid").GetComponent<DungonMapGen>().GenerateMap();
        UIManager.Instance.Push(new PlayerUIPanel(PlayerUIPanel.uIType));
    }

    public override void ExitScene()
    {
    }
}
