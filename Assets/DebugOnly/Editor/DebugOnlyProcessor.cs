using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DebugOnly
{
#if UNITY_2018_1_OR_NEWER
//2018.1 and newer
public class DebugOnlyProcessor : IProcessSceneWithReport
#else
    //2017.4 is the last version of Unity to support this interface, has been replaced with the above interface
    public class DebugOnlyProcessor : IProcessScene
#endif
    {
        /// <summary>
        /// Debug Only Tag
        /// </summary>
        public const string debugOnlyTag = "DebugOnly";

        /// <summary>
        /// Order in which the OnProcessScene methods should be run
        /// </summary>
        public int callbackOrder
        {
            get { return 0; }
        }

        /// <summary>
        /// Called when a scene is being processed
        /// </summary>
        /// <param name="scene">Scene being processed</param>
#if UNITY_2018_1_OR_NEWER
    /// <param name="report">Build Report</param>
    public void OnProcessScene(Scene scene, UnityEditor.Build.Reporting.BuildReport report)
#else
        public void OnProcessScene(Scene scene)
#endif
        {
            //Check if the "Development Build" checkbox is checked, if it is then return
            if (Debug.isDebugBuild) return;

            var sceneRootObjects = scene.GetRootGameObjects();

            foreach (var rootObject in sceneRootObjects)
            {
                var children = rootObject.GetComponentsInChildren<Transform>(true);

                foreach (var child in children)
                {
                    if (child.CompareTag(debugOnlyTag)) Object.DestroyImmediate(child.gameObject);
                }
            }
        }
    }
}
