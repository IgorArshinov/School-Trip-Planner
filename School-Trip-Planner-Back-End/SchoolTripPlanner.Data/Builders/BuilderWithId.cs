namespace SchoolTripPlanner.Data.Builders
{
    public abstract class BuilderWithId<T, TC> : Builder<T>
    {
        public abstract TC WithId(long id);
    }
}