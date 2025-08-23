namespace Zugsichtungen.Domain.Models
{
    public class Context
    {
        private Context() { }

        public int Id { get; private set; }
        public string? Name { get; private set; } = null!;

        public static Context Create(int id, string? name) 
        {
            return new Context { Id = id, Name = name };
        }
    }
}
