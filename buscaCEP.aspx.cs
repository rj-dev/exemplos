using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class buscaCEP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [System.Web.Services.WebMethod]
    public static string WsBuscaCep(string cep)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://webservice.kinghost.net/web_cep.php?auth=3374bf016d2a052bef06e546bec592a0&formato=json&cep=" + cep);
        request.Method = "GET";
        var location = String.Empty;
        using (var response = (HttpWebResponse)request.GetResponse())
        using (var stream = response.GetResponseStream())
        using (var reader = new StreamReader(stream))
        {

            //O JavaScriptSerializer vai fazer o web service retornar JSON
            JavaScriptSerializer js = new JavaScriptSerializer();

            return reader.ReadToEnd();
        }

    }

}