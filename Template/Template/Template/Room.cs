using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Template
{
    /*
     * Spring 2023
     */
    public class Room : ITrigger
    {
        private Dictionary<string, Door> _exits;
        private string _tag;
        public string Tag { get { return _tag; } set { _tag = value; } }
        private IRoomDelegate _roomDelegate;
        public IRoomDelegate RoomDelegate
        {
            get
            {
                return _roomDelegate;
            }
            set
            {
                if (value != null)
                {
                    Room oldRoom = value.ContainingRoom;
                    if (oldRoom != null)
                    {
                        oldRoom.RoomDelegate = null;
                    }
                    //_roomDelegate.ContainingRoom = null;
                    //oldRoom.RoomDelegate = null;
                }
                _roomDelegate = value;
                if (_roomDelegate != null)
                {
                    _roomDelegate.ContainingRoom = this;

                }

            }
        }
        private IItem _item;
        public Room() : this("No Tag") { }

        // Designated Constructor
        public Room(string tag)
        {
            _exits = new Dictionary<string, Door>();
            this.Tag = tag;
            this.RoomDelegate = null;
            _item = null;
        }

        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
            if (_roomDelegate != null)
            {
                _roomDelegate.RoomDidSetExit(exitName, door);
            }
        }

        public Door GetExit(string exitName)
        {
            Door door = null;
            _exits.TryGetValue(exitName, out door);
            door = _roomDelegate == null ? door :
                _roomDelegate.RoomDidGetExit(exitName, door);
            return door;
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }
            exitNames = _roomDelegate == null ? exitNames :
                _roomDelegate.RoomDidGetExits(exitNames);

            return exitNames;
        }

        public string Description()
        {

            string description = "You are " + this.Tag + ".\n *** " + this.GetExits() + "\nItems present: " + (_item == null ? "<.>" : _item.Name);

            return _roomDelegate == null ? description :
                _roomDelegate.RoomDidGetDescription(description);
        }
        public void Drop(IItem Item)
        {
            _item = Item;
        }
        public IItem Pickup(string itemName)
        {
            IItem itemToReturn = null;
            if (_item != null)
            {
                if (_item.Name.Equals(itemName))
                {
                    itemToReturn = _item;
                    _item = null;
                }
            }

            return itemToReturn;
        }
    }
    public class TrapRoom : IRoomDelegate
    {

        private string _password;
        private bool _disarmed;
        public Room ContainingRoom { get; set; }
        public TrapRoom(string password)
        {
            _disarmed = false;
            _password = password;
            NotificationCenter.Instance.AddObserver("PlayerDidShoutAWord", PlayerDidShoutAWord);

        }
        public void PlayerDidShoutAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom == ContainingRoom)
                {
                    Dictionary<string, Object> userInfo = notification.UserInfo;
                    string word = (string)userInfo["word"];
                    if (word != null)
                    {
                        if (word.Equals(_password))
                        {
                            _disarmed = true;
                            player.InfoMessage("You have disarmed the trap");
                            player.InfoMessage(player.CurrentRoom.Description());
                        }
                        else
                        {
                            player.WarningMessage("You didn't say the magic word.");
                        }
                    }
                }
            }
            Console.WriteLine("The player shouts.");
        }
        public void RoomDidSetExit(string exitName, Door door)
        {

        }

        public Door RoomDidGetExit(string exitName, Door door)
        {
            if (_disarmed)
            {
                return door;
            }
            else
            {
                return null;
            }
        }


        public string RoomDidGetExits(string exits)
        {
            if (_disarmed)
            {
                return exits;
            }
            else
            {
                return "Uh oh.";
            }
        }



        public string RoomDidGetDescription(string description)
        {
            if (_disarmed)
            {
                return description;
            }
            else
            {
                return "This is a trap room";
            }
        }
    }
    public class EchoRoom : IRoomDelegate
    {
        private int numberOfEchos;

        public EchoRoom(int numberOfEchos)
        {
            this.numberOfEchos = numberOfEchos;
            NotificationCenter.Instance.AddObserver("PlayerDidShoutAWord", PlayerDidShoutAWord);
        }
        public Room ContainingRoom { get; set; }
        public void RoomDidSetExit(string exitName, Door door)
        {
        }
        public Door RoomDidGetExit(string exitName, Door door)
        {
            return door;
        }

        public string RoomDidGetExits(string exits)
        {
            return exits;
        }
        public string RoomDidGetDescription(string description)
        {
            return "echo room\n" + description;
        }
        public void PlayerDidShoutAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom == ContainingRoom)
                {
                    Dictionary<string, Object> userInfo = notification.UserInfo;
                    string word = (string)userInfo["word"];
                    if (word != null)
                    {
                        string echos = "";
                        for (int i = 0; i < numberOfEchos; i++)
                        {
                            echos += "... " + word + "... ";
                        }
                        player.InfoMessage(echos);
                    }
                }
            }
        }


    }
}