namespace CoffeeManagementAPI.State.TableState
{
    public class BookedState : ITableState
    {
        private TableContext _context;

        public void SetContext(TableContext context)
        {
            _context = context;
        }

        public string GetStatus() => "Booked";
        public bool CanDelete() => false;

        public async Task<bool> HandleAsync()
        {
            return await Task.FromResult(false);
        }
    }

}
