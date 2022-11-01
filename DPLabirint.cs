using System;

namespace DPLabirint
{
    enum Direction
    {
        North,
        South,
        East,
        West
    }

    class DPLabirint
    {
        static void Main(string[] args)
        {
            MazeGame game = new MazeGame();
            Maze currentMaze = game.CreateMaze();
        }
    }

    public abstract class MapSide
    {
        public abstract void Enter();
    }

    class Room : MapSide
    {
        Dictionary<Direction,MapSide> _Sides;
        int _RoomNumber = 0;
        public int RoomNumber 
        { 
            get => _RoomNumber; 
            set => _RoomNumber = value; 
        }

        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
            _Sides = new Dictionary<Direction, MapSide>(4);
        }


        public override void Enter()
        {
            Console.WriteLine("Enter");
        }

        public MapSide GetSide(Direction direction)
        {
            return _Sides[direction];
        }

        public void SetSide(Direction direction, MapSide mapSide)
        {
            _Sides.Add(direction,mapSide);
        }
    }

    class Wall : MapSide
    {
        public Wall(){}

        public override void Enter()
        {
            Console.WriteLine("Wall");
        }
    }

    class Door : MapSide
    {
        Room room1;
        Room room2;
        bool _IsOpen;

        public Door(Room room1, Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }

        public override void Enter()
        {
            Console.WriteLine("Door");
        }

        public Room OtherSideFrom(Room room)
        {
            return room == room1 ? room2 : room1;
        }
    }

    class Maze
    {
        Dictionary<int,Room> _Rooms;

        public Maze()
        {
            _Rooms = new Dictionary<int, Room>();
        }

        public void AddRoom(Room room)
        {
            _Rooms.Add(room.RoomNumber,room);
        }

        public Room GetRoom(int number)
        {
            return _Rooms[number];
        }
    }

    class MazeGame
    {
        public Maze CreateMaze()
        {
            Maze maze = new Maze();
            Room r1 = new Room(1);
            Room r2 = new Room(2);
            Door door = new Door(r1,r2);

            maze.AddRoom(r1);
            maze.AddRoom(r2);

            r1.SetSide(Direction.North, new Wall());
            r1.SetSide(Direction.East, door);
            r1.SetSide(Direction.South, new Wall());
            r1.SetSide(Direction.West, new Wall());

            r2.SetSide(Direction.North, new Wall());
            r2.SetSide(Direction.East, new Wall());
            r2.SetSide(Direction.South, new Wall());
            r2.SetSide(Direction.West, door);

            return maze;
        }
    }
}
