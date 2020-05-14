using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityContainerOefening.Interfaces;

namespace UnityContainerOefening
{
    public class Driver
    {
        private ICar _car = null;
        private ICarKey _key = null;

        public Driver(ICar car)
        {
            _car = car;
        }

        // [InjectionConstructor] //als dit wordt toegevoegd dan mogen enkel drivers met ICar en IcarKey worden aangemaakt
        public Driver(ICar car, ICarKey key) : this(car)
        {
            _key = key;
        }

        public void RunCar()
        {
            if (_key == null)
                Console.WriteLine($"Running {_car.GetType().Name} - {_car.Run()} mile");
            else
                Console.WriteLine($"Running {_car.GetType().Name} with {_key.GetType().Name} - {_car.Run()} mile");
        }
    }
}
