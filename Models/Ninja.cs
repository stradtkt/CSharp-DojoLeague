using System.ComponentModel.DataAnnotations;

namespace DojoLeague.Models
{
    public class Ninja
    {
        [Key]
        public int ninja_id {get;set;}
        public string name {get;set;}
        public int level {get;set;}
        public string description {get;set;}
        public int dojo_id {get;set;}
        public Dojo Dojos {get;set;}
    }
}