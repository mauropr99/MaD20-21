using System;
using System.Data.Entity.Validation;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class SignUp : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;

            if (!IsPostBack)
            {
                /* Get current language and country from browser */
                string defaultLanguage =
                    GetLanguageFromBrowserPreferences();
                string defaultCountry =
                    GetCountryFromBrowserPreferences();

                /* Combo box initialization */
                UpdateComboLanguage(defaultLanguage);
            }
        }

        private string GetLanguageFromBrowserPreferences()
        {
            string language;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
            language = cultureInfo.TwoLetterISOLanguageName;
            LogManager.RecordMessage("Preferred language of user" +
                                     " (based on browser preferences): " + language);
            return language;
        }

        private string GetCountryFromBrowserPreferences()
        {
            string country;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            if (cultureInfo.IsNeutralCulture)
            {
                country = "";
            }
            else
            {
                // cultureInfoName is something like en-US
                string cultureInfoName = cultureInfo.Name;
                // Gets the last two caracters of cultureInfoname
                country = cultureInfoName.Substring(cultureInfoName.Length - 2);

                LogManager.RecordMessage("Preferred region/country of user " +
                                         "(based on browser preferences): " + country);
            }

            return country;
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
        /// Handles the Click event of the btnSignUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance
        /// containing the event data.</param>
        protected void BtnSignUpClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserDetails userDetailsVO =
                        new UserDetails(txtFirstName.Text, txtSurname.Text,
                            txtEmail.Text, comboLanguage.SelectedValue.Split(' ')[0],
                            comboLanguage.SelectedValue.Split(' ')[1]);

                    SessionManager.SignUpUser(Context, txtLogin.Text,
                        txtPassword.Text, userDetailsVO);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblLoginError.Visible = true;
                }
                catch (DbEntityValidationException)
                {
                    throw;
                }
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