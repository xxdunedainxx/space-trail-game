using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.scripts.core
{
    public class EventTree
    {
        public EventTreeNode head;
        public EventTreeNode currentEventIndex;
        public bool eventTreeComplete = false;

        public EventTree()
        {
        }
    }

    public class EventTreeNode
    {
        public List<EventTreeNode> children = null;
        public EventTreeNode next;
        public EventTreeNode previous;
        public IEvent associatedEvent;
        public EventTreeNode parent;
        public string identifier;

        public EventTreeNode(EventTreeNode parent = null)
        {
            this.parent = parent;
        }
    }
}