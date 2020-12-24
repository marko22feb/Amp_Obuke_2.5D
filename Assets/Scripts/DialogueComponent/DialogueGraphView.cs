using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    private DialogueNode GetEntryNode()
    {
        DialogueNode tempData = new DialogueNode()
        {
            title = "Entry",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "First NPC Dialogue",
            EntryNode = true
        };

        Port generatedPort = GetPortInstance(tempData, Direction.Output);
        generatedPort.portName = "NextDialogue";

        tempData.outputContainer.Add(tempData);

      //  tempData.RefreshExpandedState();
      //  tempData.RefreshPorts();

        tempData.SetPosition(new Rect(100, 200, 100, 200));

        return tempData;
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
