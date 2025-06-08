namespace CoffeeManagementAPI.State.TableState
{
    public class UnderRepairState : ITableState
    {
        private TableContext _context;

        public void SetContext(TableContext context)
        {
            _context = context;
        }

        public string GetStatus() => "Under repair";
        public bool CanDelete() => false;

        public async Task<bool> HandleAsync()
        {
            return await Task.FromResult(false);
        }
    }

}
