namespace Runtime
{
    public sealed class SetRoomSideObjectCommand: ICommand
    {
        public void Execute(CommandContext context)
        {
            if (context.Subject is not Player player)
                return;

            if(TryGetSideByArgs(context.Args, out RoomSide roomSide))
                context.Subject = player.Destination.GetObjectBySide(roomSide);
        }

        private bool TryGetSideByArgs(string[] args, out RoomSide roomSide)
        {
            roomSide = RoomSide.Right;

            foreach (var arg in args)
            {
                try
                {
                    roomSide = GetSideByString(arg);

                    return true;
                }
                catch (ArgumentException ex)
                {
                    continue;
                }
            }

            return false;
        }

        private RoomSide GetSideByString(string sideString)
        {
            return sideString switch
            {
                "r" or "right" => RoomSide.Right,
                "l" or "left" => RoomSide.Left,
                "b" or "backward" => RoomSide.Backward,
                "f" or "forward" => RoomSide.Forward,
                _ => throw new ArgumentException("Undefined side")
            };
        }
    }
}
