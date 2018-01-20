using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTracker.Web.Configuration
{
    public class ApplicationConfiguration
    {
        public string GoogleMapsAPiKey { get; set; }

        private string _rootPathPrefix;
        public string RootPathPrefix
        {
            get
            {
                return _rootPathPrefix;
            }
            set
            {
                _rootPathPrefix = value;
                if (string.IsNullOrWhiteSpace(_rootPathPrefix))
                {
                    _rootPathPrefix = "";
                }
                else
                {
                    if (!_rootPathPrefix.StartsWith("/"))
                    {
                        _rootPathPrefix = "/" + RootPathPrefix;
                    }
                    _rootPathPrefix = _rootPathPrefix.TrimEnd('/');
                }
            }
        }

        public string PrefixUrl(string url)
        {
            return string.Format("{0}/{1}", RootPathPrefix, url.TrimStart('/'));
        }

    }
}
