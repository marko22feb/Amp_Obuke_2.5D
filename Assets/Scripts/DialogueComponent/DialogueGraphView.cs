using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    public readonly Vector2 nodeSize = new Vector2(300, 200);

    public DialogueGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GetEntryNode()); 
    }

    private DialogueNode CreateNode(string nodeTitle, Vector2 position)
    {
        DialogueNode node = new DialogueNode()
        {
            title = nodeTitle,
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "New Dialogue",
            EntryNode = false
        };

        Port generatedPort = GetPortInstance(node, Direction.Input, Port.Capacity.Multi);
        generatedPort.portName = "Input";
        node.outputContainer.Add(generatedPort);

        string ChoiceDialogueText = "New Choice";

        TextField ButtontextField = new TextField("Choice Here");
        ButtontextField.RegisterValueChangedCallback(evt =>
        {
            ChoiceDialogueText = evt.newValue;
        });

        ButtontextField.SetValueWithoutNotify(ChoiceDialogueText);
        node.titleContainer.Add(ButtontextField);

        Button choiceButton = new Button(clickEvent: () => { AddChoice(node, ChoiceDialogueText == "New Choice" ? "" : ChoiceDialogueText); });
        choiceButton.text = "Add Choice";
        node.titleButtonContainer.Add(choiceButton);

        TextField textField = new TextField("Dialogue Here");
        textField.RegisterValueChangedCallback(evt =>
        {
            node.DialogueText = evt.newValue;
            node.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(node.DialogueText);
        node.mainContainer.Add(textField);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(position.x, position.y, 200, 400));

        return node;
    }

    public void AddChoice(DialogueNode node, string choiceName = "")
    {
        Port generatedPort = GetPortInstance(node, Direction.Output);
        Label portLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(portLabel);

        int outputCount = node.outputContainer.Query("connector").ToList().Count();
        string outputName = string.IsNullOrEmpty(choiceName) ? $"Choice {outputCount}" : choiceName;

        TextField textField = new TextField()
        {
            name = string.Empty,
            value = outputName
        };

        textField.RegisterValueChangedCallback(evt =>
        {
            generatedPort.portName = evt.newValue;
        });

        generatedPort.contentContainer.Add(new Label(" "));
        generatedPort.contentContainer.Add(textField);

        IntegerField eventTextField = new IntegerField()
        {
            name = string.Empty,
            value = -1
        };

        eventTextField.RegisterValueChangedCallback(evt =>
        {
            eventTextField.value = evt.newValue;
        });

        generatedPort.contentContainer.Add(eventTextField);
        generatedPort.contentContainer.Add(new Label("Event Index"));


        IntegerField ConditionTextField = new IntegerField()
        {
            name = string.Empty,
            value = -1
        };

        eventTextField.RegisterValueChangedCallback(evt =>
        {
            eventTextField.value = evt.newValue;
        });

        generatedPort.contentContainer.Add(ConditionTextField);
        generatedPort.contentContainer.Add(new Label("Condition Index"));

        Button deleteChoiceButton = new Button(clickEvent: () => { RemoveChoice(node, generatedPort); });
        deleteChoiceButton.text = "X";

        generatedPort.contentContainer.Add(deleteChoiceButton);

        generatedPort.portName = outputName;
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

    }

    private void RemoveChoice(DialogueNode node, Port choicePort)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == choicePort.portName && x.output.node == choicePort.node);

        if (targetEdge.Any())
        {
            Edge edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        node.outputContainer.Remove(choicePort);
        node.RefreshExpandedState();
        node.RefreshPorts();
    }

    public void CreateNewDialogueNode(string nodeTitle, Vector2 position)
    {
        AddElement(CreateNode(nodeTitle, position));
    }

    private DialogueNode GetEntryNode()
    {
        DialogueNode node = new DialogueNode()
        {
            title = "Entry",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "First NPC Dialogue",
            EntryNode = true
        };

        Port generatedPort = GetPortInstance(node, Direction.Output);
        generatedPort.portName = "NextDialogue";

        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 200));

        return node;
    } 

    private Port GetPortInstance(DialogueNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        Port startPortView = startPort;

        ports.ForEach((port) =>
        {
            Port portView = port;
            if (startPortView != portView && startPortView.node != portView.node) compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }
}
