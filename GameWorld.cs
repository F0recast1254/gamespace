using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Template
{

    public class GameWorld
    {
        private static GameWorld _instance = null;

        public static GameWorld Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameWorld();
                }
                return _instance;
            }
        }

        private Room _entrance;
        public Room Entrance { get { return _entrance; } }
        private Room _exit;


        private Dictionary<ITrigger, IGameEvent> _worldChanges;


        private GameWorld()
        {
            _worldChanges = new Dictionary<ITrigger, IGameEvent>();
            _entrance = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerWillEnterRoom", PlayerWillEnterRoom);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);

        }

        public void PlayerWillEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                //player.WarningMessage("Player will leave " + player.CurrentRoom.Tag);
            }
        }

        public void PlayerDidEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom == _exit)
                    player.InfoMessage("Player did arrive at the exit");
            }
            IGameEvent wc = null;
            _worldChanges.TryGetValue(player.CurrentRoom, out wc);

            if (wc != null)
            {
                wc.Execute(player);
            }
        }

        public Room CreateWorld()
        {
            Room foyer = new Room("in the foyer");
            Room kitchen = new Room("in the kitchen");
            Room livingroom = new Room("in a living area");
            Room diningroom = new Room("in a dining room");
            Room hallway = new Room("in the hallway");
            Room masterbedroom = new Room("in the master bedroom");
            Room bathroom = new Room("in the bathroom");
            Room closet = new Room("in the master bedroom closet");
            Room library = new Room("in a library");
            Room basement = new Room("in the basement");
            Room upstairs = new Room("in the upstairs hallway");
            Room guestbedroom = new Room("in what appears to be a guest bedroom");
            Room guestbathroom = new Room("in an upstairs bathroom");


            Door door = Door.Connect(foyer, livingroom, "west", "east");


            door = Door.Connect(livingroom, hallway, "north", "south");


            door = Door.Connect(hallway, masterbedroom, "north", "south");


            door = Door.Connect(foyer, kitchen, "east", "west");


            door = Door.Connect(kitchen, diningroom, "north", "south");


            door = Door.Connect(masterbedroom, closet, "north", "south");


            door = Door.Connect(masterbedroom, bathroom, "west", "east");
            ILockable bathroomlock = LockableFacade.MakeLockable("Regular", "key1");
            door.Close();
            door.Lock();
            IItem key = door.Remove();
            library.Drop(key);

            door = Door.Connect(diningroom, library, "north", "south");


            door = Door.Connect(library, basement, "north", "south");
            ILockable basementlock = LockableFacade.MakeLockable("Regular", "key1");
            door.Close();
            door.Lock();
            IItem key2 = door.Remove();
            bathroom.Drop(key2);



            door = Door.Connect(hallway, diningroom, "east", "west");

            // inaccessible part of the world

            door = Door.Connect(foyer, upstairs, "north", "south");


            door = Door.Connect(upstairs, guestbedroom, "north", "south");


            door = Door.Connect(upstairs, guestbathroom, "west", "east");

            // info to connect the parts of the world, Schuster will gain an exit (4th exit)
            // "unlocking" another part of the world
            // idea being once you enter the schuster center the Davidson is unlocked via Schuster's 
            // new west exit
            WorldChange wc = new WorldChange(closet, library, basement, "west", "east");
            _worldChanges[closet] = wc;

            IRoomDelegate basementtrap = new TrapRoom("basement");
            basement.RoomDelegate = basementtrap;
            // tr.ContainingRoom = scct;

            IRoomDelegate guesttrap = new TrapRoom("guest");
            guestbedroom.RoomDelegate = guesttrap;
            //tr.ContainingRoom = parkingDeck;
            IRoomDelegate er = new EchoRoom(3);
            kitchen.RoomDelegate = er;

            IItem guestitem1 = new Item("iPad", 0.75f);
            IItem decorator = new Item("cover", 0.25f);
            guestbedroom.Drop(guestitem1);
            guestitem1.AddDecorator(decorator);
            decorator = new Item("pen", 0.1f);
            guestitem1.AddDecorator(decorator);
            IItem kitchenitem1 = new Item("knife", 0.50f);
            kitchen.Drop(kitchenitem1);

            IItem masteritem1 = new Item("gun", 1.25f);
            masterbedroom.Drop(masteritem1);

            IItem bathroomitem1 = new Item("toothbrush", 0.10f);
            bathroom.Drop(bathroomitem1);





            _exit = hallway;



            return foyer;
        }
    }


}