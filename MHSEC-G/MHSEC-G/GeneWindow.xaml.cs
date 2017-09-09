using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    /// <summary>
    /// Interaction logic for GeneWindow.xaml
    /// </summary>
    public partial class GeneWindow : Window, INotifyPropertyChanged
    {
        private readonly Genes _genes;
        public Genes genes => _genes;

        public GeneWindow(Genes genes)
        {
            this._genes = genes;
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
