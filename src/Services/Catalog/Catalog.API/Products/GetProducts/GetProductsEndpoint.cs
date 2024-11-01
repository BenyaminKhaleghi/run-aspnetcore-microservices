
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductsRsponse(IEnumerable<Product> Productse);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async ([AsParameters] GetProductsRequest request,ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);

                var response = result.Adapt<GetProductsResult>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
