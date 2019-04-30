namespace sspx.web.Models
{
    public class ProtocolGroupMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProtocolGroupMenuItem(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
