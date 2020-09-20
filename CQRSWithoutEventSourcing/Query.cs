using System;

namespace CQRSWithoutEventSourcing
{
    public class Query: EventArgs
    {
        public object Result;
    }

    public class AgeQuery : Query
    {
        public Person Target;
    }
}