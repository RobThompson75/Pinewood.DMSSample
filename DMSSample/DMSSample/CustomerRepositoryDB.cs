using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Pinewood.DMSSample.Business
{
    public class CustomerRepositoryDB : Interfaces.ICustomerRepositoryDB
    {
        public Customer? GetByName(string name)
        {
            Customer? _Customer = null;

            string _ConnectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (SqlConnection _Connection = new SqlConnection(_ConnectionString))
            {
                SqlCommand _Command = new SqlCommand
                {
                    Connection = _Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CRM_GetCustomerByName"
                };

                SqlParameter parameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
                _Command.Parameters.Add(parameter);

                _Connection.Open();
                SqlDataReader _Reader = _Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (_Reader.Read())
                {
                    _Customer = new Customer(
                        id: (int)_Reader["CustomerID"],
                        name: (string)_Reader["Name"],
                        address: (string)_Reader["Address"]
                    );
                }
            }

            return _Customer;
        }
    }
}
