using Microsoft.Extensions.Configuration;
using OnurShopSecond.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace OnurShopSecond.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        SqlConnection _connection;
        public SqlDataAccess(IConfiguration config)
        {
            string conStr = config.GetConnectionString("SqlConnection");
            _connection = new SqlConnection(conStr);
        }
        public bool isPasswordValid(string password)
        {
            try
            {
                ConnectionControl();
                SqlCommand command = new SqlCommand("select Password from LoginInfo", _connection);
                SqlDataReader reader = command.ExecuteReader();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    reader.Close();
                    if ((string)dataTable.Rows[0][0] == password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Password couldnt get validated. This is not about user write password wrong, It should be database/server error. Contact your provider", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void ConnectionControl()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
            }
            catch (Exception ex)
            {
                _connection.Close();
                throw new Exception("Cannot connect to database server", ex);
            }

        }
        public List<UrunModel> GetProductsByGroupId(int groupId)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("SELECT ProductId,ProductName,Amount,Properties,GroupId,Price FROM Products WHERE GroupId=@GroupId ORDER BY ProductName", _connection);
                command.Parameters.AddWithValue("@GroupId", groupId);
                SqlDataReader reader = command.ExecuteReader();
                List<UrunModel> resultlist = new List<UrunModel>();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    reader.Close();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        UrunModel um = new UrunModel();
                        um.ProductId = (int)dataTable.Rows[i][0];
                        um.ProductName = (string)dataTable.Rows[i][1];
                        um.Amount = (int)dataTable.Rows[i][2];
                        um.Properties = (string)dataTable.Rows[i][3];
                        um.GroupId = (int)dataTable.Rows[i][4];
                        um.Price = (decimal)dataTable.Rows[i][5];
                        resultlist.Add(um);
                    }
                }
                return resultlist;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot get products with group id, group id might not valid (GetProductsByGroupId) ", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public List<UrunModel> GetAllProducts()
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("SELECT ProductId,ProductName,Amount,Properties,GroupId,Price FROM Products ORDER BY ProductName", _connection);
                SqlDataReader reader = command.ExecuteReader();
                List<UrunModel> resultlist = new List<UrunModel>();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    reader.Close();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        UrunModel um = new UrunModel();
                        um.ProductId = (int)dataTable.Rows[i][0];
                        um.ProductName = (string)dataTable.Rows[i][1];
                        um.Amount = (int)dataTable.Rows[i][2];
                        um.Properties = (string)dataTable.Rows[i][3];
                        um.GroupId = (int)dataTable.Rows[i][4];
                        um.Price = (decimal)dataTable.Rows[i][5];
                        resultlist.Add(um);
                    }
                }
                return resultlist;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot get products (GetAllProducts) ", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public List<UrunGurubuModel> GetAllProductGroups()
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("SELECT GroupId,GroupName FROM ProductGroups ORDER BY GroupName", _connection);
                SqlDataReader reader = command.ExecuteReader();
                List<UrunGurubuModel> resultlist = new List<UrunGurubuModel>();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    reader.Close();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        UrunGurubuModel ugm = new UrunGurubuModel();
                        ugm.GroupId = (int)dataTable.Rows[i][0];
                        ugm.GroupName = (string)dataTable.Rows[i][1];
                        resultlist.Add(ugm);
                    }
                }
                return resultlist;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot get product groups (GetAllProductGroups) ", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public UrunModel GetProductByProductId(int productId)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("SELECT ProductId,ProductName,Amount,Properties,GroupId,Price FROM Products WHERE ProductId=@ProductId", _connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                SqlDataReader reader = command.ExecuteReader();
                UrunModel result = new UrunModel();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    reader.Close();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        UrunModel ugm = new UrunModel();
                        ugm.ProductId = (int)dataTable.Rows[i][0];
                        ugm.ProductName = (string)dataTable.Rows[i][1];
                        ugm.Amount = (int)dataTable.Rows[i][2];
                        ugm.Properties = (string)dataTable.Rows[i][3];
                        ugm.GroupId = (int)dataTable.Rows[i][4];
                        ugm.Price = (decimal)dataTable.Rows[i][5];
                        result = ugm;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot get product by product id. You might enter invalid product id (GetProductByProductId) ", ex);
            }
            finally
            {
                _connection.Close();
            }

        }
        public string GetGroupNameFromGroupId(int groupId)
        {
            ConnectionControl();
            try
            {
                string result;
                SqlCommand command = new SqlCommand("SELECT GroupName FROM ProductGroups WHERE GroupId=@GroupId", _connection);
                command.Parameters.AddWithValue("@GroupId", groupId);
                var reader = command.ExecuteReader();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    result = dataTable.Rows[0][0].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("cannot get groupname from group id. group id might wrong (GetGroupNameFromGroupId)", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        //this will also delete all product which has same group id
        public void DeleteGroupByGroupId(int groupId)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM ProductGroups where GroupId = @GroupId", _connection);
                command.Parameters.AddWithValue("@GroupId", groupId);
                DeleteProductsByGroupId(groupId);
                ConnectionControl();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot Delete Group, you might enter invalid group id (DeleteGroupByGroupId) ", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        private void DeleteProductsByGroupId(int groupId)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Products where GroupId = @GroupId", _connection);
                command.Parameters.AddWithValue("@GroupId", groupId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot Delete Group, you might enter invalid group id (DeleteProductsByGroupId) ", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void DeleteProductProductId(int productId)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductId=@ProductId", _connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot delete product. ProductId might not valid (DeleteProductProductId)", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void UpdateProduct(UrunModel urunModel)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Products SET ProductName=@ProductName,Amount=@Amount,Properties=@Properties,GroupId=@GroupId,Price=@Price WHERE ProductId=@ProductId", _connection);
                command.Parameters.AddWithValue("@ProductName", urunModel.ProductName);
                command.Parameters.AddWithValue("@Amount", urunModel.Amount);
                command.Parameters.AddWithValue("@Properties", urunModel.Properties);
                command.Parameters.AddWithValue("@GroupId", urunModel.GroupId);
                command.Parameters.AddWithValue("@ProductId", urunModel.ProductId);
                command.Parameters.AddWithValue("@Price", urunModel.Price);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot update product. Please enter valid values (UpdateProduct)", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void ChangeGroupNameByGroupId(int groupId, string newGroupName)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("UPDATE ProductGroups SET GroupName=@GroupName WHERE GroupId=@GroupId", _connection);
                command.Parameters.AddWithValue("@GroupName", newGroupName);
                command.Parameters.AddWithValue("@GroupId", groupId);
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot change group name", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void AddNewProductGroup(string groupName)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO ProductGroups(GroupName) VALUES (@GroupName)", _connection);
                command.Parameters.AddWithValue("@GroupName", groupName);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("cannot add new group entered group name might not valid (AddNewProductGroup)", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void AddNewProduct(UrunModel urunModel)
        {
            ConnectionControl();
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO Products (ProductName,Amount,Properties,GroupId,Price) VALUES (@ProductName,@Amount,@Properties,@GroupId,@Price)", _connection);
                command.Parameters.AddWithValue("@ProductName", urunModel.ProductName);
                command.Parameters.AddWithValue("@Amount", urunModel.Amount);
                command.Parameters.AddWithValue("@Properties", urunModel.Properties);
                command.Parameters.AddWithValue("@GroupId", urunModel.GroupId);
                command.Parameters.AddWithValue("@Price", urunModel.Price);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("cannot add new product product information might not entered valid (AddNewProduct)", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public List<UrunModel> SearchProduct(string arananText)
        {
            ConnectionControl();
            try
            {
                arananText = $"%{arananText}%";
                SqlCommand command = new SqlCommand("SELECT ProductId,ProductName,Amount,Properties,GroupId,Price FROM Products WHERE ProductName LIKE @Text ORDER BY Amount", _connection);
                command.Parameters.AddWithValue("@Text", arananText);
                SqlDataReader reader = command.ExecuteReader();
                List<UrunModel> resultlist = new List<UrunModel>();
                using (DataTable dataTable = new DataTable())
                {
                    dataTable.Load(reader);
                    reader.Close();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        UrunModel um = new UrunModel();
                        um.ProductId = (int)dataTable.Rows[i][0];
                        um.ProductName = (string)dataTable.Rows[i][1];
                        um.Amount = (int)dataTable.Rows[i][2];
                        um.Properties = (string)dataTable.Rows[i][3];
                        um.GroupId = (int)dataTable.Rows[i][4];
                        um.Price = (decimal)dataTable.Rows[i][5];
                        resultlist.Add(um);
                    }
                }
                return resultlist;
            }
            catch (Exception ex)
            {

                throw new Exception("search for product is failed (UrunAra)", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
