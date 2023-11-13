using System.Collections;
using System.Collections.Generic;
using System;

namespace Template
{
    /*
     * Spring 2023
     */
    public class Player
    {
        private Room _currentRoom = null;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

        public Player(Room room)
        {
            _currentRoom = room;
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
            if(door != null)
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
            if (door != null){
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
            NormalMessage("<<<"+word+">>>");
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerDidShoutAWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
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
            if(item != null)
            {
                InfoMessage("Item description:  "+ item.Description);
                CurrentRoom.Drop(item);
            }
            else
            {
                WarningMessage("There is no item named: " + itemName+" in the room.");
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
