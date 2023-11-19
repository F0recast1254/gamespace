using System.Collections;
using System.Collections.Generic;
using System;

namespace Template
{

    /*
     * Spring 2023
     */
    public abstract class Command
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        private string _thirdWord;
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }
        public string ThirdWord { get { return _thirdWord; } set { _thirdWord = value; } }
        public Command()
        {
            this.Name = "";
            this.SecondWord = null;
            this.ThirdWord = null;
        }

        public bool HasSecondWord()
        {
            return this.SecondWord != null;
        }
        public bool HasThirdWord()
        {
            return this.ThirdWord != null;
        }
        public abstract bool Execute(Player player);
    }
}