using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace SearchPrice.View
{
    public partial class View:MainWindow
    {
        public static string index = "-";
        public static void CollectionPrint(string company, DataRow dr)
        {
            App.coll.Add(new Price()
            {
                Company = company,
                Name = Convert.ToString(dr[0]),
                Art = dr[1].ToString(),
                Brand = dr[2].ToString(),
                Model = dr[3].ToString(),
                WidthT = dr[4].ToString(),
                HeightT = dr[5].ToString(),
                Rt = dr[6].ToString(),
                SeazonT = dr[7].ToString(),
                Pin = dr[8].ToString(),
                IndexH = dr[9].ToString(),
                IndexV = dr[10].ToString(),
                RunFlat = dr[11].ToString(),
                OptPrice = dr[12].ToString(),
                RozPrice = dr[13].ToString(),
                CenterBox = dr[14].ToString(),
                RemoteBox = dr[15].ToString(),
                Balans = dr[16].ToString(),
            });
        }
        public static void ResultViewEuroDiski(IEnumerable<DataRow> result)
        {
            int column = 0, col = 0;
            foreach (var i in result)
            {
                DataRow dr = dt.NewRow();
                for (column = 1; column <= i.Table.Columns.Count; column++)
                {
                    col = column;
                    if (column == 16)
                    { col--; }
                    if (column > 16)
                    { col = col - 2; }
                    if (column == 14 || column == 16)
                    { column++; }
                    if (i[column - 1].ToString() == "")
                    {dr[col - 1] = index; }
                    else
                    {
                        dr[col - 1] = Convert.ToString(i[column - 1]);                      
                    }
                }
                CollectionPrint("EuroDiski",dr);
            }
        }
        public static void ResultViewSvrauto(IEnumerable<DataRow> result)
        {
            int column;
            foreach (var i in result)
            {
                DataRow dr = dt.NewRow();
                for (column = 0; column < i.Table.Columns.Count; column++)
                {
                    if(column == 0)
                        dr[1] = Convert.ToString(i[column]);
                    if (column == 1)
                        dr[0] = Convert.ToString(i[column]);
                    if(column == 4)
                        dr[2] = Convert.ToString(i[column]);
                    if (column == 5)
                    {
                        string run = "Run Flat";
                        dr[3] = Convert.ToString(i[column]);
                        if(Convert.ToString(i[column]).Contains(run)) 
                            dr[11] = "Да";
                    }
                    if (column == 6)
                        dr[6] = Convert.ToString(i[column]);
                    if (column == 7)
                        dr[4] = Convert.ToString(i[column]);
                    if (column == 8)
                        dr[5] = Convert.ToString(i[column]);
                    if (column == 3)
                        dr[7] = Convert.ToString(i[column]);
                    if (column == 9)
                        dr[8] = Convert.ToString(i[column]);
                    if (column == 11)
                        dr[9] = Convert.ToString(i[column]);
                    if (column == 10)
                        dr[10] = Convert.ToString(i[column]);
                    if (column == 19)
                        dr[12] = "Цена по предоплате - " + Convert.ToString(i[column]);
                    if (column == 20)
                        dr[13] = Convert.ToString(i[column]);
                    if (column == 17)
                        dr[16] = Convert.ToString(i[column]);
                }
                CollectionPrint("SVRAUTO", dr);
            }
        }
        public static void ResultViewMaster_Shina(IEnumerable<DataRow> result)
        {
            int column;
            foreach (var i in result)
            {
                DataRow dr = dt.NewRow();
                for (column = 0; column < i.Table.Columns.Count; column++)
                {
                    if (column == 0)
                    {
                        string firma = App.regexMasterShina_firma.Match(Convert.ToString(i[column])).Value;
                        dr[2] = firma.ToUpper();
                        string model =App.regexMasterShina_modlel.Match(Convert.ToString(i[column])).Value;
                        dr[3] = model;
                        string run = "RunFlat";
                        string pin = "шип.";
                        if (Convert.ToString(i[column]).Contains(run))
                            dr[11] = "Да";
                        if (Convert.ToString(i[column]).Contains(pin))
                            dr[8] = "ШИП.";
                        else dr[8] = "н/ш.";
                    }
                    if (column == 1)
                        dr[4] = Convert.ToString(i[column]);
                    if (column == 2)
                        dr[5] = Convert.ToString(i[column]);
                    if (column == 3)
                        dr[6] = Convert.ToString(i[column]);
                    if (column == 4)
                        dr[7] = Convert.ToString(i[column]);
                    if (column == 5)
                        dr[16] = Convert.ToString(i[column]);
                    if (column == 6)
                        dr[13] = Convert.ToString(i[column]);
                }
                CollectionPrint("MasterShina", dr);
            }
        }
        public static void ResultViewTWINMAX(IEnumerable<DataRow> result)
        {
            int column;
            foreach (var i in result)
            {
                DataRow dr = dt.NewRow();
                for (column = 0; column < i.Table.Columns.Count; column++)
                {
                    if (column == 0)
                        dr[1] = Convert.ToString(i[column]);
                    if (column == 1)
                    {
                        string[] RFT = new string[4] { "Flat Run", "FR", "RF", "RFT" };
                        string[] sezon = new string[2] { "Kitka", "Nasta" };
                        dr[0] = Convert.ToString(i[column]);
                        for (int j = 0; j < RFT.Count(); j++)
                        {
                            if (Convert.ToString(i[column]).Contains(RFT[j]))
                                dr[11] = "Да";
                        }
                        string firma = App.regexTwinMax.Match(Convert.ToString(i[column])).Value;
                        dr[2] = firma.ToUpper();
                        if (Convert.ToString(i[column]).Contains(sezon[0]) == true)
                        {
                            dr[7] = "Зимняя";
                            dr[8] = "н/ш.";
                        }
                        if (Convert.ToString(i[column]).Contains(sezon[1]) == true)
                        {
                            dr[7] = "Зимняя";
                            dr[8] = "Ш.";
                        }
                        if (Convert.ToString(i[column]).Contains(sezon[0]) == false && Convert.ToString(i[column]).Contains(sezon[1]) == false)
                        {
                            dr[7] = "Летняя";
                        }
                    }
                    if (column == 2)
                        dr[4] = Convert.ToString(i[column]);
                    if (column == 4)
                        dr[6] = Convert.ToString(i[column]);
                    if (column == 5)                     
                        dr[9] = Convert.ToString(i[column]);
                    if (column == 6)
                        dr[10] = Convert.ToString(i[column]);
                    if (column == 7)
                        dr[14] = Convert.ToString(i[column]);
                    if (column == 8)
                        dr[15] = Convert.ToString(i[column]);
                    if (column == 3)
                        dr[5] = Convert.ToString(i[column]);
                    if (column == 9)
                        dr[12] = "Стоимость - " + Convert.ToString(i[column]);
                    if (column == 10)
                        dr[13] = "Стоимость с доставкой - " + Convert.ToString(i[column]);
                }
                CollectionPrint("TWINMAX", dr);
            }
        }
        public static void ResultViewShinService(IEnumerable<DataRow> result)
        {
            int column;
            foreach (var i in result)
            {
                DataRow dr = dt.NewRow();
                for (column = 0; column < i.Table.Columns.Count; column++)
                {
                    if(App.VerPrice=="sun")
                    { dr[7] = "Летняя"; }
                    if(App.VerPrice=="winter")
                    { dr[7] = "Зимняя"; }
                    if (column == 1)
                        dr[1] = Convert.ToString(i[column]);
                    if (column == 2)
                    {
                        string R = App.regexShServ_R.Match(Convert.ToString(i[column])).Value;
                        string Width = App.regexShServ_W.Match(Convert.ToString(i[column])).Value;
                        string Height =App.regexShServ_H.Match(Convert.ToString(i[column])).Value;
                        dr[6] = R;
                        dr[4] = Width;
                        if (Height != "")
                        {
                            dr[5] = Height.Remove(0, 1);
                        }
                    }
                    if (column == 3)
                        dr[2] = Convert.ToString(i[column]);
                    if (column == 4)
                    {
                        if (App.VerPrice == "sun")
                        {
                            string run = "Runflat";
                            if (Convert.ToString(i[column]).Contains(run))
                                dr[11] = "Да";
                        }
                        if (App.VerPrice == "winter")
                        {
                            dr[8] = Convert.ToString(i[column]);
                        }
                    }
                    if (column == 5)
                    {
                        if (App.VerPrice == "winter")
                        {
                            string run = "Runflat";
                            if (Convert.ToString(i[column]).Contains(run))
                                dr[11] = "Да";      
                        }
                    }
                    if (column == 6)
                    {
                        if (App.VerPrice == "sun")
                        {

                            dr[0] = Convert.ToString(i[column]);
                        }
                    }
                    if (column == 7)
                    {
                        if (App.VerPrice == "winter")
                        {
                            dr[0] = Convert.ToString(i[column]);
                        }
                        if (App.VerPrice == "sun")
                        {
                            dr[12] = "B2B - " + Convert.ToString(i[column]);
                        }
                    }
                    if (column == 8)
                    {
                        if (App.VerPrice == "winter")
                        {
                            dr[12] = "B2B - " + Convert.ToString(i[column]);
                        }
                    }
                    if (column == 9)
                    {
                        if (App.VerPrice == "sun")
                        {
                            dr[13] = "МИЦ - " + Convert.ToString(i[column]);
                        }
                    }
                    if (column == 10)
                    {
                        if (App.VerPrice == "winter")
                        {
                            dr[13] = "МИЦ - " + Convert.ToString(i[column]);
                        }
                    }
                }
                CollectionPrint("ShinService", dr);
            }
        }
    }
}
