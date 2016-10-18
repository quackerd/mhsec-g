using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MHSEC_G
{
    public class ViewModel
    {

        private Model _model;
        public Model model
        {
            get { return _model; }
            set { _model = model; }
        }


        public ViewModel(byte[] data)
        {
            _model = new Model(data);
        }
    }
}
