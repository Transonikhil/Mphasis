using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Mphasis_webapp
{
    public partial class CurrentTable : System.Web.UI.Page
    {
        double la = 0;
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string q1 = "delete from dr_ctp where ISDATE(vdate)<>1";
                bucket.ExecuteQuery(q1);
            }
            catch (Exception ex)
            {

            }
            AGS(); FSS(); Euronet(); PRIZM(); HDFC(); TCPSL();
        }


        //ICICI();DCB();
        /*public string getRemarks()
        {
            string remarks = System.DateTime.Now.Date.ToString("dd'.'MM'.'yyyy");
            return remarks;
        }*/

        public void FSS()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW61;
            LNW[1] = LNW62;
            LNW[2] = LNW63;
            LNW[3] = LNW64;
            LNW[4] = LNW65;
            LNW[5] = LNW66;
            LNW[6] = LNW67;
            LNW[7] = LNW68;
            LNW[8] = LNW69;
            LNW[9] = LNW70;
            LNW[10] = LNW93;
            LNW[11] = LNW94;
            LNW[12] = LNW95;
            LNW[13] = LNW96;
            LNW[14] = LNW97;
            LNW[15] = LNW98;
            LNW[16] = LNW113;

            Label[] LW = new Label[17];
            LW[0] = LW61;
            LW[1] = LW62;
            LW[2] = LW63;
            LW[3] = LW64;
            LW[4] = LW65;
            LW[5] = LW66;
            LW[6] = LW67;
            LW[7] = LW68;
            LW[8] = LW69;
            LW[9] = LW70;
            LW[10] = LW93;
            LW[11] = LW94;
            LW[12] = LW95;
            LW[13] = LW96;
            LW[14] = LW97;
            LW[15] = LW98;
            LW[16] = LW113;

            Label[] LT = new Label[17];
            LT[0] = LT61;
            LT[1] = LT62;
            LT[2] = LT63;
            LT[3] = LT64;
            LT[4] = LT65;
            LT[5] = LT66;
            LT[6] = LT67;
            LT[7] = LT68;
            LT[8] = LT69;
            LT[9] = LT70;
            LT[10] = LT93;
            LT[11] = LT94;
            LT[12] = LT95;
            LT[13] = LT96;
            LT[14] = LT97;
            LT[15] = LT98;
            LT[16] = LT113;

            Label[] LP = new Label[17];
            LP[0] = LP61;
            LP[1] = LP62;
            LP[2] = LP63;
            LP[3] = LP64;
            LP[4] = LP65;
            LP[5] = LP66;
            LP[6] = LP67;
            LP[7] = LP68;
            LP[8] = LP69;
            LP[9] = LP70;
            LP[10] = LP93;
            LP[11] = LP94;
            LP[12] = LP95;
            LP[13] = LP96;
            LP[14] = LP97;
            LP[15] = LP98;
            LP[16] = LP113;

            table("FSS", LNW, LW, LT, LP, LA5);
            P4.Text = Math.Round(Convert.ToDouble(LA5.Text), 2) + " %";
        }
        public void AGS()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW1;
            LNW[1] = LNW2;
            LNW[2] = LNW3;
            LNW[3] = LNW4;
            LNW[4] = LNW5;
            LNW[5] = LNW6;
            LNW[6] = LNW7;
            LNW[7] = LNW8;
            LNW[8] = LNW9;
            LNW[9] = LNW10;
            LNW[10] = LNW81;
            LNW[11] = LNW82;
            LNW[12] = LNW83;
            LNW[13] = LNW84;
            LNW[14] = LNW85;
            LNW[15] = LNW86;
            LNW[16] = LNW111;

            Label[] LW = new Label[17];
            LW[0] = LW1;
            LW[1] = LW2;
            LW[2] = LW3;
            LW[3] = LW4;
            LW[4] = LW5;
            LW[5] = LW6;
            LW[6] = LW7;
            LW[7] = LW8;
            LW[8] = LW9;
            LW[9] = LW10;
            LW[10] = LW81;
            LW[11] = LW82;
            LW[12] = LW83;
            LW[13] = LW84;
            LW[14] = LW85;
            LW[15] = LW86;
            LW[16] = LW111;

            Label[] LT = new Label[17];
            LT[0] = LT1;
            LT[1] = LT2;
            LT[2] = LT3;
            LT[3] = LT4;
            LT[4] = LT5;
            LT[5] = LT6;
            LT[6] = LT7;
            LT[7] = LT8;
            LT[8] = LT9;
            LT[9] = LT10;
            LT[10] = LT81;
            LT[11] = LT82;
            LT[12] = LT83;
            LT[13] = LT84;
            LT[14] = LT85;
            LT[15] = LT86;
            LT[16] = LT111;

            Label[] LP = new Label[17];
            LP[0] = LP1;
            LP[1] = LP2;
            LP[2] = LP3;
            LP[3] = LP4;
            LP[4] = LP5;
            LP[5] = LP6;
            LP[6] = LP7;
            LP[7] = LP8;
            LP[8] = LP9;
            LP[9] = LP10;
            LP[10] = LP81;
            LP[11] = LP82;
            LP[12] = LP83;
            LP[13] = LP84;
            LP[14] = LP85;
            LP[15] = LP86;
            LP[16] = LP111;
            table("AGS", LNW, LW, LT, LP, LA);
            P1.Text = Math.Round(Convert.ToDouble(LA.Text), 2) + " %";
        }
        public void TCPSL()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW11;
            LNW[1] = LNW12;
            LNW[2] = LNW13;
            LNW[3] = LNW14;
            LNW[4] = LNW15;
            LNW[5] = LNW16;
            LNW[6] = LNW17;
            LNW[7] = LNW18;
            LNW[8] = LNW19;
            LNW[9] = LNW20;
            LNW[10] = LNW105;
            LNW[11] = LNW106;
            LNW[12] = LNW107;
            LNW[13] = LNW108;
            LNW[14] = LNW109;
            LNW[15] = LNW110;
            LNW[15] = LNW110;
            LNW[16] = LNW115;

            Label[] LW = new Label[17];
            LW[0] = LW11;
            LW[1] = LW12;
            LW[2] = LW13;
            LW[3] = LW14;
            LW[4] = LW15;
            LW[5] = LW16;
            LW[6] = LW17;
            LW[7] = LW18;
            LW[8] = LW19;
            LW[9] = LW20;
            LW[10] = LW105;
            LW[11] = LW106;
            LW[12] = LW107;
            LW[13] = LW108;
            LW[14] = LW109;
            LW[15] = LW110;
            LW[16] = LW115;

            Label[] LT = new Label[17];
            LT[0] = LT11;
            LT[1] = LT12;
            LT[2] = LT13;
            LT[3] = LT14;
            LT[4] = LT15;
            LT[5] = LT16;
            LT[6] = LT17;
            LT[7] = LT18;
            LT[8] = LT19;
            LT[9] = LT20;
            LT[10] = LT105;
            LT[11] = LT106;
            LT[12] = LT107;
            LT[13] = LT108;
            LT[14] = LT109;
            LT[15] = LT110;
            LT[16] = LT115;

            Label[] LP = new Label[17];
            LP[0] = LP11;
            LP[1] = LP12;
            LP[2] = LP13;
            LP[3] = LP14;
            LP[4] = LP15;
            LP[5] = LP16;
            LP[6] = LP17;
            LP[7] = LP18;
            LP[8] = LP19;
            LP[9] = LP20;
            LP[10] = LP105;
            LP[11] = LP106;
            LP[12] = LP107;
            LP[13] = LP108;
            LP[14] = LP109;
            LP[15] = LP110;
            LP[16] = LP115;

            table("TCPSL", LNW, LW, LT, LP, LA0);
            P9.Text = Math.Round(Convert.ToDouble(LA0.Text), 2) + " %";
        }

        public void HDFC()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW116;
            LNW[1] = LNW117;
            LNW[2] = LNW118;
            LNW[3] = LNW119;
            LNW[4] = LNW120;
            LNW[5] = LNW121;
            LNW[6] = LNW122;
            LNW[7] = LNW123;
            LNW[8] = LNW124;
            LNW[9] = LNW125;
            LNW[10] = LNW126;
            LNW[11] = LNW127;
            LNW[12] = LNW128;
            LNW[13] = LNW129;
            LNW[14] = LNW130;
            LNW[15] = LNW131;
            LNW[16] = LNW132;


            Label[] LW = new Label[17];
            LW[0] = LW116;
            LW[1] = LW117;
            LW[2] = LW118;
            LW[3] = LW119;
            LW[4] = LW120;
            LW[5] = LW121;
            LW[6] = LW122;
            LW[7] = LW123;
            LW[8] = LW124;
            LW[9] = LW125;
            LW[10] = LW126;
            LW[11] = LW127;
            LW[12] = LW128;
            LW[13] = LW129;
            LW[14] = LW130;
            LW[15] = LW131;
            LW[16] = LW132;

            Label[] LT = new Label[17];
            LT[0] = LT116;
            LT[1] = LT117;
            LT[2] = LT118;
            LT[3] = LT119;
            LT[4] = LT120;
            LT[5] = LT121;
            LT[6] = LT122;
            LT[7] = LT123;
            LT[8] = LT124;
            LT[9] = LT125;
            LT[10] = LT126;
            LT[11] = LT127;
            LT[12] = LT128;
            LT[13] = LT129;
            LT[14] = LT130;
            LT[15] = LT131;
            LT[16] = LT132;

            Label[] LP = new Label[17];
            LP[0] = LP116;
            LP[1] = LP117;
            LP[2] = LP118;
            LP[3] = LP119;
            LP[4] = LP120;
            LP[5] = LP121;
            LP[6] = LP122;
            LP[7] = LP123;
            LP[8] = LP124;
            LP[9] = LP125;
            LP[10] = LP126;
            LP[11] = LP127;
            LP[12] = LP128;
            LP[13] = LP129;
            LP[14] = LP130;
            LP[15] = LP131;
            LP[16] = LP132;

            table("HDFC", LNW, LW, LT, LP, LA7);
            P10.Text = Math.Round(Convert.ToDouble(LA7.Text), 2) + " %";
        }
        public void Euronet()
        {


            Label[] LNW = new Label[17];
            LNW[0] = LNW21;
            LNW[1] = LNW22;
            LNW[2] = LNW23;
            LNW[3] = LNW24;
            LNW[4] = LNW25;
            LNW[5] = LNW26;
            LNW[6] = LNW27;
            LNW[7] = LNW28;
            LNW[8] = LNW29;
            LNW[9] = LNW30;
            LNW[10] = LNW87;
            LNW[11] = LNW88;
            LNW[12] = LNW89;
            LNW[13] = LNW90;
            LNW[14] = LNW91;
            LNW[15] = LNW92;
            LNW[16] = LNW112;
            Label[] LW = new Label[17];
            LW[0] = LW21;
            LW[1] = LW22;
            LW[2] = LW23;
            LW[3] = LW24;
            LW[4] = LW25;
            LW[5] = LW26;
            LW[6] = LW27;
            LW[7] = LW28;
            LW[8] = LW29;
            LW[9] = LW30;
            LW[10] = LW87;
            LW[11] = LW88;
            LW[12] = LW89;
            LW[13] = LW90;
            LW[14] = LW91;
            LW[15] = LW92;
            LW[16] = LW112;

            Label[] LT = new Label[17];
            LT[0] = LT21;
            LT[1] = LT22;
            LT[2] = LT23;
            LT[3] = LT24;
            LT[4] = LT25;
            LT[5] = LT26;
            LT[6] = LT27;
            LT[7] = LT28;
            LT[8] = LT29;
            LT[9] = LT30;
            LT[10] = LT87;
            LT[11] = LT88;
            LT[12] = LT89;
            LT[13] = LT90;
            LT[14] = LT91;
            LT[15] = LT92;
            LT[16] = LT112;

            Label[] LP = new Label[17];
            LP[0] = LP21;
            LP[1] = LP22;
            LP[2] = LP23;
            LP[3] = LP24;
            LP[4] = LP25;
            LP[5] = LP26;
            LP[6] = LP27;
            LP[7] = LP28;
            LP[8] = LP29;
            LP[9] = LP30;
            LP[10] = LP87;
            LP[11] = LP88;
            LP[12] = LP89;
            LP[13] = LP90;
            LP[14] = LP91;
            LP[15] = LP92;
            LP[16] = LP112;
            table("EURONET", LNW, LW, LT, LP, LA1);
            P3.Text = Math.Round(Convert.ToDouble(LA1.Text), 2) + " %";
        }
        public void PRIZM()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW31;
            LNW[1] = LNW32;
            LNW[2] = LNW33;
            LNW[3] = LNW34;
            LNW[4] = LNW35;
            LNW[5] = LNW36;
            LNW[6] = LNW37;
            LNW[7] = LNW38;
            LNW[8] = LNW39;
            LNW[9] = LNW40;
            LNW[10] = LNW99;
            LNW[11] = LNW100;
            LNW[12] = LNW101;
            LNW[13] = LNW102;
            LNW[14] = LNW103;
            LNW[15] = LNW104;
            LNW[16] = LNW114;

            Label[] LW = new Label[17];
            LW[0] = LW31;
            LW[1] = LW32;
            LW[2] = LW33;
            LW[3] = LW34;
            LW[4] = LW35;
            LW[5] = LW36;
            LW[6] = LW37;
            LW[7] = LW38;
            LW[8] = LW39;
            LW[9] = LW40;
            LW[10] = LW99;
            LW[11] = LW100;
            LW[12] = LW101;
            LW[13] = LW102;
            LW[14] = LW103;
            LW[15] = LW104;
            LW[16] = LW114;

            Label[] LT = new Label[17];
            LT[0] = LT31;
            LT[1] = LT32;
            LT[2] = LT33;
            LT[3] = LT34;
            LT[4] = LT35;
            LT[5] = LT36;
            LT[6] = LT37;
            LT[7] = LT38;
            LT[8] = LT39;
            LT[9] = LT40;
            LT[10] = LT99;
            LT[11] = LT100;
            LT[12] = LT101;
            LT[13] = LT102;
            LT[14] = LT103;
            LT[15] = LT104;
            LT[16] = LT114;

            Label[] LP = new Label[17];
            LP[0] = LP31;
            LP[1] = LP32;
            LP[2] = LP33;
            LP[3] = LP34;
            LP[4] = LP35;
            LP[5] = LP36;
            LP[6] = LP37;
            LP[7] = LP38;
            LP[8] = LP39;
            LP[9] = LP40;
            LP[10] = LP99;
            LP[11] = LP100;
            LP[12] = LP101;
            LP[13] = LP102;
            LP[14] = LP103;
            LP[15] = LP104;
            LP[16] = LP114;

            table("HITACHI", LNW, LW, LT, LP, LA2);
            P7.Text = (Convert.ToDecimal(LA2.Text)).ToString("F") + " %";
        }

        public void DCB()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW51;
            LNW[1] = LNW52;
            LNW[2] = LNW53;
            LNW[3] = LNW54;
            LNW[4] = LNW55;
            LNW[5] = LNW56;
            LNW[6] = LNW57;
            LNW[7] = LNW58;
            LNW[8] = LNW59;
            LNW[9] = LNW60;

            Label[] LW = new Label[17];
            LW[0] = LW51;
            LW[1] = LW52;
            LW[2] = LW53;
            LW[3] = LW54;
            LW[4] = LW55;
            LW[5] = LW56;
            LW[6] = LW57;
            LW[7] = LW58;
            LW[8] = LW59;
            LW[9] = LW60;

            Label[] LT = new Label[17];
            LT[0] = LT51;
            LT[1] = LT52;
            LT[2] = LT53;
            LT[3] = LT54;
            LT[4] = LT55;
            LT[5] = LT56;
            LT[6] = LT57;
            LT[7] = LT58;
            LT[8] = LT59;
            LT[9] = LT60;

            Label[] LP = new Label[17];
            LP[0] = LP51;
            LP[1] = LP52;
            LP[2] = LP53;
            LP[3] = LP54;
            LP[4] = LP55;
            LP[5] = LP56;
            LP[6] = LP57;
            LP[7] = LP58;
            LP[8] = LP59;
            LP[9] = LP60;

            table("DCB", LNW, LW, LT, LP, LA4);
            P2.Text = Math.Round(Convert.ToDouble(LA4.Text), 2) + " %";
        }
        public void ICICI()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW71;
            LNW[1] = LNW72;
            LNW[2] = LNW73;
            LNW[3] = LNW74;
            LNW[4] = LNW75;
            LNW[5] = LNW76;
            LNW[6] = LNW77;
            LNW[7] = LNW78;
            LNW[8] = LNW79;
            LNW[9] = LNW80;

            Label[] LW = new Label[17];
            LW[0] = LW71;
            LW[1] = LW72;
            LW[2] = LW73;
            LW[3] = LW74;
            LW[4] = LW75;
            LW[5] = LW76;
            LW[6] = LW77;
            LW[7] = LW78;
            LW[8] = LW79;
            LW[9] = LW80;

            Label[] LT = new Label[17];
            LT[0] = LT71;
            LT[1] = LT72;
            LT[2] = LT73;
            LT[3] = LT74;
            LT[4] = LT75;
            LT[5] = LT76;
            LT[6] = LT77;
            LT[7] = LT78;
            LT[8] = LT79;
            LT[9] = LT80;

            Label[] LP = new Label[17];
            LP[0] = LP71;
            LP[1] = LP72;
            LP[2] = LP73;
            LP[3] = LP74;
            LP[4] = LP75;
            LP[5] = LP76;
            LP[6] = LP77;
            LP[7] = LP78;
            LP[8] = LP79;
            LP[9] = LP80;

            table("ICICI", LNW, LW, LT, LP, LA6);
            P6.Text = Math.Round(Convert.ToDouble(LA6.Text), 2) + " %";
        }
        /*public void TCBIL()
        {
            Label[] LNW = new Label[17];
            LNW[0] = LNW81;
            LNW[1] = LNW82;
            LNW[2] = LNW83;
            LNW[3] = LNW84;
            LNW[4] = LNW85;
            LNW[5] = LNW86;
            LNW[6] = LNW87;
            LNW[7] = LNW88;
            LNW[8] = LNW89;
            LNW[9] = LNW90;

            Label[] LW = new Label[17];
            LW[0] = LW81;
            LW[1] = LW82;
            LW[2] = LW83;
            LW[3] = LW84;
            LW[4] = LW85;
            LW[5] = LW86;
            LW[6] = LW87;
            LW[7] = LW88;
            LW[8] = LW89;
            LW[9] = LW90;

            Label[] LT = new Label[17];
            LT[0] = LT81;
            LT[1] = LT82;
            LT[2] = LT83;
            LT[3] = LT84;
            LT[4] = LT85;
            LT[5] = LT86;
            LT[6] = LT87;
            LT[7] = LT88;
            LT[8] = LT89;
            LT[9] = LT90;

            Label[] LP = new Label[17];
            LP[0] = LP81;
            LP[1] = LP82;
            LP[2] = LP83;
            LP[3] = LP84;
            LP[4] = LP85;
            LP[5] = LP86;
            LP[6] = LP87;
            LP[7] = LP88;
            LP[8] = LP89;
            LP[9] = LP90;

            table("TCBIL", LNW, LW, LT, LP, LA7);
            P8.Text = Math.Round(Convert.ToDouble(LA7.Text),2)+" %";
        }*/


        public void table(string client, Label[] LNW, Label[] LW, Label[] LT, Label[] LP, Label LAP)
        {
            string q = "select vid,q1,q2,q3,q4,q5,q6,q7,q8,q9,q10,q11,q12,q13,q14,q15,q16,q17 from lastexception d inner join atms a on d.atmid=a.atmid where client='" + client + "'";
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLconnstr"].ConnectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand(q, cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int i = 0; int j = 1;
                while (j <= 17)
                {
                    try
                    {
                        if (reader[j].ToString().Contains("E|"))
                        {
                            if (LNW[i].Text == "" || LNW[i].Text == null) { LNW[i].Text = "0"; }

                            LNW[i].Text = Convert.ToString(Convert.ToInt32(LNW[i].Text) + 1);
                        }
                        else
                        {
                            if (LW[i].Text == "" || LW[i].Text == null) { LW[i].Text = "0"; }

                            LW[i].Text = Convert.ToString(Convert.ToInt32(LW[i].Text) + 1);
                        }
                        if (LNW[i].Text == "" || LNW[i].Text == null) { LNW[i].Text = "0"; }
                        if (LW[i].Text == "" || LW[i].Text == null) { LW[i].Text = "0"; }

                        LT[i].Text = Convert.ToString(Convert.ToInt32(LW[i].Text) + Convert.ToInt32(LNW[i].Text));
                        try
                        {
                            LP[i].Text = (Convert.ToDecimal(Convert.ToDouble(Convert.ToDouble(LW[i].Text) / Convert.ToDouble(LT[i].Text) * 100))).ToString("F") + " %";
                        }
                        catch { LP[i].Text = Convert.ToString(Convert.ToDouble(Convert.ToDouble(LW[i].Text) / Convert.ToDouble(LT[i].Text) * 100)) + " %"; }
                    }
                    catch
                    {
                    }
                    j++; i++;
                }
            }
            reader.Close();
            cn.Close();
            cn.Dispose();
            LAP.Text = "0";
            for (int k = 0; k < 17; k++)
            {
                try
                {
                    if (LAP.Text == "" || LAP.Text == null) { LAP.Text = "0"; }
                    LAP.Text = Convert.ToString(Convert.ToDouble(LAP.Text) + Convert.ToDouble(LP[k].Text.Replace("%", "")));
                }
                catch { }
            }
            LAP.Text = Convert.ToDecimal(Convert.ToDouble(LAP.Text) / 17).ToString("F");
            /*
            LAP.Text = Convert.ToString
                (
                Convert.ToDouble(Convert.ToDouble(LP1.Text.Replace("%","")) +
                Convert.ToDouble(LP2.Text.Replace("%","")) +
                Convert.ToDouble(LP3.Text.Replace("%","")) +
                Convert.ToDouble(LP4.Text.Replace("%","")) +
                Convert.ToDouble(LP5.Text.Replace("%","")) +
                Convert.ToDouble(LP6.Text.Replace("%","")) +
                Convert.ToDouble(LP7.Text.Replace("%","")) +
                Convert.ToDouble(LP8.Text.Replace("%","")) +
                Convert.ToDouble(LP9.Text.Replace("%","")))/10
                );*/
        }
        public string[] buildRecord(int i, string client)
        {
            string column = "";
            if (i == 1) { column = "q1"; }
            else if (i == 2) { column = "q2"; }
            else if (i == 3) { column = "q3"; }
            else if (i == 4) { column = "q4"; }
            else if (i == 5) { column = "q5"; }
            else if (i == 6) { column = "q6"; }
            else if (i == 7) { column = "q7"; }
            else if (i == 8) { column = "q8"; }
            else if (i == 9) { column = "q9"; }
            else if (i == 10) { column = "q10"; }
            else if (i == 11) { column = "q11"; }
            else if (i == 12) { column = "q12"; }
            else if (i == 13) { column = "q13"; }
            else if (i == 14) { column = "q14"; }
            else if (i == 15) { column = "q15"; }
            else if (i == 16) { column = "q16"; }
            else if (i == 17) { column = "q17"; }

            string[] output = new string[3];
            int j = -1;
            string query =
            @"select count(*) from dr_ctp d inner join atms a on d.atmid=a.atmid where " + column + @" like 'E|%' and client = '" + client + "' union all" +
            @"select count(*) from dr_ctp d inner join atms a on d.atmid=a.atmid where " + column + @" not like 'E|%' and client = '" + client + "' union all" +
            @"select count(*) from dr_ctp d inner join atms a on d.atmid=a.atmid and client = '" + client + "'";

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLconnstr"].ConnectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                j++;
                if (j == 0)
                {
                    output[0] = reader[0].ToString();
                }
                else if (j == 1)
                {
                    output[1] = reader[0].ToString();
                }
                else if (j == 2)
                {
                    output[2] = reader[0].ToString();
                }
            }
            reader.Close();
            cn.Close();
            cn.Dispose();
            return output;
        }
        public SqlDataSource bindGrid(string query, string dsID)
        {
            SqlDataSource sql = new SqlDataSource(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString, query);
            sql.ID = dsID;
            sql.DataBind();
            sql.Dispose();
            return sql;
        }
        public void bindGrid(GridView g, string DataSourceId)
        {
            g.DataSourceID = DataSourceId;
            g.AllowPaging = true;
            g.AllowSorting = true;
            g.DataBind();
        }
    }
}