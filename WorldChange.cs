using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Template 
{

     // connection to another part of the world (triggered when Party gets into the trigger room then we make the connections)
    public class WorldChange : IGameEvent
    {
        private bool triggered;
        private ITrigger _trigger;
        private Room _inWorldRoom;
        private Room _outWorldRoom;
        private string _inOutDirection;
        private string _outInDirection;

        public Room inWorldRoom {get {return _inWorldRoom;}}


        public WorldChange (ITrigger trigger, Room inWorldRoom, Room outWorldRoom,
        string inOutDirection, string outInDirection)
        {
            _trigger = trigger;
            _inWorldRoom = inWorldRoom;
            _outWorldRoom = outWorldRoom;
            _inOutDirection = inOutDirection;
            _outInDirection = outInDirection;
            triggered = false;
        }

        public void Execute(Player player) 
        {
            Door door = Door.Connect(_inWorldRoom, _outWorldRoom, _inOutDirection, _outInDirection);
            player.InfoMessage("There is a new location " + _inWorldRoom.Tag);
            triggered = true;
        }
    }
}