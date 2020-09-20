using System;
using System.Collections.Generic;

namespace CQRSWithEventSourcing
{
    public class EventBroker
    {
        // 1. All events
        public IList<Event> AllEvents = new List<Event>();

        // 2. Commands
        public event EventHandler<Command> Commands;

        // 3. Query
        public event EventHandler<Query> Queries;

        public void Command(Command command)
        {
            Commands?.Invoke(this, command);
        }

        public T Query<T>(Query query)
        {
            Queries?.Invoke(this, query);
            return (T)query.Result;
        }
    }
}
