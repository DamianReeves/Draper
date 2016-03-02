namespace Draper
{
    public abstract class DataAccessModule: IDataAccessModule
    {
        public void Configure(IDataAccessBuilder dataAccessBuilder)
        {
            OnConfigure(dataAccessBuilder);
        }

        protected virtual void OnConfigure(IDataAccessBuilder builder)
        {
            
        }
    }

    public interface IDataAccessModule
    {
        void Configure(IDataAccessBuilder dataAccessBuilder);
    }

    public interface IDataAccessBuilder { }

    class DataAccessBuilder : IDataAccessBuilder
    {
        public DataAccessBuilder()
        {           
        }
    }
}