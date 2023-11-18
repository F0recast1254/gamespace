using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public interface ITrigger
    {
    }

    public interface IGameEvent
    {
        public void Execute(Player player);
    }
    public interface IRoomDelegate
    {
        public Room ContainingRoom { get; set; }
        public void RoomDidSetExit(string exitName, Door door);
        public Door RoomDidGetExit(string exitName, Door door);

        public string RoomDidGetExits(string exits);
        public string RoomDidGetDescription(string description);
    }
    public interface ICloseable
    {
        public bool IsClosed { get; }

        public bool IsOpened { get; }

        bool Close();
        bool Open();
        bool CanClose { get; }
        bool CanOpen { get; }

    }

    public interface IKeyed
    {
        bool HasKey { get; }
        IItem Insert(IItem key);
        IItem Remove();
    }


    public interface ILockable : IKeyed
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool Lock();
        bool Unlock();
        bool CanLock { get; }
        bool CanUnlock { get; }
        bool CanClose { get; }
        bool CanOpen { get; }
        IKeyed Keyed { set; get; }
    }




    public interface IItem
    {
        string Name { get; }

        string LongName { get; }
        float Weight { get; }
        string Description { get; }

        bool isContainer { get; }

        void AddDecorator(IItem decorator);

    }

    public interface IItemContainer : IItem
    {
        void Add(IItem item);


    }

}