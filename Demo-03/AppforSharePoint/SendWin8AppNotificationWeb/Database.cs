using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SendWin8AppNotificationWeb
{
    public class Database
    {
        private Database()
        {}

        private static Database _instance = null;
        private static object _lockO = new object();
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockO)
                    {
                        if (_instance == null)
                        {
                            _instance = new Database();
                            _instance.CreateSchema();
                        }
                    }
                }
                return _instance;
            }
        }

        private void CreateSchema()
        {
            string cmdText = @"
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ApplicationState' AND TABLE_SCHEMA = 'dbo')
BEGIN

CREATE TABLE [dbo].[ApplicationState](
	[TenantId] [varchar](256) NOT NULL,
	[Value] [varchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
END
";
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["PersistenceConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand(cmdText, c))
                {
                    c.Open();
                    com.ExecuteNonQuery();
                }
            }
        }

        public void SaveChannelUrl(string channelUrl)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["PersistenceConnection"].ConnectionString))
            {
                string cmdText = string.Format(@"
IF NOT EXISTS (SELECT 1 FROM dbo.ApplicationState WHERE TenantId = '{0}')
BEGIN 
	INSERT INTO dbo.ApplicationState(TenantId, Value)
	SELECT '{0}', '{1}'
END ELSE 
BEGIN 
	UPDATE dbo.ApplicationState
	SET Value = '{1}'
	WHERE TenantId = '{0}'
END
", FullyQualifiedApplicationPath, channelUrl);
                using (SqlCommand com = new SqlCommand(cmdText, c))
                {
                    c.Open();
                    com.ExecuteNonQuery();
                }
            }
        }

        public string GetChannelUrl(string tenantId)
        {
            string ret = string.Empty;
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["PersistenceConnection"].ConnectionString))
            {
                string cmdText = string.Format(@"
SELECT Value FROM dbo.ApplicationState WHERE TenantId = '{0}'
", tenantId);
                using (SqlCommand com = new SqlCommand(cmdText, c))
                {
                    c.Open();
                    ret  = (string)com.ExecuteScalar();
                }
            }
            return ret;
        }

        private string FullyQualifiedApplicationPath
        {
            get
            {
                //Return variable declaration
                var appPath = string.Empty;

                //Getting the current context of HTTP request
                var context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}",
                                            context.Request.Url.Scheme,
                                            context.Request.Url.Host,
                                            //context.Request.Url.Port == 80
                                            //    ? string.Empty
                                             //   : ":" + context.Request.Url.Port,
                                            context.Request.ApplicationPath);
                }

                //if (!appPath.EndsWith("/"))
                //    appPath += "/";

                if (appPath.EndsWith("/"))
                        appPath = appPath.Substring(0, appPath.Length -1);

                return appPath;
            }
        }
    }
}