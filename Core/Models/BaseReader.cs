using System.Globalization;
using CsvHelper.Configuration;

namespace Core.Models
{
    public abstract class BaseReader<T>
    {
        public CsvConfiguration config;

        public void Configuration()
        {
            this.config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
        }

        public abstract IEnumerable<T> ReadFile(Stream file);
    }
}
