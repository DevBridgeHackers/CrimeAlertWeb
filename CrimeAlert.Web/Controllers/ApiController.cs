using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrimeAlert.DataEntities.Entities;
using CrimeAlert.DataEntities.Enums;
using CrimeAlert.ServiceContracts;
using Newtonsoft.Json.Linq;

namespace CrimeAlert.Web.Controllers
{
    public partial class ApiController : Controller
    {
        private readonly IUploadService uploadService;
        private readonly IUserService userService;
        private readonly IReportService reportService;

        public ApiController(IUploadService uploadService, IUserService userService, IReportService reportService)
        {
            this.uploadService = uploadService;
            this.userService = userService;
            this.reportService = reportService;
        }

        [HttpPost]
        public virtual int UploadImage(HttpPostedFileBase fileData, string securityToken)
        {
            var fileName = Guid.NewGuid() + ".jpg";
            var fullSavePath = Path.GetTempPath() + fileName;

            if (fileData.ContentLength > 0 && securityToken != null)
            {
                try
                {
                    fileData.SaveAs(fullSavePath);
                }
                catch (Exception)
                {
                    return 0;
                }

                if (uploadService.UploadFile(fullSavePath))
                {
                    var user = userService.GetUser(securityToken);
                    if (user != null)
                    {
                        var report = reportService.AddReportPhoto(user, fileName, FileType.Photo);
                        if (report != null)
                        {
                            return report.Id;
                        }
                        return 0;
                    }

                    var facebookUser = RetrieveFacebookUser(securityToken);
                    if (facebookUser != null)
                    {
                        user = userService.AddUser(securityToken, facebookUser.FirstName, facebookUser.LastName, facebookUser.Email);
                        if (user != null)
                        {
                            var report = reportService.AddReportPhoto(user, fileName, FileType.Photo);
                            if (report != null)
                            {
                                return report.Id;
                            }
                            return 0;
                        }
                    }
                    return 0;
                }
                return 0;
            }
            return 0;
        }

        [HttpPost]
        public virtual JsonResult UpdateReport(int reportId, string securityToken, bool isPublic, string comment, string locationLongtitude, string locationLatitude)
        {
            reportService.UpdateReportInfo(reportId, securityToken, isPublic, comment, locationLatitude, locationLongtitude);
            return Json(new { Status = "done", Message = "Updated!", ReportId = reportId, AUthToken = securityToken, IsPublic = isPublic, Comment =  comment, Long = locationLongtitude, Lat = locationLatitude });
        }

        [HttpPost]
        public virtual JsonResult DeleteReport(int reportId, string securityToken)
        {
            reportService.DeleteReport(reportId, securityToken);
            return Json(new { Status = "success", Message = "Updated!" });
        }

        [HttpPost]
        public virtual int UploadVideo(HttpPostedFileBase fileData, string securityToken)
        {
            var fileName = Guid.NewGuid() + ".mov";
            var fullSavePath = Path.GetTempPath() + fileName;

            if (fileData.ContentLength > 0 && securityToken != null)
            {
                try
                {
                    fileData.SaveAs(fullSavePath);
                }
                catch (Exception)
                {
                    return 0;
                }

                if (uploadService.UploadFile(fullSavePath))
                {
                    var user = userService.GetUser(securityToken);
                    if (user != null)
                    {
                        var report = reportService.AddReportPhoto(user, fileName, FileType.Video);
                        if (report != null)
                        {
                            return report.Id;
                        }
                        return 0;
                    }

                    var facebookUser = RetrieveFacebookUser(securityToken);
                    if (facebookUser != null)
                    {
                        user = userService.AddUser(securityToken, facebookUser.FirstName, facebookUser.LastName, facebookUser.Email);
                        if (user != null)
                        {
                            var report = reportService.AddReportPhoto(user, fileName, FileType.Photo);
                            if (report != null)
                            {
                                return report.Id;
                            }
                            return 0;
                        }
                    }
                    return 0;
                }
                return 0;
            }
            return 0;
        }

        private FacebookUser RetrieveFacebookUser(string authToken)
        {
            string hitUrl = string.Format("https://graph.facebook.com/me?access_token={0}", authToken);
            oAuthFacebook objFbCall = new oAuthFacebook();
            string JSONInfo = objFbCall.WebRequest(oAuthFacebook.Method.GET, hitUrl, "");

            JObject Job = JObject.Parse(JSONInfo);
            JToken Jdata = Job.Root;


            FacebookUser fbu = null;

            if (Jdata.HasValues)
            {
                string UID = (string)Jdata.SelectToken("id");
                string firstname = (string)Jdata.SelectToken("first_name");
                string lastname = (string)Jdata.SelectToken("last_name");
                fbu = new FacebookUser();
                fbu.Id = UID;
                fbu.FirstName = firstname;
                fbu.LastName = lastname;
            }

            return fbu;

        } 
    }

    public class oAuthFacebook
    {
        public enum Method { GET, POST };
        public const string AUTHORIZE = "https://graph.facebook.com/oauth/authorize";
        public const string ACCESS_TOKEN = "https://graph.facebook.com/oauth/access_token";
        public const string CALLBACK_URL = "http://www.blahblah.com/facebookcallback.aspx";

        private string _consumerKey = "";
        private string _consumerSecret = "";
        private string _token = "";

        #region Properties

        public string ConsumerKey
        {
            get
            {
                if (_consumerKey.Length == 0)
                {
                    _consumerKey = "164542847016744"; //Your application ID
                }
                return _consumerKey;
            }
            set { _consumerKey = value; }
        }

        public string ConsumerSecret
        {
            get
            {
                if (_consumerSecret.Length == 0)
                {
                    _consumerSecret = "1b6d2887ebce0e673df70283745ad497"; //Your application secret
                }
                return _consumerSecret;
            }
            set { _consumerSecret = value; }
        }

        public string Token { get { return _token; } set { _token = value; } }

        #endregion

        /// <summary>
        /// Get the link to Facebook's authorization page for this application.
        /// </summary>
        /// <returns>The url with a valid request token, or a null string.</returns>
        public string AuthorizationLinkGet()
        {
            return string.Format("{0}?client_id={1}&redirect_uri={2}", AUTHORIZE, this.ConsumerKey, CALLBACK_URL);
        }

        /// <summary>
        /// Exchange the Facebook "code" for an access token.
        /// </summary>
        /// <param name="authToken">The oauth_token or "code" is supplied by Facebook's authorization page following the callback.</param>
        public void AccessTokenGet(string authToken)
        {
            this.Token = authToken;
            string accessTokenUrl = string.Format("{0}?client_id={1}&redirect_uri={2}&client_secret={3}&code={4}",
            ACCESS_TOKEN, this.ConsumerKey, CALLBACK_URL, this.ConsumerSecret, authToken);

            string response = WebRequest(Method.GET, accessTokenUrl, String.Empty);

            if (response.Length > 0)
            {
                //Store the returned access_token
                NameValueCollection qs = HttpUtility.ParseQueryString(response);

                if (qs["access_token"] != null)
                {
                    this.Token = qs["access_token"];
                }
            }
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Method method, string url, string postData)
        {

            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.UserAgent = "[You user agent]";
            webRequest.Timeout = 20000;

            if (method == Method.POST)
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";

                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());

                try
                {
                    requestWriter.Write(postData);
                }
                catch
                {
                    throw;
                }

                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }
            }

            responseData = WebResponseGet(webRequest);
            webRequest = null;
            return responseData;
        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }
    }

}