namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Department
    {
        public Department()
        { }
        public Department(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
