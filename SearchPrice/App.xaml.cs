using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace SearchPrice
{
    public class Price
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public string Art { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string WidthT { get; set; }
        public string HeightT { get; set; }
        public string Rt { get; set; }
        public string SeazonT { get; set; }
        public string Pin { get; set; }
        public string IndexH { get; set; }
        public string IndexV { get; set; }
        public string RunFlat { get; set; }
        public string OptPrice { get; set; }
        public string RozPrice { get; set; }
        public string CenterBox { get; set; }
        public string RemoteBox { get; set; }
        public string Balans { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
        }
        public static string CompanyNamePrice;
        public static string BrandNamePrice;
        public static string Rtires;
        public static string Hprofile;
        public static string Wprofile;
        public static string SeasonTires;
        public static double PriceTires;
        public static string path = "";
        public static string pathShinService_summer = "";
        public static string pathShinService_winter = "";
        public static string[] path_all = new string[5];
        public static string BrandNameSVR = "";
        public static Regex regexTwinMax = new Regex(@"\s[A-Za-z]*\s");
        public static Regex regexMasterShina_firma = new Regex(@"[A-Za-z]*\s");
        public static Regex regexMasterShina_modlel = new Regex(@"\s.*");
        public static Regex regexShServ_R = new Regex(@"R\d*");
        public static Regex regexShServ_W = new Regex(@"\d*");
        public static Regex regexShServ_H = new Regex(@"/\d*");
        public static string VerPrice = "";
        public static ObservableCollection<Price> coll = new ObservableCollection<Price>();
        [STAThread]
        static void Main()
        {
            App app = new App();
            MainWindow window = new MainWindow();
            app.Run(window);
        }
    }
}
