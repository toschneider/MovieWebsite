using System;
using System.Collections.Generic;
using System.Net;

namespace MovieWebsite.Models
{
	/// <summary>
	/// imdb api, request and query class
	/// </summary>
	public class IMDB
	{
		private List<string> querystrings = new List<string>();
		private string imdbstring = "https://sg.media-imdb.com/suggests/{0}/{1}.json";
		private string xaaxstring = "http://www.xaaax.de/xfilme.php?film={0}&imdb=1&ofdb=1&fsk=1&art=a";
		public void ADD(string querystring)
		{
			this.querystrings.Add(querystring);
		}

		public void ADD(List<string> querystrings)
		{
			this.querystrings.AddRange(querystrings);
		}

		public List<string> GetXaaxQueryAndDeleteStrings()
		{
			return GetAndDeleteStrings(xaaxstring, false);

		}
		private List<string> GetAndDeleteStrings(string formatstring, bool imdbBool)
		{
			var results = new List<string>();
			System.Diagnostics.Debug.WriteLine(querystrings.Count);
			using (WebClient wc = new WebClient())
			{
				foreach (var querystring in querystrings)
				{
					string query;
					if (imdbBool)
					{
						query = String.Format(formatstring, Char.ToLower(querystring[0]), querystring);
					} 
					else
					{
						query = String.Format(formatstring, querystring);
					}
					
					try
					{
						System.Diagnostics.Debug.WriteLine(query);
						var json = wc.DownloadString(query);
						System.Diagnostics.Debug.WriteLine(json);
						if (json != null)
						{
							results.Add(json);
						}
					}
					catch
					{

					}




				}
			}
			querystrings = new List<string>();
			return results;
		}
		/// <summary>
		/// gets results and deletes all querys
		/// </summary>
		/// <returns>results</returns>
		public List<string> GetAndDeleteIMDBQueryResults()
		{
			return GetAndDeleteStrings(imdbstring, true);
		}
	}
}