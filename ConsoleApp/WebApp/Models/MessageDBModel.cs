
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class MessageDBModel
    {
        [Key]
        public int id { get; set; }
        public string message { get; set; }
        public string time { get; set; }
    }
}