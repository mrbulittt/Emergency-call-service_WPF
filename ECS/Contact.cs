using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public class Contactis
    {
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string FamilyStatus { get; set; }  // Семейное положение
        public string Notes { get; set; }
        public string CommentDisplay =>
        $"{(string.IsNullOrWhiteSpace(FamilyStatus) ? "" : $"Семейное положение: {FamilyStatus}")}"
        +
        $"{(string.IsNullOrWhiteSpace(Notes) ? "" : $"\nПримечание: {Notes}")}";
    }
}
