using KPO_ConsoleApp.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO_ConsoleApp
{
    public interface IVeterinaryClinic
    {
        bool CheckHealth(Animal animal);
    }

    public class VeterinaryClinic : IVeterinaryClinic
    {
        public bool CheckHealth(Animal animal)
        {
            return animal.IsHealthy;
        }
    }
}
