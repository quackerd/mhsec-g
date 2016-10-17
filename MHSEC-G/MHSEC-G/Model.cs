using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSEC_G
{
    public class Model
    {
        private readonly byte[] save_file;
        private readonly List<View> views;

        public Model(byte[] save_file)
        {
            this.save_file = save_file;
            views = new List<View>();
        }

        public byte[] read()
        {
            byte[] ret = new byte[save_file.Length];
            Array.Copy(save_file, ret, save_file.Length);
            return ret;
        }

        public void write(int offset, byte[] val)
        {

            notify_view();
        }

        private void notify_view()
        {
            for(int i = 0; i < views.Count; i++)
            {
                views.ElementAt(i).update();
            }
        }

        public void register_view(View view)
        {
            views.Add(view);
        }

        public void deregister_view(View view)
        {
            views.Remove(view);
        }
    }
}
