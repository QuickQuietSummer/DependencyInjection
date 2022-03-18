namespace DependencyInjection.App
{
    public class Calculator
    {
        private readonly IIntOperation _operation;

        public Calculator(IIntOperation operation)
        {
            _operation = operation;
        }   
    }
}