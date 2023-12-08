using Tasks.Helpers;

namespace Tasks.DayEight
{
    public class HauntedWasteland
    {
        private readonly Node[] _nodes;
        private readonly Dictionary<string, Node> _nodeMap;
        private readonly string _directions;

        public HauntedWasteland(string[] inputLines)
        {
            _directions = inputLines[0].Trim();
            _nodes = new Node[inputLines.Length - 2];
            _nodeMap = new();

            for (var i = 2; i < inputLines.Length; i++)
            {
                var id = inputLines[i][..3];
                _nodeMap[id] = new Node()
                {
                    Id = id
                };
            }

            for (var i = 2; i < inputLines.Length; i++)
            {
                var id = inputLines[i][..3];
                var leftChildNodeId = inputLines[i][7..10];
                var rightChildNodeId = inputLines[i][12..15];

                var node = _nodeMap[id];
                node.Left = _nodeMap[leftChildNodeId];
                node.Right = _nodeMap[rightChildNodeId];

                _nodes[i - 2] = node;
            }
        }

        public int FindStepsToDestination(string startingNode = "AAA", string destinationNode = "ZZZ")
        {
            var queue = new Queue<Node>();
            var directionIndex = 0;
            var steps = 0;

            queue.Enqueue(_nodeMap[startingNode]);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Id == destinationNode)
                {
                    return steps;
                }

                var direction = _directions[directionIndex];
                var nextNode = SelectNode(node, direction);

                queue.Enqueue(nextNode);
                steps++;
                directionIndex++;

                if (directionIndex == _directions.Length)
                {
                    directionIndex = 0;
                }
            }

            return steps;
        }

        public long FindStepsToDestinationAsGhost(char startingNodeEndsWith = 'A', char destinationNodeEndsWith = 'Z')
        {
            var queue = new Queue<(Node node, int level)>();
            var directionIndex = 0;
            long steps = 0;
            var currentLevel = 0;

            foreach (var node in _nodes)
            {
                if (node.Id[2] == startingNodeEndsWith)
                {
                    queue.Enqueue((node, currentLevel));
                }
            }

            var ghostCount = queue.Count;
            var ghostsReachedDestination = 0;

            var maxSoFar = 0;
            while (queue.Count > 0)
            {
                if (ghostsReachedDestination == ghostCount)
                {
                    return steps;
                }

                if (currentLevel != queue.Peek().level)
                {
                    // All ghosts are done the level
                    steps++;
                    directionIndex++;
                    if (directionIndex == _directions.Length)
                    {
                        directionIndex = 0;
                    }

                    currentLevel++;
                    ghostsReachedDestination = 0;
                }

                var path = queue.Dequeue();
                if (path.node.Id[2] == destinationNodeEndsWith)
                {
                    ghostsReachedDestination++;
                    maxSoFar = Math.Max(maxSoFar, ghostsReachedDestination);
                }

                var direction = _directions[directionIndex];
                var nextNode = SelectNode(path.node, direction);
                queue.Enqueue((nextNode, currentLevel + 1));

                if (steps == long.MaxValue)
                {
                    throw new Exception();
                }
            }

            return steps;
        }

        public long FindStepsToDestinationAsGhostViaLcm(char startingNodeEndsWith = 'A', char destinationNodeEndsWith = 'Z')
        {
            var queue = new Queue<Node>();
            var ghostNodes = _nodes.Where(node => node.Id[2] == startingNodeEndsWith).ToArray();
            var stepsPerGhost = new int[ghostNodes.Length];

            for (int i = 0; i < ghostNodes.Length; i++)
            {
                int steps = 0;
                var directionIndex = 0;
                queue.Enqueue(ghostNodes[i]);

                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (node.Id[2] == destinationNodeEndsWith)
                    {
                        stepsPerGhost[i] = steps;
                        break;
                    }

                    var direction = _directions[directionIndex];
                    var nextNode = SelectNode(node, direction);

                    queue.Enqueue(nextNode);
                    steps++;
                    directionIndex++;

                    if (directionIndex == _directions.Length)
                    {
                        directionIndex = 0;
                    }
                }
            }

            return MathHelper.CalculateLeastCommonMultiple(stepsPerGhost);
        }

        private static Node SelectNode(Node node, char direction) => 
            direction == 'L' ? node.Left : node.Right;

        public class Node
        {
            public string Id { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }
        }
    }
}
