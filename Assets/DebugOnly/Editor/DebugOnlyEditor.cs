using UnityEditor;

[InitializeOnLoad]
public class DebugOnlyEditor : Editor
{
    /// <summary>
    /// Sets the debug tag
    /// </summary>
    static DebugOnlyEditor()
    {
        //there is no type to cast to, so we just get all tag manager assets, in which there is only one, and load it as a serializedObject
        var tagManagerAssetObject = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0];
        var tagManagerAsset = new SerializedObject(tagManagerAssetObject);
        var tagsProperty = tagManagerAsset.FindProperty("tags");

        //First check if the tag is already present
        var foundDebugTag = false;
        for (var i = 0; i < tagsProperty.arraySize; i++)
        {
            var tag = tagsProperty.GetArrayElementAtIndex(i);
            if (tag.stringValue.Equals(DebugOnlyProcessor.debugOnlyTag))
            {
                foundDebugTag = true;
                break;
            }
        }

        //if the tag was found, then we don't need to add it
        if (foundDebugTag) return;

        //insert entry into tag list, and then set the tag value, then save
        tagsProperty.InsertArrayElementAtIndex(0);

        var newTagIndex = tagsProperty.GetArrayElementAtIndex(0);
        newTagIndex.stringValue = DebugOnlyProcessor.debugOnlyTag;

        tagManagerAsset.ApplyModifiedProperties();
    }
}
