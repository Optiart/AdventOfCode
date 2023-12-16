namespace Tasks.Shared
{
    public struct Movement
    {
        public const char Up = '^';

        public const char Down = 'V';

        public const char Left = '<';

        public const char Right = '>';

        public Point Position { get; set; }

        public char Direction { get; set; }

        public Movement(Point position, char direction)
        {
            Position = position;
            Direction = direction;
        }

        public Movement MoveUp() => new(new Point(Position.I - 1, Position.J), Up);

        public Movement MoveDown() => new(new Point(Position.I + 1, Position.J), Down);

        public Movement MoveLeft() => new(new Point(Position.I, Position.J - 1), Left);

        public Movement MoveRight() => new(new Point(Position.I, Position.J + 1), Right);

        public override string ToString() => $"{Position} {Direction}";
    }
}
