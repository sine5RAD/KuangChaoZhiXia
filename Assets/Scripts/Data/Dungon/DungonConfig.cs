using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KCGame;

/* 
 * 描述：地牢配置类
 * 作者：sine5RAD
 */
public class DungonConfig: KCGame.KCSingleton<DungonConfig>
{
    public bool isInDungon; // 是否在地牢中
    public int floor; // 地牢层数
    public float difficulty; // 地牢难度
    public float difficultyGap; // 地牢难度间隔

    public Player player = new Player(); // 玩家数据

    public void Init(float difficulty, float difficultyGap, Player player)
    {
        floor = -1;
        this.difficulty = difficulty;
        this.difficultyGap = difficultyGap;
        this.player = player;
    }

    public void EnterDungon()
    {
        isInDungon = true;
    }
    public void ExitDungon()
    {
        isInDungon = false;
        floor = 0; // 重置地牢层数
        difficulty = 0; // 重置地牢难度
        difficultyGap = 0; // 重置地牢难度间隔
    }
}
