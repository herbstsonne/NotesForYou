using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesForYou.Core.Settings
{
    public class Setting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TimeSpan Difference { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}