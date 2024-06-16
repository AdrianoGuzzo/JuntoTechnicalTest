namespace JuntoTechnicalTest.App.Services.Interfaces
{
    public interface IClientServer
    {
        Task<TResult?> Post<TResult, TModel>(string action, TModel obj);
        Task<TResult?> Get<TResult, TModel>(string action = "", TModel? obj = null)
            where TModel : class;

    }
}
