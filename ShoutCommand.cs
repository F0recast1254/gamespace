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
            if (this.HasSecondWord())
            {
                if (this.HasThirdWord())
                {
                    player.Shout(this.SecondWord + " " + this.ThirdWord);
                }
                else
                {
                    player.Shout(this.SecondWord);
                }

            }
            else
            {
                player.WarningMessage("\nshout what?");

            }
            return false;

        }

    }
}