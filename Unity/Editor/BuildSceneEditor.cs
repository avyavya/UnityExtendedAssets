using UnityEngine;
using UnityEditor;
using System.Linq;


namespace ExtendedAssets.Unity.Editor
{
    public static class BuildSceneEditor
    {
        [MenuItem("Assets/Add Scene to Build", false, 999)]
        private static void AddScenesForBuild()
        {
            var addition = false;
            var scenes = EditorBuildSettings.scenes.ToList();

            foreach (var guid in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                if (!path.EndsWith(".unity"))
                {
                    Debug.LogWarning(path + " is ignored due to not a Scene.");
                    continue;
                }

                if (IsBuildScenesContainsPath(path))
                {
                    Debug.LogWarning(path + " is ignored due to already included.");
                    continue;
                }

                var s = new EditorBuildSettingsScene(path, true);
                scenes.Add(s);
                addition = true;
                Debug.Log(path + " is added into build scenes.");
            }

            if (!addition) return;

            EditorBuildSettings.scenes = scenes.ToArray();
        }

        [MenuItem("Assets/Add Scene to Build", true)]
        private static bool IsSelected()
        {
            return Selection.assetGUIDs.Length > 0;
        }

        private static bool IsBuildScenesContainsPath(string path)
        {
            return EditorBuildSettings.scenes.Any(scene => scene.path == path);
        }
    }
}
