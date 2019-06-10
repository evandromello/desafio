namespace DesafioSouthSystem.Models
{
    public class Client
    {
        public Client(string cnpj, string name, string businessArea)
        {
            CNPJ = cnpj;
            Name = name;
            BusinessArea = businessArea;
        }
        private string CNPJ { get; set; }
        private string Name { get; set; }
        private string BusinessArea { get; set; }
    }
}