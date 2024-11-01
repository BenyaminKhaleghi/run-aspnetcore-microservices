namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await session.LoadAsync<Product>(request.Id, cancellationToken);

            if (result is null)
                throw new ProductNotFoundException(request.Id);

            return new GetProductByIdResult(result);
        }
    }
}
