using UnityEditor;
using UnityEngine;

public class TestWindow : EditorWindow
{
    [MenuItem("Fabrika/Test Pencere")]
    public static void ShowWindow() => GetWindow<TestWindow>("Test Pencere");
    private void OnGUI() => GUILayout.Label("✅ TestWindow çalışıyor!", EditorStyles.boldLabel);
}
