using CaptainLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaptainLog.UserControls
{
    /// <summary>
    /// Interaction logic for SingleLog.xaml
    /// </summary>
    public partial class SingleLog : UserControl
    {
        public delegate void OnOpenLogeventHandler(object sender, OpenLogEventArgs args);
        public event OnOpenLogeventHandler OnOpenLog;

        private SingleLogModel _model;

        public SingleLog(SingleLogModel model)
        {
            InitializeComponent();
            this._model = model;
            this.DataContext = this._model;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnOpenLog(this, new OpenLogEventArgs(this._model.LogName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnOpenLog(this, new OpenLogEventArgs(this._model.LogName));
        }
    }
}
