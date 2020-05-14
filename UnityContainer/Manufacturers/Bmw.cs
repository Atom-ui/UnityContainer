using System;
using System.Collections.Generic;
using System.Text;

namespace UnityContainerOefening
{
    public class Bmw : ICar
    {
        private int _miles = 0;
        public int Run()
        {
            return ++_miles;
        }
    }
}
