namespace DependencyInjection.App
{
    public class IntSum : IIntOperation
    {
        public int Execute(int a, int b)
        {
            return a + b;
        }
    }
}