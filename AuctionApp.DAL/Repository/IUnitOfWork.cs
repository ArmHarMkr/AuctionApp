namespace AuctionApp.DAL.Repository
{
    public interface IUnitOfWork
    {
        //Add other repositories
        IAuctionProdRepository AuctionProd{ get; }

        Task Save();
    }
}
