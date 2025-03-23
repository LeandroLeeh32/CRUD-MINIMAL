using System.Net.NetworkInformation;

namespace Person.Models
{
    public class PersonModel
    {

        public PersonModel(string nome, bool status)
        {
            Nome = nome;
            Status = status;
            Id = Guid.NewGuid();
        }

        public void ChangeStatus()
        {
            Status = false;
        }

        public void ChangeName(string name)
        {
            Nome = name;
        }

        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public bool Status { get; private set; }
    }




}
