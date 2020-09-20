using System;

namespace CQRSWithoutEventSourcing
{
    public class Command : EventArgs
    {

    }

    public class ChangeAgeCommand : Command
    {
        public Person Target;
        public int Age;

        public ChangeAgeCommand(Person target, int age)
        {
            this.Target = target;
            this.Age = age;
        }
    }
}