using System.ComponentModel.Design;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimClipAdder
{
    [MenuItem("Assets/Animation/Create Anim Clip", false, 1)]
    static void Create()
    {
        var selection = Selection.activeObject;
        Debug.Log(selection.GetType());
        if (selection.GetType() != typeof(AnimatorController)) return;

        var animClip = new AnimationClip() {name = "clip name"};
        AssetDatabase.AddObjectToAsset(animClip, selection);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(animClip));
    }

    [MenuItem("Assets/Animation/Delete Anim Clip", false, 1)]
    static void Delete()
    {
        var selection = Selection.activeObject;
        Debug.Log(selection.GetType());
        if(selection.GetType() != typeof(AnimationClip)) return;

        AssetDatabase.RemoveObjectFromAsset(selection);
        AssetDatabase.Refresh();
    }
}