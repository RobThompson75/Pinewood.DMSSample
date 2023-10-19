namespace Pinewood.DMSSample.Business
{
    public class Customer
    {
        public Customer(int id, string name, string address)
        {
            ID = id;
            Name = name;
            Address = address;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
