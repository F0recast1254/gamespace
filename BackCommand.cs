using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public bool Execute(Player player)
        {
                player.Back();
            return false;
        }
    }
}
