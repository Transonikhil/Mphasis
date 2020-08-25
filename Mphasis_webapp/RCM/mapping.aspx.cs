using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RCM
{
    public partial class mapping : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.m1.Hide();
                ddlfrom.DataBind();
                ddlfrom.Items.Insert(0, "---Select---");
                ddlfrom.Items.FindByText("---Select---").Selected = true;

                ddlto.DataBind();
                ddlto.Items.Insert(0, "---Select---");
                ddlto.Items.FindByText("---Select---").Selected = true;

                GridView1.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = false;
            }
        }

        protected void ddlfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            if (ddlfrom.SelectedValue != "---Select---")
            {
                //this.m1.Show();
                GridView1.Visible = true;
                GridView1.DataBind();
                ddlto.DataBind();
                ddlto.Items.Remove(ddlto.Items.FindByText(ddlfrom.SelectedValue.Trim()));

                if (GridView1.Rows.Count > 0)
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }
            }
            else
            {
                GridView1.Visible = false;
                Panel1.Visible = false;
            }
            Panel2.Visible = false;
        }

        protected void btnmove_Click(object sender, EventArgs e)
        {
            //this.m1.Show();
            //Label1.Text = "";
            System.Threading.Thread.Sleep(2000);
            string str = string.Empty;
            string strname = string.Empty;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkEmp");
                if (chk != null & chk.Checked)
                {
                    string b = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                                IF EXISTS (SELECT userid FROM usermap where userid='" + ddlto.SelectedValue.Trim() + "' and atmid='" + gvrow.Cells[2].Text.Trim() +
                                "') BEGIN UPDATE usermap set status='CRE',serverstatus='CRE' where userid='" + ddlto.SelectedValue.Trim() + "' and atmid='" + gvrow.Cells[2].Text.Trim() +
                                "' END ELSE BEGIN Insert into Usermap (userid,atmid,status,serverstatus) values ('" + ddlto.SelectedValue.Trim() + "','" + gvrow.Cells[2].Text.Trim() + "','CRE','CRE')  END COMMIT TRANSACTION";
                    //Response.Write(b);

                    if (obj.NonExecuteQuery(b) != 0)
                    {
                        string a = "Update UserMap set status='DEL',serverstatus='DEL' where userid='" + GridView1.DataKeys[gvrow.RowIndex].Value.ToString().Trim() + "' and atmid='" + gvrow.Cells[2].Text.Trim() + "'";
                        obj.NonExecuteQuery(a);
                        string c = "Update Users set datastatus='MOD' where userid='" + GridView1.DataKeys[gvrow.RowIndex].Value.ToString().Trim() + "' and status <> 'DEL'";
                        obj.NonExecuteQuery(c);
                    }
                    else
                    {
                        //Label1.Text = gvrow.Cells[2].Text.Trim() + " , ";
                    }
                }
            }

            Panel2.Visible = true;
            GridView2.DataBind();
            GridView1.DataBind();
            //this.m1.Hide();

            #region extra
            //str += GridView1.DataKeys[gvrow.RowIndex].Value.ToString() + ',';
            //strname += gvrow.Cells[2].Text + ',';
            //str = str.Trim(",".ToCharArray());
            //strname = strname.Trim(",".ToCharArray());
            //Response.Write("Selected UserIds: <b>" + str + "</b><br/>" + "Selected UserNames: <b>" + strname + "</b>");
            #endregion
        }

        protected void btncopy_Click(object sender, EventArgs e)
        {
            //this.m1.Show();
            System.Threading.Thread.Sleep(2000);
            string str = string.Empty;
            string strname = string.Empty;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkEmp");
                if (chk != null & chk.Checked)
                {
                    string b = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                                IF EXISTS (SELECT userid FROM usermap where userid='" + ddlto.SelectedValue.Trim() + "' and atmid='" + gvrow.Cells[2].Text.Trim() +
                                "') BEGIN UPDATE usermap set status='CRE',serverstatus='CRE' where userid='" + ddlto.SelectedValue.Trim() + "' and atmid='" + gvrow.Cells[2].Text.Trim() +
                                "' END ELSE BEGIN Insert into Usermap (userid,atmid,status,serverstatus) values ('" + ddlto.SelectedValue.Trim() + "','" + gvrow.Cells[2].Text.Trim() + "','CRE','CRE')  END COMMIT TRANSACTION";
                    if (obj.NonExecuteQuery(b) != 0)
                    {
                        string c = "Update Users set datastatus='MOD' where userid='" + GridView1.DataKeys[gvrow.RowIndex].Value.ToString().Trim() + "' and status <> 'DEL'";
                        obj.NonExecuteQuery(c);
                    }
                }
            }

            Panel2.Visible = true;
            GridView2.DataBind();
            GridView1.DataBind();
            //this.m1.Hide();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            //this.m1.Show();
            System.Threading.Thread.Sleep(2000);
            string str = string.Empty;
            string strname = string.Empty;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkEmp");
                if (chk != null & chk.Checked)
                {
                    string a = "Update UserMap set status='DEL',serverstatus='DEL' where userid='" + GridView1.DataKeys[gvrow.RowIndex].Value.ToString().Trim() + "' and atmid='" + gvrow.Cells[2].Text.Trim() + "'";
                    obj.NonExecuteQuery(a);
                    string c = "Update Users set datastatus='MOD' where userid='" + GridView1.DataKeys[gvrow.RowIndex].Value.ToString().Trim() + "' and status <> 'DEL'";
                    obj.NonExecuteQuery(c);
                }
            }

            Panel2.Visible = false;
            GridView1.DataBind();
            //this.m1.Hide();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            ////for (int i = 0; i < GridView1.Columns.Count; i++)
            ////{
            //    TableHeaderCell cell = new TableHeaderCell();
            //    TextBox txtSearch = new TextBox();
            //    txtSearch.Attributes["placeholder"] = GridView1.Columns[2].HeaderText;
            //    txtSearch.CssClass = "search_textbox";

            //    Label a = new Label();
            //    a.Text = "ENTER ATMID ";
            //    cell.ColumnSpan = 4;
            //    cell.Controls.Add(a);
            //    cell.BorderColor = Color.Black;
            //    cell.BorderStyle = BorderStyle.Solid;
            //    cell.BorderWidth = 1;
            //    cell.Controls.Add(txtSearch);
            //    row.Controls.Add(cell);
            ////}
            //GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
        }

        protected void btn_Submit0_Click(object sender, EventArgs e)
        {

        }
    }
}