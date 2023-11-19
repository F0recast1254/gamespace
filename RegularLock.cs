using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class RegularLock : ILockable
    {
        private bool _locked;
        private IKeyed _keyed;
        public IKeyed Keyed { set { _keyed = value; } get { return _keyed; } }


        public RegularLock()
        {
            _locked = false;
            Keyed = null;
        }
        public bool IsLocked { get { return _locked; } }

        public bool IsUnlocked { get { return !_locked; } }


        public bool CanLock { get { return Keyed == null ? true : Keyed.HasKey; } }
        public bool CanUnlock { get { return Keyed == null ? true : Keyed.HasKey; } }

        public bool CanClose { get { return true; } }

        public bool CanOpen { get { return IsUnlocked; } }


        public bool Lock()
        {
            bool result = false;
            if (IsUnlocked)
            {
                if (CanLock)
                {
                    _locked = true;
                    result = true;
                }
            }
            return result;
        }


        public bool Unlock()
        {
            bool result = false;
            if (IsLocked)
            {
                if (CanUnlock)
                {
                    _locked = false;
                    result = true;
                }
            }
            return result;

        }
        public bool HasKey { get { return Keyed == null ? true : Keyed.HasKey; } }
        public IItem Insert(IItem key)
        {
            return Keyed == null ? key : Keyed.Insert(key);
        }
        public IItem Remove()
        {
            return Keyed == null ? null : Keyed.Remove();
        }
    }
}