using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using ExcelDataReader;
using System.IO;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace SearchPrice
{
    public class ExcelData
    {
        public string pathFile { get; set; }
        public IEnumerable<DataRow> DataXLS
        {
            get
            {
                var stream = File.Open(Environment.CurrentDirectory + pathFile, FileMode.Open, FileAccess.Read);
                var excelReader = ExcelReaderFactory.CreateReader(stream);
                var dataSet = excelReader.AsDataSet();
                var enumerable = dataSet.Tables[0].AsEnumerable();
                stream.Close();
                return enumerable;
            }
        }
    }
    public class CsvData
    {
        public string pathFile { get; set; }
        public IEnumerable<DataRow> DataCSV
        {
            get
            {
                var srcEncoding = Encoding.GetEncoding(1251);
                var dstEncoding = Encoding.UTF8;
                var stream = Environment.CurrentDirectory + pathFile;
                var columnPrice = 8;
                App.coll.Clear();
                DataTable TABLE = new DataTable();
                for (int i = 0; i < columnPrice; i++)
                {
                    TABLE.Columns.Add(i.ToString(), typeof(string));
                }
                string[] str = { "\n" };
                using (StreamReader rd = new StreamReader(new FileStream(stream, FileMode.Open), encoding: srcEncoding))
                {
                    str = rd.ReadToEnd().Split(str, StringSplitOptions.RemoveEmptyEntries);
                }
                MessageBox.Show("Str: "+ str.Length);
                for (int i = 0; i < str.Length; i++)
                {
                    TABLE.Rows.Add();
                    Char delimiter = ';';
                    string[] row = str[i].Split(delimiter);
                    for (int j = 0; j < row.Length; j++)
                    {
                        TABLE.Rows[i][j] = row[j];
                    }
                }
                var enumerable = TABLE.AsEnumerable();
                return enumerable;
            }
        }
    }
    public class IdToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Все проверки для краткости выкинул
            if ((string)value == "EuroDiski") { return new SolidColorBrush(Colors.Green); }
            else if ((string)value == "SVRAUTO") { return new SolidColorBrush(Colors.Cyan); }
            else if ((string)value == "TWINMAX") { return new SolidColorBrush(Colors.Red); }
            else if ((string)value == "ShinService") { return new SolidColorBrush(Colors.Yellow); }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void btnCut_Click(object sender, ExecutedRoutedEventArgs e)
        {
            /*MessageBox.Show("CUT command activated");*/
        }

        public void btnCopy_Click(object sender, ExecutedRoutedEventArgs e)
        {
            /*MessageBox.Show("COPY command activated");*/
        }

        public void btnPaste_Click(object sender, ExecutedRoutedEventArgs e)
        {
           /* MessageBox.Show("PASTE command activated");*/
        }
        public static DataTable dt = new DataTable();
        public static string[] Season = new string[2] { "Summer", "Winter" };
        public string[] Company = new string[4] { "EuroDiski", /*"Fortohki", "Liga", "Master Shina",*/ "Svrauto", "Twinmax", "ShinService" };
        public static string[] Brand = new string[134] { "AEOLUS","ANTARES","APOLLO","ATTURO","AUSTONE","AVALANCHE","AVATYRE","CHENGSHAN","CONTINENTAL","CONTYRE",
                                                  "BARUM", "BFGOODRICH", "BONTYRE", "BRASA", "BRIDGESTONE", "BRIWY", "COOPER", "CORDIANT", "DELINTE", "DIAMONDBACK",
                                                  "DMACK", "DUNLOP", "DURUN", "EFFIPLUS", "EUZKADI", "FALKEN", "FARROAD", "FEDERAL", "FIREMAX", "FIRENZA",
                                                  "FIRESTONE", "FORUMLA","FULDA","GISLAVED","GOFORM","GOLDWAY","GOODYEAR","GREMAX","GT RADIAL","HABILEAD",
                                                  "HAIDA GROUP","HANKOOK","HEADWAY","HEMISPHERE","ACHILLES","ALTENZO", "YOKOHAMA","HERCULES","HIFLY",
                                                  "HORIZON","IMPERIAL","INFINITY TYRES","INTERTSTATE","INVOVIC","JINYU","KAPSEN","KELLY","KINFOREST","KINGSTAR",
                                                  "KLEBER","KORMORAN","KPATOS","KUMHO","LANDSAIL","LASSA","LINGLONG","MAGNUM","MARSHAL","MASTERCRAFT",
                                                  "MATADOR","MAXTREK","MAXXIS","MAYRUN","MENTOR","MICHELIN","MINERVA","NANKANG","NEXEN","NITTO","NORDMAN",
                                                  "NOKIAN","ODYKING","OVATION TYRES","PACE","PIRELLI","PREMIORRI","RAPID","RIKEN","ROADCLAW","ROADCRUZA",
                                                  "ROADSTONE","ROSAVA","ROYAL","SATOYA","SAFFIRO","SAGITAR","SAILUN","SAVA","SONNY","SPORTIVA","STARFIRE",
                                                  "SUMITOMO","SUNFULL","SUNITRAC","SUNNY","SUNTEK","SUNWIDE","TIGAR","TOYO","TRACMAX","TRAYAL","TRI-ACE",
                                                  "TRIANGLE","UNIROYAL","VALLEYSTONE","VIATTI","VICTORUN","VITOUR","VREDESTEIN","WANLI","WESTLAKE TYRES","WINDFORCE",
                                                  "WINRUN","ZEETEX","АЛТАЙСКИЙ ШИННЫЙ КОМБИНАТ","АМТЕЛ","БЕЛШИНА","ВОЛТАЙР","КШЗ","МШЗ","НИЖНЕКАМСКШИНА",
                                                  "ОМСКШИНА","УРАЛШИНА","ЯШЗ"};
        public string[] ProfileHeight = new string[19] { "5", "8.5", "9.5", "10.5", "11.5",
                                                        "12.5", "25", "30", "35", "40",
                                                          "45", "50", "55", "60", "65",
                                                          "70", "75", "80", "85" };
        public string[] ProfileWidth = new string[34] { "5", "6.5", "7.5", "8.5",
                                                        "11", "27", "30", "31", "32",
                                                        "33", "35", "37", "135", "145",
                                                        "155", "165", "175", "185", "195",
                                                        "205","215","225","235","245",
                                                        "255","265","275","285","295",
                                                        "305","315","325","335","345"};
        public void dt_Column()
        {
             for (int i = 0; i < dataGrid.Columns.Count - 1; i++)
             {
                 dt.Columns.Add(i.ToString(), typeof(string));
             }
        }
        public static void SelectFileXls_Xml_Csv()
        {
            if (App.CompanyNamePrice == "EuroDiski")
                App.path = @"\\Price\\EuroDiski\\prices001.xls";
            if (App.CompanyNamePrice == "Fortohki")
                App.path = @"\\Price\\Fortohki\\Tires.xml";
            if (App.CompanyNamePrice == "Liga")
                App.path = @"\\Price\\Liga\\liga-b2b-catalogue.xml";
            if (App.CompanyNamePrice == "Master Shina")
                App.path = @"\Price\MasterShina\MasterChina.csv";
            if (App.CompanyNamePrice == "Svrauto")
                App.path = @"\\Price\\Svrauto\\svrauto.xlsx";
            if (App.CompanyNamePrice == "Twinmax")
                App.path = @"\\Price\\Twinmax\\Stocklist.xlsx";
            if (App.CompanyNamePrice == "ShinService")
            {
                App.pathShinService_summer = @"\\Price\\Shinservice\\shinservice_b2b_summer.xlsx";
                App.pathShinService_winter = @"\\Price\\Shinservice\\shinservice_b2b_winter.xlsx";
            }
            if (App.CompanyNamePrice == "All")
            {
                App.path_all[0] = @"\\Price\\EuroDiski\\prices001.xls";
                /*App.path_all[1] = @"\\Price\\Fortohki\\Tires.xml";
                App.path_all[2] = @"\\Price\\Liga\\liga-b2b-catalogue.xml";
                App.path_all[3] = @"\\Price\\MasterShina\\Master-Shina.Ru_tyres.csv";*/
                App.path_all[1] = @"\\Price\\Svrauto\\svrauto.xlsx";
                App.path_all[2] = @"\\Price\\Twinmax\\Stocklist.xlsx";
                App.path_all[3] = @"\\Price\\Shinservice\\shinservice_b2b_summer.xlsx";
                App.path_all[4] = @"\\Price\\Shinservice\\shinservice_b2b_winter.xlsx";
            }
        }
        public static void ClearPathDATA()
        {
            App.path = "";
            App.pathShinService_summer = "";
            App.pathShinService_winter = "";
            Array.Clear(App.path_all, 0, App.path_all.Length);
        }
        /// <summary>
        /// Прайс компании EURODISKI
        /// </summary>
        /// <param name="enumerable"></param>
        public static void ResultEuroDiski(IEnumerable<DataRow> enumerable)
        {
            string sezon = "";
            if (App.SeasonTires == Season[0])
                sezon = "летние";
            if (App.SeasonTires == Season[1])
                sezon = "зимние";
            //R
            if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Брэнд
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString() == App.BrandNamePrice
                                   && item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Брэнд, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString() == App.BrandNamePrice
                                   && item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                                   && item[5].ToString() != null && item[5].ToString() == App.Hprofile
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Брэнд, Ширина, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString() == App.BrandNamePrice
                                   && item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                                   && item[5].ToString() != null && item[5].ToString() == App.Hprofile
                                   && item[4].ToString() != null && item[4].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Брэнд, Ширина
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString() == App.BrandNamePrice
                                   && item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                                   && item[4].ToString() != null && item[4].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString() == App.BrandNamePrice
                                   && item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                                   && item[5].ToString() != null && item[5].ToString() == App.Hprofile
                                   && item[4].ToString() != null && item[4].ToString() == App.Wprofile
                                   && item[7].ToString() != null && item[7].ToString() == sezon
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон, Цена
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires != 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString() == App.BrandNamePrice
                                   && item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                                   && item[5].ToString() != null && item[5].ToString() == App.Hprofile
                                   && item[4].ToString() != null && item[4].ToString() == App.Wprofile
                                   && item[7].ToString() != null && item[7].ToString() == sezon
                                   && item[12].ToString() != null && Convert.ToInt32(App.PriceTires) >= Convert.ToInt32(item[12].ToString())
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
            //R, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[6].ToString() != null && item[6].ToString() == "R" + App.Rtires
                                   && item[5].ToString() != null && item[5].ToString() == App.Hprofile
                                   && item[4].ToString() != null && item[4].ToString() == App.Wprofile
                                   && item[7].ToString() != null && item[7].ToString() == sezon
                             select (DataRow)item;
                View.View.ResultViewEuroDiski(result);
                return;
            }
        }
        /// <summary>
        /// Прайс компании TWINMAX
        /// </summary>
        /// <param name="enumerable"></param>
        public static void ResultTWINMAX(IEnumerable<DataRow> enumerable)
        {
            string[] winter = new string[2] { "Kitka", "Nasta"};
            //R
            if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.Rtires
                             select (DataRow)item;
                View.View.ResultViewTWINMAX(result);
                return;
            }
            //R, Брэнд
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable 
                             where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                   && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)  
                             select (DataRow)item;
                View.View.ResultViewTWINMAX(result);
                return;
            }
            //R, Брэнд, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                   && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                             select (DataRow)item;
                View.View.ResultViewTWINMAX(result);
                return;
            }
            //R, Брэнд, Ширина, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                   && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                   && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewTWINMAX(result);
                return;
            }
            //R, Брэнд, Ширина
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                   && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewTWINMAX(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                if (App.SeasonTires == Season[0])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && item[1].ToString().Contains(winter[0]) == false
                                       && item[1].ToString().Contains(winter[1]) == false
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                if (App.SeasonTires == Season[1])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && (item[1].ToString().Contains(winter[0]) || item[1].ToString().Contains(winter[1]))
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон, Цена
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires != 0)
            {
                if (App.SeasonTires == Season[0])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && item[1].ToString().Contains(winter[0]) == false
                                       && item[1].ToString().Contains(winter[1]) == false
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                       && item[9].ToString() != null && App.PriceTires >= Convert.ToDouble(item[9].ToString())
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                if (App.SeasonTires == Season[1])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && (item[1].ToString().Contains(winter[0]) || item[1].ToString().Contains(winter[1]))
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                       && item[9].ToString() != null && App.PriceTires >= Convert.ToDouble(item[9].ToString())
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                return;
            }
            //R, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                if (App.SeasonTires == Season[0])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString().Contains(winter[0]) == false
                                       && item[1].ToString().Contains(winter[1]) == false
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                if (App.SeasonTires == Season[1])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && (item[1].ToString().Contains(winter[0]) || item[1].ToString().Contains(winter[1]))
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                return;
            }
        }
        /// <summary>
        /// SVRAUTO BRAND PARSE
        /// </summary>
        /// <param name="BrandNamePrice"></param>
        /// <returns></returns>
        public static void SvrautoBrandParse()
        {
            if (App.BrandNamePrice == "BRIDGESTONE")
                App.BrandNameSVR = "Бриджстоун";
            if (App.BrandNamePrice == "BFGOODRICH")
                App.BrandNameSVR = "БФ гудрич";
            if (App.BrandNamePrice == "GISLAVED")
                App.BrandNameSVR = "Гиславед";
            if (App.BrandNamePrice == "GOODYEAR")
                App.BrandNameSVR = "ГУД-ЕАР";
            if (App.BrandNamePrice == "НИЖНЕКАМСКШИНА")
                App.BrandNameSVR = "НК.ШЗ";
            if (App.BrandNamePrice == "LINGLONG")
                App.BrandNameSVR = "LEAO/LINGLONG";
            if (App.BrandNamePrice == "RIKEN")
                App.BrandNameSVR = "Riken";
            if (App.BrandNamePrice == "ROSAVA")
                App.BrandNameSVR = "Росава";
            if (App.BrandNamePrice == "AEOLUS")
                App.BrandNameSVR = "Аеолус";
            if (App.BrandNamePrice == "АМТЕЛ")
                App.BrandNameSVR = "Амт";
            if (App.BrandNamePrice == "ВОЛТАЙР")
                App.BrandNameSVR = "Волж.ШЗ";
            if (App.BrandNamePrice == "DUNLOP")
                App.BrandNameSVR = "Данлоп";
            if (App.BrandNamePrice == "YOKOHAMA")
                App.BrandNameSVR = "Йокохама";
            if (App.BrandNamePrice == "CONTINENTAL")
                App.BrandNameSVR = "Континенталь";
            if (App.BrandNamePrice == "CORDIANT")
                App.BrandNameSVR = "Срш";
            if (App.BrandNamePrice == "KUMHO")
                App.BrandNameSVR = "Кумхо";
            if (App.BrandNamePrice == "КШЗ")
                App.BrandNameSVR = "Кир.ШЗ";
            if (App.BrandNamePrice == "MAXXIS")
                App.BrandNameSVR = "Максис";
            if (App.BrandNamePrice == "MARSHAL")
                App.BrandNameSVR = "Маршал";
            if (App.BrandNamePrice == "MATADOR")
                App.BrandNameSVR = "Матадор";
            if (App.BrandNamePrice == "MICHELIN")
                App.BrandNameSVR = "Мишелин";
            if (App.BrandNamePrice == "NOKIAN")
                App.BrandNameSVR = "Нокиан";
            if (App.BrandNamePrice == "NORDMAN")
                App.BrandNameSVR = "Нордман";
            if (App.BrandNamePrice == "PIRELLI")
                App.BrandNameSVR = "Пирелли";
            if (App.BrandNamePrice == "ROADSTONE")
                App.BrandNameSVR = "Роудстоун";
            if (App.BrandNamePrice == "SAVA")
                App.BrandNameSVR = "Сава";
            if (App.BrandNamePrice == "TIGAR")
                App.BrandNameSVR = "Тайгер";
            if (App.BrandNamePrice == "TOYO")
                App.BrandNameSVR = "Тойя";
            if (App.BrandNamePrice == "FIRESTONE")
                App.BrandNameSVR = "Файрстоун";
            if (App.BrandNamePrice == "FORUMLA")
                App.BrandNameSVR = "Формула";
            if (App.BrandNamePrice == "FULDA")
                App.BrandNameSVR = "Фулда";
            if (App.BrandNamePrice == "HANKOOK")
                App.BrandNameSVR = "Ханкук";
        }
        /// <summary>
        /// Прайс компании SVRAUTO
        /// </summary>
        /// <param name="enumerable"></param>
        public static void ResultSvrauto(IEnumerable<DataRow> enumerable)
        {
            string sezon = "";
            
            if (App.SeasonTires == Season[0])
                sezon = "Летняя";
            if (App.SeasonTires == Season[1])
                sezon = "Зимняя";
            //R
            if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[6].ToString() != null && item[6].ToString() == App.Rtires
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
                return;
            }
            //R, Брэнд
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.BrandNameSVR
                                   && item[6].ToString() != null && item[6].ToString() == App.Rtires
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
                return;
            }
            //R, Брэнд, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.BrandNameSVR
                                   && item[6].ToString() != null && item[6].ToString() == App.Rtires
                                   && item[8].ToString() != null && item[8].ToString() == App.Hprofile
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
                return;
            }
            //R, Брэнд, Ширина, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.BrandNameSVR
                                   && item[6].ToString() != null && item[6].ToString() == App.Rtires
                                   && item[8].ToString() != null && item[8].ToString() == App.Hprofile
                                   && item[7].ToString() != null && item[7].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
                return;
            }
            //R, Брэнд, Ширина
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.BrandNameSVR
                                   && item[6].ToString() != null && item[6].ToString() == App.Rtires
                                   && item[7].ToString() != null && item[7].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.BrandNameSVR
                                   && item[6].ToString() != null && item[6].ToString() == App.Rtires
                                   && item[8].ToString() != null && item[8].ToString() == App.Hprofile
                                   && item[7].ToString() != null && item[7].ToString() == App.Wprofile
                                   && item[3].ToString() != null && item[3].ToString() == sezon
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон, Цена
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires != 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.BrandNameSVR
                                   && item[6].ToString() != null && item[6].ToString() == App.Rtires
                                   && item[8].ToString() != null && item[8].ToString() == App.Hprofile
                                   && item[7].ToString() != null && item[7].ToString() == App.Wprofile
                                   && item[3].ToString() != null && item[3].ToString() == sezon
                                   && item[19].ToString() != null && Convert.ToInt32(App.PriceTires) >= Convert.ToInt32(item[19].ToString())
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
            }
            //R, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[6].ToString() != null && item[6].ToString() == App.Rtires
                                   && item[8].ToString() != null && item[8].ToString() == App.Hprofile
                                   && item[7].ToString() != null && item[7].ToString() == App.Wprofile
                                   && item[3].ToString() != null && item[3].ToString() == sezon
                             select (DataRow)item;
                View.View.ResultViewSvrauto(result);
            }
        }
        /// <summary>
        /// Прайс компании SHINSERVICE
        /// </summary>
        /// <param name="enumerable"></param>
        public static void ResultShinService(IEnumerable<DataRow> enumerable)
        {
            //R
            if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
            //R, Брэнд
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                   && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
            //R, Брэнд, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                   && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[2].ToString() != null && item[2].ToString().ToUpper().Contains("/" + App.Hprofile)
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
            //R, Брэнд, Ширина, Высота
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                   && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[2].ToString() != null && item[2].ToString().ToUpper().Contains("/" + App.Hprofile)
                                   && item[2].ToString().ToUpper().Contains(App.Wprofile + "/")
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
            //R, Брэнд, Ширина
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                   && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[2].ToString() != null && item[2].ToString().ToUpper().Contains(App.Wprofile + "/")
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                                              && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                                                              && item[2].ToString() != null && item[2].ToString().ToUpper().Contains("/" + App.Hprofile)
                                                              && item[2].ToString().ToUpper().Contains(App.Wprofile + "/")
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон, Цена
            else if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires != 0)
            {
                if (App.SeasonTires == "Summer")
                {
                    var result = from item in enumerable
                                 where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                       && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && item[2].ToString() != null && item[2].ToString().ToUpper().Contains("/" + App.Hprofile)
                                       && item[2].ToString().ToUpper().Contains(App.Wprofile + "/")
                                       && item[7].ToString() != null && Convert.ToInt32(App.PriceTires) >= Convert.ToInt32(item[7].ToString())
                                 select (DataRow)item;
                    View.View.ResultViewShinService(result);
                    return;
                }
                if (App.SeasonTires == "Winter")
                {
                    var result = from item in enumerable
                                 where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                       && item[3].ToString() != null && item[3].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && item[2].ToString() != null && item[2].ToString().ToUpper().Contains("/" + App.Hprofile)
                                       && item[2].ToString().ToUpper().Contains(App.Wprofile + "/")
                                       && item[8].ToString() != null && Convert.ToInt32(App.PriceTires) >= Convert.ToInt32(item[8].ToString())
                                 select (DataRow)item;
                    View.View.ResultViewShinService(result);
                    return;
                }
            }
            //R, Ширина, Высота, Сезон
            else if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[2].ToString() != null && item[2].ToString().ToUpper().Contains("R" + App.Rtires)
                                                              && item[2].ToString() != null && item[2].ToString().ToUpper().Contains("/" + App.Hprofile)
                                                              && item[2].ToString().ToUpper().Contains(App.Wprofile + "/")
                             select (DataRow)item;
                View.View.ResultViewShinService(result);
                return;
            }
        }
        /// <summary>
        /// Прайс компании MASTER SHINA
        /// </summary>
        /// <param name="enumerable"></param>
        /*------------------------------------------------------------------------------------------------------------------------------------
        public static void ResultMaster_Shina(IEnumerable<DataRow> enumerable)
        {
            string sezon = "";
            if (App.SeasonTires == Season[0])
                sezon = "Лето";
            if (App.SeasonTires == Season[1])
                sezon = "Зима";
            //R
            if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[3].ToString() != null && item[3].ToString() == App.Rtires
                             select (DataRow)item;
                MessageBox.Show("Enumerable" + result.Count());
                View.View.ResultViewMaster_Shina(result);
                return;
            }
            //R, Брэнд
            if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[3].ToString() != null && item[3].ToString() == App.Rtires
                                   && item[0].ToString() != null && App.regexMasterShina_firma.Match(Convert.ToString(item[0])).Value.ToUpper().Contains(App.BrandNamePrice)
                             select (DataRow)item;
                View.View.ResultViewMaster_Shina(result);
                return;
            }
            //R, Брэнд, Высота
            if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[3].ToString() != null && item[3].ToString() == App.Rtires
                                   && item[0].ToString() != null && App.regexMasterShina_firma.Match(Convert.ToString(item[0])).Value.ToUpper().Contains(App.BrandNamePrice)
                                   && item[2].ToString() != null && item[2].ToString() == App.Hprofile
                             select (DataRow)item;
                View.View.ResultViewMaster_Shina(result);
                return;
            }
            //R, Брэнд, Ширина, Высота
            if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[0].ToString() != null && App.regexMasterShina_firma.Match(Convert.ToString(item[0])).Value.ToUpper().Contains(App.BrandNamePrice)
                                   && item[1].ToString() != null && item[1].ToString() == App.Wprofile
                                   && item[2].ToString() != null && item[2].ToString() == App.Hprofile
                                   && item[3].ToString() != null && item[3].ToString() == App.Rtires
                             select (DataRow)item;
                View.View.ResultViewMaster_Shina(result);
                return;
            } ------------------------------------------------------------------------------------------------------- */
            /*//R, Брэнд, Ширина
            if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                var result = from item in enumerable
                             where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                   && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                   && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                             select (DataRow)item;
                View.View.ResultViewTWINMAX(result);
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон
            if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                if (App.SeasonTires == Season[0])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && item[1].ToString().Contains(winter[0]) == false
                                       && item[1].ToString().Contains(winter[1]) == false
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                if (App.SeasonTires == Season[1])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && (item[1].ToString().Contains(winter[0]) || item[1].ToString().Contains(winter[1]))
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                return;
            }
            //R, Брэнд, Ширина, Высота, Сезон, Цена
            if (App.Rtires != "" && App.BrandNamePrice != "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires != 0)
            {
                if (App.SeasonTires == Season[0])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && item[1].ToString().Contains(winter[0]) == false
                                       && item[1].ToString().Contains(winter[1]) == false
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                       && item[9].ToString() != null && App.PriceTires >= Convert.ToDouble(item[9].ToString())
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                if (App.SeasonTires == Season[1])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString() != null && item[1].ToString().ToUpper().Contains(App.BrandNamePrice)
                                       && (item[1].ToString().Contains(winter[0]) || item[1].ToString().Contains(winter[1]))
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                       && item[9].ToString() != null && App.PriceTires >= Convert.ToDouble(item[9].ToString())
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                return;
            }
            //R, Ширина, Высота, Сезон
            if (App.Rtires != "" && App.BrandNamePrice == "" && App.Hprofile != "" && App.SeasonTires != "" && App.Wprofile != "" && App.PriceTires == 0)
            {
                if (App.SeasonTires == Season[0])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && item[1].ToString().Contains(winter[0]) == false
                                       && item[1].ToString().Contains(winter[1]) == false
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                if (App.SeasonTires == Season[1])
                {
                    var result = from item in enumerable
                                 where item[4].ToString() != null && item[4].ToString() == App.Rtires
                                       && (item[1].ToString().Contains(winter[0]) || item[1].ToString().Contains(winter[1]))
                                       && item[3].ToString() != null && item[3].ToString() == App.Hprofile
                                       && item[2].ToString() != null && item[2].ToString() == App.Wprofile
                                 select (DataRow)item;
                    View.View.ResultViewTWINMAX(result);
                }
                return;
            }
            else { MessageBox.Show("Please input more filter parameters!", "Attention", MessageBoxButton.OKCancel, MessageBoxImage.Warning); }
        }*/
        //Search Price
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Controller.Controller.ControllerClickSearh(dataGrid, comboBox.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox5.Text, comboBox4.Text, slider.Value);
        }
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Controller.Controller.ControllerSlider_ValueChanged(sender, e, label5);
        }
        //Events after the form is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Size tires list
            for (int i = 12; i < 23; i++)
            {
                comboBox2.Items.Add(i);
            }
            //Season list
            for (int i = 0; i < Season.Length; i++)
            {
                comboBox4.Items.Add(Season[i]);
            }
            //Height profile tires list
            for (int i = 0; i < ProfileHeight.Length; i++)
            {
                comboBox3.Items.Add(ProfileHeight[i]);
            }
            //Width profile tires list
            for (int i = 0; i < ProfileWidth.Length; i++)
            {
                comboBox5.Items.Add(ProfileWidth[i]);
            }
            //Brand tires list
            List<string> BrandList = new List<string>();
            foreach (var i in Brand)
                BrandList.Add(i);
            BrandList.Sort();
            for (int i = 0; i < BrandList.Count; i++)
            {                
                comboBox1.Items.Add(BrandList[i]);
            }
            //Company list
            List<string> CompanyList = new List<string>();
            CompanyList.AddRange(Company);
            CompanyList.Sort();        
            for (int i = 0; i < CompanyList.Count; i++)
            {                
                comboBox.Items.Add(CompanyList[i]);
            }
            dt_Column();
        }
        //Closing to form
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Clear filter
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Controller.Controller.ControllercCearFilter(comboBox,comboBox1,comboBox2,comboBox3,comboBox4,comboBox5,slider);
        }
    }
}
