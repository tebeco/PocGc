namespace Runtime
{
    public class RuntimeHostBuilder<TStartup>
        where TStartup : RuntimeHost, new()
    {
        public RuntimeHost Build()
        {
            return new TStartup();
        }
    }
}
