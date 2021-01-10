using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;



namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        public static readonly string USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            int mostUsedLabels = 5;

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (HyperLinkUpdate != null)
                    HyperLinkUpdate.Visible = false;
                if (HyperLinkLogout != null)
                    HyperLinkLogout.Visible = false;
                if (HyperLinkViewCreditCards != null)
                    HyperLinkViewCreditCards.Visible = false;
                if (HyperLinkOrderHistory != null)
                    HyperLinkOrderHistory.Visible = false;

            }
            else
            {
                if (HyperLinkAuth != null)
                    HyperLinkAuth.Visible = false;

            }

            IShoppingService shoppingService = SessionManager.GetShoppingService();
            ShoppingCartSize.Text = "(" + shoppingService.TotalProducts().ToString() + ")";

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            ICommentService commentService = iocManager.Resolve<ICommentService>();

            List<LabelDetails> labels = new List<LabelDetails>();
            labels = commentService.ViewMostUsedLabels(mostUsedLabels);

            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;

            if (labels.Count > 0)
            {
                Label1.Visible = true;
                Label1.Text = labels[0].LabelName;
                Label1Count.Text = " (" + labels[0].TimesUsed + ")";

                Label1.Font.Size = LabelSize(labels[0].TimesUsed);
                Label1Count.Font.Size = LabelSize(labels[0].TimesUsed);
            }

            if (labels.Count > 1)
            {
                Label2.Visible = true;
                Label2.Text = labels[1].LabelName;
                Label2Count.Text = " (" + labels[1].TimesUsed + ")";

                Label2.Font.Size = LabelSize(labels[1].TimesUsed);
                Label2Count.Font.Size = LabelSize(labels[1].TimesUsed);
            }

            if (labels.Count > 2)
            {
                Label3.Visible = true;
                Label3.Text = labels[2].LabelName;
                Label3Count.Text = " (" + labels[2].TimesUsed + ")";

                Label3.Font.Size = LabelSize(labels[2].TimesUsed);
                Label3Count.Font.Size = LabelSize(labels[2].TimesUsed);
            }

            if (labels.Count > 3)
            {
                Label4.Visible = true;
                Label4.Text = labels[3].LabelName;
                Label4Count.Text = " (" + labels[3].TimesUsed + ")";

                Label4.Font.Size = LabelSize(labels[3].TimesUsed);
                Label4Count.Font.Size = LabelSize(labels[3].TimesUsed);
            }

            if (labels.Count > 4)
            {
                Label5.Visible = true;
                Label5.Text = labels[4].LabelName;
                Label5Count.Text = " (" + labels[4].TimesUsed + ")";

                Label5.Font.Size = LabelSize(labels[4].TimesUsed);
                Label5Count.Font.Size = LabelSize(labels[4].TimesUsed);
            }


        }

        protected void Label_Click(object sender, EventArgs e)
        {
            LinkButton linkbutton = (LinkButton)sender;

            Response.Redirect("~/Pages/Product/ProductsByLabelView.aspx?labelName=" + linkbutton.Text);
        }

        private int LabelSize(int timesUsed)
        {
            return (int)Math.Log(timesUsed) + 10;
        }
    }
}
