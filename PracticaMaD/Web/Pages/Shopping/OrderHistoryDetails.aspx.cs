﻿using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Web.Pages.Shopping
{
    public partial class OrderHistoryDetails : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            List<OrderLineDetails> orderLineDetails;
            long orderId = long.Parse(Request.Params.Get("orderId"));


            orderLineDetails = shoppingService.ViewOrderLineDetails(orderId);

            GridOrderHistoryDetails.DataSource = orderLineDetails;

            GridOrderHistoryDetails.DataBind();


            for (int i = 0; i < GridOrderHistoryDetails.Rows.Count; i++)
            {
                GridOrderHistoryDetails.Rows[i].Cells[2].Text = orderLineDetails[i].Price.ToString("C2");
            }


            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }

    }
}