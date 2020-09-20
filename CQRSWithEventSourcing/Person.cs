using System.Linq;

namespace CQRSWithEventSourcing
{
    public class Person
    {
        public int age;
        EventBroker eventBroker;

        public Person(EventBroker eventBroker, int age)
        {
            this.age = age;

            this.eventBroker = eventBroker;
            eventBroker.Commands += BrokerOnCommands;
            eventBroker.Queries += BrokerOnQueries;
        }

        private void BrokerOnCommands(object sender, Command command)
        {
            if (command is CreateAgeCommand)
            {
                var currentAgeCommand = command as CreateAgeCommand;

                if (currentAgeCommand != null && currentAgeCommand.Target == this)
                {
                    eventBroker.AllEvents.Add(
                        new AgeCreatedEvent(this, currentAgeCommand.Age)
                    );

                    age = currentAgeCommand.Age;
                }
            }

            if (command is ChangeAgeCommand)
            {
                var changeAgeCommand = command as ChangeAgeCommand;

                if (changeAgeCommand != null && changeAgeCommand.Target == this)
                {
                    eventBroker.AllEvents.Add(
                        new AgeChangedEvent(this, age, changeAgeCommand.Age)
                    );

                    age = changeAgeCommand.Age;
                }
            }
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            var ageQuery = query as AgeQuery;

            if (ageQuery != null && ageQuery.Target == this)
            {
                ageQuery.Result = age;
            }
        }

        public void UndoLastEnteredAge()
        {
            var lastEvent = eventBroker.AllEvents.LastOrDefault();

            var ageChangeEvent = lastEvent as AgeChangedEvent;

            if (ageChangeEvent != null)
            {
                eventBroker.Command(
                    new ChangeAgeCommand(ageChangeEvent.Target, ageChangeEvent.OldAge)
                );
            }
        }
    }
}
