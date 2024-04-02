using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    internal class ZenjectNodeInstaller : MonoInstaller
    {
        private static ZenjectNodeInstaller s_instance;
        private static Stack<Node> s_nodes = new Stack<Node>();  // Stack to hold nodes for future injection (static).

        public override void InstallBindings()
        {
            foreach (var node in s_nodes)
            {
                // Queue nodes for injection.
                Container.QueueForInject(node);
            }
            s_instance = this;
        }

        public static void InjectDependenciesOnNode(Node node)
        {
            if (s_instance == null)
            {
                if (s_nodes.Any(n => n == node))
                {
                    Debug.LogWarning($"Node {node.GetType().Name} is already in the injection queue.");
                    return;
                }

                // Add the node to the injection queue.
                s_nodes.Push(node);
            }
            else
            {
                // Inject dependencies immediately.
                s_instance.Container.Inject(node);
                Debug.LogWarning($"Immediately dependencies injected for node {node.GetType().Name}.");
            }
        }
    }
}