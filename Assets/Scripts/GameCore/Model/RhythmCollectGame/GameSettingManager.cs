using System;
using System.Collections.Generic;
using UnityEditor;

namespace GameCore
{
    public class GameSettingManager
    {
        private static string[] SETTING_FOLDER_PATH = new string[] { @"Assets/Settings/ScriptableObject" };
        private Dictionary<Type, object> dict_gameDifficultyEvaluator;

        private void BuildGameSettingReference<T>()
        {
            string[] settingFileNames = AssetDatabase.FindAssets("t:scriptableobject", SETTING_FOLDER_PATH);
            foreach (string fileName in settingFileNames)
            {
                string path = AssetDatabase.GUIDToAssetPath(fileName);
                object settingObj = AssetDatabase.LoadAssetAtPath(path, typeof(IGameSettingGetter<T>));

                if (settingObj != null)
                {
                    dict_gameDifficultyEvaluator[typeof(T)] = settingObj;
                    return;
                }
            }

            dict_gameDifficultyEvaluator[typeof(T)] = null;
        }

        public T GetSetting<T>(int difficulty)
        {
            if (dict_gameDifficultyEvaluator == null)
                dict_gameDifficultyEvaluator = new Dictionary<Type, object>();

            if (dict_gameDifficultyEvaluator.ContainsKey(typeof(T)) == false)
                BuildGameSettingReference<T>();

            object settingObj = dict_gameDifficultyEvaluator[typeof(T)];
            IGameSettingGetter<T> gameSettingGetter = (IGameSettingGetter<T>)settingObj;
            return gameSettingGetter.GetGameSetting(difficulty);
        }
    }
}