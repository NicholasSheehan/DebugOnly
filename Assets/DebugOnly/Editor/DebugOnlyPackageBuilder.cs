using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DebugOnly
{
    /// <summary>
    /// Builds the DebugOnly package, also used by CI
    /// </summary>
    public static class DebugOnlyPackageBuilder
    {
        /// <summary>
        /// Builds the package
        /// </summary>
        [MenuItem("Tools/DebugOnly/Build Package")]
        public static void Build()
        {
            var allAssetsInProject = AssetDatabase.GetAllAssetPaths().ToList();
            //Remove all assets that are not in the assets folder from the package
            allAssetsInProject.RemoveAll(path => !path.StartsWith("Asset"));

            //Remove the assets folder, if not, then there will an empty Assets folder at the top of the import package tree view
            allAssetsInProject.Remove("Assets");
            //Remove this script from the package as well, as this is used to build
            allAssetsInProject.Remove("Assets/DebugOnly/Editor/DebugOnlyPackageBuilder.cs");

            //Saves the package to the project root
            var exportPath = Application.dataPath.Replace("Assets", "") + "DebugOnly.unitypackage";
            AssetDatabase.ExportPackage(allAssetsInProject.ToArray(), exportPath);
            Debug.Log("Exported Package To: " + exportPath);
        }
    }
}
