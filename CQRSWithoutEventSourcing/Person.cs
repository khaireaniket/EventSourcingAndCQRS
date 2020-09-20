namespace CQRSWithoutEventSourcing
{
    public class Person
    {
        public int age;
        EventBroker eventBroker;

        public Person(EventBroker eventBroker)
        {
            this.eventBroker = eventBroker;
            eventBroker.Commands += BrokerOnCommands;
            eventBroker.Queries += BrokerOnQueries;
        }

        private void BrokerOnCommands(object sender, Command command)
        {
            var cac = command as ChangeAgeCommand;

            if (cac != null && cac.Target == this)
            {
                age = cac.Age;
            }
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            var ac = query as AgeQuery;

            if (ac != null && ac.Target == this)
            {
                ac.Result = age;
            }
        }
    }
}
