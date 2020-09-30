using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using nadis.Models.sp;
using nadis.tools;
using Dapper;

namespace nadis.DAL
{

    public static class spDAL
    {
        static string[] mmm = 
        new string[]{"янв","фев","мар",
                    "апр","май","июн",
                    "июл","авг","сен",
                    "окт","ноя","дек"
            };

        public static string connStr{get;}

        static spDAL()
        {
            var appSettingsJson = AppSettingJSON.GetAppSettings();
            connStr = appSettingsJson["DefaultConnection"];
        }

        public static SelectList PlanYearList()
        {
            List<sp_values> tmp = new List<sp_values>();
            tmp.Add(new sp_values(){ID = (DateTime.Today.Year+1).ToString(),
                                    Text = (DateTime.Today.Year+1).ToString()});
            tmp.Add(new sp_values(){ID = (DateTime.Today.Year).ToString(),
                                    Text = (DateTime.Today.Year).ToString()});
            tmp.Add(new sp_values(){ID = (DateTime.Today.Year-1).ToString(),
                                    Text = (DateTime.Today.Year-1).ToString()});

            return new SelectList(tmp, "ID", "Text",tmp[1].ID);        
        }
        public static SelectList ReportsList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            tmpList.Add(new sp_values(){ID = "ctvet1a-CA-Raion",
                                        Text = "Заразные по районам"});
            tmpList.Add(new sp_values(){ID = "ctvet1a-CA-Obl",
                                        Text = "Заразные по области"});
            tmpList.Add(new sp_values(){ID = "ctvet1a-CA-KR",
                                        Text = "Заразные по КР"});

            tmpList.Add(new sp_values(){ID = "ctvet1b-CA-Raion",
                                        Text = "Диагностика по районам"});
            tmpList.Add(new sp_values(){ID = "ctvet1b-CA-Obl",
                                        Text = "Диагностика по области"});
            tmpList.Add(new sp_values(){ID = "ctvet1b-CA-KR",
                                        Text = "Диагностика по КР"});

            tmpList.Add(new sp_values(){ID = "ctvet1c-CA-Raion",
                                        Text = "Профилактика по районам"});
            tmpList.Add(new sp_values(){ID = "ctvet1c-CA-Obl",
                                        Text = "Профилактика по области"});
            tmpList.Add(new sp_values(){ID = "ctvet1c-CA-KR",
                                        Text = "Профилактика по КР"});

            tmpList.Add(new sp_values(){ID = "BioPrep-CA-Raion",
                                        Text = "Биопрепараты по районам"});
            tmpList.Add(new sp_values(){ID = "BioPrep-CA-Obl",
                                        Text = "Биопрепараты по области"});
            tmpList.Add(new sp_values(){ID = "BioPrep-CA-KR",
                                        Text = "Биопрепараты по КР"});
            tmpList.Add(new sp_values(){ID = "PEM-CA-Raion",
                                        Text = "Анали ПЭМ по району"});
            tmpList.Add(new sp_values(){ID = "PEM-CA-Obl",
                                        Text = "Анали ПЭМ по области"});
            tmpList.Add(new sp_values(){ID = "PEM-CA-KR",
                                        Text = "Анализ ПЭМ по КР"});
            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }

