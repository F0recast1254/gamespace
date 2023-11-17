using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class Keyed : IKeyed
    {
        private IItem originalKey;
        private IItem insertedKey;

        public bool HasKey { get { return insertedKey == originalKey; } }


        public Keyed(string keyname)
        {
            originalKey = new Item("key", 0.1f);
            insertedKey = originalKey;
        }

        public IItem Insert(IItem key)
        {
            IItem oldkey = insertedKey;
            insertedKey = key;
            return oldkey;
        }

        public IItem Remove()
        {
            IItem oldkey = insertedKey;
            insertedKey = null;
            return oldkey;
        }





    }
}