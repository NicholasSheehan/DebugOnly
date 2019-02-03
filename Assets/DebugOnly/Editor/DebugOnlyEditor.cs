using UnityEditor;
using UnityEngine;

namespace DebugOnly
{
    [InitializeOnLoad]
    public class DebugOnlyEditor : Editor
    {
        /// <summary>
        /// Adds the DebugOnly tag to the tag manager
        /// </summary>
        static DebugOnlyEditor()
        {
            //Loads the TagManager asset
            var tagManagerAssetObject = AssetDatabase.LoadAssetAtPath<Object>("ProjectSettings/TagManager.asset");
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

            //Insert entry into tag array
            tagsProperty.InsertArrayElementAtIndex(0);
            var newTagIndex = tagsProperty.GetArrayElementAtIndex(0);

            //Set the tag value
            newTagIndex.stringValue = DebugOnlyProcessor.debugOnlyTag;

            //Save
            tagManagerAsset.ApplyModifiedProperties();
        }
    }
}
