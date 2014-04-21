using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using Georakel.Core.Domain;

namespace Georakel.Core
{
	public class OracleService
	{
		public OracleService ()
		{
		}

		public void Ask(BusStop a, BusStop b, Action<Result<String>> callback)
		{
			string resource = 
				"https://www.atb.no/xmlhttprequest.php?" +
				"service=routeplannerOracle.getOracleAnswer&question="+
				"N%C3%A5r+g%C3%A5r+neste+buss+fra+" + a.Name + "+til+" + b.Name;

			var webRequest = (HttpWebRequest) WebRequest.Create(resource);

			webRequest.BeginGetResponse(responseResult => {
				try
				{
					var response = webRequest.EndGetResponse(responseResult);
					var encoding = Encoding.GetEncoding("utf-8");            
					using (var sr = new StreamReader(response.GetResponseStream(), encoding))
					{
						var result = sr.ReadToEnd();
						try
						{
							callback(new Result<String>(result));
						}
						catch (Exception ignore) 
						{}
					}
				}
				catch (Exception ex)
				{
					try {
					callback(new Result<String>(ex));
					}
					catch (Exception ignore)
					{}
				}

			}, webRequest);
		}

	}
}

