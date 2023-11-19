using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class Door : ICloseable, ILockable
    {
        private Room _room1;
        private Room _room2;
        private bool _closed;
        private ILockable _locable;
        public ILockable Lockable { get { return _locable; } set { _locable = value; } }
        public IKeyed Keyed
        {
            get { return _locable == null ? null : _locable.Keyed; }
            set
            {
                if (_locable != null)
                {
                    _locable.Keyed = value;
                }
            }
        }
        public bool IsLocked { get { return _locable == null ? false : _locable.IsLocked; } }
        public bool IsUnlocked { get { return _locable == null ? true : _locable.IsUnlocked; } }
        public bool Lock()
        {
            return _locable == null ? false : _locable.Lock();
        }
        public bool Unlock()
        {
            return _locable == null ? false : _locable.Unlock();
        }
        public bool CanLock { get { return _locable == null ? false : _locable.CanLock; } }
        public bool CanUnlock { get { return _locable == null ? false : _locable.CanUnlock; } }
        public Door(Room room1, Room room2)
        {
            _room1 = room1;
            _room2 = room2;
            _closed = false;
        }

        public Room RoomOnTheOtherSide(Room MyRoom)
        {
            if (MyRoom == _room1)
            {
                return _room2;
            }
            else
            {
                return _room1;
            }
        }
        public static Door Connect(Room rooma, Room roomb, string labeltoroomB, string labeltorooma)
        {
            Door door = new Door(rooma, roomb);
            rooma.SetExit(labeltoroomB, door);
            roomb.SetExit(labeltorooma, door);
            return door;

        }
        public bool IsClosed { get { return _closed; } }

        public bool IsOpened { get { return !_closed; } }

        public bool Close()
        {
            bool result = false;
            if (_closed)
            {
                return false;
            }
            else
            {
                if (CanClose)
                {
                    _closed = true;
                    result = true;
                }

            }
            return result;
        }
        public bool CanClose { get { return _locable == null ? true : _locable.CanClose; } }
        public bool CanOpen { get { return _locable == null ? true : _locable.CanOpen; } }
        public bool Open()
        {
            bool result = false;
            if (_closed)
            {

            }
            if (CanOpen)
            {
                _closed = false;
                result = true;
            }
            return result;
        }


        public bool HasKey { get { return Lockable == null ? true : Lockable.HasKey; } }


        public IItem Insert(IItem key)
        {
            return Lockable == null ? key : Lockable.Insert(key);
        }

        public IItem Remove()
        {
            return Lockable == null ? null : Lockable.Remove();
        }
    }
}