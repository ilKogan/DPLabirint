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
            EnchantedMazeFactory enchantedMazeFactory = new EnchantedMazeFactory();
            BombedMazeFactory bombMazeFactory = new BombedMazeFactory();
            MazeGame game = new MazeGame();
            Maze currentMaze = game.CreateMaze(enchantedMazeFactory);
            currentMaze = game.CreateMaze(bombMazeFactory);
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
        MazeFactory _Factory;

        public Maze CreateMaze(MazeFactory factory)
        {
            _Factory = factory;
            Maze maze = factory.MakeMaze();
            Room r1 = factory.MakeRoom(1);
            Room r2 = factory.MakeRoom(2);
            Door door = factory.MakeDoor(r1,r2);

            maze.AddRoom(r1);
            maze.AddRoom(r2);

            r1.SetSide(Direction.North, factory.MakeWall());
            r1.SetSide(Direction.East, door);
            r1.SetSide(Direction.South, factory.MakeWall());
            r1.SetSide(Direction.West, factory.MakeWall());

            r2.SetSide(Direction.North, factory.MakeWall());
            r2.SetSide(Direction.East, factory.MakeWall());
            r2.SetSide(Direction.South, factory.MakeWall());
            r2.SetSide(Direction.West, door);

            return maze;
        }
    }

    class MazeFactory
    {
        public virtual Maze MakeMaze()
        {
            return new Maze();
        }
        public virtual Room MakeRoom(int number)
        {
            return new Room(number);
        }
        public virtual Wall MakeWall()
        {
            return new Wall();
        }
        public virtual Door MakeDoor(Room room1,Room room2)
        {
            return new Door(room1,room2);
        }
    }
//--------------------------------------

    class EnchantedMazeFactory : MazeFactory
    {
        public override Room MakeRoom(int number)
        {
            return new EnchantedRoom(number,SpawnEnemy());
        }

        protected Enemy SpawnEnemy()
        {
            return null;
        }
    }

    class Enemy
    {
    }

    class EnchantedRoom : Room
    {
        private Enemy _Enemy;

        public EnchantedRoom(int number):base(number)
        {
        }

        public EnchantedRoom(int number, Enemy enemy):base(number)
        {
            _Enemy = enemy;
        }
    }
//--------------------------------------
    class BombedWall : Wall {}

    class RoomWhithBomb : Room
    {
        public RoomWhithBomb(int roomNumber) : base(roomNumber)
        {

        }
    }

    class BombedMazeFactory : MazeFactory
    {
        public override Wall MakeWall()
        {
            return new BombedWall();
        }

        public override Room MakeRoom(int number)
        {
            return new RoomWhithBomb(number);
        }
    }
}
