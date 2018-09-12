using System;
//using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace SearchPrice.Controller
{ 
    public partial class Controller:MainWindow
    {
        static string ext;
        public static void ControllercCearFilter(ComboBox comboBox, ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3, ComboBox comboBox4, ComboBox comboBox5, Slider slider)
        {
            comboBox.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            slider.Value = 0;
        }
        public static void ControllerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e,Label label5)
        {
            (sender as Slider).Value = Math.Round(e.NewValue, 0);
            label5.Content = (sender as Slider).Value;
        }
        public static void ControllerClickSearh(DataGrid dataGrid,string comboBox, string comboBox1, string comboBox2, string comboBox3, string comboBox5, string comboBox4, double slider)
        {
            Stopwatch stopWatch = new Stopwatch();
            App.CompanyNamePrice = comboBox;
            App.BrandNamePrice = comboBox1;
            App.Rtires = comboBox2;
            App.Hprofile = comboBox3;
            App.Wprofile = comboBox5;
            App.SeasonTires = comboBox4;
            App.PriceTires = slider;
            ClearPathDATA();
            if (dataGrid.Items.Count > 0)
            {
                dt.Clear();
                dataGrid.DataContext = dt;
                App.coll.Clear();
            }
            if (App.CompanyNamePrice == "" && App.BrandNamePrice == "" && App.Rtires == "" && App.Hprofile == "" && App.SeasonTires == "" && App.Wprofile == "" && App.PriceTires == 0)
            {
                MessageBox.Show("Please add more parameters!", "Attention", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                return;
            }
            stopWatch.Start();
            if (App.CompanyNamePrice != "")
            {
                SelectFileXls_Xml_Csv();
                // ПОИСК ПО ОДНОМУ ИЗ ПРАЙСОВ
                if (App.path_all[0] == null && (App.path != "" || App.pathShinService_summer != ""))
                {
                    if (App.CompanyNamePrice != "ShinService")
                    {
                        ext = App.path.Substring(App.path.LastIndexOf('.'));
                    }
                    else { ext = App.pathShinService_summer.Substring(App.pathShinService_summer.LastIndexOf('.'));  }
                    if (ext == ".xlsx" || ext == ".xls") // Если прайс формата Excel
                    {
                        ExcelData exceldata = new ExcelData();
                        if (App.CompanyNamePrice == "EuroDiski") //ПРАЙС ЕВРОДИСКИ
                        {
                            exceldata.pathFile = App.path;
                            var enumerable = exceldata.DataXLS;
                            ResultEuroDiski(enumerable);
                        }
                        if (App.CompanyNamePrice == "Svrauto") //ПРАЙС СВРАВТО
                        {
                            exceldata.pathFile = App.path;
                            var enumerable = exceldata.DataXLS;
                            App.BrandNameSVR = App.BrandNamePrice;
                            SvrautoBrandParse();
                            ResultSvrauto(enumerable);
                        }
                        if (App.CompanyNamePrice == "Twinmax") //ПРАЙС ТВИНМАКС
                        {
                            exceldata.pathFile = App.path;
                            var enumerable = exceldata.DataXLS;
                            ResultTWINMAX(enumerable);
                        }
                        if (App.CompanyNamePrice == "ShinService") //ПРАЙС ШИНСЕРВИС
                        {
                            //dt.Rows.Add("ShinService");
                            if (App.SeasonTires == "Summer")
                            {
                                exceldata.pathFile = App.pathShinService_summer;
                                var enumerable_summer = exceldata.DataXLS;
                                App.VerPrice = "sun";
                                ResultShinService(enumerable_summer);

                            }
                            if (App.SeasonTires == "Winter")
                            {
                                exceldata.pathFile = App.pathShinService_winter;
                                var enumerable_winter = exceldata.DataXLS;
                                App.VerPrice = "winter";
                                ResultShinService(enumerable_winter);                             
                            }
                            if(App.SeasonTires == "")
                            {
                                exceldata.pathFile = App.pathShinService_summer;
                                var enumerable_summer = exceldata.DataXLS;
                                App.VerPrice = "sun";
                                ResultShinService(enumerable_summer);
                                
                                exceldata.pathFile = App.pathShinService_winter;
                                var enumerable_winter = exceldata.DataXLS;
                                App.VerPrice = "winter";
                                ResultShinService(enumerable_winter);
                            }
                        }
                    }
                    if (ext == ".xml") { } // Если прайс формата XML
                    if (ext == ".csv") // Если прайс формата CSV
                    {
                        CsvData exceldata = new CsvData();
                        if (App.CompanyNamePrice == "Master Shina") //ПРАЙС МАСТЕРШИНА
                        {
                            exceldata.pathFile = App.path;
                            var enumerable = exceldata.DataCSV;
                            //ResultMaster_Shina(enumerable);
                        }
                    } 
                }
                // ПОИСК ПО ВСЕМУ ПРАЙСУ
                if (App.path_all[0] != "" && App.path=="" && App.pathShinService_winter == "" && App.pathShinService_summer == "") 
                {
                    ExcelData exceldata = new ExcelData();

                        App.CompanyNamePrice = "EuroDiski"; //ПРАЙС ЕВРОДИСКИ
                        exceldata.pathFile = App.path_all[0];
                        var enumerable_EuroDiski = exceldata.DataXLS;
                        ResultEuroDiski(enumerable_EuroDiski);

                        App.CompanyNamePrice = "Svrauto"; //ПРАЙС СВРАВТО
                        exceldata.pathFile = App.path_all[1];
                        var enumerable_Svrauto = exceldata.DataXLS;
                            App.BrandNameSVR = App.BrandNamePrice;
                            SvrautoBrandParse();
                        ResultSvrauto(enumerable_Svrauto);

                        App.CompanyNamePrice = "Twinmax"; //ПРАЙС Twinmax
                        exceldata.pathFile = App.path_all[2];
                        var enumerable_Twinmax = exceldata.DataXLS;
                        ResultTWINMAX(enumerable_Twinmax);

                        App.CompanyNamePrice = "ShinService"; //ПРАЙС ShinService
                        if (App.SeasonTires == "")
                        {
                            exceldata.pathFile = App.path_all[3];
                            var enumerable_ShinService_summer = exceldata.DataXLS;
                                App.VerPrice = "sun";
                            ResultShinService(enumerable_ShinService_summer);
                            exceldata.pathFile = App.path_all[4];
                            var enumerable_ShinService_winter = exceldata.DataXLS;
                                App.VerPrice = "winter";
                            ResultShinService(enumerable_ShinService_winter);
                        }
                        if (App.SeasonTires == "Summer")
                        {
                            exceldata.pathFile = App.path_all[3];
                            var enumerable_ShinService_summer = exceldata.DataXLS;
                            App.VerPrice = "sun";
                            ResultShinService(enumerable_ShinService_summer);
                        }
                        if (App.SeasonTires == "Winter")
                        { 
                            exceldata.pathFile = App.path_all[4];
                            var enumerable_ShinService_winter = exceldata.DataXLS;
                                App.VerPrice = "winter";
                            ResultShinService(enumerable_ShinService_winter);
                        }
                }
                dataGrid.ItemsSource = App.coll;
                dataGrid.Items.Refresh();
            }
            if (dataGrid.Items.Count <= 0)
            {
                MessageBox.Show("Data not found! Try adding more parameters!", "Attention", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
            }
            MessageBox.Show("Timer Search: " + stopWatch.Elapsed.ToString());
            stopWatch.Reset();
        }
    }
}
