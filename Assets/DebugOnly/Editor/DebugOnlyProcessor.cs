using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_2018_1_OR_NEWER
//2018.1 and newer
public class DebugOnlyProcessor : IProcessSceneWithReport
#else
//2017.4 is the last version of Unity to support this interface, has been replaced with the above interface
public class DebugOnlyProcessor : IProcessScene
#endif
{
    public const string debugOnlyTag = "DebugOnly";

    public int callbackOrder { get; private set; }

#if UNITY_2018_1_OR_NEWER
    public void OnProcessScene(Scene scene, UnityEditor.Build.Reporting.BuildReport report)
#else
    public void OnProcessScene(Scene scene)
#endif
    {
#if DEBUG
        return;
#endif
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