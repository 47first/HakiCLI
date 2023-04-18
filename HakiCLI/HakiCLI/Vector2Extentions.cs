using System.Numerics;

namespace Runtime
{
    public static class Vector2Extensions
    {
        public static RoomSide GetRelativeSide(Vector2 posFrom, Vector2 posTo)
        {
            var relativePosition = posTo - posFrom;

            return GetSide(relativePosition);
        }

        public static RoomSide GetSide(Vector2 position)
        {
            if (position == new Vector2(1, 0))
                return RoomSide.Right;

            if (position == new Vector2(-1, 0))
                return RoomSide.Left;

            if (position == new Vector2(0, 1))
                return RoomSide.Forward;

            if (position == new Vector2(0, -1))
                return RoomSide.Backward;

            throw new ArgumentException($"{position}");
        }

        public static Vector2 GetRandomDirection()
        {
            Random rnd = new();

            var isNewDirByX = rnd.Next(2) == 1;
            var isDirPositive = rnd.Next(2) == 1;

            return isNewDirByX switch
            {
                true => new Vector2(isDirPositive ? 1 : -1, 0),
                false => new Vector2(0, isDirPositive ? 1 : -1)
            };
        }
    }
}
