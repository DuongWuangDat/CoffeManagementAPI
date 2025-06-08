namespace CoffeeManagementAPI.State.TableState
{
    public class NotBookedState : ITableState
    {
        private TableContext _context;

        public void SetContext(TableContext context)
        {
            _context = context;
        }

        public string GetStatus() => "Not booked";
        public bool CanDelete() => true;

        public async Task<bool> HandleAsync()
        {
            return await Task.FromResult(true);
        }
    }

}
