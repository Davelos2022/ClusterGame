using UnityEngine;
using UnityEditor;
using MulticastProject.Scr;
using Cysharp.Threading.Tasks;
using MulticastProject.Utils;
using MulticastProject.Data;

namespace MulticastProject
{
    [CustomEditor(typeof(LevelConfig))]
    public class LevelConfigEditor : Editor
    {
        private const int PADDING_SPACE = 30;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(PADDING_SPACE);
            LevelConfig config = (LevelConfig)target;

            if (GUILayout.Button("Save Config as JSON"))
                SaveConfiguration(config).Forget();
        }

        private async UniTaskVoid SaveConfiguration(LevelConfig config)
        {
            await JSONService.SaveJSONAsync(NameDataSystem.DIRECTORY_LEVEL, NameDataSystem.NAME_LEVEL_JSON_FILE, config.LevelData, TypeTargetJson.Resources);
        }
    }
}