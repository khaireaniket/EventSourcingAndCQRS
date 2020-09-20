using System;

namespace CQRSWithoutEventSourcing
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBroker eventBroker = new EventBroker();
            Person person = new Person(eventBroker);

            eventBroker.Command(new ChangeAgeCommand(person, 33));
            int age = eventBroker.Query<int>(new AgeQuery { Target = person });

            Console.WriteLine(age);

            Console.ReadLine();
        }
    }
}
