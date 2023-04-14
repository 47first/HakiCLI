﻿namespace Runtime
{
    public sealed class SetRoomSideObjectCommand: ICommand
    {
        public void Execute(CommandContext context)
        {
            if (context.Subject is not MazeRoom mazeRoom)
                return;

            if(TryGetSideByArgs(context.Args, out RoomSide roomSide))
                context.Subject = mazeRoom.GetObjectBySide(roomSide);
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
                "r" => RoomSide.Right,
                "l" => RoomSide.Left,
                "b" => RoomSide.Backward,
                "f" => RoomSide.Forward,
                _ => throw new ArgumentException("Undefined side")
            };
        }
    }
}
