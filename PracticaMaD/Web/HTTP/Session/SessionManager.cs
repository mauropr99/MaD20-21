﻿using System;
using System.Web;
using System.Web.Security;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Util;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    ///<summary>
    ///
    /// A facade utility class to transparently manage session objects and
    /// cookies. The following objects are mantained in the session:
    /// <list>
    ///  <item>* The user's data, stored in the UserSession object under the key
    ///    <c>USER_SESSION_ATTRIBUTE</c>. This attribute is only present
    ///    for authenticated users, and is only of interest to the view. The
    ///    UserSession object stores the firstName and the UserId</item>
    ///
    ///  <item>* The user's language and the user's country, stored in the
    ///     Locale object under the key <c>LOCALE_SESSION_ATTRIBUTE</c>. This
    ///     attribute is only present for authenticated users, and is only of
    ///     interest to the view.</item>
    ///</list>
    ///
    /// For users that select "remember my password" in the login wizard, the
    /// following cookies are used (managed in the CookiesManager object):
    /// <list>
    ///   <item>* <c>LOGIN_NAME_COOKIE</c>: to store the login name.</item>
    ///   <item>* <c>ENCRYPTED_PASSWORD_COOKIE</c>: to store the
    ///              encrypted password.</item>
    ///   <item>* <c>.ASPXAUTH</c>: Authentication Cookie.</item>
    /// </list>
    /// In order to make transparent the management of session objects and cookies
    /// to the implementation of controller actions, this class provides a number
    /// of methods equivalent to some of the ones provided by
    /// <c>UserService</c>, which manage session objects and cookies,
    /// and call upon the corresponding <c>UserService's</c> method.
    /// These methods are as follows:
    /// <list>
    ///   <item>* <c>Login</c>.</item>
    ///   <item>* <c>SignUpUser</c>.</item>
    ///   <item>* <c>FindUserDetails</c>.</item>
    ///   <item>* <c>UpdateUserDetails</c>.</item>
    ///   <item>* <c>ChangePassword</c>.</item>
    /// </list>
    ///
    /// It is important to remember that when needing to do some of the above
    /// actions from the controller, the corresponding method of this class
    /// (one of the previous list) must be called, and <b>not</b> the one in
    /// <c>UserService</c>. The rest of methods of <c>UserService</c> must be
    /// called directly.
    ///
    /// For example, in a personalizable portal,<c>UserService</c> could include:
    ///
    ///   <c>findServicePreferences</c>, <c>updateServicePreferences</c>,
    ///   <c>findLayout</c>, <c>updateLayout</c>, etc.
    ///
    /// In a electronic commerce shop <c>UserFacadeDelegate</c> could include:
    ///
    ///   <c>getShoppingCart</c>, <c>addToShoppingCart</c>,
    ///   <c>removeFromShoppingCart</c>, <c>makeOrder</c>,
    ///   <c>findPendingOrders</c>, etc.
    ///
    /// When needing to invoke directly a method of <c>UserSession</c>, the
    /// property <c>SessionManager.UserService</c> must be invoked in order
    /// to get the personal delegate (each user has his/her own delegate).
    ///
    /// Same as <c>UserService</c>, there exist some logical
    /// restrictions with regard to the order of method calling. In particular,
    /// <c>updateUserDetails</c> and <c>changePassword</c>
    /// can not be called if <c>login</c> or <c>SignUpUser</c>
    /// have not been previously called. After the user calls one of these two
    ///  methods, the user is said to be authenticated.
    /// </summary>
    public class SessionManager
    {
        public static readonly string LOCALE_SESSION_ATTRIBUTE = "locale";

        public static readonly string USER_SESSION_ATTRIBUTE =
               "userSession";

        private static IUserService userService;
        private static IShoppingService shoppingService;

        public IUserService UserService
        {
            set { userService = value; }
        }

        static SessionManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            userService = iocManager.Resolve<IUserService>();

        }

        public static IShoppingService GetShoppingService()
        {
            return shoppingService;
        }

        /// <summary>
        /// SignUps the user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="UserDetails">The user  details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static void SignUpUser(HttpContext context,
            string loginName, string clearPassword,
            UserDetails UserDetails)
        {
            /* SignUp user. */
            long usrId = userService.SingUpUser(loginName, clearPassword,
                UserDetails);

            /* Insert necessary objects in the session. */
            UserSession userSession = new UserSession
            {
                UserId = usrId,
                FirstName = UserDetails.Name
            };

            Locale locale = new Locale(UserDetails.LanguageName,
                UserDetails.LanguageCountry);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            FormsAuthentication.SetAuthCookie(loginName, false);
        }

        /// <summary>
        /// Login method. Authenticates an user in the current context.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public static void Login(HttpContext context, string loginName,
           string clearPassword, bool rememberMyPassword)
        {
            /* Try to login, and if successful, update session with the necessary
             * objects for an authenticated user. */
            LoginResult loginResult = DoLogin(context, loginName,
                clearPassword, false, rememberMyPassword);

            /* Add cookies if requested. */
            if (rememberMyPassword)
            {
                CookiesManager.LeaveCookies(context, loginName,
                    loginResult.EncryptedPassword);
            }
        }

        /// <summary>
        /// Tries to log in with the corresponding method of
        /// <c>UserService</c>, and if successful, inserts in the
        /// session the necessary objects for an authenticated user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="password">User Password</param>
        /// <param name="passwordIsEncrypted">Password is either encrypted or
        /// in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next
        /// logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        private static LoginResult DoLogin(HttpContext context,
             string loginName, string password, bool passwordIsEncrypted,
             bool rememberMyPassword)
        {
            LoginResult loginResult =
                userService.Login(loginName, password,
                    passwordIsEncrypted);

            /* Insert necessary objects in the session. */

            UserSession userSession = new UserSession
            {
                UserId = loginResult.Id,
                FirstName = loginResult.Name
            };

            Locale locale =
                new Locale(loginResult.Language, loginResult.Country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            return loginResult;
        }

        /// <summary>
        /// Updates the session values for an previously authenticated user
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="userSession">The user data stored in session.</param>
        /// <param name="locale">The locale info.</param>
        private static void UpdateSessionForAuthenticatedUser(
            HttpContext context, UserSession userSession, Locale locale)
        {
            /* Insert objects in session. */
            context.Session.Add(USER_SESSION_ATTRIBUTE, userSession);
            context.Session.Add(LOCALE_SESSION_ATTRIBUTE, locale);
        }

        /// <summary>
        /// Determine if a user is authenticated
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <returns>
        /// 	<c>true</c> if is user authenticated
        ///     <c>false</c> otherwise
        /// </returns>
        public static bool IsUserAuthenticated(HttpContext context)
        {
            if (context.Session == null)
                return false;

            return (context.Session[USER_SESSION_ATTRIBUTE] != null);
        }

        public static Locale GetLocale(HttpContext context)
        {
            Locale locale =
                (Locale)context.Session[LOCALE_SESSION_ATTRIBUTE];

            return locale;
        }

        /// <summary>
        /// Updates the user  details.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="UserDetails">The user  details.</param>
        public static void UpdateUserDetails(HttpContext context,
            UserDetails UserDetails)
        {
            /* Update user's  details. */

            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.UpdateUserDetails(userSession.UserId,
                UserDetails);

            /* Update user's session objects. */

            Locale locale = new Locale(UserDetails.LanguageName,
                UserDetails.LanguageCountry);

            userSession.FirstName = UserDetails.Name;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
        }

        /// <summary>
        /// Finds the user  with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserDetails FindUserDetails(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            UserDetails UserDetails =
                userService.FindUserDetails(userSession.UserId);

            return UserDetails;
        }

        /// <summary>
        /// Gets the user info stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserSession GetUserSession(HttpContext context)
        {
            if (IsUserAuthenticated(context))
                return (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            else
                return null;
        }

        /// <summary>
        /// Changes the user's password
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="oldClearPassword">The old password in clear text</param>
        /// <param name="newClearPassword">The new password in clear text</param>
        /// <exception cref="IncorrectPasswordException"/>
        public static void ChangePassword(HttpContext context,
               string oldClearPassword, string newClearPassword)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.ChangePassword(userSession.UserId,
                oldClearPassword, newClearPassword);

            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);
        }

        /// <summary>
        /// Destroys the session, and removes the cookies if the user had
        /// selected "remember my password".
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void Logout(HttpContext context)
        {
            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);

            /* Invalidate session. */
            context.Session.Abandon();

            /* Invalidate Authentication Ticket */
            FormsAuthentication.SignOut();
        }

        /// <sumary>
        /// Guarantees that the session will have the necessary objects if the
        /// user has been authenticated or had selected "remember my password"
        /// in the past.
        /// </sumary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void TouchSession(HttpContext context)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            /* Check if "UserSession" object is in the session. */
            UserSession userSession = null;

            if (context.Session != null)
            {
                userSession =
                    (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

                // If userSession object is in the session, nothing should be doing.
                if (userSession != null)
                {
                    if (shoppingService == null)
                        shoppingService = iocManager.Resolve<IShoppingService>();
                    return;
                }
            }
            shoppingService = iocManager.Resolve<IShoppingService>();
            /*
             * The user had not been authenticated or his/her session has
             * expired. We need to check if the user has selected "remember my
             * password" in the last login (login name and password will come
             * as cookies). If so, we reconstruct user's session objects.
             */
            UpdateSessionFromCookies(context);



        }

        /// <summary>
        /// Tries to login (inserting necessary objects in the session) by using
        /// cookies (if present).
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        private static void UpdateSessionFromCookies(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request.Cookies == null)
            {
                return;
            }

            /*
             * Check if the login name and the encrypted password come as
             * cookies.
             */
            string loginName = CookiesManager.GetLoginName(context);
            string encryptedPassword = CookiesManager.GetEncryptedPassword(context);

            if ((loginName == null) || (encryptedPassword == null))
            {
                return;
            }

            /* If loginName and encryptedPassword have valid values (the user selected "remember
             * my password" option) try to login, and if successful, update session with the
             * necessary objects for an authenticated user.
             */
            try
            {
                DoLogin(context, loginName, encryptedPassword, true, true);

                /* Authentication Ticket. */
                FormsAuthentication.SetAuthCookie(loginName, true);
            }
            catch (Exception)
            { // Incorrect loginName or encryptedPassword
                return;
            }
        }
    }
}