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

		public void ADD(string querystring)
		{
			this.querystrings.Add(querystring);
		}

		public void ADD(List<string> querystrings)
		{
			this.querystrings.AddRange(querystrings);
		}

		/// <summary>
		/// gets results and deletes all querys
		/// </summary>
		/// <returns>results</returns>
		public List<string> GetAndDeleteQueryResults()
		{
			var results = new List<string>();
			System.Diagnostics.Debug.WriteLine(querystrings.Count);
			using (WebClient wc = new WebClient())
			{
				foreach (var querystring in querystrings)
				{
					string query = String.Format(imdbstring, Char.ToLower(querystring[0]), querystring);
					try
					{
						System.Diagnostics.Debug.WriteLine(query);
						var json = wc.DownloadString(query);
						System.Diagnostics.Debug.WriteLine(json);
						if (json != null)
						{
							results.Add(json);
						}
					} catch
					{

					}
					



				}
			}
			querystrings = new List<string>();
			return results;
		}
	}
}