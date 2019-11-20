﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using EntityCore.DTO;
using EntityCore.DTO.General;
using EntityCore.Filter;
using SmartCore.Contract;

namespace CAS.UI.Helpers
{
	public class ApiProvider
	{
		private HttpClient _httpClient;

		public ApiProvider(string serverName)
		{
			_httpClient = new HttpClient() {BaseAddress = new Uri(serverName)};
		}

		#region Seuence

		public long GetInitialSequence()
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			var res = _httpClient.GetSequenceAsync($"initialequence");
			return res.Data;
		}

		#endregion

		#region User

		public void CheckAPIConnection()
		{
			try
			{
				var res = _httpClient.GetAsync("/monitoring/ping").Result;
				if (res.StatusCode != HttpStatusCode.OK)
					throw new Exception($"API was not found on server");
			}
			catch
			{
				throw new Exception($"API was not found on server");
			}
		}

		public UserDTO GetUserAsync(string login, string password)
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["login"] = login,
				["password"] = password,
			});
			var res = _httpClient.GetJsonAsync<UserDTO>($"user/get?{param}");
			return res?.Data;
		}

		public List<UserDTO> GetAllUsersAsync()
		{
			var res =  _httpClient.GetJsonAsync<List<UserDTO>>($"user/getall");
			return res?.Data;
		}

		public UserDTO GetByIdAsync(int userId)
		{
			var res = _httpClient.GetJsonAsync<UserDTO>($"user/{userId}");
			return res?.Data;
		}

		public void UpdatePassword(int id, string password)
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["id"] = id.ToString(),
				["password"] = password
			});
			_httpClient.SendJsonAsync(HttpMethod.Post, $"user/updatepaswword?{param}");
		}

		public void DeleteUser(int id)
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["id"] = id.ToString(),
			});
			_httpClient.SendJsonAsync(HttpMethod.Post, $"user/delete?{param}");
		}

		public void AddOrUpdateUser(UserDTO user)
		{
			_httpClient.SendJsonAsync<UserDTO>(HttpMethod.Post, $"user/addorupdate", user);
		}

		#endregion

		#region Executor

		public DataSet Execute(string sql)
		{
			var res = _httpClient.SendJsonAsync<QueryParams, DataSet>(HttpMethod.Post, "executor/query",
				new QueryParams() {Query = sql});
			return res?.Data ?? new DataSet() { Tables = { new DataTable() } }; ;
		}

		public DataSet Execute(string query, SqlParameter[] parameters)
		{
			var p = new List<SerializedSqlParam>();
			p.AddRange(parameters.Select(i => new SerializedSqlParam(i)));

			var s = new XmlSerializer(typeof(List<SerializedSqlParam>));
			string xml;

			using (var sww = new StringWriter())
			{
				using (var writer = XmlWriter.Create(sww))
				{
					s.Serialize(writer, p);
					xml = sww.ToString();
				}
			}

			var res = _httpClient.SendJsonAsync<QueryParams, DataSet>(HttpMethod.Post, "executor/query",
				new QueryParams() { Query = query, SqlParams = xml });
			return res?.Data ?? new DataSet(){Tables = { new DataTable()}};
		}

		#endregion

		#region EFCore

		public IList<int> GetSelectColumnOnly<T>(IEnumerable<Filter> filters, string selectProperty) where T: BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["selectProperty"] = selectProperty,
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, List<int>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO","").ToLower()}/getcolumn?{param}", filters);
			return res?.Data;
		}

		public T GetObjectById<T>(int id, bool loadChild = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["id"] = id.ToString(),
				["loadChild"] = loadChild.ToString()
			});
			var res = _httpClient.GetJsonAsync<T>($"{typeof(T).Name.Replace("DTO", "").ToLower()}/getbyid?{param}");
			return res?.Data;
		}

		public T GetObject<T>(IEnumerable<Filter> filters = null, bool loadChild = false, bool getDeleted = false,
			bool getAll = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = loadChild.ToString(),
				["getDeleted"] = getDeleted.ToString(),
				["getAll"] = getAll.ToString(),
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, T>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/get?{param}", filters);
			return res?.Data;
		}

		public List<T> GetObjectList<T>(IEnumerable<Filter> filters = null, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = loadChild.ToString(),
				["getDeleted"] = getDeleted.ToString(),
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, List<T>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/getlist?{param}", filters);
			return res?.Data;
		}

		public List<T> GetObjectListAll<T>(IEnumerable<Filter> filters = null, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = loadChild.ToString(),
				["getDeleted"] = getDeleted.ToString(),
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, List<T>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/getlistall?{param}", filters);
			return res?.Data;
		}

		public void Delete<T>(T entity) where T : BaseEntity
		{
			_httpClient.SendJsonAsync<T>(HttpMethod.Post, $"{ typeof(T).Name.Replace("DTO", "").ToLower()}/delete", entity);
		}

		public int Save<T>(T value, bool saveAttachedFile = true, bool writeAudit = true) where T : BaseEntity
		{
			var res = _httpClient.SendJsonAsync<T, int>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/save", value);
			return res?.Data ?? -1;
		}

		public void BulkDelete<T>(IEnumerable<T> entity, int? batchSize = null) where T : BaseEntity
		{
			_httpClient.SendJsonAsync<IEnumerable<T>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/bulkdelete", entity);
		}

		public void BulkUpdate<T>(IEnumerable<T> entity, int? batchSize = null) where T : BaseEntity
		{
			_httpClient.SendJsonAsync<IEnumerable<T>>(HttpMethod.Post,
				$"{typeof(T).Name.Replace("DTO", "").ToLower()}/bulkupdate", entity);
		}

		public void BulkInsert<T>(IEnumerable<T> entity, int? batchSize = null) where T : BaseEntity
		{
			_httpClient.SendJsonAsync<IEnumerable<T>>(HttpMethod.Post,
				$"{typeof(T).Name.Replace("DTO", "").ToLower()}/bulkinsert", entity);
		}

		#endregion

	}
}