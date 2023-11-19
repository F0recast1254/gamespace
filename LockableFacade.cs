using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class LockableFacade
    {
        public static ILockable MakeLockable(string LockableName, string keyName)
        {
            ILockable lockable = null;
            switch (LockableName)
            {
                case "Regular":
                    lockable = new RegularLock();
                    break;
                default:
                    lockable = new RegularLock();
                    break;
            }
            if (keyName.Equals(""))
            {
                lockable.Keyed = new Keyed(keyName);
            }
            return lockable;
        }




    }
}