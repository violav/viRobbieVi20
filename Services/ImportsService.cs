
using System.Globalization;
using System.Net;
using System.Text;
using Core.Contracts;
using CsvHelper;
using CsvHelper.Configuration;
namespace Robbie.Services
{
    public class ImportsService
    {
        private CsvConfiguration _config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            BadDataFound = null
        };
        private WebClient _webClient = new WebClient();

        public ImportsService()
        {
        }

        public List<SupplierFile> ReadDataToSuppliers(string i_file)
        {
            return this.GetRecordOfStream<SupplierFile>(i_file);
        }

        public List<CondominiumPeopleFile> ReadDataToCondominiumPeople(string i_file)
        {
            return this.GetRecordOfStream<CondominiumPeopleFile>(i_file);
        }

        private List<T> GetRecordOfStream<T>(string i_file)
        {
            string m_inputStr = Encoding.UTF8.GetString(Convert.FromBase64String(i_file));

            List<T> o_file = new();

            using (var reader = new StringReader(m_inputStr))
            using (var csv = new CsvReader(reader, this._config))
            {
                o_file = csv.GetRecords<T>().ToList();
            }

            return o_file;
        }
    }

}