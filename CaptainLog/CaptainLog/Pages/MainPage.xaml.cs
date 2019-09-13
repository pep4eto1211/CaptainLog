using CaptainLog.Core.Services;
using CaptainLog.Global;
using CaptainLog.Models;
using CaptainLog.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CaptainLog.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private LogsCrudService _crudService;
        private ObservableCollection<SingleLogModel> _logs;

        public MainPage()
        {
            InitializeComponent();
            InitializeGlobalResources();
            this._crudService = new LogsCrudService();
        }

        public ObservableCollection<SingleLogModel> Logs { get => this._logs; set => this._logs = value; }

        private void InitializeGlobalResources()
        {
            GlobalNavigationUtility.MainPageFrameNavigationService = this.MainPageFrame.NavigationService;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Logs = new ObservableCollection<SingleLogModel>(this._crudService.ListLogData().Select(l =>
            new SingleLogModel()
            {
                LogName = l.LogName,
                CreatedDate = l.CreatedDate,
                LastModifiedDate = l.LastModifiedDate
            }).ToList());

            foreach (var item in this.Logs)
            {
                var singleLogControl = new SingleLog(item);
                singleLogControl.Margin = new Thickness(5);
                singleLogControl.OnOpenLog += SingleLogControl_OnOpenLog;
                this.logsStackPanel.Children.Add(singleLogControl);
            }
        }

        private void SingleLogControl_OnOpenLog(object sender, OpenLogEventArgs args)
        {
            LogPage logPage = new LogPage(this._crudService.ReadLog(args.LogName));
            GlobalNavigationUtility.MainPageFrameNavigationService.Navigate(logPage);
        }
    }
}
