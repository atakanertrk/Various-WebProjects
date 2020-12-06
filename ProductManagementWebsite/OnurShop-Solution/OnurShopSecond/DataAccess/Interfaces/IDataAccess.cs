using OnurShopSecond.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnurShopSecond.DataAccess
{
    public interface IDataAccess
    {
        void AddNewProduct(UrunModel urunModel);
        void AddNewProductGroup(string groupName);
        void ChangeGroupNameByGroupId(int groupId, string newGroupName);
        void ConnectionControl();
        void DeleteGroupByGroupId(int groupId);
        void DeleteProductProductId(int productId);
        List<UrunGurubuModel> GetAllProductGroups();
        List<UrunModel> GetAllProducts();
        string GetGroupNameFromGroupId(int groupId);
        UrunModel GetProductByProductId(int productId);
        List<UrunModel> GetProductsByGroupId(int groupId);
        void UpdateProduct(UrunModel urunModel);
        List<UrunModel> SearchProduct(string arananText);
        bool isPasswordValid(string password);
    }
}