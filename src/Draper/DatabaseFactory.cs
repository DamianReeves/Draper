using System;

namespace Draper
{
    /// <summary>
    /// Contains factory methods for creating <see cref="Database"/> objects.
    /// </summary>
    public static class DatabaseFactory
    {
        private static volatile Func<string, Database> _createNamedDatabase;
        private static volatile Func<Database> _createDefaultDatabase;

        /// <summary>
        /// Creates the default <see cref="Database"/> object
        /// </summary>
        /// <example>
        /// <code>
        /// Database dbSvc = DatabaseFactory.CreateDatabase();
        /// </code>
        /// </example>
        /// <returns>The default database</returns>
        /// <exception cref="System.InvalidOperationException">The database factory has not been intialized or some configuration information is missing.</exception>
        public static Database CreateDatabase()
        {
            return GetCreateDefaultDatabase().Invoke();
        }

        /// <summary>
        /// Creates the <see cref="Database"/> object with the specified name.
        /// </summary>
        /// <example>
        /// <code>
        /// Database dbSvc = DatabaseFactory.CreateDatabase("SQL_Customers");
        /// </code>
        /// </example>
        /// <param name="name">The configuration key for database service</param>
        /// <returns>The database with the specified name</returns>
        /// <exception cref="System.InvalidOperationException">The database factory has not been intialized or some configuration information is missing.</exception>
        public static Database CreateDatabase(string name)
        {
            return GetCreateDatabase().Invoke(name);
        }

        /// <summary>
        /// Sets the provider factory for the static <see cref="DatabaseFactory"/>.
        /// </summary>
        /// <param name="factory">The provider factory.</param>
        /// <param name="throwIfSet"><see langword="true"/> to thrown an exception if the factory is already set; otherwise, <see langword="false"/>. Defaults to <see langword="true"/>.</param>
        /// <exception cref="InvalidOperationException">The factory is already set and <paramref name="throwIfSet"/> is <see langword="true"/>.</exception>
        public static void SetDatabaseProviderFactory(DatabaseProviderFactory factory, bool throwIfSet = true)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            SetDatabases(factory.CreateDefault, factory.Create, throwIfSet);
        }

        /// <summary>
        /// Sets the database mappings.
        /// </summary>
        /// <param name="createDefaultDatabase">A method that returns the default database.</param>
        /// <param name="createNamedDatabase">A method that returns a database for the specified name.</param>
        /// <param name="throwIfSet"><see langword="true"/> to thrown an exception if the factory is already set; otherwise, <see langword="false"/>. Defaults to <see langword="true"/>.</param>
        /// <exception cref="InvalidOperationException">The factory is already set and <paramref name="throwIfSet"/> is <see langword="true"/>.</exception>
        public static void SetDatabases(Func<Database> createDefaultDatabase, Func<string, Database> createNamedDatabase, bool throwIfSet = true)
        {
            if (createDefaultDatabase == null) throw new ArgumentNullException(nameof(createDefaultDatabase));
            if (createNamedDatabase == null) throw new ArgumentNullException(nameof(createNamedDatabase));


            var currentCreateDb = DatabaseFactory._createNamedDatabase;
            var currentCreateDefaultDb = DatabaseFactory._createDefaultDatabase;

            if ((currentCreateDb != null && currentCreateDefaultDb != null) && throwIfSet)
            {
                throw new InvalidOperationException(Resources.ExceptionMessages.ExceptionDatabaseProviderFactoryAlreadySet);
            }

            DatabaseFactory._createDefaultDatabase = createDefaultDatabase;
            DatabaseFactory._createNamedDatabase = createNamedDatabase;
        }

        /// <summary>
        /// Clears the provider factory for the static <see cref="DatabaseFactory"/>.
        /// </summary>
        public static void ClearDatabaseProviderFactory()
        {
            _createNamedDatabase = null;
            _createDefaultDatabase = null;
        }

        private static Func<string, Database> GetCreateDatabase()
        {
            var func = _createNamedDatabase;

            if (func == null)
            {
                throw new InvalidOperationException(Resources.ExceptionMessages.ExceptionDatabaseProviderFactoryNotSet);
            }

            return func;
        }

        private static Func<Database> GetCreateDefaultDatabase()
        {
            var func = _createDefaultDatabase;

            if (func == null)
            {
                throw new InvalidOperationException(Resources.ExceptionMessages.ExceptionDatabaseProviderFactoryNotSet);
            }

            return func;
        }
    }
}