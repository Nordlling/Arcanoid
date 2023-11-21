using System.Collections.Generic;
using Main.Scripts.Configs;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Editor
{
    [CustomEditor(typeof(ScenesConfig))]
    public class SceneNamesEditor : UnityEditor.Editor
    {
        private ScenesConfig _scenesConfig;

        public override void OnInspectorGUI()
        {
	        _scenesConfig = (ScenesConfig)target;

            DrawDefaultInspector();
            DisplayButtons();
        }

        private void DisplayButtons()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, fixedHeight = 30 };

            if (GUILayout.Button("Fill Scene Names", buttonStyle))
            {
                FillSceneNames();
            }
        }

        private void FillSceneNames()
        {
            List<string> sceneNames = new List<string>();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                sceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(scene.path));
            }

	        _scenesConfig.SceneNames = sceneNames;
        }
        
    }
}