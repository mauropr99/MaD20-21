using System;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{

    public partial class UpdateUser : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                UserDetails userDetails =
                    SessionManager.FindUserDetails(Context);

                txtFirstName.Text = userDetails.Name;
                txtSurname.Text = userDetails.Lastname;
                txtEmail.Text = userDetails.Email;

                Locale locale = SessionManager.GetLocale(Context);
                /* Combo box initialization */
                UpdateComboLanguage(locale.Language + ' ' + locale.Country);
            }

        }

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*. 
        /// Also, the selectedLanguage will appear selected in the 
        /// ComboBox
        /// </summary>
        private void UpdateComboLanguage(string selectedLanguage)
        {
            comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            comboLanguage.DataTextField = "text";
            comboLanguage.DataValueField = "value";
            comboLanguage.DataBind();
            comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void BtnUpdateClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                UserDetails userDetails =
                    new UserDetails(txtFirstName.Text, txtSurname.Text,
                            txtEmail.Text, comboLanguage.SelectedValue.Split(' ')[0],
                            comboLanguage.SelectedValue.Split(' ')[1]);

                SessionManager.UpdateUserDetails(Context,
                    userDetails);

                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));

            }
        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            /* After a language change, the countries are printed in the
             * correct language.
             */
            UpdateComboLanguage(comboLanguage.SelectedValue);
        }
    }
}
