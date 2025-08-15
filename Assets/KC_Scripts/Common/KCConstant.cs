using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace KCGame
{
    public static class KCConstant
    {
        //
        public const string 过度场景名称 = "LoadTransitionScene";

        //MapObjectManager的resPath
        public const string MapObjectManager_resPath = "Prefab/Objects/Map";




        public const string Tag_Player = "Player";
        public const string Tag_Wall = "Wall";
        public const string Tag_Map = "Map";
        public const string Tag_MapGround = "MapGround";
        public const string Tag_MapObstacle = "MapObstacle";

        public const float RushCoolDown = 5f; // 冲刺冷却时间
        public const float RushTempDelta = 0.6f; // 冲刺增加的温度
        public const float MoveTempDelta = 0.1f; // 移动增加的温度
        public const float PushTempDelta = 0.3f; // 推动增加的温度
        public const float PullTempDelta = 0.5f; // 拉动增加的温度
        public const float CarryTempDelta = 1f; // 携带增加的温度

        public const float PushWeightFactor = 1.0f; // 推动时，物体重量对温度增加的影响系数
        public const float PullWeightFactor = 1.2f; // 拉动时，物体重量对温度增加的影响系数
        public const float CarryWeightFactor = 2.0f; // 携带时，物体重量对温度增加的影响系数

        public const int MoveCostGas = 1; // 每次移动消耗的燃料
        public const int PushCostGas = 2; // 每次推动消耗的燃料
        public const int PullCostGas = 3; // 每次拉动消耗的燃料
        public const int CarryCostGas = 5; // 每次携带消耗的燃料
    }
}
