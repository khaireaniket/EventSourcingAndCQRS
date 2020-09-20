using System;

namespace CQRSWithEventSourcing
{
    public class Command : EventArgs
    {
        
    }

    public class CreateAgeCommand : Command
    {
        public Person Target;
        public int Age;

        public CreateAgeCommand(Person target, int age)
        {
            this.Target = target;
            this.Age = age;
        }
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