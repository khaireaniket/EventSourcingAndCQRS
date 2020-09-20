using System;

namespace CQRSWithEventSourcing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your age:");
            string currentAge = Console.ReadLine();
            int age = Int32.Parse(currentAge);

            EventBroker eventBroker = new EventBroker();
            Person person = new Person(eventBroker, age);

            // Raise Command
            eventBroker.Command(
                new CreateAgeCommand(person, age)
            );

            Console.WriteLine("\nWould you like to change your age (Y/N)");
            var changeAge = Console.ReadKey();

            while (changeAge.KeyChar == 'Y' || changeAge.KeyChar == 'y')
            {
                Console.WriteLine("\nPlease enter your new age:");
                currentAge = Console.ReadLine();
                age = Int32.Parse(currentAge);

                // Raise Command
                eventBroker.Command(
                    new ChangeAgeCommand(person, age)
                );

                Console.WriteLine("\nWould you like to change your age (Y/N)");
                changeAge = Console.ReadKey();
            }

            ConsoleKeyInfo goback;

            do
            {
                Console.WriteLine("\nChoose any option:");
                Console.WriteLine("\n1.Print all events\n2.Print current age\n3.Undo to last entered age");
                var choose = Console.ReadKey();

                if (choose.KeyChar == '1')
                {
                    Console.WriteLine("\nAll the events:");

                    // Print all events
                    PrintAllEvents(eventBroker);
                }
                else if (choose.KeyChar == '2')
                {
                    // Raise Query
                    age = eventBroker.Query<int>(
                        new AgeQuery { Target = person }
                    );

                    Console.WriteLine($"\nYou current age is {age}");
                }
                else if (choose.KeyChar == '3')
                {
                    person.UndoLastEnteredAge();

                    // Raise Query
                    age = eventBroker.Query<int>(
                        new AgeQuery { Target = person }
                    );

                    Console.WriteLine($"\nYour age reverted to {age}");
                }

                Console.WriteLine("Go back to menu (Y/N)");
                goback = Console.ReadKey();

            } while (goback.KeyChar == 'Y' || goback.KeyChar == 'y');

            Console.ReadLine();
        }

        private static void PrintAllEvents(EventBroker eventBroker)
        {
            foreach (var evnt in eventBroker.AllEvents)
            {
                Console.WriteLine(evnt);
            }
        }

    }
}
