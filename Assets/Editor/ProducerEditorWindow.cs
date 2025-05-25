#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ProducerEditorWindow : EditorWindow
{
    private List<ProducerData> producers = new List<ProducerData>();
    private Vector2 scrollPos;

    [MenuItem("Tools/Producer Editor")]
    public static void ShowWindow()
    {
        GetWindow<ProducerEditorWindow>("Producer Editor");
    }

    private void OnEnable()
    {
        LoadAllProducers();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        if (GUILayout.Button("Yeni Üretici Oluþtur", GUILayout.Height(30)))
        {
            CreateNewProducer();
        }

        GUILayout.Space(10);
        scrollPos = GUILayout.BeginScrollView(scrollPos);

        foreach (var producer in producers)
        {
            if (producer == null) continue;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Adý:", producer.producerName);
            EditorGUILayout.LabelField("Gelir:", producer.baseIncome.ToString());
            EditorGUILayout.LabelField("Süre:", producer.productionInterval.ToString());
            EditorGUILayout.LabelField("Upgrade Maliyeti:", producer.upgradeCost.ToString());

            if (producer.icon != null)
                GUILayout.Label(producer.icon.texture, GUILayout.Width(64), GUILayout.Height(64));

            if (GUILayout.Button("Seç & Düzenle"))
            {
                Selection.activeObject = producer;
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
        }

        GUILayout.EndScrollView();
    }

    private void LoadAllProducers()
    {
        producers.Clear();
        string[] guids = AssetDatabase.FindAssets("t:ProducerData");
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ProducerData data = AssetDatabase.LoadAssetAtPath<ProducerData>(path);
            if (data != null)
                producers.Add(data);
        }
    }

    private void CreateNewProducer()
    {
        string path = "Assets/Resources/ProducerData";
        if (!AssetDatabase.IsValidFolder(path))
            AssetDatabase.CreateFolder("Assets/Resources", "ProducerData");

        ProducerData newProducer = ScriptableObject.CreateInstance<ProducerData>();
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/YeniUretici.asset");
        AssetDatabase.CreateAsset(newProducer, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeObject = newProducer;

        LoadAllProducers();
    }
}
#endif
