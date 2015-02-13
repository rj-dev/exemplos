using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MailMessage msg = new MailMessage("agendamento@megaimagem.com.br", "renato@bark.com.br");
        msg.Subject = "teste";
        msg.Body = "teste";
        msg.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smurf-papa.assistecinformatica.eti.br");
        smtpClient.Credentials = new System.Net.NetworkCredential("agendamento@megaimagem.com.br", "callconect");
        
        try
        {
            smtpClient.Send(msg);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}