#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimationClipManager
{
    [MenuItem("Assets/Animation/Create Anim Clip", false, 1)]
    static void Create()
    {
        var animController = Selection.activeObject;

        if (animController.GetType() != typeof(AnimatorController))
        {
            Debug.LogWarning("Could not create because the selected object is not an animator controller.");
            return;
        }

        var animClip = new AnimationClip() {name = AnimationClipNameSetter.clipName};
        AssetDatabase.AddObjectToAsset(animClip, animController);

        string animClipPath = AssetDatabase.GetAssetPath(animClip);

        // This script changes the "*.controller" asset file
        // so until it is saved, the changes won't be displayed on the editor view.
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Animation/Delete Anim Clip", false, 1)]
    static void Delete()
    {
        var animClip = Selection.activeObject;

        if(animClip.GetType() != typeof(AnimationClip))
        {
            Debug.LogWarning("Could not delete because the selected object is not an animator clip.");
            return;
        }

        AssetDatabase.RemoveObjectFromAsset(animClip);

        // See line 25
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Animation/Rename Anim Clip", false, 3)]
    static void Rename()
    {
        var animClip = Selection.activeObject;

        if (animClip.GetType() != typeof(AnimationClip))
        {
            Debug.LogWarning("Could not rename because the selected object is not an animator clip.");
            return;
        }

        animClip.name = AnimationClipNameSetter.clipName;
        AssetDatabase.SaveAssets();
    }
}
#endif