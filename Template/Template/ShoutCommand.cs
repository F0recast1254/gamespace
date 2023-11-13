using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class ShoutCommand : Command
    {
        public ShoutCommand() : base() 
        {
            this.Name = "shout";
                }
        public override bool Execute(Player player)
        {
            if(this.HasSecondWord())
            {
                player.Shout(this.SecondWord);
               
            }
            else
            {
                player.WarningMessage("\nshout what?");
               
            }
            return false;

        }

    }
}
