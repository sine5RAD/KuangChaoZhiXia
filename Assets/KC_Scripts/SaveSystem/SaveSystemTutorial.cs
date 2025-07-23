using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering.Universal;



namespace SaveSystemTutorial
{
    public static class SaveSystem
    {

     

        //-----------------------使用说明--------------------------
        #region 使用说明

        ////需要保存的数据 不局限余字典
        //private Dictionary<string, UnityEngine.Vector3> data = new Dictionary<string, UnityEngine.Vector3>()
        //{
        //    {"key1",UnityEngine.Vector3.down},
        //    {"key2",UnityEngine.Vector3.forward}

        //};


        ////Save调用说明
        //public void TestSave()
        //{

        //    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //    string path = Path.Combine(desktopPath, "kc游戏数据.kcdata");

        //    SaveSystem.Save<Dictionary<string, UnityEngine.Vector3>>("key_测试数据", data, path);


        //}


        ////Load调用说明
        //public void TestLoad()
        //{

        //    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //    string path = Path.Combine(desktopPath, "kc游戏数据.kcdata");

        //    data =  SaveSystem.Load<Dictionary<string, UnityEngine.Vector3>>("key_测试数据", path);
        //}


        #endregion
        public static void Save<T>(string key, T value, string savePath)
        {
            ES3.Save<T>(key, value, savePath);
            Debug.Log("保存成功:"+ savePath);
        }

        public static void Save<T>(string key, T value)
        {
            ES3.Save<T>(key, value);
            Debug.Log("保存成功");
        }

        public static T Load<T>(string key, string savePath)
        {

            return ES3.Load<T>(key, savePath);
        }

        public static T Load<T>(string key)
        {
            return ES3.Load<T>(key);
        }


        #region Deleteing
        //TODO: 这段代码我还没测试，所以到时候测试一下

        /// <summary>
        /// 删除Application.persistentDataPath中的目标文件
        /// </summary>
        /// <param name="saveFilePath">需要删除的文件路径，记得带扩展名</param>
        public static void DeleteFile(string saveFilePath)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFilePath);
            try
            {
                File.Delete(path);
#if UNITY_EDITOR
                Debug.Log($"成功删除{path}");
#endif
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError($"{path}删除失败。\n错误原因：{e}");
#endif
            }
        }

        #endregion




       
        #region JSON
        /// <summary>
        /// 将数据序列化为Json字符串，并保存到Application.persistentDataPath/saveFilePath
        /// </summary>
        /// <param name="saveFilePath">相对路径，记得带扩展名</param>
        /// <param name="data">需要序列化的数据</param>
        [Obsolete("使用SaveSystem.Save<T>()", false)]
        public static void SaveByJson(string saveFilePath, object data)
        {
            string json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, saveFilePath);
            FileInfo fileInfo = new FileInfo(path);
            Debug.Log(fileInfo.Directory.FullName);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
                #if UNITY_EDITOR
                Debug.Log("成功创建文件夹{saveFilePath}");
                #endif
            }
            try
            {
                File.WriteAllText(path, json);
                #if UNITY_EDITOR
                Debug.Log($"成功保存到{path}");
                #endif
            }
            catch(System.Exception e)
            {
                Debug.LogError($"将数据保存到{path}时发生错误。\n错误原因：{e}");
            }
        }

        /// <summary>
        /// 从本地Json文件中加载数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="saveFilePath">Json文件路径，记得带扩展名</param>
        /// <returns>读取的数据</returns>
        public static T LoadFromJson<T>(string saveFilePath)
        {
            string path = Path.Combine(Application.persistentDataPath , saveFilePath);
            try
            {
                string json = File.ReadAllText(path);
                T data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (System.Exception e)
            {
                #if UNITY_EDITOR
                Debug.LogError($"从{path}中读取数据时发生错误\n错误原因：{e}");
                #endif
                throw e;
            }
        }
        #endregion
       


       




      
    }

}
