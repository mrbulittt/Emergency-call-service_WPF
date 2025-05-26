using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public class EmergencyService
    {
        public string ServiceName { get; set; }
        public string ServicePhoneNumber { get; set; }
        public string ServiceComment { get; set; }

        // Для отображения в списке
        public override string ToString()
        {
            return $"{ServiceName} - {ServicePhoneNumber}";
        }
    }
}
