using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AddCharsetOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Tạo header Content-Type với charset
        var jsonMediaType = new OpenApiMediaType
        {
            Schema = new OpenApiSchema
            {
                Type = "object"
            }
        };

        // Thêm charset UTF-8 vào content types cho tất cả các endpoint POST, PUT
        foreach (var response in operation.Responses)
        {
            if (response.Value.Content.ContainsKey("application/json"))
            {
                response.Value.Content["application/json; charset=utf-8"] = jsonMediaType;
            }
        }

        // Áp dụng với request content types (nếu POST, PUT, PATCH)
        if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey("application/json"))
        {
            operation.RequestBody.Content["application/json; charset=utf-8"] = jsonMediaType;
        }
    }
}