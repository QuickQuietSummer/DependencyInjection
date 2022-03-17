namespace DependencyInjection.Lib.DI
{
    public struct Binding<T1, T2>
    {
        public T1 Binding1 { get; }
        public T2 Binding2 { get; }

        public Binding(T1 binding1, T2 binding2)
        {
            Binding1 = binding1;

            Binding2 = binding2;
        }
    }
}