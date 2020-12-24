using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView graphView;

    [MenuItem("Graph/Dialogue Graph")]
    public static void CreateGraphViewWindow() 
    {
        EditorWindow window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void ConstructGraphView()
    {
        graphView = new DialogueGraphView { name = "Dialogue Graph" };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    public void OnEnable()
    {
        ConstructGraphView();
    }

    public void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}
