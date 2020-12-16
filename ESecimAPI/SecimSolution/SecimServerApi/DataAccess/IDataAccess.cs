using SecimServerApi.Models;
using System.Collections.Generic;

namespace SecimServerApi.DataAccess
{
    public interface IDataAccess
    {
        List<AdayModel> GetAdayList();
        List<OyModel> GetAllOylar();
        List<SecmenModel> GetAllSecmenler();
        void InsertOy(string oy);
        void InsertSecmen(SecmenModel model);
        void OylariTemizle();
        void UpdateSecmenOyDurumu(int durum, string tcno);
    }
}