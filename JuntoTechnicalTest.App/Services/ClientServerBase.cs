using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text;
using JuntoTechnicalTest.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using JuntoTechnicalTest.Common.Exceptions;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace JuntoTechnicalTest.App.Services
{
    public class ClientServerBase : IClientServer
    {
        protected readonly HttpClient _httpClient;
     
        protected ClientServerBase(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            var context = httpContextAccessor.HttpContext;
            if ( context?.User?.Identity?.IsAuthenticated is true)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
              
                _httpClient.DefaultRequestHeaders.Add("user-id", userId);
            }         
        }

        public async Task<TResult?> Post<TResult, TModel>(string action, TModel obj)
        {
            var builder = new UriBuilder($"{_httpClient?.BaseAddress?.OriginalString}/{action}");

            var json = JsonSerializer.Serialize(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(builder.ToString(), data);

            await HandleErrors(response);

            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result;
        }

        public async Task<TResult?> Get<TResult, TModel>(string action = "", TModel? obj = null)
            where TModel : class
        {
            var builder = new UriBuilder($"{_httpClient?.BaseAddress?.OriginalString}/{action}");

            if (ConstructUrlWithParams(obj, builder, out string? query))
                builder.Query = query;

            var json = JsonSerializer.Serialize(obj);

            using StringContent data = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage? response = await _httpClient.GetAsync(builder.ToString());

            await HandleErrors(response);

            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result;
        }

        private async Task HandleErrors(HttpResponseMessage response)
        {

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ValidationProblemDetails? validationProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
                throw new ValidationException(validationProblemDetails?.Errors ?? new Dictionary<string, string[]>());
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new AuthorizationException("Unauthorized");
        }

        private bool ConstructUrlWithParams<TModel>(TModel? obj, UriBuilder builder, out string? query)
            where TModel : class
        {
            query = null;
            NameValueCollection? nameValueCollection = null;

            if (obj == null)
                return false;

            nameValueCollection = System.Web.HttpUtility.ParseQueryString(builder.Query);

            Type type = typeof(TModel);

            // Obter todas as propriedades públicas da classe
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                object? propertyValue = propertyInfo.GetValue(obj);
                Console.WriteLine($"{propertyName}: {propertyValue}");

                if (propertyValue == null)
                    nameValueCollection[propertyName] = propertyValue?.ToString();
            }
            query = nameValueCollection.ToString();
            return true;
        }
    }
}
