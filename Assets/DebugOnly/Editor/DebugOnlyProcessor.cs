using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugOnlyProcessor : IProcessScene
{
    public const string debugOnlyTag = "DebugOnly";

    public int callbackOrder { get; private set; }

    public void OnProcessScene(Scene scene)
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