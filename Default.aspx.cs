using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnProcurar_Click(object sender, EventArgs e)
    {

        OleDbConnection conexao = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Desktop\tabela-base.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES';");
        OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [Plan1$]", conexao);
        DataSet ds = new DataSet();

        try
        {
            conexao.Open();
            adapter.Fill(ds);

            string LL = "";
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //if (ds.Tables[0].Rows[i][10].ToString() != "")
            //{
            //SqlDataSource1.InsertCommand = "Insert INTO enderecos (nomeFantasia,endereco,numero,complemento,bairro,cep,cidade,uf,ddd,telefone) " +
            //                               "Values('" + ds.Tables[0].Rows[i][0] + "','" + ds.Tables[0].Rows[i][1] + "','" + ds.Tables[0].Rows[i][2] + "'," +
            //                               "'" + ds.Tables[0].Rows[i][3] + "','" + ds.Tables[0].Rows[i][4] + "','" + ds.Tables[0].Rows[i][10] + "','" + ds.Tables[0].Rows[i][6] + "'," +
            //                               "'" + ds.Tables[0].Rows[i][7] + "','" + ds.Tables[0].Rows[i][8] + "','" + ds.Tables[0].Rows[i][9] + "')";
            //SqlDataSource1.Insert();
            //string cep = ds.Tables[0].Rows[i][10].ToString();
            //string latitudedb = ds.Tables[0].Rows[i][11].ToString();
            //string longitudedb = ds.Tables[0].Rows[i][12].ToString();

            double latitude = 0, longitude = 0;

            string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=";
            url += HttpUtility.UrlEncode(txbLogradouro.Text + " " + txbNumero.Text + ", " + txbCidade.Text + " - " + txbUF.Text + ", Brasil - " + txtCep.Text);
            //url += txtCep.Text + "&components=postal_code:" + txtCep.Text ;
            url += "&sensor=false";

            //var locationService = new GoogleLocationService();
            //var point = locationService.GetLatLongFromAddress("Eugenio Messiano, Praia Grande - SP");

            //var latitude = point.Latitude;
            //var longitude = point.Longitude;

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.Timeout = 50000;//5 segundos
            WebResponse resp = wr.GetResponse();
            Stream stream = resp.GetResponseStream();

            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();//coloca todo o HTML na variável content
                if (content != null && content != "")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(content);
                    XmlNode xmlLatitude = xmlDoc.GetElementsByTagName("lat").Item(0);
                    XmlNode xmlLongitude = xmlDoc.GetElementsByTagName("lng").Item(0);

                    if (!double.TryParse(xmlLatitude.InnerText, out latitude))
                        latitude = 0;//se não for um número coloca a latitude 0
                    if (!double.TryParse(xmlLongitude.InnerText, out longitude))
                        longitude = 0;//se não for um número coloca a longitude 0


                    //SqlDataSource1.UpdateCommand = "UPDATE enderecos SET latitude=" + latitude + ", longitude=" + longitude + " WHERE cep=" + cep;
                    //SqlDataSource1.Update();

                    //LL += latitude + "&nbsp;&nbsp;&nbsp;&nbsp;" + longitude + "</br>";
                    //string[] coordenadas = content.Split(',');//separa o html em um array
                    //if (coordenadas.Length >= 4)//verifica se existem 4 elementos no array
                    //{
                    //    if (!double.TryParse(coordenadas[2].Replace(".", ","), out latitude))
                    //        latitude = 0;//se não for um número coloca a latitude 0
                    //    if (!double.TryParse(coordenadas[3].Replace(".", ","), out longitude))
                    //        longitude = 0;//se não for um número coloca a longitude 0
                    //}

                    //buscando amigo mais próximo
                    //SqlConnection sqlCom = new SqlConnection("data source=localhost\\sqlexpress; initial catalog=TesteGeo; Integrated Security=SSPI;");
                    SqlConnection sqlCom = new SqlConnection("data source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security=True;Connect Timeout=30;");
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" select top 5 endereco,numero,bairro,cidade,telefone ");//pega os 5 mais próximos
                    sql.Append(" from( ");
                    sql.Append(" select endereco,numero,bairro,cidade,telefone, ");
                    sql.Append(" SQRT(power(" + latitude.ToString().Replace(",", ".") + "-LATITUDE,2)+POWER(" + longitude.ToString().Replace(",", ".") + "-LONGITUDE,2)) as distancia ");//formula da distancia
                    sql.Append(" from enderecos ");
                    sql.Append(" where LATITUDE between (" + latitude.ToString().Replace(",", ".") + ") and (" + latitude.ToString().Replace(",", ".") + "+0.01) ");//pega os próximos de um perímetro de 3 km
                    sql.Append(" and LONGITUDE between (" + longitude.ToString().Replace(",", ".") + "-0.01) and (" + longitude.ToString().Replace(",", ".") + "+0.01) ");//pega os próximos de um perímetro de 3 km
                    sql.Append(" )endereco ");
                    sql.Append(" order by distancia ");//ordena do mais próximo ao mais distante
                    SqlDataAdapter SqlDA = new SqlDataAdapter(sql.ToString(), sqlCom);
                    DataSet ds2 = new DataSet();
                    SqlDA.Fill(ds2);
                    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    {
                        string resultado = "";
                        for (int k = 0; k < ds2.Tables.Count; k++)
                        {
                            resultado += ds2.Tables[0].Rows[k]["endereco"].ToString().TrimEnd() + ", " + ds2.Tables[0].Rows[k]["numero"].ToString().TrimEnd() + ", " + ds2.Tables[0].Rows[k]["bairro"].ToString().TrimEnd() + " - " + ds2.Tables[0].Rows[k]["cidade"].ToString().TrimEnd() + "\\n" + "Tel.: " + ds2.Tables[0].Rows[k]["telefone"].ToString().TrimEnd();
                        }

                        //ClientScript.RegisterStartupScript(typeof(void), "mostraamigo", "alert('" + ds2.Tables[0].Rows[0]["nome"].ToString() + " é o amigo mais próximo.');", true);
                        ClientScript.RegisterStartupScript(typeof(void), "mostraamigo", "alert('" + resultado + " loja mais proxima.');", true);
                    }
                    //else
                    //ClientScript.RegisterStartupScript(typeof(void), "mostraamigo", "alert('Não foi encontrado nenhum amigo próximo');", true);
                }

                //JavaScriptSerializer js = new JavaScriptSerializer();
                //var jsonObject = js.Serialize(content);

                //Response.Write(jsonObject);
                //var objText = reader.ReadToEnd();
                //MyObject myojb = (MyObject)js.Deserialize(objText, typeof(MyObject));
            }
            //}
            //}
            Response.Write(LL);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
        }
        finally
        {
            conexao.Close();

        }

    }
}


