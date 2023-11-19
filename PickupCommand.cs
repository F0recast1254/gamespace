using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class PickupCommand : Command
    {
        public PickupCommand() : base()
        {
            this.Name = "pickup";

        }

        public override bool Execute(Player player)
        {
            {
                if (this.HasSecondWord())
                {
                    if (this.HasThirdWord())
                    {
                        player.Pickup(this.SecondWord + " " + this.ThirdWord);
                    }
                    else
                    {
                        player.Pickup(this.SecondWord);
                    }

                }

                else
                {
                    player.WarningMessage("\n What are you picking up?");

                }
            }
            return false;
        }
    }
}
