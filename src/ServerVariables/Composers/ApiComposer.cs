using Umbraco.Cms.Api.Common.OpenApi;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace ServerVariables.Composers;

public class ApiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // Related documentation:
        // https://docs.umbraco.com/umbraco-cms/tutorials/creating-a-backoffice-api
        // https://docs.umbraco.com/umbraco-cms/tutorials/creating-a-backoffice-api/adding-a-custom-openapi-document

        // Add in a new OpenAPI document solely for our own package that can be browsed via Swagger UI
        // and is protected by the backoffice authentication
        builder.AddBackOfficeOpenApiDocument(
            Constants.ApiName,
            document => document
                .WithTitle("Server Variables API")
                .WithBackOfficeAuthentication()
                .WithJsonOptions(Umbraco.Cms.Core.Constants.JsonOptionsNames.BackOffice)
                .ConfigureOpenApiOptions(options =>
                {
                    options.AddDocumentTransformer((doc, _, _) =>
                    {
                        doc.Info.Version = "1.0";
                        return Task.CompletedTask;
                    });

                    // Use the action name as operation ID, so a generated TypeScript client has short method names
                    options.AddOperationTransformer((operation, context, _) =>
                    {
                        operation.OperationId = context.Description.ActionDescriptor.RouteValues["action"];
                        return Task.CompletedTask;
                    });
                }));
    }
}
