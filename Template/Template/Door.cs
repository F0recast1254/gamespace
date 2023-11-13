using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class Door : ICloseable ,ILockable
    {
        private Room _room1;
        private Room _room2;

        private bool _closed;
        private ILockable _lockable;
        public ILockable Lockable { get { return _lockable; } set { _lockable = value; } }
        public bool IsLocked { get { return _lockable == null? false: _lockable.IsLocked; } }
        public bool IsUnlocked { get { return _lockable == null ? true : _lockable.IsUnlocked; } }
        public bool Lock()
        {
            return _lockable == null ? false : _lockable.Lock();
        }
        public bool Unlock()
        {
            return _lockable == null ? false: _lockable.Unlock();
        }
        public bool CanLock { get { return _lockable == null ? false : _lockable.CanLock; }  }
        public bool CanUnlock { get { return _lockable == null ? false : _lockable.CanUnlock; } }
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
        public bool CanClose { get { return _lockable == null ? true : _lockable.CanClose; } }
        public bool CanOpen { get { return _lockable == null ? true : _lockable.CanOpen; } }
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
    } }
