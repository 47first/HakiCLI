namespace Runtime
{
    public sealed class SetRoomSideObjectCommand: ICommand
    {
        public void Execute(CommandContext context)
        {
            if (context.Subject is MazeRoom mazeRoom == false ||
                IsArgsSuit(context.Args) == false)
                return;

            var argsSide = GetSideByArgs(context.Args);

            context.Subject = mazeRoom.GetObjectBySide(argsSide);
        }

        private bool IsArgsSuit(string[] args)
        {
            foreach (var arg in args)
            {
                if(arg == "e" || arg == "w" || arg == "s" || arg == "n")
                    return true;
            }

            return false;
        }

        private RoomSide GetSideByArgs(string[] args)
        {
            foreach (var arg in args)
            {
                try
                {
                    var side = GetSideByString(arg);

                    return side;
                }
                catch(ArgumentException ex)
                {
                    continue;
                }
            }

            throw new InvalidOperationException("No side have found");
        }

        private RoomSide GetSideByString(string sideString)
        {
            return sideString switch
            {
                "e" => RoomSide.East,
                "w" => RoomSide.West,
                "s" => RoomSide.South,
                "n" => RoomSide.North,
                _ => throw new ArgumentException("Undefined side")
            };
        }
    }
}
