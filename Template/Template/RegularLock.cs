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
        public bool IsLocked { get { return _locked; } }

        public bool IsUnlocked { get { return !_locked; } }


        public bool CanLock { get { return true; } }
        public bool CanUnlock { get { return true; } }

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

    }
}
