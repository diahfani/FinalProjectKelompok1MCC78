using System.Xml;

namespace API.ViewModel.Other
{
    public class ResponseVM<Entity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Entity? Data { get; set; }
    }
}
