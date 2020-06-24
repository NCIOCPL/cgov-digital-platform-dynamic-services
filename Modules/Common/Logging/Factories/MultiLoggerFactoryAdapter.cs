using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Factory;

namespace NCI.Logging.Factories
{
    /// <summary>
    /// Adapter hub for Common.Logging that can send logs to multiple other adapters
    /// </summary>
    public class MultiLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        private readonly List<ILoggerFactoryAdapter> LoggerFactoryAdapters;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFactoryLoggerFactoryAdapter"/> class.
        /// </summary>
        public MultiLoggerFactoryAdapter(Common.Logging.Configuration.NameValueCollection properties)
        {
            LoggerFactoryAdapters = new List<ILoggerFactoryAdapter>();

            foreach (var factoryAdapter in properties.Where(e => e.Key.EndsWith(".factoryAdapter")))
            {
                string adapterName = factoryAdapter.Key.Substring(0, factoryAdapter.Key.Length - 15);
                string adapterType = factoryAdapter.Value;

                var adapterConfig = new Common.Logging.Configuration.NameValueCollection();
                foreach (var entry in properties.Where(e1 => e1.Key.StartsWith(adapterName + ".")))
                {
                    adapterConfig.Add(entry.Key.Substring(adapterName.Length + 1), entry.Value);
                }

                var adapter = (ILoggerFactoryAdapter)Activator.CreateInstance(Type.GetType(adapterType), adapterConfig);
                LoggerFactoryAdapters.Add(adapter);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFactoryLoggerFactoryAdapter"/> class.
        /// </summary>
        /// <param name="factoryAdapters">The factory adapters.</param>
        public MultiLoggerFactoryAdapter(List<ILoggerFactoryAdapter> factoryAdapters)
        {
            LoggerFactoryAdapters = factoryAdapters;
        }

        protected override ILog CreateLogger(string name)
        {
            var loggers = new List<ILog>(LoggerFactoryAdapters.Count);

            foreach (var f in LoggerFactoryAdapters)
            {
                loggers.Add(f.GetLogger(name));
            }

            return new MultiLogger(loggers);
        }
    }
}
