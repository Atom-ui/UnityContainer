using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using UnityContainerOefening.Interfaces;
using UnityContainerOefening.ManufactererKeys;

namespace UnityContainerOefening
{
    class Program
    {
        static void Main()
        {
            // methode zonder IOC
            Console.WriteLine("zonder IOC");
            Driver driver = new Driver(new Bmw());
            driver.RunCar();
            Console.WriteLine();

            // methode met IOC
            Console.WriteLine("Met IOC");
            IUnityContainer container = new UnityContainer();

            // nu wordt er Bmw meegegeven
            container.RegisterType<ICar, Bmw>();
            Driver drv = container.Resolve<Driver>();
            drv.RunCar();
            Console.WriteLine();

            // unity gebruikt het laatste geregistreerde type
            // dus Audi 'overschrijft' Bmw in dit geval
            //container.RegisterType<ICar, Audi>();
            //Driver drv = container.Resolve<Driver>();
            //Console.WriteLine("nu moet er Audi zijn (uitcomenteren)");
            //drv.RunCar();
            //Console.WriteLine();

            // type registreren
            container.RegisterType<ICar, Ford>("piece of junk");
            ICar ford = container.Resolve<ICar>("piece of junk");
            
            container.RegisterType<Driver>("junkdriver", new InjectionConstructor(ford));
            Driver drv1 = container.Resolve<Driver>(); // injecteert Bmw
            drv1.RunCar();
            Driver drv2 = container.Resolve<Driver>("junkdriver"); // injecteert ford
            drv2.RunCar();
            Console.WriteLine();

            //instanties registrreren
            var containerInst = new UnityContainer();
            ICar audi = new Audi();
            containerInst.RegisterInstance<ICar>(audi);
            Driver driverInst = containerInst.Resolve<Driver>();
            driverInst.RunCar();
            driverInst.RunCar();
            Driver driverInst2 = containerInst.Resolve<Driver>();
            driverInst2.RunCar();
            Console.WriteLine();

            // meerdere parameters
            var containerPar = new UnityContainer();
            containerPar.RegisterType<ICar, Audi>();
            containerPar.RegisterType<ICarKey, AudiKey>();
            var driverPar = containerPar.Resolve<Driver>();
            driverPar.RunCar();
            Console.WriteLine();

            // lifetimemanagers

            // standaard gang van zaken
            var containerLife = new UnityContainer().RegisterType<ICar, Bmw>(new TransientLifetimeManager()); // new TransientLifetimeManager() is onnodig omdat dit standaard is
            var driverLife = containerLife.Resolve<Driver>();
            driverLife.RunCar();
            driverLife.RunCar();
            var driverLife2 = containerLife.Resolve<Driver>();
            driverLife2.RunCar();
            Console.WriteLine();

            // singleton instanties aanmaken
            var containerContLife = new UnityContainer().RegisterType<ICar, Audi>(new ContainerControlledLifetimeManager());
            var driverContLife = containerContLife.Resolve<Driver>();
            driverContLife.RunCar();
            var driverContLife2 = containerContLife.Resolve<Driver>();
            driverContLife2.RunCar();
            Console.WriteLine();

            // hierarchise singleton instanties
            var containerHier = new UnityContainer().RegisterType<ICar, Ford>(new HierarchicalLifetimeManager()); 
            var childContainerHier = containerHier.CreateChildContainer(); 
            var driverHier = containerHier.Resolve<Driver>(); 
            driverHier.RunCar(); 
            var driverHier2 = containerHier.Resolve<Driver>(); 
            driverHier2.RunCar(); 
            var driverHier3 = childContainerHier.Resolve<Driver>(); 
            driverHier3.RunCar(); 
            var driverHier4 = childContainerHier.Resolve<Driver>(); 
            driverHier4.RunCar();
        }
    }
}
