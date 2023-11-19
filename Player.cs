using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Template
{
    /*
     * Spring 2023
     */
    public class Player : IStats
    {

        private Stack<Room> _rooms = new Stack<Room>();
        private Room _currentRoom = null;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

        private IStats characterinfo;

        public int health {  get; set; }

        public int attack { get; set; }

        public int defense {  get; set; }

        public int maxHealth { get; set; }
        public void Levelup()
        {
            int randomnumber;            
            Random rng = new Random();
            for (int i = 0; i < 3; i++)
            {
                randomnumber = rng.Next(1, 3);
                switch (randomnumber)
                {
                    case 1:
                        maxHealth += 10;
                        break;
                    case 2:
                        attack += 5;
                        break;
                    case 3:
                        defense += 5;
                        break;

                }
            }
            InfoMessage("Player stats:\nMax Health: " + maxHealth + "\nAttack: " + attack + "\nDefense: " + defense);

        }
        




        private IItem _hand;

        public Player(Room room)
        {
            _currentRoom = room;
            _hand = null;
            maxHealth = 10;
            health = 10;
            attack = 5;
            defense = 5;
        }
        public void Back()
        {
            CurrentRoom=_rooms.Pop();
            Notification notification = new Notification("PlayerDidEnterRoom", this);
            NotificationCenter.Instance.PostNotification(notification);
            NormalMessage("\n" + this.CurrentRoom.Description());
        }
        public void Pickup(string Itemname)
        {
            IItem item = CurrentRoom.Pickup(Itemname);
            if (item != null)
            {
                _hand = item;
                InfoMessage(_hand.Name + " has been added to your hand");
                Levelup();
            }
            else
            {
                WarningMessage("There is no item with this name in the room");
            }
        }

        public void Insert(string exitname)
        {
            Door door = CurrentRoom.GetExit(exitname);
            if (door != null)
            {
                if (_hand != null)
                {
                    door.Insert(_hand);
                    InfoMessage("You inserted " + _hand.Name + " into the door " + exitname);
                }
                else
                {
                    WarningMessage("You aren't holding anything!");
                }
            }
            else
            {
                WarningMessage("There is no door on " + exitname);
            }
        }

        public void Walkto(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpened)
                {
                    Notification notification = new Notification("PlayerWillEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    _rooms.Push(CurrentRoom);
                    CurrentRoom = door.RoomOnTheOtherSide(CurrentRoom);
                    notification = new Notification("PlayerDidEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    NormalMessage("\n" + this.CurrentRoom.Description());
                }
                else
                {
                    WarningMessage("\nThe door on " + direction + " is closed.");
                }
            }
            else
            {
                WarningMessage("\n There is no door on " + direction);
            }

        }
        public void Open(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {

                if (door.IsClosed)
                {
                    if (door.Open())
                    {
                        InfoMessage("The door on " + direction + " is now open.");
                    }
                    else
                    {
                        InfoMessage("The door on " + direction + " did not open.");
                    }
                }
                else
                {
                    InfoMessage("The door on " + direction + " is already open.");
                }
            }
            else
            {
                ErrorMessage("\nThere is no door on" + direction);
            }
        }
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void Unlock(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsLocked)
                {
                    if (door.Unlock())
                    {
                        InfoMessage("The door on " + direction + " is now unlocked (Remember to open!).");
                    }
                    else
                    {
                        InfoMessage("The door on " + direction + " did not unlock");
                    }
                }
            }
        }
        public void Shout(string word)
        {
            NormalMessage("<<<" + word + ">>>");
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerDidShoutAWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
            InfoMessage("The player shouts.");
        }
        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void Inspect(string itemName)
        {
            IItem item = CurrentRoom.Pickup(itemName);
            if (item != null)
            {
                InfoMessage("Item description:  " + item.Description);
                CurrentRoom.Drop(item);
            }
            else
            {
                WarningMessage("There is no item named: " + itemName + " in the room.");
            }
        }
        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }

}