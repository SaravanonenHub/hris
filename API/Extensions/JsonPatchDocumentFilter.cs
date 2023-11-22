using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions
{
    public class JsonPatchDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var schemas = swaggerDoc.Components.Schemas.ToList();
            foreach (var item in schemas)
            {
                if (item.Key.StartsWith("Operation") || item.Key.StartsWith("JsonPatchDocument"))
                    swaggerDoc.Components.Schemas.Remove(item.Key);
            }
            swaggerDoc.Components.Schemas.Add("Operation", new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    { "op", new OpenApiSchema { Type = "string" } },
                                { "value", new OpenApiSchema { Type = "string" } },
                                { "path", new OpenApiSchema { Type = "string" } }
                            }
            });
            swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema
                {
                    Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Operation" }
                },
                Description = "Array of operations to perform"
            });
            //foreach (var path in swaggerDoc.Paths.SelectMany(p => p.Value.Operations)
            //    .Where(p => p.Key == Microsoft.OpenApi.Models.OperationType.Patch))
            //{
            //    foreach (var item in path.Value.RequestBody.Content.Where(c => c.Key != "application/json-patch+json"))
            //        path.Value.RequestBody.Content.Remove(item.Key);

            //    var response = path.Value.RequestBody.Content.SingleOrDefault(c => c.Key == "application/json-patch+json");

            //    response.Value.Schema = new OpenApiSchema
            //    {
            //        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
            //    };
            //}
                foreach (var pathEntry in swaggerDoc.Paths)
            {
                var path = pathEntry.Value;

                if (path.Operations.TryGetValue(OperationType.Patch, out var operation))
                {
                    if (operation.RequestBody != null)
                    {
                        var contentToRemove = operation.RequestBody.Content
                            .Where(content => content.Key != "application/json-patch+json")
                            .Select(content => content.Key)
                            .ToList();

                        foreach (var key in contentToRemove)
                        {
                            operation.RequestBody.Content.Remove(key);
                        }

                        if (operation.RequestBody.Content.TryGetValue("application/json-patch+json", out var jsonPatchContent))
                        {
                            jsonPatchContent.Schema = new OpenApiSchema
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
                            };
                        }
                    }
                }
            }

        }
    }
}
