using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
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

    private void ConstructToolBar()
    {
        Toolbar toolbar = new Toolbar();

        Button CreateNewNodeButton = new Button(clickEvent: () => { graphView.CreateNewDialogueNode("New Dialogue", new Vector2(300, 300)); });
        CreateNewNodeButton.text = "Create Node";
        toolbar.Add(CreateNewNodeButton);

        rootVisualElement.Add(toolbar);
    }

    public void OnEnable()
    {
        ConstructGraphView();
        ConstructToolBar();
    }

    public void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}
