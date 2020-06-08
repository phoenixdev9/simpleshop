using api.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace api.Utils
{
    public static class Extensions
    {
        public static void AddApplicationErrorHeaders(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {

            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage,
                totalItems, totalPages);
            //Making Pagination Header Camel Case to match the angular application
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination",
                JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}