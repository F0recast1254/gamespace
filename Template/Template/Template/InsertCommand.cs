using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class InsertCommand : Command
    {


        public InsertCommand() : base()
        {
            this.Name = "insert";
        }



        public override bool Execute(Player player)
        {
            {
                if (this.HasSecondWord())
                {
                    if (this.HasThirdWord())
                    {
                        player.Insert(this.SecondWord + " " + this.ThirdWord);
                    }
                    else
                    {
                        player.Insert(this.SecondWord);
                    }

                }
                else
                {
                    player.WarningMessage("\n uh?");

                }
            }
            return false;
        }
    }
}