        public static SelectList RaznaryankaReportsList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            tmpList.Add(new sp_values(){ID = "Raznaryadka BrycellezREV-1",
                                        Text = "Вакцина РЕВ-1"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka BrycellezSHT19",
                                        Text = "Вакцина Штамм-19"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka Chyma",
                                        Text = "Вакцина против чумы"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka Ospa",
                                        Text = "Вакцина против Оспы"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka Sibir",
                                        Text = "Вакцина против сибирской язвы"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka TyberkylinBirds",
                                        Text = "Туберкулин для птиц"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka TyberkylinKRS",
                                        Text = "Туберкулин для млекопитающих"});
            tmpList.Add(new sp_values(){ID = "Raznaryadka Yashyr",
                                        Text = "Вакцина против ящура"});

            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }

        public static SelectList OblastList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            tmpList.Add(new sp_values(){ID = "",Text = "ВСЕ области"});
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT SocUnit as ID, SocUnit FROM [71GEO1]" +
                                                " ORDER BY SocUnit",
                                                 _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["ID"].ToString();
                        tmp.Text = dr["Socunit"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }

        public static SelectList RaionsList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            tmpList.Add(new sp_values(){ID = "",Text = "ВСЕ районы"});
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT SocUnit as ID, SocUnit FROM [72GEO2]" +
                                                " ORDER BY SocUnit",
                                                 _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["ID"].ToString();
                        tmp.Text = dr["Socunit"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }

        //d3DISTYPES
        public static string KIDdtpName(string KIDtyp)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [DisType] FROM [d3DISTYPES] WHERE [KID]=@val", new { val = KIDtyp});
                if(rez==null)return "";else return rez.Trim();
            }
        }
        public static SelectList KIDdtpList()
        {
            using(SqlConnection _conn = new SqlConnection(connStr))
            {
                var tmp = _conn.Query<sp_values>("SELECT KID as ID, [DisType] as Text FROM [d3DISTYPES]");
                List<sp_values> tL = new List<sp_values>();
                foreach (var tt in tmp)
                {
                    tL.Add(tt);
                }
                return new SelectList(tL, "ID", "Text");
            }
        }
        //KIDtyp
        public static string KIDtypName(string KIDtyp)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Sanitary] FROM [d3SANITARY] WHERE [KID]=@val", new { val = KIDtyp});
                if(rez==null)return "";else return rez.Trim();
            }
        }

        public static SelectList KIDtypList()
        {
            using(SqlConnection _conn = new SqlConnection(connStr))
            {
                var tmp = _conn.Query<sp_values>("SELECT KID as ID, [Sanitary] as Text FROM [d3SANITARY]");
                List<sp_values> tL = new List<sp_values>();
                foreach (var tt in tmp)
                {
                    tL.Add(tt);
                }
            return new SelectList(tL, "ID", "Text");
            }
        }

        public static SelectList KIDroList()
        {
            using SqlConnection _conn = new SqlConnection(connStr);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, [VetUnit] as Text FROM [1VETUNITS]");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }
            return new SelectList(tL, "ID", "Text");
        }
        
        //d3PREVMEASURES
        public static SelectList KIDtrtList()
        {
            using SqlConnection _conn = new SqlConnection(connStr);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, [Measure] as Text FROM [d3PREVMEASURES]");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }
            return new SelectList(tL, "ID", "Text");
        }
        
        public static string KIDtrtName(string KIDtrt)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Measure] FROM [d3PREVMEASURES] WHERE [KID]=@val", new { val = KIDtrt});
                if(rez==null)return "";else return rez.Trim();
            }
        }

        public static string KIDdisName(string KIDdis)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string rez = _conn.QueryFirstOrDefault<string>(
                        "SELECT TOP 1 [Disease] FROM [d2DISEASES] WHERE [KID]=@val", new { val = KIDdis});
                if(rez==null)return "";else return rez.Trim();
            }
        }


        public static string KIDdivName(string KIDdiv)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Socunit] FROM [73GEO3] WHERE [gAID]=@val", new { val = KIDdiv});
                if(rez==null)return "";else return rez.Replace("АИЛЬНЫЙ ОКРУГ","").Trim();
            }
        }
        
        public static string KIDspcName(string KIDspc)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                if(KIDspc is null) return "";
                if(KIDspc.Length==0) return "";
                string rez = _conn.QueryFirst<string>(
                        "SELECT TOP 1 [Species] FROM [d2SPECIES] WHERE KID=@val", new { val = KIDspc});
                if(rez==null) return ""; else return rez.Trim();
            }
        }

        public static SelectList VetPrepList()
        {
            using SqlConnection _conn = new SqlConnection(connStr);
            string q = "SELECT VetPrep.KID as ID, (VetPrep.PrepType + ' (' + EdIzm.EdIzm + ')') as Text  FROM VetPrep "+
                       "INNER JOIN  EdIzm ON VetPrep.EdizmID = EdIzm.KID";
            //"SELECT KID as ID, PrepType as Text FROM VetPrep"
            var tmp = _conn.Query<sp_values>(q);
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp)
            {
                tL.Add(tt);
            }
            return new SelectList(tL, "ID", "Text");
        }

        public static SelectList EdIzmList()
        {
            using SqlConnection _conn = new SqlConnection(connStr);
            var tmp = _conn.Query<sp_values>("SELECT KID as ID, EdIzm as Text FROM EdIzm");
            List<sp_values> tL = new List<sp_values>();
            foreach (var tt in tmp) tL.Add(tt);
            return new SelectList(tL, "ID", "Text");
        }

        public static string VetPrepName(string KIDVetPrep)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string q = "SELECT (VetPrep.PrepType + ' ('+EdIzm.EdIzm+')') as Text FROM VetPrep "+
                            "INNER JOIN  EdIzm ON VetPrep.EdizmID = EdIzm.KID WHERE VetPrep.KID=@VetPP";
                            //"SELECT TOP 1 PrepType FROM [VetPrep] WHERE KID=@VetPP"
                string VetUnit = _conn.QueryFirst<string>(q, new { VetPP = KIDVetPrep});
                if(VetUnit==null)return "";else return VetUnit.Trim();
            }
        }

        
        public static string EdIzmID(string vetP_id)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string q = "SELECT EdIzm.KID as ID FROM EdIzm "+
                            "INNER JOIN  VetPrep ON VetPrep.EdizmID = EdIzm.KID "+
                            "WHERE VetPrep.KID=@VetPP";
                //"SELECT TOP 1 EdIzm FROM [EdIzm] WHERE KID=@VetPP"
                return _conn.QueryFirst<string>(q, new { VetPP = vetP_id}).Trim();
            }
        }
        

        public static SelectList ReportToToday()
        {
            List<sp_values> tmpList = new List<sp_values>();
            DateTime dtB = new DateTime(DateTime.Today.Year-1,1,1);
            DateTime dtE =  DateTime.Today;
            while(dtB <= dtE)
            {
                sp_values tmp_sp = new sp_values
                {
                    ID = dtB.ToString("MM yyyy"),//dtB.Month.ToString(),
                    Text = mmm[dtB.Month-1] + " " + dtB.Year.ToString()
                };
                dtB = dtB.AddMonths(1);
                tmpList.Add(tmp_sp);
            }
            SelectList sl = new SelectList(tmpList,"ID","Text");
            string dv = DateTime.Today.ToString("MM yyyy");
            foreach(var it in sl)
                if(it.Value.ToString()== dv) it.Selected = true;
            return sl;//new SelectList(tmpList,"ID","Text");
        }

        public static List<sp_values> __RepMO1YearList(int YY, int MM)
        {
            List<sp_values> tmpList = new List<sp_values>();
            DateTime dtB = new DateTime(YY,MM,1);
            DateTime dtE =  DateTime.Today;
            while(dtB <= dtE)
            {
                sp_values tmp_sp = new sp_values
                {
                    ID = dtB.ToString().Trim(),
                    Text = mmm[dtB.Month-1] + " " + dtB.Year.ToString()
                };
                dtB = dtB.AddMonths(1);
                tmpList.Add(tmp_sp);
            }
            return tmpList;
        }
        public static SelectList RepMO1YearList(int y,int m)
        {
            return new SelectList(__RepMO1YearList(y,m), "ID", "Text");
        }

        public static SelectList KIDdivList(string KIDro)
        {
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT gAID as KID,Socunit FROM[73GEO3] " +
                    "INNER JOIN[1VETUNITS] ON[1VETUNITS].KIDray =[73GEO3].gRID " +
                        "WHERE[1VETUNITS].KID = '"+KIDro+"'", _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Socunit"].ToString().Replace("АИЛЬНЫЙ ОКРУГ","").Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text",tmpList[0].ID);
        }
        
        public static SelectList KIDspcList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT KID, Species FROM [d2SPECIES]", _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Species"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text");
        }

        public static SelectList KIDdisList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            tmpList.Add(new sp_values{ ID="",Text=""});
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT KID, Disease FROM [d2DISEASES] order by Disease", _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Disease"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            return new SelectList(tmpList, "ID", "Text");
        }

        public static SelectList testList()
        {
            List<sp_values> tmpList = new List<sp_values>();
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT KID,Testname FROM [d2TESTS]", _conn);
                _conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sp_values tmp = new sp_values();
                    {
                        //tmp.ID = dr["KID"].ToString().Trim();
                        tmp.ID = dr["KID"].ToString();
                        tmp.Text = dr["Testname"].ToString().Trim();
                    }
                    tmpList.Add(tmp);
                }
                _conn.Close();
            }
            //return new SelectList(tmpList, "ID", "Text", tmpList[0].ID);
            return new SelectList(tmpList, "ID", "Text");
        }

        public static string testName(string tst)
        {
            using (SqlConnection _conn = new SqlConnection(connStr))
            {
                string t = _conn.QueryFirstOrDefault<string>(
                        "SELECT TOP 1 Testname FROM [d2TESTS] WHERE KID=@tt", new { tt = tst});
                if(t==null) return ""; else return t.Trim();
            }
        }        
    }
}
