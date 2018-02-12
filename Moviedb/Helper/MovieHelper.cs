using System;
using System.Web;
using System.Web.Configuration;

namespace Moviedb.Helper
{
    public static class MovieHelper
    {
        public static String MoviePosterWebPath  {
            get
            {

                return WebConfigurationManager.AppSettings["MoviePosterWebPath"];
            }

        }

        public static String MoviePosterFileSystemPath
        {
            get
            {

                return HttpRuntime.AppDomainAppPath + WebConfigurationManager.AppSettings["MoviePosterFileSystemPath"];
            }

        }
    };

    
}