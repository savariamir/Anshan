namespace Anshan.Test.Faker
{
    public abstract class BaseSteps
    {
        protected readonly ContextData ContextData;

        protected BaseSteps()
        {
            ContextData = new ContextData();
        }
    }
}