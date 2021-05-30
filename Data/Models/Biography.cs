using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Biography
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
