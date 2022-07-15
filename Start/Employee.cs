internal class Employee : System.Object {

    private string Name;

    public Employee()
    {
        this.Name = "Andrei";
    }

    public Employee(string Name)
    {
        this.Name = Name;
    }
    public override string ToString()
    {
        return Name;
    }
}