using System.ComponentModel.DataAnnotations;

namespace DojoLeague.Models
{
    public class Dojo
    {
        [Key]
        public int dojo_id {get;set;}
        public string name {get;set;}
        public string location {get;set;}
        public string description {get;set;}

    }
}