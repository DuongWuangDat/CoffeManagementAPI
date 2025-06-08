namespace CoffeeManagementAPI.State.TableState
{
    public interface ITableState
    {
        void SetContext(TableContext context);
        Task<bool> HandleAsync(); 
        string GetStatus();
        bool CanDelete(); 
    }

}
