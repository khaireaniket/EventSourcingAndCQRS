namespace CQRSWithEventSourcing
{
    public class Event
    {
    }

    public class AgeCreatedEvent : Event
    {
        public Person Target;
        public int Age;

        public AgeCreatedEvent(Person target, int age)
        {
            this.Target = target;
            this.Age = age;
        }

        public override string ToString()
        {
            return $"You have entered your age as {Age}";
        }
    }

    public class AgeChangedEvent : Event
    {
        public Person Target;
        public int OldAge, NewAge;

        public AgeChangedEvent(Person target, int oldAge, int newAge)
        {
            this.Target = target;
            this.OldAge = oldAge;
            this.NewAge = newAge;
        }

        public override string ToString()
        {
            return $"Age changed from {OldAge} to {NewAge}";
        }
    }
}