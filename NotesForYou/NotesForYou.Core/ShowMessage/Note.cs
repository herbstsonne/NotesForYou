using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesForYou.Core
{
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Link { get; set; }
        public int Category { get; set; }
        public DateTime? Date { get; set; }
    }
}