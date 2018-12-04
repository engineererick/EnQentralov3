using EnQentralov3.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnQentralov3.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
