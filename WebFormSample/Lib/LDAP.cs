using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Configuration;

namespace WebFormSample.Lib
{
    public class LDAP
    {
        public string Path
        {
            get;
            set;
        }

        public string FilterAttribute
        {
            get;
            set;
        }

        public string Domain
        {
            get;
            set;
        }

        public bool ByPassADAuth
        {
            get;
            set;
        }

        public string ByPassADAuthPwd
        {

            get;
            set;
        }

        public LDAP()
        {
            //Path = ConfigurationManager.AppSettings["LDAPPath"];
            //Domain = ConfigurationManager.AppSettings["DomainDefault"];
            //ByPassADAuth = bool.Parse(ConfigurationManager.AppSettings["ByPassADAuth"]);
            //ByPassADAuthPwd = ConfigurationManager.AppSettings["ByPassADAuthPwd"];

            Path = "LDAP://DC=asia,DC=AD,DC=flextronics,DC=com";
            Domain = "asia.ad.flextronics.com";
            ByPassADAuth = true;
            ByPassADAuthPwd = "df";
              
        }


        public bool IsAuthenticated(string username, string pwd)
        {
            bool Result = false;
            DirectoryEntry entry = new DirectoryEntry(Path, username, pwd);
            try
            {
                // Bind to the native AdsObject to force authentication. 
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    Path = String.Empty;
                    FilterAttribute = String.Empty;
                    Result = false;
                }
                else
                {
                    // Update the new path to the user in the directory
                    Path = result.Path;
                    FilterAttribute = (String)result.Properties["cn"][0];
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                return Result;
                //throw new Exception("Error authenticating user. " + ex.Message);
            }
            return Result;
        }

        public bool IsAuthenticated(string username, string pwd, out string msg)
        {
            string sDomain = "", sName = "";
            bool isAuthenticated = false;
            msg = "";

            if (ByPassADAuth)
            {
                if (pwd == ByPassADAuthPwd)
                {
                    isAuthenticated = true;
                }
                else
                {
                    msg = "用户名或密码错误。";
                }

                return isAuthenticated;
            }

            if (username.IndexOf("\\") > 0)
            {
                sDomain = username.Substring(0, username.IndexOf("\\"));
                sName = username.Substring(username.IndexOf("\\") + 1, username.Length - username.IndexOf("\\") - 1);
            }
            else
            {
                sName = username;
            }

            try
            {
                if (sDomain == "")
                {
                    isAuthenticated = this.IsAuthenticated(Domain, sName, pwd);
                }
                else
                {
                    isAuthenticated = this.IsAuthenticated(sDomain, sName, pwd);
                }
            }
            catch (Exception ex)
            {
                isAuthenticated = false;
                msg = ex.Message;
            }

            return isAuthenticated;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            //string domainAndUsername = domain + @"\" + username;
            string domainAndUsername = username;
            DirectoryEntry entry = new DirectoryEntry(Path, domainAndUsername, pwd);
            try
            {
                // Bind to the native AdsObject to force authentication. 
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    Path = String.Empty;
                    FilterAttribute = String.Empty;
                }
                else
                {
                    // Update the new path to the user in the directory
                    Path = result.Path;
                    FilterAttribute = (String)result.Properties["cn"][0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }


        /// <summary>
        /// Authenticate the specfied user is a domain user (Use default domain which configurated in Web.config)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool IsDomainUser(string username, string pwd, out string errorMsg)
        {
            errorMsg = String.Empty;
            try
            {
                return new LDAP().IsAuthenticated(username, pwd, out errorMsg);
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }
            return false;
        }
    }
}