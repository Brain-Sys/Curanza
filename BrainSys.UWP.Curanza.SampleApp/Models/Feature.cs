
namespace BrainSys.UWP.Curanza.SampleApp.Models
{
    public class Feature
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Feature(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}