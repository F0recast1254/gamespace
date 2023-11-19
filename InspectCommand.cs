using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class InspectCommand : Command
    {

        public override bool Execute(Player player)
        {
            {
                if (this.HasSecondWord())
                {
                    if (this.HasThirdWord())
                    {
                        player.Inspect(this.SecondWord + " " + this.ThirdWord);
                    }
                    else
                    {
                        player.Inspect(this.SecondWord);
                    }
                }
                else
                {
                    player.WarningMessage("\n uh?");

                }
            }
            return false;
        }

        public InspectCommand() : base()
        {
            this.Name = "inspect";
        }

    }
}