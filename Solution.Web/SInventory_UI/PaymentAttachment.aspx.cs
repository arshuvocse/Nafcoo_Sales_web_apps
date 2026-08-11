using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Image = System.Web.UI.WebControls.Image;

public partial class SInventory_UI_PaymentAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
               hfCustPayDetailId.Value = Request.QueryString["MID"].ToString();

            }
            else
            {
                //string connectionString = @"Data Source=23.106.122.61\SQLSERVER2014;Initial Catalog=SalesDisDB_UniWorld_DB;Integrated Security=false; User Id=sa; password=sa1234";
                //SqlConnection cnn = new SqlConnection(connectionString);

                //MemoryStream stream = new MemoryStream();
                //cnn.Open();
                //SqlCommand command = new SqlCommand("Select PayImg from tblCustPayDetail Where  CustPayDetailId=1019", cnn);
                //byte[] image = (byte[])command.ExecuteScalar();

                //string base64String = Convert.ToBase64String(image, 0, image.Length);
                //Image1.ImageUrl = "data:image/png;base64," + base64String;

            }

        }

    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            if (hfCustPayDetailId.Value != "")
            {
                int length = FUDocument.PostedFile.ContentLength;
                byte[] imagebyt = new byte[length];
                HttpPostedFile img = FUDocument.PostedFile;
                img.InputStream.Read(imagebyt, 0, length);

                using (SqlConnection sqlConnection =
                    new SqlConnection(
                        @"Data Source=NASA-PC\MSSQLSERVER2019;Initial Catalog=SalesDisDB_UniWorld_DB;Integrated Security=false; User Id=sa; password=sa1234"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"UPDATE tblCustPayDetail SET PayImg=@Image WHERE CustPayDetailId = @CustPayDetailId";
                        cmd.Parameters.Add("@Image", SqlDbType.Image).Value = imagebyt;
                        cmd.Parameters.Add("@CustPayDetailId", SqlDbType.NVarChar).Value = hfCustPayDetailId.Value;
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            AlertMessageBoxShow("Error....");
                        }
                    }

                    AlertMessageBoxShow("Done Upload Successfully...");
                }
            }

        }

    }



    private void button1_Click(object sender, EventArgs e)
    {
       string  connectionString = "Data Source=servername; Initial Catalog=databasename; User ID=sa; Password=password";
       SqlConnection  cnn = new SqlConnection(connectionString);

        MemoryStream stream = new MemoryStream();
        cnn.Open();
        SqlCommand command = new SqlCommand("Select PayImg from tblCustPayDetail Where  CustPayDetailId=1022", cnn);
        byte[] image = (byte[])command.ExecuteScalar();


        string base64String = Convert.ToBase64String(image, 0, image.Length);
        Image1.ImageUrl = "data:image/png;base64," + base64String;



       // stream.Write(image, 0, image.Length);
        cnn.Close();
        //Bitmap bitmap = new Bitmap(stream);
           
    }









    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(@"Data Source=DataSourceName;Initial Catalog=CalalogName;User ID=sa ; password=*******"))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void FetchImage(object sender, EventArgs e)
    {
      // // string id = ddlImages.SelectedItem.Value;
      ////  Image1.Visible = id != "0";
      //  if (id != "0")
      //  {
      //      byte[] bytes = (byte[])GetData("SELECT ProductoImagen FROM Producto WHERE ProductoId =" + id).Rows[0]["ProductoImagen"];
      //      string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
      //      Image1.ImageUrl = "data:image/png;base64," + base64String;
      //  }
    }
    private System.Drawing.Image BinaryToImage(byte[] b)
    {
        if (b == null)
            return null;
        using (MemoryStream ms = new MemoryStream(b, 0, b.Length))
        {
            ms.Write(b, 0, b.Length);
            System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms);
            return newImage;
        }
    }  




 
}