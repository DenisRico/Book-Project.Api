using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SecondProject.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecondProject.Api.Services
{
	public class DataService : IDataService
	{
		private static readonly HttpClient Client = new HttpClient();
		private readonly IConfiguration configuration;

		public DataService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public async Task<IEnumerable<BookApiModel>> GetBooksAsync(string auth)
		{
			var url = $"{configuration["Api:Main"]}/{configuration["Api:Book"]}";

			var responseBody = await GetDataFromApi(url, auth);
			var data = JsonConvert.DeserializeObject<IEnumerable<BookApiModel>>(responseBody);
			return data;
		}

		private static async Task<string> GetDataFromApi(string url, string auth)
		{
			SetAuthHeader(auth);
			var response = await Client.GetAsync(url);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return responseBody;
		}

		private static void SetAuthHeader(string auth)
		{
			if (!string.IsNullOrWhiteSpace(auth))
			{
				Client.DefaultRequestHeaders.Clear();
				Client.DefaultRequestHeaders.Add("Authorization", auth);
			}
		}
	}
}
