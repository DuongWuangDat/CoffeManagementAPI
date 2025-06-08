using CoffeeManagementAPI.Factory;

namespace CoffeeManagementAPI.State.TableState
{
    public class TableContext
    {
        private ITableState _state;
        public Model.Table Table { get; }

        public TableContext(Model.Table table, ITableState initialState = null)
        {
            Table = table;
            if (initialState != null)
                ChangeState(initialState);
            else
                SetStateFromStatus(table.Status);
        }

        public void ChangeState(ITableState state)
        {
            _state = state;
            _state.SetContext(this);
            Table.Status = state.GetStatus(); // cập nhật trạng thái thực tế trong DB
        }

        public async Task<bool> HandleAsync()
        {
            return await _state.HandleAsync();
        }

        public string GetStatus()
        {
            return _state.GetStatus();
        }
        public bool CanDelete()
        {
            return _state.CanDelete();
        }

        private void SetStateFromStatus(string status)
        {
            ChangeState(TableStateFactory.Create(status));
        }
    }

}
