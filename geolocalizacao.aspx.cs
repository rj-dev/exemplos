using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subgurim.Controles;
using System.Text;
public partial class geolocalizacao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        getInfoUser();
    }

    private void getInfoUser()
    {
        string db = Server.MapPath("~/App_Data/GeoLiteCity.dat");
        LookupService servico = new LookupService(db);
        string User_IP = "200.221.2.45";
        Location localizacao = servico.getLocation(User_IP);

        if (localizacao != null)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("Cidade: " + localizacao.city + "<br />");
            msg.Append("País: " + localizacao.countryName + "<br />");
            msg.Append("Código do País: " + localizacao.countryCode + "<br />");
            msg.Append("Região: " + localizacao.region + "<br />");
            msg.Append("Código da Área: " + localizacao.area_code + "<br />");
            msg.Append("Latitude: " + localizacao.latitude + "<br />");
            msg.Append("Longitude: " + localizacao.longitude + "<br />");

            GMap1.addControl(new GControl(GControl.preBuilt.GOverviewMapControl));
            GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl));

            GMarker marker = new GMarker(new GLatLng(localizacao.latitude, localizacao.longitude));
            GInfoWindow window = new GInfoWindow(marker, "<center><b>Sua localização:<br>" + msg.ToString() + "</b></center>", true);
            GMap1.addInfoWindow(window);
        }

    }
}