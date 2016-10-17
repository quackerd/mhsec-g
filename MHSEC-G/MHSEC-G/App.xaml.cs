using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MHSEC_G
{
    public partial class App : Application
    {
        private Model model;
        public Model get_model()
        {
            return model;
        }

        public void set_model(Model model)
        {

        }
    }
}
